
namespace AESystem
{
    partial class AESystem
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AESystem));
            this.label18 = new System.Windows.Forms.Label();
            this.Button_ID = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TextBox_SamplingCount = new System.Windows.Forms.TextBox();
            this.TextBox_SamplingRate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TextBox_Threshold = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_ProcessMessage = new System.Windows.Forms.Label();
            this.comboBox_Range2 = new System.Windows.Forms.ComboBox();
            this.comboBox_Range1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_Ch2 = new System.Windows.Forms.CheckBox();
            this.checkBox_Ch1 = new System.Windows.Forms.CheckBox();
            this.comboBox_DeviceID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_Start = new System.Windows.Forms.ToolStripButton();
            this.Button_Trigger = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.label_FilePath = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上書き保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.名前を付けて保存AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.表示VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.TextBox_Threshold2 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Button_Threshold_Measure = new System.Windows.Forms.Button();
            this.checkBox_loop = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.miyatamasakioutlookjpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(193, 284);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(0, 12);
            this.label18.TabIndex = 96;
            // 
            // Button_ID
            // 
            this.Button_ID.Location = new System.Drawing.Point(178, 60);
            this.Button_ID.Name = "Button_ID";
            this.Button_ID.Size = new System.Drawing.Size(25, 23);
            this.Button_ID.TabIndex = 83;
            this.Button_ID.Text = "ID";
            this.Button_ID.UseVisualStyleBackColor = true;
            this.Button_ID.Click += new System.EventHandler(this.Button_ID_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(179, 159);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(13, 12);
            this.label20.TabIndex = 82;
            this.label20.Text = "V";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(179, 125);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(13, 12);
            this.label19.TabIndex = 81;
            this.label19.Text = "V";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(193, 254);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 12);
            this.label13.TabIndex = 76;
            this.label13.Text = "×20 ns (500-)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(179, 197);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 12);
            this.label12.TabIndex = 75;
            this.label12.Text = "V";
            // 
            // TextBox_SamplingCount
            // 
            this.TextBox_SamplingCount.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TextBox_SamplingCount.Location = new System.Drawing.Point(112, 281);
            this.TextBox_SamplingCount.Name = "TextBox_SamplingCount";
            this.TextBox_SamplingCount.ShortcutsEnabled = false;
            this.TextBox_SamplingCount.Size = new System.Drawing.Size(80, 19);
            this.TextBox_SamplingCount.TabIndex = 74;
            this.TextBox_SamplingCount.Text = "0";
            this.TextBox_SamplingCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_SamplingCount_KeyPress);
            // 
            // TextBox_SamplingRate
            // 
            this.TextBox_SamplingRate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TextBox_SamplingRate.Location = new System.Drawing.Point(112, 251);
            this.TextBox_SamplingRate.Name = "TextBox_SamplingRate";
            this.TextBox_SamplingRate.ShortcutsEnabled = false;
            this.TextBox_SamplingRate.Size = new System.Drawing.Size(80, 19);
            this.TextBox_SamplingRate.TabIndex = 73;
            this.TextBox_SamplingRate.Text = "1000";
            this.TextBox_SamplingRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_SamplingRate_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 285);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 12);
            this.label11.TabIndex = 72;
            this.label11.Text = "サンプリング数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 12);
            this.label8.TabIndex = 71;
            this.label8.Text = "サンプリング周期";
            // 
            // TextBox_Threshold
            // 
            this.TextBox_Threshold.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TextBox_Threshold.Location = new System.Drawing.Point(112, 191);
            this.TextBox_Threshold.Name = "TextBox_Threshold";
            this.TextBox_Threshold.ShortcutsEnabled = false;
            this.TextBox_Threshold.Size = new System.Drawing.Size(60, 19);
            this.TextBox_Threshold.TabIndex = 70;
            this.TextBox_Threshold.Text = "0";
            this.TextBox_Threshold.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Threshold_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 12);
            this.label7.TabIndex = 69;
            this.label7.Text = "Threshold";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 12);
            this.label5.TabIndex = 68;
            this.label5.Text = "処理メッセージ";
            // 
            // textBox_ProcessMessage
            // 
            this.textBox_ProcessMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textBox_ProcessMessage.Location = new System.Drawing.Point(14, 340);
            this.textBox_ProcessMessage.Name = "textBox_ProcessMessage";
            this.textBox_ProcessMessage.Size = new System.Drawing.Size(260, 64);
            this.textBox_ProcessMessage.TabIndex = 67;
            // 
            // comboBox_Range2
            // 
            this.comboBox_Range2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Range2.FormattingEnabled = true;
            this.comboBox_Range2.Items.AddRange(new object[] {
            "±10",
            "±5",
            "±2.5",
            "±1.25",
            "10",
            "5",
            "2.5"});
            this.comboBox_Range2.Location = new System.Drawing.Point(112, 153);
            this.comboBox_Range2.Name = "comboBox_Range2";
            this.comboBox_Range2.Size = new System.Drawing.Size(60, 20);
            this.comboBox_Range2.TabIndex = 66;
            // 
            // comboBox_Range1
            // 
            this.comboBox_Range1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Range1.FormattingEnabled = true;
            this.comboBox_Range1.Items.AddRange(new object[] {
            "±10",
            "±5",
            "±2.5",
            "±1.25",
            "10",
            "5",
            "2.5"});
            this.comboBox_Range1.Location = new System.Drawing.Point(112, 123);
            this.comboBox_Range1.Name = "comboBox_Range1";
            this.comboBox_Range1.Size = new System.Drawing.Size(60, 20);
            this.comboBox_Range1.TabIndex = 65;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 64;
            this.label3.Text = "Range";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 12);
            this.label2.TabIndex = 63;
            this.label2.Text = "Ch";
            // 
            // checkBox_Ch2
            // 
            this.checkBox_Ch2.AutoSize = true;
            this.checkBox_Ch2.Location = new System.Drawing.Point(17, 155);
            this.checkBox_Ch2.Name = "checkBox_Ch2";
            this.checkBox_Ch2.Size = new System.Drawing.Size(30, 16);
            this.checkBox_Ch2.TabIndex = 62;
            this.checkBox_Ch2.Text = "2";
            this.checkBox_Ch2.UseVisualStyleBackColor = true;
            // 
            // checkBox_Ch1
            // 
            this.checkBox_Ch1.AutoSize = true;
            this.checkBox_Ch1.Location = new System.Drawing.Point(17, 125);
            this.checkBox_Ch1.Name = "checkBox_Ch1";
            this.checkBox_Ch1.Size = new System.Drawing.Size(30, 16);
            this.checkBox_Ch1.TabIndex = 61;
            this.checkBox_Ch1.Text = "1";
            this.checkBox_Ch1.UseVisualStyleBackColor = true;
            // 
            // comboBox_DeviceID
            // 
            this.comboBox_DeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DeviceID.FormattingEnabled = true;
            this.comboBox_DeviceID.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.comboBox_DeviceID.Location = new System.Drawing.Point(112, 62);
            this.comboBox_DeviceID.Name = "comboBox_DeviceID";
            this.comboBox_DeviceID.Size = new System.Drawing.Size(60, 20);
            this.comboBox_DeviceID.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 59;
            this.label1.Text = "Device ID";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_Start,
            this.Button_Trigger,
            this.toolStripSeparator1,
            this.Button_Stop,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.label_FilePath});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(286, 31);
            this.toolStrip1.TabIndex = 99;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Button_Start
            // 
            this.Button_Start.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Start.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_Start.Image = ((System.Drawing.Image)(resources.GetObject("Button_Start.Image")));
            this.Button_Start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(28, 28);
            this.Button_Start.Text = "Start";
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // Button_Trigger
            // 
            this.Button_Trigger.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Trigger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_Trigger.Image = ((System.Drawing.Image)(resources.GetObject("Button_Trigger.Image")));
            this.Button_Trigger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Trigger.Name = "Button_Trigger";
            this.Button_Trigger.Size = new System.Drawing.Size(28, 28);
            this.Button_Trigger.Text = "toolStripButton1";
            this.Button_Trigger.Click += new System.EventHandler(this.Button_Trigger_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // Button_Stop
            // 
            this.Button_Stop.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_Stop.Image = ((System.Drawing.Image)(resources.GetObject("Button_Stop.Image")));
            this.Button_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(28, 28);
            this.Button_Stop.Text = "Stop";
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(55, 28);
            this.toolStripLabel1.Text = "FilePath :";
            // 
            // label_FilePath
            // 
            this.label_FilePath.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label_FilePath.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label_FilePath.Name = "label_FilePath";
            this.label_FilePath.Size = new System.Drawing.Size(12, 28);
            this.label_FilePath.Text = "-";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.表示VToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(286, 24);
            this.menuStrip1.TabIndex = 98;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開くToolStripMenuItem,
            this.開くOToolStripMenuItem,
            this.上書き保存SToolStripMenuItem,
            this.名前を付けて保存AToolStripMenuItem,
            this.終了XToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 開くToolStripMenuItem
            // 
            this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
            this.開くToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.開くToolStripMenuItem.Text = "新規作成(&N)";
            this.開くToolStripMenuItem.Click += new System.EventHandler(this.開くToolStripMenuItem_Click);
            // 
            // 開くOToolStripMenuItem
            // 
            this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
            this.開くOToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.開くOToolStripMenuItem.Text = "開く(&O)";
            this.開くOToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripMenuItem_Click);
            // 
            // 上書き保存SToolStripMenuItem
            // 
            this.上書き保存SToolStripMenuItem.Name = "上書き保存SToolStripMenuItem";
            this.上書き保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.上書き保存SToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.上書き保存SToolStripMenuItem.Text = "上書き保存(&S)";
            // 
            // 名前を付けて保存AToolStripMenuItem
            // 
            this.名前を付けて保存AToolStripMenuItem.Name = "名前を付けて保存AToolStripMenuItem";
            this.名前を付けて保存AToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.名前を付けて保存AToolStripMenuItem.Text = "名前を付けて保存(&A)";
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // 表示VToolStripMenuItem
            // 
            this.表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
            this.表示VToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.表示VToolStripMenuItem.Text = "表示(&V)";
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miyatamasakioutlookjpToolStripMenuItem});
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 200;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 250;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // TextBox_Threshold2
            // 
            this.TextBox_Threshold2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TextBox_Threshold2.Location = new System.Drawing.Point(112, 216);
            this.TextBox_Threshold2.Name = "TextBox_Threshold2";
            this.TextBox_Threshold2.ShortcutsEnabled = false;
            this.TextBox_Threshold2.Size = new System.Drawing.Size(60, 19);
            this.TextBox_Threshold2.TabIndex = 127;
            this.TextBox_Threshold2.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(179, 219);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 12);
            this.label15.TabIndex = 128;
            this.label15.Text = "V";
            // 
            // Button_Threshold_Measure
            // 
            this.Button_Threshold_Measure.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Threshold_Measure.Location = new System.Drawing.Point(198, 214);
            this.Button_Threshold_Measure.Name = "Button_Threshold_Measure";
            this.Button_Threshold_Measure.Size = new System.Drawing.Size(23, 23);
            this.Button_Threshold_Measure.TabIndex = 95;
            this.Button_Threshold_Measure.UseVisualStyleBackColor = false;
            this.Button_Threshold_Measure.Click += new System.EventHandler(this.Button_Threshold_Measure_Click);
            // 
            // checkBox_loop
            // 
            this.checkBox_loop.AutoSize = true;
            this.checkBox_loop.Location = new System.Drawing.Point(200, 283);
            this.checkBox_loop.Name = "checkBox_loop";
            this.checkBox_loop.Size = new System.Drawing.Size(48, 16);
            this.checkBox_loop.TabIndex = 131;
            this.checkBox_loop.Text = "Loop";
            this.checkBox_loop.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 132;
            this.label4.Text = "label4";
            // 
            // miyatamasakioutlookjpToolStripMenuItem
            // 
            this.miyatamasakioutlookjpToolStripMenuItem.Name = "miyatamasakioutlookjpToolStripMenuItem";
            this.miyatamasakioutlookjpToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.miyatamasakioutlookjpToolStripMenuItem.Text = "miyatamasaki@outlook.jp";
            // 
            // AESystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 416);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox_loop);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.TextBox_Threshold2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.Button_Threshold_Measure);
            this.Controls.Add(this.Button_ID);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.TextBox_SamplingCount);
            this.Controls.Add(this.TextBox_SamplingRate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TextBox_Threshold);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_ProcessMessage);
            this.Controls.Add(this.comboBox_Range2);
            this.Controls.Add(this.comboBox_Range1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_Ch2);
            this.Controls.Add(this.checkBox_Ch1);
            this.Controls.Add(this.comboBox_DeviceID);
            this.Controls.Add(this.label1);
            this.Name = "AESystem";
            this.Text = "AESystem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AESystem_FormClosed);
            this.Load += new System.EventHandler(this.AESystem_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button Button_ID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TextBox_SamplingCount;
        private System.Windows.Forms.TextBox TextBox_SamplingRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TextBox_Threshold;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label textBox_ProcessMessage;
        private System.Windows.Forms.ComboBox comboBox_Range2;
        private System.Windows.Forms.ComboBox comboBox_Range1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_Ch2;
        private System.Windows.Forms.CheckBox checkBox_Ch1;
        private System.Windows.Forms.ComboBox comboBox_DeviceID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Button_Start;
        private System.Windows.Forms.ToolStripButton Button_Trigger;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Button_Stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel label_FilePath;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開くToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開くOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上書き保存SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 名前を付けて保存AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 表示VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.TextBox TextBox_Threshold2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button Button_Threshold_Measure;
        private System.Windows.Forms.CheckBox checkBox_loop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem miyatamasakioutlookjpToolStripMenuItem;
    }
}

