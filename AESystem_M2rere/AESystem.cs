using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;


namespace AESystem
{
    public partial class AESystem : Form
    {
        //測定条件
        short DevId ;    //デバイスID
        byte Ch, Range1, Range2;
        int SampleCount, DataLeng, filenumber, Cycle;//周期
        double[] Threshold_Data;
        decimal SamplingRate;

        //測定中に使用するバッファ
        private int DDTLimit = 200;     //サンプリングレートとセンサの共振周波数と許容する周期数から設定できるようにしたい
        private double[] offset;

        //ループをまたぐヒット状態の保持用変数
        private bool[] PreviousHitSts = new bool[2];
        private int[] PreviousDDTime = new int[2];
        private double[] PreviousMaxAmplitude = new double[2];
        private double[] PreviousHitEnergy = new double[2];
        private int[] PreviousHitStsCount = new int[2];//持続時間
        private int[] PreviousHitFreqCount = new int[2];  //ゼロクロス回数
        private int[] PrevioustmpHitFreqCount = new int[2];
        private int[] PreviousRiseTimeCount = new int[2];  //立ち上がり時間
        private int[] PrevioustmpRiseTimeCount = new int[2];

        //累積AEパラメータ
        private int[] TotalHitCount = new int[2];
        private double[] TotalHitEnergy = new double[2];

        //平均パラメータ
        double[] AvrHitCount = new double[2];
        double[] AvrMaxAmp = new double[2];
        double[] AvrDrtTime = new double[2];
        double[] AvrHitEnergy = new double[2];

        //状態管理用フラグ
        private bool DevSts, StopReq, LoopSts;      //ADCの接続確認，測定停止要求，ループ測定
        private short ErrorNum;
        private string ErrorText;

        //ファイル操作系
        string FileName;        //ファイル名（タグ，拡張子無し）
        string AllFilePath;
        string FilePath;        //ファイルパス
        StreamWriter SaveFile;

        //子フォーム
        HitStatusForm hitStatusForm = null;


        public AESystem()
        {
            InitializeComponent();
        }

        //親システムフォーム起動時の初期化処理
        private void AESystem_Load(object sender, EventArgs e)
        {
            InitializeUIComponents();
            InitializeFormState();
        }


        //UI初期化
        private void InitializeUIComponents()
        {
            comboBox_DeviceID.SelectedIndex = 0;
            comboBox_Range1.SelectedIndex = 0;
            comboBox_Range2.SelectedIndex = 0;
            checkBox_Ch1.Checked = true;
            checkBox_Ch2.Checked = false;
        }


        //内部フラグ・ボタンの状態を初期化
        private void InitializeFormState()
        {
            //DevOpen関連以外の操作を拒否
            DevSts = false;
            StopReq = false;
            LoopSts = false;

            Button_ID.Enabled = true;
            Button_Start.Enabled = false;
            Button_Stop.Enabled = false;
            Button_Trigger.Enabled = false;
            Button_Threshold_Measure.Enabled = false;

            //データ取得用タイマー
            timer1.Interval = 250;
            timer1.Enabled = false;
        }


        //AD変換器接続
        private void Button_ID_Click(object sender, EventArgs e)
        {
            TryConnectDevice((short)comboBox_DeviceID.SelectedIndex);
        }


        // デバイス接続を試み、状態に応じて処理を分岐
        private void TryConnectDevice(short deviceId)
        {
            ErrorNum = TUSB16AD.TUSB0216AD_Device_Open(deviceId);

            if (ErrorNum == 0 && !DevSts)
            {
                DevId = deviceId;
                DevSts = true;

                ErrorText = "AD変換器との接続に成功しました。";
                UpdateUI_DeviceConnected();
            }
            else if (DevSts)
            {
                ErrorText = "すでにAD変換器と接続されています。";
                Button_ID.Enabled = false;
            }
            else
            {
                ErrorText = "AD変換器との接続に失敗しました。";
                DevSts = false;
                UpdateUI_DeviceDisconnected();
            }
        }


        // デバイス接続成功時のUI更新
        private void UpdateUI_DeviceConnected()
        {
            Button_ID.Enabled = false;
            Button_Start.Enabled = true;
            Button_Threshold_Measure.Enabled = true;
        }


        // デバイス接続失敗時のUI更新
        private void UpdateUI_DeviceDisconnected()
        {
            Button_ID.Enabled = true;
            Button_Threshold_Measure.Enabled = false;
        }


        //サンプリング開始
        private void Button_Start_Click(object sender, EventArgs e)
        {
            Button_Start.Enabled = false;
            InitializeMeasurementBuffers();
            ConfigureChannels();
            ConfigureRanges();
            ConfigureThreshold();
            ConfigureSamplingRate(0);
            ConfigureSampleCount();

            StartSampling();



            //測定条件
            byte TrgType = 0;   //ソフトウェア固定
            byte TrgCh = 0;     //使用しない
            int PreLen = 0;     //使用しない
            byte ClkType = 1;
        }


        // 測定バッファの初期化
        private void InitializeMeasurementBuffers()
        {
            //測定中に使用するバッファ
            offset = Enumerable.Repeat(0.0, 2).ToArray();

            //ループをまたぐヒット状態の保持用変数
            PreviousHitSts = Enumerable.Repeat(false, 2).ToArray();
            PreviousDDTime = Enumerable.Repeat(DDTLimit, 2).ToArray();
            PreviousMaxAmplitude = Enumerable.Repeat(0.0, 2).ToArray();
            PreviousHitEnergy = Enumerable.Repeat(0.0, 2).ToArray();
            PreviousHitStsCount = Enumerable.Repeat(0, 2).ToArray();
            PreviousHitFreqCount = Enumerable.Repeat(0, 2).ToArray();
            PreviousRiseTimeCount = Enumerable.Repeat(0, 2).ToArray();

            //累積AEパラメータ
            TotalHitCount = Enumerable.Repeat(0, 2).ToArray();
            TotalHitEnergy = Enumerable.Repeat(0.0, 2).ToArray();

            //ヒット別AEパラメータ

            //平均パラメータ
            AvrHitCount = Enumerable.Repeat(0.0, 2).ToArray();
            AvrMaxAmp = Enumerable.Repeat(0.0, 2).ToArray();
            AvrDrtTime = Enumerable.Repeat(0.0, 2).ToArray();
            AvrHitEnergy = Enumerable.Repeat(0.0, 2).ToArray();
        }


        //測定チャンネル設定
        private void ConfigureChannels()
        {
            if (checkBox_Ch1.Checked && checkBox_Ch2.Checked)
            {
                Ch = 2;
                ErrorText = "1chと2chの測定を行います。";
            }
            else if (checkBox_Ch1.Checked)
            {
                Ch = 0;
                ErrorText = "1chの測定を行います。";
            }
            else if (checkBox_Ch2.Checked)
            {
                Ch = 1;
                ErrorText = "2chの測定を行います。";
            }
            else
            {
                ErrorText = "測定チャンネルの設定に失敗しました。";
            }
        }


        //入力レンジの設定
        private void ConfigureRanges()
        {
            Range1 = (byte)comboBox_Range1.SelectedIndex;
            Range2 = (byte)comboBox_Range2.SelectedIndex;
            ErrorNum = TUSB16AD.TUSB0216AD_Input_Set(DevId, Range1, Range2);
        }


        //閾値の設定
        private void ConfigureThreshold()
        {
            Threshold_Data[0] = double.Parse(TextBox_Threshold.Text);
            Threshold_Data[1] = double.Parse(TextBox_Threshold2.Text);
        }


        //サンプリング周期とレートの設定
        private void ConfigureSamplingRate(byte preClockSource)
        {
            Cycle = int.Parse(TextBox_SamplingRate.Text);
            SamplingRate = (decimal)(1 / (Cycle * 0.00000002));
            ErrorNum = TUSB16AD.TUSB0216AD_AdClk_Set(DevId, Cycle, preClockSource);
        }


        //サンプル数のチェックと無限ループ設定
        private void ConfigureSampleCount()
        {
            SampleCount = int.Parse(TextBox_SamplingCount.Text);

            if (checkBox_loop.Checked)
            {
                LoopSts = true;
            }
            else if (SampleCount > 0 && SampleCount <= 100000)
            {
                LoopSts = false;
            }
            else
            {
                LoopSts = false;
                ErrorText = "サンプリング数の設定に失敗しました。\nサンプリング数が多すぎるか不正な値です。";
                StopReq = true;
            }
        }


        //実際の測定開始処理
        private void StartSampling()
        {
            ErrorNum = TUSB16AD.TUSB0216AD_Start(DevId, Ch, 0, 0, 0);

            if (ErrorNum == 0)
            {
                Button_Trigger.Enabled = true;
                timer1.Enabled = true;

                if (hitStatusForm == null || hitStatusForm.IsDisposed)
                {
                    hitStatusForm = new HitStatusForm();
                    hitStatusForm.Show();
                }
            }
            else
            {
                Button_Start.Enabled = true;
                ErrorText = "計測の開始に失敗しました。";
            }
        }


        //閾値の設定
        private void Button_Threshold_Measure_Click(object sender, EventArgs e)
        {
            Button_Threshold_Measure.Enabled = false;
            Button_Start.Enabled = false;

            ConfigureChannels();
            ConfigureRanges();
            ConfigureSamplingRate(0);

            ErrorNum = TUSB16AD.TUSB0216AD_Start(DevId, Ch, 0, 0, 0);

            if (ErrorNum == 0)
            {
                //閾値計測用タイマーを回す
                timer3.Interval = 200;
                timer3.Enabled = true;
                ErrorNum = TUSB16AD.TUSB0216AD_Trigger(DevId);
            }
            else
            {
                Button_Threshold_Measure.Enabled = true;
                ErrorText = "しきい値の計測に失敗しました。";
            }
        }


        //閾値を測定
        private void timer3_Tick(object sender, EventArgs e)
        {
            byte Status;                        //ADCの状態
            byte[] ovf = new byte[2];           //ADC内メモリオーバーフロー
            int[] TmpLength = new int[2];         //ADC取り込み済みデータ数
            double[] AvrData = new double[2];   //各チャンネルの平均電圧（= オフセット)
            
            ErrorNum = TUSB16AD.TUSB0216AD_Ad_Status(DevId, out Status, ovf, TmpLength);

            switch (Status)
            {
                case 1: //トリガ待機中
                    ErrorText = "トリガ待機中";
                    break;

                case 3: //トリガ後変換中
                    //ADCバッファオーバーフロー
                    if (ovf[0] != 0 || ovf[1] != 0)
                    {
                        ErrorText = "バッファオーバーフローが発生しています。";
                        SmpleStop();
                        return;
                    }

                    DataLeng = (Ch == 0) ? TmpLength[0] :
                               (Ch == 1) ? TmpLength[1] :
                               Math.Min(TmpLength[0], TmpLength[1]);
                    break;
                //停止中 or その他異常
                default:
                    DataLeng = 0;
                    break;
            }

            //1秒以上のデータが取得できているかチェック
            double SampleRate = Cycle * 0.00000002;
            SampleRate = 1.0 / SampleRate;  //周波数（Hz）

            if (DataLeng <= SampleRate)
            {
                return;
            }

            offset = new double[2];
            Threshold_Data = new double[2];

            switch (Ch)
            {
                case 0:
                    MeasureThresholdForChannel(0, Range1);
                    break;
                case 1:
                    MeasureThresholdForChannel(1, Range2);
                    break;
                case 2:
                    MeasureThresholdForChannel(0, Range1);
                    MeasureThresholdForChannel(1, Range2);
                    break;
                default:
                    ErrorText = "測定チャンネル設定が不正です。";
                    SmpleStop();
                    return;
            }

            //UIへの反映
            TextBox_Threshold.Text = Threshold_Data[0].ToString("f4");
            TextBox_Threshold2.Text = Threshold_Data[1].ToString("f4");

            //測定終了処理
            TUSB16AD.TUSB0216AD_Stop(DevId);
            timer3.Enabled = false;

            //UIの再有効化
            Button_Threshold_Measure.Enabled = true;
            Button_Start.Enabled = true;

        }


        //指定チャンネルのデータ取得・電圧変換・オフセット計算・標準偏差算出
        private void MeasureThresholdForChannel(byte ch, byte range)
        {
            int[] tmpArray = new int[DataLeng];
            int[,] dataArray = new int[2, DataLeng];
            double[,] arrayV = new double[2, DataLeng];
            double[] avrData = new double[2];
            double[,] offsetArray = new double[2, DataLeng];

            //TUSB呼び出し + 生データ → 電圧変換 + オフセット → 標準偏差算出
            ThresholdCalculate(ch, range, tmpArray, dataArray, arrayV, avrData, offsetArray);
        }


        //測定を行う関数をまとめたもの
        private void ThresholdCalculate(byte preCh, byte preRange, int [] preTmpArray, int [,]preDataArray, double [,]preArrayV, double []preAvrData, double [,]preoffsetArray)
        {
            ErrorNum = TUSB16AD.TUSB0216AD_Ad_Data(DevId, preCh, preTmpArray, out DataLeng);
            CopyData(preCh, preTmpArray, preDataArray);
            CmbVlt(preCh, preRange, preTmpArray, preArrayV);
            ClcAvrData(preCh, preAvrData, preArrayV);
            offset[preCh] = preAvrData[preCh];
            ClcOffSet(preCh, preAvrData, preArrayV, preoffsetArray);
            Threshold_Data[preCh] = ClcStandardDeviation(preCh, preoffsetArray);
        }


        //標準偏差*4を返す
        private double ClcStandardDeviation(int channel, double[,] preoffsetArray)
        {
            double sum = 0;
            double sumSq = 0;

            for (int i = 0; i < DataLeng; i++)
            {
                double value = preoffsetArray[channel, i];
                sum += value;
                sumSq += value * value;
            }

            double mean = sum / DataLeng;
            double variance = (sumSq / DataLeng) - (mean * mean);
            double stddev = Math.Sqrt(variance);

           //return stddev * 4;
           return stddev * 16;
        }


        //トリガーボタン
        private void Button_Trigger_Click(object sender, EventArgs e)
        {
            Button_Trigger.Enabled = false;

            ErrorNum = TUSB16AD.TUSB0216AD_Trigger(DevId);

            if (ErrorNum != 0)
            {
                Button_Start.Enabled = true;
                ErrorText = "トリガーの入力に失敗しました．";
            }
            else if (ErrorNum == 0)
            {
                Button_Stop.Enabled = true;
            }
        }


        //データ取得用--メイン
        private void timer1_Tick(object sender, EventArgs e)
        {
            byte Status;                        //AD変換器の状態
            byte[] ovf = new byte[2];           //AD内メモリオーバーフロー
            int[] TmpLeng = new int[2];         //AD取り込み済みデータ数
            //double[] AvrData = new double[2];   //データの平均値（電圧値）
            //AvrData[0] = offset[0];
            //AvrData[1] = offset[1];


            //停止要求
            if (StopReq)
            {
                SmpleStop();
                return;
            }

            //状態確認
            ErrorNum = TUSB16AD.TUSB0216AD_Ad_Status(DevId, out Status, ovf, TmpLeng);

            if (ErrorNum != 0)
            {
                SmpleStop();
                return;
            }

            switch (Status)
            {
                case 1: //トリガ待機中
                    ErrorText = "トリガ待機中";
                    DataLeng = 0;
                    break;
                case 3: //トリガ後データ蓄積中
                    ErrorText = "データ変換中";

                    //ADCバッファオーバーフロー
                    if (ovf[0] != 0 || ovf[1] != 0)
                    {
                        ErrorText = ("バッファオーバーフローです");

                        // ポップアップ表示を追加
                        MessageBox.Show("オーバーフローが発生しました。\n測定を停止します。",
                                        "オーバーフロー警告",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                        SmpleStop();
                        return;
                    }
                    break;
                default:    //停止中
                    return;
            }

            //データの取得は秒数固定で繰り返して行う（1秒） or 指定サンプル数までいったら (-100000)
            //AEパラメータの抽出はデータの取得とともに行う（1秒）
            //波形の表示も同時に
            //保存も同時に
            //1000*0.00000002 = 周期
            DataLeng = (Ch == 0) ? TmpLeng[0] :
                       (Ch == 1) ? TmpLeng[1] :
                       Math.Min(TmpLeng[0], TmpLeng[1]);


            if (LoopSts == true)
            {
                if (DataLeng < SamplingRate)
                {
                    // 1秒未満のデータしか蓄積されていない → 保留
                    return;
                }
                DataLeng = (int)SamplingRate;
            }
            else
            {
                if (DataLeng < SampleCount)
                {
                    // 指定以下のデータしか蓄積されていない
                    return;
                }
                DataLeng = SampleCount;
            }


            double[,] AEAry = new double[6, 2];  //ヒット別AEパラメータ以外のAEパラメータ[AEpara. Ch], (TotalHitCount, HitCount, AvrHitCount, MaxAmp, DrtTime, TotalHitEnergy)
            double[,,] AEparaAry = new double[20, 4, 2];    //ヒットごとにAEパラメータの値を格納[Hit , AEpara, Ch] 
                
            //ヒット数
            int[] HitCount = new int[2];

            int[] TmpArray = new int[DataLeng];   //変換データ数を配列の長さに
            int[,] DataArray = new int[2, DataLeng];
            double[,] ArrayV = new double[2, DataLeng];   //取得データを電圧値に変換
            double[,] offsetArray = new double[2, DataLeng];
            double[,] SquaredArray = new double[2, DataLeng];

            //double[] AverageHit = new double[2];
            //double[] MaxAverageCmp = new double[2];
            //double[] AverageTime = new double[2];

            bool[,] HitStsAry = new bool[2, DataLeng]; //ヒットが立っているか

            //データ取得 ---ADCの仕様書嘘ついてます。
            TUSB16AD.TUSB0216AD_Ad_Data(DevId, 0, TmpArray, out DataLeng);


            //データ取得＆電圧変換
            if (Ch == 0 || Ch == 2)
            {
                CopyData(0, TmpArray, DataArray);
                CmbVlt(0, Range1, TmpArray, ArrayV);
                //ClcAvrData(0, offset, ArrayV);
                ClcSquaredArray(0, ArrayV, SquaredArray);
                ClcOffSet(0, offset, ArrayV, offsetArray);
            }

            //データ取得 ---ADCの仕様書嘘ついてます。
            TUSB16AD.TUSB0216AD_Ad_Data(DevId, 1, TmpArray, out DataLeng);

            if (Ch == 1 || Ch == 2)
            {
                CopyData(1, TmpArray, DataArray);
                CmbVlt(1, Range2, TmpArray, ArrayV);
                //ClcAvrData(1, offset, ArrayV);
                ClcSquaredArray(1, ArrayV, SquaredArray);
                ClcOffSet(1, offset, ArrayV, offsetArray);
            }

            ProcessAEData(HitStsAry, offsetArray, HitCount, AEparaAry, SquaredArray);
            WriteData(HitStsAry, offsetArray, DataArray, HitCount, AEparaAry);


            //AEAryにAEパラメータを格納
            AEAry[0, 0] = TotalHitCount[0];
            AEAry[0, 1] = TotalHitCount[1];
            AEAry[1, 0] = HitCount[0];
            AEAry[1, 1] = HitCount[1];
            AEAry[2, 0] = AvrHitCount[0];
            AEAry[2, 1] = AvrHitCount[1];
            AEAry[3, 0] = AvrMaxAmp[0];
            AEAry[3, 1] = AvrMaxAmp[1];
            AEAry[4, 0] = AvrDrtTime[0];
            AEAry[4, 1] = AvrDrtTime[1];
            AEAry[5, 0] = TotalHitEnergy[0];
            AEAry[5, 1] = TotalHitEnergy[1];

            //AEパラメータを子フォームに送信
            hitStatusForm.Get_AEArray(DataLeng, AEAry);
            hitStatusForm.Get_Array(AEparaAry);
            hitStatusForm.Get_ArrayOffset(Threshold_Data, offsetArray);

            //ファイルはwritedataで保存して閉じている
            AutoFile(); //次の連番のファイルを生成して開く

            if (LoopSts == false)
            {
                StopReq = true;
            }
        }


        //停止ボタン
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            StopReq = true;
            ErrorText = "停止要求を出しました。";
        }


        //サンプリング停止
        private void SmpleStop()
        {
            ErrorNum = TUSB16AD.TUSB0216AD_Stop(DevId);

            //測定関連の状態リセット
            timer1.Enabled = false;
            StopReq = false;
            LoopSts = false;
            DataLeng = 0;

            //測定ファイルを閉じる
            try
            {
                if (SaveFile != null)
                {
                    SaveFile.Close();
                    SaveFile = null;
                }

                if (!string.IsNullOrEmpty(AllFilePath) && File.Exists(AllFilePath))
                {
                    File.Delete(AllFilePath);
                }
            }
            catch (Exception ex)
            {
                ErrorText = $"ファイル処理中にエラー: {ex.Message}";
            }

            //AEパラメータ表示フォームを閉じる
            if (hitStatusForm != null && !hitStatusForm.IsDisposed)
            {
                hitStatusForm.Form_Close();
                hitStatusForm = null;
            }

            //UI更新
            Button_Start.Enabled = true;
            Button_Stop.Enabled = false;
            Button_Trigger.Enabled = false;
            Button_Threshold_Measure.Enabled = true;

            label_FilePath.Text = "";
            this.Refresh();
            ErrorText = "測定を終了しました。";
        }


        //取得データを電圧値に変換
        private void CmbVlt(int ch, int range, int[] src, double[,] dst)
        {
            double scale;
            double offset;

            switch (range)
            {
                case 0: // ±10V（差動・双極性）
                    scale = 20.0 / 65536.0;
                    offset = -10.0;
                    break;
                case 1: // ±5V
                    scale = 10.0 / 65536.0;
                    offset = -5.0;
                    break;
                case 2: // ±2.5V
                    scale = 5.0 / 65536.0;
                    offset = -2.5;
                    break;
                case 3: // ±1.25V
                    scale = 2.5 / 65536.0;
                    offset = -1.25;
                    break;
                case 4: // 0〜10V（単極性）
                    scale = 10.0 / 65536.0;
                    offset = 0.0;
                    break;
                case 5: // 0〜5V
                    scale = 5.0 / 65536.0;
                    offset = 0.0;
                    break;
                case 6: // 0〜2.5V
                    scale = 2.5 / 65536.0;
                    offset = 0.0;
                    break;
                default: // 不明なレンジ → ±10V相当とする
                    scale = 20.0 / 65536.0;
                    offset = -10.0;
                    break;
            }

            for (int i = 0; i < src.Length; i++)
            {
                dst[ch, i] = src[i] * scale + offset;
            }
        }


        //取得データを移す
        private void CopyData(int ch, int[] src, int[,] dst)
        {
            for (int i = 0; i < src.Length; i++)
            {
                dst[ch, i] = src[i];
            }
        }


        //平均値算出
        private void ClcAvrData(int ch, double[] avg, double[,] data)
        {
            double sum = 0.0;
            for (int i = 0; i < DataLeng; i++)
            {
                sum += data[ch, i];
            }
            avg[ch] = sum / DataLeng;
        }


        //オフセット処理
        private void ClcOffSet(int ch, double[] avg, double[,] data, double[,] offsetArray)
        {
            for (int i = 0; i < DataLeng; i++)
            {
                offsetArray[ch, i] = data[ch, i] - avg[ch];
            }
        }


        //オフセット処理をしたサンプリングデータを全て二乗
        private void ClcSquaredArray(int channel, double[,] preoffsetArray, double[,] preSquaredArray)
        {
            int cnt;

            for (cnt = 0; cnt < DataLeng; cnt++)
            {
                preSquaredArray[channel, cnt] = preoffsetArray[channel, cnt] * preoffsetArray[channel, cnt];
            }
        }


        //AEパラメータを抽出
        private void ProcessAEData(bool[,] preHitStsAry, double[,] preoffsetArray, int[] preHitCount, double[,,] preAEparaAry, double[,] preSquaredArray)
        {
            int cnt;

            int[] DDTime = new int[2];
            DDTime[0] = DDTLimit;
            DDTime[1] = DDTLimit;

            double FileTime;

            preHitCount[0] = 0;
            preHitCount[1] = 0;

            //前回ループの状態を取得
            //ほかのパラメータもこの時点で追加
            if (PreviousHitSts[0])
            {
                DDTime[0] = PreviousDDTime[0];
            }
            if (PreviousHitSts[1])
            {
                DDTime[1] = PreviousDDTime[1];
            }


            //ヒット検知
            if (Ch == 0 || Ch == 2)    //1chのヒット検知
            {
                if (preoffsetArray[0, 0] < Threshold_Data[0]) //波形が閾値を下回った
                {
                    DDTime[0]++;

                    //
                    if (DDTime[0] < DDTLimit)
                    {
                        preHitStsAry[0, 0] = true;
                    }
                    else if (DDTime[0] >= DDTLimit)
                    {
                        DDTime[0] = DDTLimit;
                        preHitStsAry[0, 0] = false;
                    }
                }
                else if (preoffsetArray[0, 0] >= Threshold_Data[0])  //波形が閾値を上回った
                {
                    DDTime[0] = 0;
                    preHitStsAry[0, 0] = true;

                    //新規ヒットのとき
                    if (PreviousHitSts[0] == false)
                    {
                        preHitCount[0]++;
                        TotalHitCount[0]++;
                    }
                }
                PreviousHitSts[0] = preHitStsAry[0, 0];

                for (cnt = 1; cnt < DataLeng; cnt++)
                {
                    if (preoffsetArray[0, cnt] < Threshold_Data[0]) //波形が閾値を下回った
                    {
                        DDTime[0]++;

                        if (DDTime[0] < DDTLimit)
                        {
                            preHitStsAry[0, cnt] = true;
                        }
                        else if (DDTime[0] >= DDTLimit)
                        {
                            DDTime[0] = DDTLimit;
                            preHitStsAry[0, cnt] = false;
                        }
                    }
                    else if (preoffsetArray[0, cnt] >= Threshold_Data[0])  //波形が閾値を上回った
                    {
                        DDTime[0] = 0;
                        preHitStsAry[0, cnt] = true;

                        //新規ヒット
                        if (preHitStsAry[0, cnt - 1] == false)
                        {
                            preHitCount[0]++;
                            TotalHitCount[0]++;
                        }
                    }
                    PreviousHitSts[0] = preHitStsAry[0, cnt];
                }
                PreviousDDTime[0] = DDTime[0];
            }

            if (Ch == 1 || Ch == 2)   ////2chのヒット検知
            {
                if (preoffsetArray[1, 0] < Threshold_Data[1]) //波形が閾値を下回った
                {
                    DDTime[1]++;

                    if (DDTime[1] < DDTLimit)
                    {
                        preHitStsAry[1, 0] = true;
                    }
                    else if (DDTime[1] >= DDTLimit)
                    {
                        DDTime[1] = DDTLimit;
                        preHitStsAry[1, 0] = false;
                    }
                }
                else if (preoffsetArray[1, 0] >= Threshold_Data[1])  //波形が閾値を上回った
                {
                    DDTime[1] = 0;
                    preHitStsAry[1, 0] = true;

                    //新規ヒット
                    if (PreviousHitSts[1] == false)
                    {
                        preHitCount[1]++;
                        TotalHitCount[1]++;
                    }
                }
                PreviousHitSts[1] = preHitStsAry[1, 0];

                for (cnt = 1; cnt < DataLeng; cnt++)
                {
                    if (preoffsetArray[1, cnt] < Threshold_Data[1]) //波形が閾値を下回った
                    {
                        DDTime[1]++;

                        if (DDTime[1] < DDTLimit)
                        {
                            preHitStsAry[1, cnt] = true;
                        }
                        else if (DDTime[1] >= DDTLimit)
                        {
                            DDTime[1] = DDTLimit;
                            preHitStsAry[1, cnt] = false;
                        }
                    }
                    else if (preoffsetArray[1, cnt] >= Threshold_Data[1])  //波形が閾値を上回った
                    {
                        DDTime[1] = 0;
                        preHitStsAry[1, cnt] = true;

                        //新規ヒット
                        if (preHitStsAry[1, cnt - 1] == false)
                        {
                            preHitCount[1]++;
                            TotalHitCount[1]++;
                        }
                    }
                    PreviousHitSts[1] = preHitStsAry[1, cnt];
                }
                PreviousDDTime[1] = DDTime[1];
            }


            //平均ヒット数を抽出
            FileTime = 0.00000002 * Cycle; // 1クロック時間(s) 20ns * 1000 = 20µs  
            FileTime *= DataLeng;               // 1ファイルの長さ
            AvrHitCount[0] = preHitCount[0] / FileTime;
            AvrHitCount[1] = preHitCount[1] / FileTime;

            //ヒットを認識するタイミングが違うため，こっちでもヒットの回数を数える
            int[] HitTimeCount = new int[2];
            HitTimeCount[0] = 0;
            HitTimeCount[1] = 0;

            //ヒット持続時間
            int[] HitStsCount = new int[2];
            HitStsCount[0] = PreviousHitStsCount[0];
            HitStsCount[1] = PreviousHitStsCount[1];
            int[] tmpHitStsCount = new int[2];         //１ヒット内のTrueの数を数える
            tmpHitStsCount[0] = PreviousHitStsCount[0];
            tmpHitStsCount[1] = PreviousHitStsCount[1];

            //ヒット最大振幅
            double[] MaxAmplitude = new double[2];    
            MaxAmplitude[0] = PreviousMaxAmplitude[0];
            MaxAmplitude[1] = PreviousMaxAmplitude[1];
            double[] tmpMaxAmplitude = new double[2];
            tmpMaxAmplitude[0] = PreviousMaxAmplitude[0];
            tmpMaxAmplitude[1] = PreviousMaxAmplitude[1];

            //ヒットエネルギー
            double[] HitEnergy = new double[2];
            HitEnergy[0] = PreviousHitEnergy[0];
            HitEnergy[1] = PreviousHitEnergy[1];
            double[] tmpHitEnergy = new double[2];
            tmpHitEnergy[0] = PreviousHitEnergy[0];
            tmpHitEnergy[1] = PreviousHitEnergy[1];

            //ヒットゼロクロス数
            int[] HitFreqCount = new int[2];
            HitFreqCount[0] = PreviousHitFreqCount[0];
            HitFreqCount[1] = PreviousHitFreqCount[1];
            //注意
            int[] tmpHitFreqCount = new int[2];
            tmpHitFreqCount[0] = PrevioustmpHitFreqCount[0];
            tmpHitFreqCount[1] = PrevioustmpHitFreqCount[1];

            //ヒット立ち上がり時間
            int[] RiseTime = new int[2];    
            RiseTime[0] = PreviousRiseTimeCount[0];
            RiseTime[1] = PreviousRiseTimeCount[1];
            //注意
            int[] tmpRiseTime = new int[2];
            tmpRiseTime[0] = PrevioustmpRiseTimeCount[0];
            tmpRiseTime[1] = PrevioustmpRiseTimeCount[1];

            int tmpHitCount;


            for (cnt = 0; cnt < DataLeng; cnt++)
            {
                if (preHitStsAry[0, cnt] == true)   //ヒット内  //1ch
                {
                    //ヒット内サンプリングデータの数を数える
                    tmpHitStsCount[0]++;
                    tmpRiseTime[0]++;

                    //カウントを計算
                    if (cnt > 0 && preoffsetArray[0, cnt - 1] * preoffsetArray[0, cnt] < 0)
                    {
                        //ゼロクロス数を数える
                        tmpHitFreqCount[0]++;
                    }

                    if (preoffsetArray[0, cnt] > tmpMaxAmplitude[0])
                    {
                        //電圧値をより高い方に更新
                        tmpMaxAmplitude[0] = preoffsetArray[0, cnt];
                        //最後に最大値を更新したときのサンプリング数を保存
                        RiseTime[0] = tmpRiseTime[0];   
                        //最後に最大値を更新したときのゼロクロス数を保存
                        HitFreqCount[0] = tmpHitFreqCount[0];
                    }

                    //積分値を計算
                    tmpHitEnergy[0] += preSquaredArray[0, cnt] * (1 / (double)SamplingRate);
                }
                else if (preHitStsAry[0, cnt] == false) //ヒット外
                {
                    //ヒット内サンプリングデータの数を足す
                    //平均持続時間用
                    HitStsCount[0] += tmpHitStsCount[0];
                    //falseのとき最大電圧値を足す
                    //平均最大振幅用
                    MaxAmplitude[0] += tmpMaxAmplitude[0];
                    //falseのときエネルギーを足す
                    //累計AEエネルギー用
                    HitEnergy[0] += tmpHitEnergy[0];

                    //trueを数えていたら（ヒットが立っていたら）ヒットカウントを増やす
                    //ヒットが終わったタイミングでAEパラメータ配列に格納
                    if (tmpHitStsCount[0] > 0)
                    {
                        double tmp;
                        tmp = HitFreqCount[0] + 1;
                        tmp /= 2;   //  立ち上がりが何周期分か

                        HitTimeCount[0]++;
                        tmpHitCount = HitTimeCount[0];

                        preAEparaAry[tmpHitCount - 1, 0, 0] = tmpMaxAmplitude[0];
                        preAEparaAry[tmpHitCount - 1, 1, 0] = tmpHitStsCount[0] * Cycle * 0.00000002 - 0.00332;//0.00332 = Cycle * 166
                        preAEparaAry[tmpHitCount - 1, 2, 0] = tmpHitEnergy[0];
                        preAEparaAry[tmpHitCount - 1, 3, 0] = tmp / (RiseTime[0] * Cycle * 0.00000002);
                    }

                    tmpHitStsCount[0] = 0;
                    tmpMaxAmplitude[0] = 0;
                    tmpHitEnergy[0] = 0;
                    tmpHitFreqCount[0] = 0;
                    tmpRiseTime[0] = 0;

                    HitFreqCount[0] = 0;
                    RiseTime[0] = 0;
                }

                PreviousDDTime[0] = DDTime[0];
                PreviousHitStsCount[0] = tmpHitStsCount[0];
                PreviousMaxAmplitude[0] = tmpMaxAmplitude[0];
                PreviousHitEnergy[0] = tmpHitEnergy[0];
                //注意
                PreviousHitFreqCount[0] = HitFreqCount[0];
                PrevioustmpHitFreqCount[0] = tmpHitFreqCount[0];
                PreviousRiseTimeCount[0] = RiseTime[0];     //前ループの最大値を更新した地点までのサンプリングデータ数
                PrevioustmpRiseTimeCount[0] = tmpRiseTime[0];    //前ループの最大値を更新した地点からファイル終端までのサンプリングデータ数(残りのデータがDDTに達するかどうか不明なため)


                if (preHitStsAry[1, cnt] == true)   //ヒット内  //2ch
                {
                    //ヒット内サンプリングデータの数を数える
                    tmpHitStsCount[1]++;
                    tmpRiseTime[1]++;

                    //カウントを計算
                    if (cnt > 0 && preoffsetArray[1, cnt - 1] * preoffsetArray[1, cnt] < 0)
                    {
                        //ゼロクロス数を数える
                        tmpHitFreqCount[1]++;
                    }

                    if (preoffsetArray[1, cnt] > tmpMaxAmplitude[1])
                    {
                        //電圧値をより高い方に更新
                        tmpMaxAmplitude[1] = preoffsetArray[1, cnt];
                        //最後に最大値を更新したときのサンプリング数を保存
                        RiseTime[0] = tmpRiseTime[1];
                        //最後に最大値を更新したときのゼロクロス数を保存
                        HitFreqCount[1] = tmpHitFreqCount[1];
                    }

                    //積分値を計算
                    tmpHitEnergy[1] += preSquaredArray[1, cnt] * (1 / (double)SamplingRate);
                }
                else if (preHitStsAry[0, cnt] == false) //ヒット外
                {
                    //ヒット内サンプリングデータの数を足す
                    //平均持続時間用
                    HitStsCount[1] += tmpHitStsCount[1];
                    //falseのとき最大電圧値を足す
                    //平均最大振幅用
                    MaxAmplitude[1] += tmpMaxAmplitude[1];
                    //falseのときエネルギーを足す
                    //累計AEエネルギー用
                    HitEnergy[1] += tmpHitEnergy[1];

                    //trueを数えていたら（ヒットが立っていたら）ヒットカウントを増やす
                    //ヒットが終わったタイミングでAEパラメータ配列に格納
                    if (tmpHitStsCount[1] > 0)
                    {
                        double tmp;
                        tmp = HitFreqCount[1] + 1;
                        tmp /= 2;

                        HitTimeCount[1]++;
                        tmpHitCount = HitTimeCount[1];

                        preAEparaAry[tmpHitCount - 1, 0, 1] = tmpMaxAmplitude[1];
                        preAEparaAry[tmpHitCount - 1, 1, 1] = tmpHitStsCount[1] * Cycle * 0.00000002 - 0.00332;//0.00332 = Cycle * 166
                        preAEparaAry[tmpHitCount - 1, 2, 1] = tmpHitEnergy[1];
                        preAEparaAry[tmpHitCount - 1, 3, 1] = tmp / (RiseTime[1] * Cycle * 0.00000002);
                    }

                    tmpHitStsCount[1] = 0;
                    tmpMaxAmplitude[1] = 0;
                    tmpHitEnergy[1] = 0;
                    tmpHitFreqCount[1] = 0;
                    tmpRiseTime[1] = 0;

                    HitFreqCount[1] = 0;
                    RiseTime[1] = 0;
                }

                PreviousHitStsCount[1] = tmpHitStsCount[1];
                PreviousMaxAmplitude[1] = tmpMaxAmplitude[1];
                PreviousHitEnergy[1] = tmpHitEnergy[1];
                //注意
                PreviousHitFreqCount[1] = HitFreqCount[1];
                PrevioustmpHitFreqCount[1] = tmpHitFreqCount[1];
                PreviousRiseTimeCount[1] = RiseTime[1];     //前ループの最大値を更新した地点までのサンプリングデータ数
                PrevioustmpRiseTimeCount[1] = tmpRiseTime[1];    //前ループの最大値を更新した地点からファイル終端までのサンプリングデータ数(残りのデータがDDTに達するかどうか不明なため)
            }


            //ヒット持続時間平均を抽出
            //trueの数にサンプリング間隔をかける
            AvrDrtTime[0] = HitStsCount[0] * Cycle * 0.00000002; //単位s
            AvrDrtTime[0] /= HitTimeCount[0];
            AvrDrtTime[1] = HitStsCount[1] * Cycle * 0.00000002; //単位s
            AvrDrtTime[1] /= HitTimeCount[1];

            //ヒット最大電圧平均を抽出
            AvrMaxAmp[0] = MaxAmplitude[0] / HitTimeCount[0];
            AvrMaxAmp[1] = MaxAmplitude[1] / HitTimeCount[1];

            //累計ヒットAEエネルギー
            TotalHitEnergy[0] = HitEnergy[0];
            TotalHitEnergy[1] = HitEnergy[1];
        }


        //データ書き込み
        //検査用でDataArrayを入れているため後で抜く
        private void WriteData(bool[,] preHitStsAry, double[,] preoffsetArray, int[,] preDataArray, int[]preHitCount, double[,,]preAEparaAry)
        {
            int cnt;

            //---測定条件---
            //測定チャンネル
            SaveFile.WriteLine("Channel,");
            SaveFile.WriteLine(Ch + ",");
            //測定レンジ
            SaveFile.WriteLine("Range");
            SaveFile.WriteLine(Range1 + "," + Range2 + ",");
            //サンプリングレート(Hz)
            SaveFile.WriteLine("SamplingRate(Hz),");
            SaveFile.WriteLine(SamplingRate + ",");
            //サンプリング数（いらないかも）
            SaveFile.WriteLine("SamplingCount,");
            SaveFile.WriteLine(DataLeng + ",");
            //平均電圧（いらないかも，一応復元用に）
            //閾値（ヒット検知とかに使うやつ）
            SaveFile.WriteLine("Threshold(V),");
            SaveFile.WriteLine(Threshold_Data[0].ToString("f5") + "," + Threshold_Data[1].ToString("f5") + ",");

            //---AEパラメータ---
            SaveFile.WriteLine("AEParameters,");
            //累計ヒット数
            SaveFile.WriteLine("TotalHitCount,");
            SaveFile.WriteLine(TotalHitCount[0].ToString() + "," + TotalHitCount[1].ToString() + ",");
            //ヒット数
            SaveFile.WriteLine("HitCount,");
            SaveFile.WriteLine(preHitCount[0].ToString() + "," + preHitCount[1].ToString() + ",");
            //平均ヒット数(/s)
            SaveFile.WriteLine("AverageHitCount(/s),");
            SaveFile.WriteLine(AvrHitCount[0].ToString("f3") + "," +    AvrHitCount[1].ToString("f3") + ",");
            //ヒット持続時間平均
            SaveFile.WriteLine("AverageDurationTime(s),");
            SaveFile.WriteLine(AvrDrtTime[0].ToString("f3") + "," + AvrDrtTime[1].ToString("f3") + ",");
            //平均ヒット最大振幅
            SaveFile.WriteLine("AverageMaxAmp(V),");
            SaveFile.WriteLine(AvrMaxAmp[0].ToString("f3") + "," + AvrMaxAmp[1].ToString("f3") + ",");
            //累計AEエネルギー
            SaveFile.WriteLine("TotalHitEnergy(V^2*s,");
            SaveFile.WriteLine(TotalHitEnergy[0].ToString("f3") + "," + TotalHitEnergy[1].ToString("f3") + ",");

            //---ヒット別AEパラメータ--- - 20ヒット固定
            SaveFile.WriteLine("AEParameters by Hit,,,");
            SaveFile.WriteLine("1chAmplitude,1chDurationTime,1chHitEnergy,1chHitFreq,2chAmplitude,2chDurationTime,2chHitEnergy,2chHitFreq");
            for (cnt = 0; cnt < 20; cnt++)
            {
                SaveFile.WriteLine(preAEparaAry[cnt, 0, 0].ToString("f5") + "," + preAEparaAry[cnt, 1, 0].ToString("f5") + "," +
                                   preAEparaAry[cnt, 2, 0].ToString("f5") + "," + preAEparaAry[cnt, 3, 0].ToString("f5") + "," +
                                   preAEparaAry[cnt, 0, 1].ToString("f5") + "," + preAEparaAry[cnt, 1, 1].ToString("f5") + "," +
                                   preAEparaAry[cnt, 2, 1].ToString("f5") + "," + preAEparaAry[cnt, 3, 1].ToString("f5") + ",");
            }


            //---測定データ（オフセット電圧値）---
            SaveFile.WriteLine("MeasurementData,");
            for (cnt = 0; cnt < DataLeng; cnt++)
            {
                switch (Ch)
                {
                    case 0:
                        SaveFile.WriteLine(preoffsetArray[0, cnt].ToString("f5") + "," + preHitStsAry[0, cnt] + ",");
                        break;
                    case 1:
                        SaveFile.WriteLine(preoffsetArray[1, cnt].ToString("f5") + "," + preHitStsAry[1, cnt] + ",");
                        break;
                    case 2:
                        SaveFile.WriteLine(preoffsetArray[0, cnt].ToString("f5") + "," + preoffsetArray[1, cnt].ToString("f5") + preHitStsAry[0, cnt] + "," + preHitStsAry[1, cnt] + ",");
                        break;
                    default:
                        //やばい
                        break;
                }
            }
        }


        //ファイルを自動で生成
        private void AutoFile()
        {
            SaveFile.Flush();
            SaveFile.Close(); //ファイルを閉じる
            filenumber++;

            AllFilePath = FilePath + "\\" + FileName + "_" + filenumber.ToString("D3") + ".csv";   //ファイルパスを更新

            SaveFile = new StreamWriter(File.Create(AllFilePath));      //ファイルを開く

            //ファイル名を表示
            label_FilePath.Text = AllFilePath;
        }


        //ファイルの読み込み：解析用
        private void OpenFileDialogForm()
        {
            OpenFileDialog od = new OpenFileDialog
            {
                Title = "ファイルを開く",
                InitialDirectory = @"E:\AEdata",
                FileName = @"data.csv",
                Filter = "データファイル(*.csv)|*.csv",
                FilterIndex = 1
            };

            //ダイアログを表示する
            DialogResult result = od.ShowDialog();

            //ダイアログを開いたあとの動作
            if (result == DialogResult.OK)
            {
                int lines;

                string fileName = od.FileName;
                label_FilePath.Text = fileName;

                string[] anlys_ary1 = File.ReadAllLines(fileName);  //データを配列に格納
                lines = anlys_ary1.Length;  //行数を取得

                DataAnlys(lines, anlys_ary1);
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hitStatusForm == null || hitStatusForm.IsDisposed)
            {
                hitStatusForm = new HitStatusForm();
                hitStatusForm.Show();
            }
        }


        //読み込み時の配列内のデータの解析--メインその２
        private void DataAnlys(int lines, string[] ary)
        {
            int cnt = 0;
            double a;
            double[,] MeasureAE;         //測定条件用
            double[,] ParaAE;            //AEパラメータ用
            double[,] ParaAEbyHit;       //ヒット別AEパラメータ用
            double[,] DataArray;         //数値データ取得用
            int SamplingCount;


            //文字列から数字を抽出
            //1-10行目測定条件，11-23行目AEパラメータ，24行目から66行目ヒット別AEパラメータ，67行目以降測定データ
            //1-10行目測定条件，11-23行目AEパラメータ，24行目から45行目ヒット別AEパラメータ，46行目以降測定データ
            //測定条件1-10行目
            MeasureAE = new double[2, 10];
            
            for (cnt = 0; cnt <= 9; cnt++)
            {
                string[] parts = ary[cnt].Split(',');

                if (double.TryParse(parts[0], out a))
                {    
                    MeasureAE[0, cnt] = double.Parse(parts[0]);
                }

                if (double.TryParse(parts[1], out a))
                {
                    MeasureAE[1, cnt] = double.Parse(parts[1]);
                }
            }


            //AEパラメータ11-23行目
            ParaAE = new double[2, 13];    //[2, (AEparaCount)]

            for (cnt = 0; cnt <= 12; cnt++)
            {
                string[] parts = ary[cnt + 10].Split(',');

                if (double.TryParse(parts[0], out a))
                {
                    ParaAE[0, cnt] = double.Parse(parts[0]);
                }

                if (double.TryParse(parts[1], out a))
                {
                    ParaAE[1, cnt] = double.Parse(parts[1]);
                }
            }


            //ヒット別AEパラメータ24-45行目66
            ParaAEbyHit = new double[8, 22];    //[4, (AEparaCount)
               
            for (cnt = 0; cnt <= 21; cnt++)
            {
                string[] parts = ary[cnt + 23].Split(',');

                if (double.TryParse(parts[0], out a))
                {
                    ParaAEbyHit[0, cnt] = double.Parse(parts[0]);
                }

                if (double.TryParse(parts[1], out a))
                {
                    ParaAEbyHit[1, cnt] = double.Parse(parts[1]);
                }

                if (double.TryParse(parts[2], out a))
                {
                    ParaAEbyHit[2, cnt] = double.Parse(parts[2]);
                }

                if (double.TryParse(parts[3], out a))
                {
                    ParaAEbyHit[3, cnt] = double.Parse(parts[3]);
                }

                if (double.TryParse(parts[4], out a))
                {
                    ParaAEbyHit[4, cnt] = double.Parse(parts[4]);
                }

                if (double.TryParse(parts[5], out a))
                {
                    ParaAEbyHit[5, cnt] = double.Parse(parts[5]);
                }

                if (double.TryParse(parts[6], out a))
                {
                    ParaAEbyHit[6, cnt] = double.Parse(parts[6]);
                }

                if (double.TryParse(parts[7], out a))
                {
                    ParaAEbyHit[7, cnt] = double.Parse(parts[7]);
                }
            }


            //測定データ67行目-
            SamplingCount = (int) MeasureAE[0, 7];

            DataArray = new double[2, SamplingCount];

            for (cnt = 0; cnt < SamplingCount; cnt++)
            {
                string[] parts = ary[cnt + 66].Split(',');

                if (double.TryParse(parts[0], out a))
                {
                    DataArray[0, cnt] = double.Parse(parts[0]);
                }

                if (double.TryParse(parts[1], out a))
                {
                    DataArray[1, cnt] = double.Parse(parts[1]);
                }
            }


            //追加でAEパラメータ解析をする場合，以降に記述

            //AEパラメータを表示
            //AEパラメータの表示を行うフォームを表示
            if (hitStatusForm == null || hitStatusForm.IsDisposed)
            {
                hitStatusForm = new HitStatusForm();
                hitStatusForm.Show();
            }

            //ヒット別AEパラメータをサブフォームに送信
            hitStatusForm.Get_Array_read(ParaAEbyHit);
            hitStatusForm.Get_AEArray_read(MeasureAE, ParaAE);
            hitStatusForm.Get_MeasureArray_read(DataArray);
        }


        //ファイルを作成&開く--データ取得用
        private void SaveFileDialogForm()
        {
            SaveFileDialog sd = new SaveFileDialog
            {
                Title = "ファイルを作成する",
                InitialDirectory = @"c:\",
                FileName = @"data",
                Filter = "データファイル(*.csv)|*.csv",
                FilterIndex = 1
            };

            //ダイアログを表示する
            DialogResult result = sd.ShowDialog();

            //ダイアログを開いたあとの動作
            if (result == DialogResult.OK)
            {
                filenumber = 1;

                //SaveFile = new StreamWriter(File.Create(sd.FileName));      //ファイルを開く
                FilePath = Path.GetDirectoryName(sd.FileName);              //ディレクトリ名を取得
                FileName = Path.GetFileNameWithoutExtension(sd.FileName);   //ファイル名以外を取り除く
                AllFilePath = FilePath + "\\" + FileName + "_" + filenumber.ToString("D3") + ".csv";   //3桁0埋め, ファイルパスを取得

                //ファイル消去-再生成
                //SaveFile.Close();
                //File.Delete(sd.FileName);
                SaveFile = new StreamWriter(File.Create(AllFilePath));      //ファイルを開く

                //ファイル名を表示
                label_FilePath.Text = AllFilePath;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }


        //終了処理
        private void AESystem_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DevSts == true)
            {
                TUSB16AD.TUSB0216AD_Device_Close(DevId);
                DevSts = false;
            }
        }


        //メニューバー//
        //新規作成
        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialogForm();
        }

        //開く
        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialogForm();
        }

        //終了
        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DevSts == true)
            {
                TUSB16AD.TUSB0216AD_Device_Close(DevId);
                DevSts = false;
            }
            this.Close();
        }
        //メニューバーここまで//


        //文字入力を number，delete, BackSpace キーに限定する
        private void TextBox_Threshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                return;
            }

            if (e.KeyChar < '0' || '9' < e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void TextBox_SamplingRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                return;
            }

            if (e.KeyChar < '0' || '9' < e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void TextBox_SamplingCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                return;
            }

            if (e.KeyChar < '0' || '9' < e.KeyChar)
            {
                e.Handled = true;
            }
        }
        //ここまで


        //エラー表示
        private void timer2_Tick(object sender, EventArgs e)
        {
            textBox_ProcessMessage.Text = "A/D変換器：" + TUSB16AD.GetErrMessage(ErrorNum) + "\n" +
                                          "動作状況：" + ErrorText;
        }
    }
}
