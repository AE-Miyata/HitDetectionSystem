using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AESystem
{
    public partial class HitStatusForm : Form
    {
        public HitStatusForm()
        {
            InitializeComponent();
        }

        int DataLeng = 0;
        double[] Threshold = new double[2];
        double[,] AEArray = new double[6, 2];
        double[,,] AEparaArray = new double[20, 4, 2];
        double[,] ArrayOffset = new double[2, 50000];    //仮

        //read
        bool ReadSts = false;

        //波形表示用
        Series series1;
        Series series2;
        Series series_th1;
        Series series_th2;

        //double[,] ch1Array;
        //double[,] ch2Array;

        private void HitStatusForm_Load(object sender, EventArgs e)
        {
            int cnt;

            radioButton1.Checked = true;
            radioButton2.Checked = false;
            //radioButtonCh1.Checked = true;
            //radioButtonCh2.Checked = false;

            //チャートとそのひな形の作成
            /*
            chart1 = new Chart();
            chart2 = new Chart();
            */

            series1 = new Series("ch1");
            series2 = new Series("ch2");
            series_th1 = new Series("threshold1");
            series_th2 = new Series("threshold2");

            series1.ChartType = SeriesChartType.Line;
            series2.ChartType = SeriesChartType.Line;
            series_th1.ChartType = SeriesChartType.Line;
            series_th2.ChartType = SeriesChartType.Line;

            chart1.Series.Add(series1);
            chart1.Series.Add(series_th1);
            chart2.Series.Add(series2);
            chart2.Series.Add(series_th2);

            series1.Color = Color.Black;
            series_th1.Color = Color.Red;
            series2.Color = Color.Black;
            series_th2.Color = Color.Red;

            for (cnt = 0; cnt < 50000; cnt++) 
            {
                //series1.Points.AddXY(cnt + 1, cnt + 1) ;
                //series2.Points.AddXY(cnt + 1, 50000 - cnt);
                //series_th1.Points.AddXY(cnt + 1, 10000);
                //series_th2.Points.AddXY(cnt + 1, 15000);
            }
            
            //series.Points.AddXY(1, 10);
            //chart1.Series["Ch1"].Points.AddXY(x1[i], y1[i]);
            //chart2.Series["Ch2"].Points.AddXY(x2[i], y2[i]);// データの追加
        }


        //AEパラメータを随時表示，更新
        private void timer1_Tick(object sender, EventArgs e)
        {
            byte MeasureCh = 2;


            //
            //label.Text = "S1= " + y1[index].ToString("0.00");
            //

            if (radioButton1.Checked == true)
            {
                MeasureCh = 0;
            }
            else if (radioButton2.Checked == true)
            {
                MeasureCh = 1;
            }
            else
            {

            }


            //測定条件
            //サンプリングカウント
            textBox_SampleCountView.Text = DataLeng.ToString();

            //累計ヒット数
            textBox_TotalHit1.Text = AEArray[0, 0].ToString();
            textBox_TotalHit2.Text = AEArray[0, 1].ToString();
            
            //ヒット数
            textBox_HitCount1.Text = AEArray[1, 0].ToString();
            textBox_HitCount2.Text = AEArray[1, 1].ToString();

            //平均ヒット数
            textBox_AvrHitCount1.Text = AEArray[2, 0].ToString("f3");
            textBox_AvrHitCount2.Text = AEArray[2, 1].ToString("f3");
            
            //平均ヒット内最大振幅
            textBox_AvrMaxAmp1.Text = AEArray[3, 0].ToString("f3");
            textBox_AvrMaxAmp2.Text = AEArray[3, 1].ToString("f3");

            //平均ヒット持続時間
            textBox_AvrHitTime1.Text = AEArray[4, 0].ToString("f3");
            textBox_AvrHitTime2.Text = AEArray[4, 1].ToString("f3");

            //累計ヒットAEエネルギー
            textBox_TotalHitEnergy1.Text = AEArray[5, 0].ToString("f3");
            textBox_TotalHitEnergy2.Text = AEArray[5, 1].ToString("f3");



            //ヒット別AEパラメータ
            //ヒット内最大出力電圧
            textBox_MVol1.Text = AEparaArray[0, 0, MeasureCh].ToString("f5");
            textBox_MVol2.Text = AEparaArray[1, 0, MeasureCh].ToString("f5");
            textBox_MVol3.Text = AEparaArray[2, 0, MeasureCh].ToString("f5");
            textBox_MVol4.Text = AEparaArray[3, 0, MeasureCh].ToString("f5");
            textBox_MVol5.Text = AEparaArray[4, 0, MeasureCh].ToString("f5");
            textBox_MVol6.Text = AEparaArray[5, 0, MeasureCh].ToString("f5");
            textBox_MVol7.Text = AEparaArray[6, 0, MeasureCh].ToString("f5");
            textBox_MVol8.Text = AEparaArray[7, 0, MeasureCh].ToString("f5");
            textBox_MVol9.Text = AEparaArray[8, 0, MeasureCh].ToString("f5");
            textBox_MVol10.Text = AEparaArray[9, 0, MeasureCh].ToString("f5");
            
            //ヒット持続時間
            textBox_DTime1.Text = AEparaArray[0, 1, MeasureCh].ToString("f5");
            textBox_DTime2.Text = AEparaArray[1, 1, MeasureCh].ToString("f5");
            textBox_DTime3.Text = AEparaArray[2, 1, MeasureCh].ToString("f5");
            textBox_DTime4.Text = AEparaArray[3, 1, MeasureCh].ToString("f5");
            textBox_DTime5.Text = AEparaArray[4, 1, MeasureCh].ToString("f5");
            textBox_DTime6.Text = AEparaArray[5, 1, MeasureCh].ToString("f5");
            textBox_DTime7.Text = AEparaArray[6, 1, MeasureCh].ToString("f5");
            textBox_DTime8.Text = AEparaArray[7, 1, MeasureCh].ToString("f5");
            textBox_DTime9.Text = AEparaArray[8, 1, MeasureCh].ToString("f5");
            textBox_DTime10.Text = AEparaArray[9, 1, MeasureCh].ToString("f5");

            //ヒットAEエネルギー
            textBox_Energy1.Text = AEparaArray[0, 2, MeasureCh].ToString("f5");
            textBox_Energy2.Text = AEparaArray[1, 2, MeasureCh].ToString("f5");
            textBox_Energy3.Text = AEparaArray[2, 2, MeasureCh].ToString("f5");
            textBox_Energy4.Text = AEparaArray[3, 2, MeasureCh].ToString("f5");
            textBox_Energy5.Text = AEparaArray[4, 2, MeasureCh].ToString("f5");
            textBox_Energy6.Text = AEparaArray[5, 2, MeasureCh].ToString("f5");
            textBox_Energy7.Text = AEparaArray[6, 2, MeasureCh].ToString("f5");
            textBox_Energy8.Text = AEparaArray[7, 2, MeasureCh].ToString("f5");
            textBox_Energy9.Text = AEparaArray[8, 2, MeasureCh].ToString("f5");
            textBox_Energy10.Text = AEparaArray[9, 2, MeasureCh].ToString("f5");

            //ヒット平均周波数
            textBox_Freq1.Text = AEparaArray[0, 3, MeasureCh].ToString("f1");
            textBox_Freq2.Text = AEparaArray[1, 3, MeasureCh].ToString("f1");
            textBox_Freq3.Text = AEparaArray[2, 3, MeasureCh].ToString("f1");
            textBox_Freq4.Text = AEparaArray[3, 3, MeasureCh].ToString("f1");
            textBox_Freq5.Text = AEparaArray[4, 3, MeasureCh].ToString("f1");
            textBox_Freq6.Text = AEparaArray[5, 3, MeasureCh].ToString("f1");
            textBox_Freq7.Text = AEparaArray[6, 3, MeasureCh].ToString("f1");
            textBox_Freq8.Text = AEparaArray[7, 3, MeasureCh].ToString("f1");
            textBox_Freq9.Text = AEparaArray[8, 3, MeasureCh].ToString("f1");
            textBox_Freq10.Text = AEparaArray[9, 3, MeasureCh].ToString("f1");
        }


        //測定条件を格納した配列を取得
        public void Get_AEArray(int preDataLeng, double [,] preAEArray)
        {
            int i;

            DataLeng = preDataLeng;

            for(i = 0; i < 5; i++)
            {
                AEArray[i, 0] = preAEArray[i, 0];
                AEArray[i, 1] = preAEArray[i, 1];
            }

        }


        //ヒットAEパラメータを格納した配列を取得
        public void Get_Array(double[,,] preAEparaArray)
        {
            int i;
            
            for(i = 0; i < 20; i++)
            {
                AEparaArray[i, 0, 0] = preAEparaArray[i, 0, 0];
                AEparaArray[i, 0, 1] = preAEparaArray[i, 0, 1];
                AEparaArray[i, 1, 0] = preAEparaArray[i, 1, 0];
                AEparaArray[i, 1, 1] = preAEparaArray[i, 1, 1];
                AEparaArray[i, 2, 0] = preAEparaArray[i, 2, 0];
                AEparaArray[i, 2, 1] = preAEparaArray[i, 2, 1];
                AEparaArray[i, 3, 0] = preAEparaArray[i, 3, 0];
                AEparaArray[i, 3, 1] = preAEparaArray[i, 3, 1];
            }
        }


        //測定データとしきい値を取得
        //timerのたびに50000点コピーはつらいので，このタイミングで表示までしてもいい
        public void Get_ArrayOffset(double[] preThreshold, double[,] preArrayOffset)
        {
            int i;


            //描画のリセット
            chart1.Series.Clear();
            chart2.Series.Clear();

            //描画の追加
            series1 = new Series("ch1");
            series2 = new Series("ch2");
            series_th1 = new Series("threshold1");
            series_th2 = new Series("threshold2");

            series1.ChartType = SeriesChartType.Line;
            series2.ChartType = SeriesChartType.Line;
            series_th1.ChartType = SeriesChartType.Line;
            series_th2.ChartType = SeriesChartType.Line;

            chart1.Series.Add(series1);
            chart1.Series.Add(series_th1);
            chart2.Series.Add(series2);
            chart2.Series.Add(series_th2);

            Axis axisX1 = chart1.ChartAreas[0].AxisX;
            axisX1.Title = "サンプリングカウント";
            Axis axisY1 = chart1.ChartAreas[0].AxisY;
            axisY1.Title = "電圧(V)";

            Axis axisX2 = chart2.ChartAreas[0].AxisX;
            axisX2.Title = "サンプリングカウント";
            Axis axisY2 = chart1.ChartAreas[0].AxisY;
            axisY2.Title = "電圧(V)";

            series1.Color = Color.Black;
            series_th1.Color = Color.Red;
            series2.Color = Color.Black;
            series_th2.Color = Color.Red;

            Threshold[0] = preThreshold[0];
            Threshold[1] = preThreshold[1];

            ArrayOffset = new double[2, DataLeng];

            for(i = 0; i < DataLeng; i++)
            {
                ArrayOffset[0, i] = preArrayOffset[0, i];
                ArrayOffset[1, i] = preArrayOffset[1, i];
            }

            DateTime start = DateTime.Now;
            for (i = 0; i < DataLeng; i++)
            {
                //series_th1.Points.AddXY(i+1, Threshold[0]);

                //描画メイン
                //series1.Points.AddXY(i + 1, ArrayOffset[0, i]);
                

                //series2.Points.AddXY(i, ArrayOffset[1, i]);
                //series_th1.Points.AddXY(i, Threshold[0]);
                
            }
            DateTime End = DateTime.Now;
            TimeSpan duration = End - start;
            time.Text = duration.TotalMilliseconds.ToString() + "ms" ;


        }


        //測定条件，AEパラメータを格納した配列を取得 - 読み込み用
        public void Get_AEArray_read(double [,] preMeasureArray, double [,]preAEArray)
        {
            //測定条件
            DataLeng = (int)preMeasureArray[0, 7];

            Threshold[0] = preMeasureArray[0, 9]; 
            Threshold[1] = preMeasureArray[0, 9]; 

            //累計ヒット数
            AEArray[0, 0] = preAEArray[0, 2];
            AEArray[0, 1] = preAEArray[1, 2];

            //ヒット数
            AEArray[1, 0] = preAEArray[0, 4];
            AEArray[1, 1] = preAEArray[1, 4];

            //平均ヒット数
            AEArray[2, 0] = preAEArray[0, 6];
            AEArray[2, 1] = preAEArray[1, 6];

            //ヒット内最大振幅
            AEArray[3, 0] = preAEArray[0, 10];
            AEArray[3, 1] = preAEArray[1, 10];

            //ヒット持続時間
            AEArray[4, 0] = preAEArray[0, 8];
            AEArray[4, 1] = preAEArray[1, 8];

            //累計ヒットAEエネルギー
            AEArray[5, 0] = preAEArray[0, 12];
            AEArray[5, 1] = preAEArray[1, 12];
        }


        //ヒット別AEパラメータを格納した配列を取得 - 読み込み用
        public void Get_Array_read(double[,] preAEArray)
        {
            int i;

            for (i = 0; i < 20; i++)
            {
                AEparaArray[i, 0, 0] = preAEArray[0, i + 2];
                AEparaArray[i, 1, 0] = preAEArray[1, i + 2];
                AEparaArray[i, 2, 0] = preAEArray[2, i + 2];
                AEparaArray[i, 3, 0] = preAEArray[3, i + 2];
                AEparaArray[i, 0, 1] = preAEArray[0, i + 23];
                AEparaArray[i, 1, 1] = preAEArray[1, i + 23];
                AEparaArray[i, 2, 1] = preAEArray[2, i + 23];
                AEparaArray[i, 3, 1] = preAEArray[3, i + 23];
            }
        }


        //測定データを格納した配列を取得 - 読み込み用
        public void Get_MeasureArray_read(double[,] preArrayOffset)
        {
            int i;

            for (i = 0; i < DataLeng; i++){
                ArrayOffset[0, i] = preArrayOffset[0, i];
                ArrayOffset[1, i] = preArrayOffset[1, i];
            }

            //波形表示
            //描画のリセット
            chart1.Series.Clear();
            chart2.Series.Clear();

            //描画の追加
            series1 = new Series("ch1");
            series2 = new Series("ch2");
            series_th1 = new Series("threshold1");
            series_th2 = new Series("threshold2");

            series1.ChartType = SeriesChartType.Line;
            series2.ChartType = SeriesChartType.Line;
            series_th1.ChartType = SeriesChartType.Line;
            series_th2.ChartType = SeriesChartType.Line;

            chart1.Series.Add(series1);
            chart1.Series.Add(series_th1);
            chart2.Series.Add(series2);
            chart2.Series.Add(series_th2);

            Axis axisX1 = chart1.ChartAreas[0].AxisX;
            axisX1.Title = "時間(s)";
            Axis axisY1 = chart1.ChartAreas[0].AxisY;
            axisY1.Title = "電圧(V)";

            Axis axisX2 = chart2.ChartAreas[0].AxisX;
            axisX2.Title = "時間(s)";
            Axis axisY2 = chart1.ChartAreas[0].AxisY;
            axisY2.Title = "電圧(V)";

            series1.Color = Color.Black;
            series_th1.Color = Color.Red;
            series2.Color = Color.Black;
            series_th2.Color = Color.Red;


            for (i = 0; i < DataLeng; i++)
            {
                series_th1.Points.AddXY((i + 1)*0.00002, Threshold[0]);
                series1.Points.AddXY((i + 1)*0.00002, ArrayOffset[0, i]);

                //series_th2.Points.AddXY(i + 1, Threshold[1]);
                //series2.Points.AddXY(i + 1, ArrayOffset[1, i]);
            }
        }


        //フォームを閉じる
        public void Form_Close()
        {
            this.Close();
        }


        //


        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            chart1.ChartAreas[0].CursorX.Interval = 0.001;
            chart1.ChartAreas[0].CursorY.Interval = 0.001;
            try
            {
                double x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);

                /*
                int index = 0;
                for (int i = 0; i < DataLeng; i++)
                {
                    if (x - i < 0.1) break; //取得した座標（x）に対して近い配列の要素をさがし、そのindexを取得する　
                    index = i;
                }

                */
                chart1.ChartAreas[0].CursorX.Position = x;
                chart1.ChartAreas[0].CursorY.Position = ArrayOffset[0, (int)(x * 50000)];

                /*
                double x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                double y = ArrayOffset[0, (int)x];
                chart1.ChartAreas[0].CursorX.Position = x;
                chart1.ChartAreas[0].CursorY.Position = y;
                */

                ch1_x.Text = "x : " + x.ToString("f5");
                ch1_y.Text = "y : " + ArrayOffset[0, (int)(x * 50000)].ToString("f5");
                ch1_th.Text = "threshold : " + Threshold[0].ToString("f3");
            }
            catch
            {

            }
        }


        private void chart2_MouseMove(object sender, MouseEventArgs e)
        {
            chart2.ChartAreas[0].CursorX.Interval = 0.001;
            chart2.ChartAreas[0].CursorY.Interval = 0.001;
            try
            {
                int x = (int)chart2.ChartAreas[0].AxisX.PixelPositionToValue(e.X);

                /*
                int index = 0;
                for (int i = 0; i < DataLeng; i++)
                {
                    if (x - i < 0.1) break; //取得した座標（x）に対して近い配列の要素をさがし、そのindexを取得する　
                    index = x;
                }
                */

                chart2.ChartAreas[0].CursorX.Position = x;
                chart2.ChartAreas[0].CursorY.Position = ArrayOffset[1, x];

                ch2_x.Text = "x : " + x.ToString("f5");
                ch2_y.Text = "y : " + ArrayOffset[1, x].ToString("f3");
                ch2_th.Text = "threshold : " + Threshold[1].ToString("f3");
            }
            catch
            {

            }
        }
    }
}