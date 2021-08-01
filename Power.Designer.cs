﻿
namespace PomiaryGUI
{
    partial class Power
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.DateFrom = new System.Windows.Forms.DateTimePicker();
            this.DateTo = new System.Windows.Forms.DateTimePicker();
            this.HoursFrom = new System.Windows.Forms.ComboBox();
            this.HoursTo = new System.Windows.Forms.ComboBox();
            this.MinutesFrom = new System.Windows.Forms.ComboBox();
            this.MinutesTo = new System.Windows.Forms.ComboBox();
            this.buttonShow = new System.Windows.Forms.Button();
            this.chart = new LiveCharts.WinForms.CartesianChart();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkP = new System.Windows.Forms.CheckBox();
            this.checkP_L1 = new System.Windows.Forms.CheckBox();
            this.checkP_L2 = new System.Windows.Forms.CheckBox();
            this.checkP_L3 = new System.Windows.Forms.CheckBox();
            this.checkQ = new System.Windows.Forms.CheckBox();
            this.checkQ_L1 = new System.Windows.Forms.CheckBox();
            this.checkQ_L2 = new System.Windows.Forms.CheckBox();
            this.checkQ_L3 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // DateFrom
            // 
            this.DateFrom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateFrom.Location = new System.Drawing.Point(80, 1);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(90, 26);
            this.DateFrom.TabIndex = 0;
            // 
            // DateTo
            // 
            this.DateTo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTo.Location = new System.Drawing.Point(80, 27);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(90, 26);
            this.DateTo.TabIndex = 0;
            // 
            // HoursFrom
            // 
            this.HoursFrom.BackColor = System.Drawing.Color.White;
            this.HoursFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HoursFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HoursFrom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HoursFrom.ForeColor = System.Drawing.Color.Black;
            this.HoursFrom.FormattingEnabled = true;
            this.HoursFrom.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.HoursFrom.Location = new System.Drawing.Point(0, 0);
            this.HoursFrom.Name = "HoursFrom";
            this.HoursFrom.Size = new System.Drawing.Size(40, 27);
            this.HoursFrom.TabIndex = 1;
            // 
            // HoursTo
            // 
            this.HoursTo.BackColor = System.Drawing.Color.White;
            this.HoursTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HoursTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HoursTo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HoursTo.ForeColor = System.Drawing.Color.Black;
            this.HoursTo.FormattingEnabled = true;
            this.HoursTo.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.HoursTo.Location = new System.Drawing.Point(0, 27);
            this.HoursTo.Name = "HoursTo";
            this.HoursTo.Size = new System.Drawing.Size(40, 27);
            this.HoursTo.TabIndex = 1;
            // 
            // MinutesFrom
            // 
            this.MinutesFrom.BackColor = System.Drawing.Color.White;
            this.MinutesFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MinutesFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinutesFrom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MinutesFrom.ForeColor = System.Drawing.Color.Black;
            this.MinutesFrom.FormattingEnabled = true;
            this.MinutesFrom.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.MinutesFrom.Location = new System.Drawing.Point(40, 0);
            this.MinutesFrom.Name = "MinutesFrom";
            this.MinutesFrom.Size = new System.Drawing.Size(40, 27);
            this.MinutesFrom.TabIndex = 1;
            // 
            // MinutesTo
            // 
            this.MinutesTo.BackColor = System.Drawing.Color.White;
            this.MinutesTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MinutesTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinutesTo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MinutesTo.ForeColor = System.Drawing.Color.Black;
            this.MinutesTo.FormattingEnabled = true;
            this.MinutesTo.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.MinutesTo.Location = new System.Drawing.Point(40, 27);
            this.MinutesTo.Name = "MinutesTo";
            this.MinutesTo.Size = new System.Drawing.Size(40, 27);
            this.MinutesTo.TabIndex = 1;
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(170, 0);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(75, 54);
            this.buttonShow.TabIndex = 2;
            this.buttonShow.Text = "Show";
            this.buttonShow.UseVisualStyleBackColor = true;
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chart.ForeColor = System.Drawing.Color.Black;
            this.chart.Location = new System.Drawing.Point(0, 60);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(840, 540);
            this.chart.TabIndex = 3;
            this.chart.Text = "cartesianChart1";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Szyn-1",
            "Szyn-2",
            "Szyn-3",
            "Szyn-4",
            "Szyn-5",
            "Piec 5",
            "Piec 6",
            "Piec 7",
            "Parowy BM",
            "Parowy AB_BQ",
            "Piec 3",
            "Piec 8"});
            this.comboBox1.Location = new System.Drawing.Point(245, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(595, 32);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // checkP
            // 
            this.checkP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP.AutoSize = true;
            this.checkP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkP.Location = new System.Drawing.Point(376, 33);
            this.checkP.Name = "checkP";
            this.checkP.Size = new System.Drawing.Size(37, 23);
            this.checkP.TabIndex = 5;
            this.checkP.Text = "P";
            this.checkP.UseVisualStyleBackColor = true;
            // 
            // checkP_L1
            // 
            this.checkP_L1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP_L1.AutoSize = true;
            this.checkP_L1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkP_L1.Location = new System.Drawing.Point(413, 33);
            this.checkP_L1.Name = "checkP_L1";
            this.checkP_L1.Size = new System.Drawing.Size(63, 23);
            this.checkP_L1.TabIndex = 5;
            this.checkP_L1.Text = "P_L1";
            this.checkP_L1.UseVisualStyleBackColor = true;
            // 
            // checkP_L2
            // 
            this.checkP_L2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP_L2.AutoSize = true;
            this.checkP_L2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkP_L2.Location = new System.Drawing.Point(476, 33);
            this.checkP_L2.Name = "checkP_L2";
            this.checkP_L2.Size = new System.Drawing.Size(63, 23);
            this.checkP_L2.TabIndex = 5;
            this.checkP_L2.Text = "P_L2";
            this.checkP_L2.UseVisualStyleBackColor = true;
            // 
            // checkP_L3
            // 
            this.checkP_L3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkP_L3.AutoSize = true;
            this.checkP_L3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkP_L3.Location = new System.Drawing.Point(539, 33);
            this.checkP_L3.Name = "checkP_L3";
            this.checkP_L3.Size = new System.Drawing.Size(63, 23);
            this.checkP_L3.TabIndex = 5;
            this.checkP_L3.Text = "P_L3";
            this.checkP_L3.UseVisualStyleBackColor = true;
            // 
            // checkQ
            // 
            this.checkQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkQ.AutoSize = true;
            this.checkQ.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkQ.Location = new System.Drawing.Point(602, 33);
            this.checkQ.Name = "checkQ";
            this.checkQ.Size = new System.Drawing.Size(40, 23);
            this.checkQ.TabIndex = 5;
            this.checkQ.Text = "Q";
            this.checkQ.UseVisualStyleBackColor = true;
            // 
            // checkQ_L1
            // 
            this.checkQ_L1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkQ_L1.AutoSize = true;
            this.checkQ_L1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkQ_L1.Location = new System.Drawing.Point(642, 33);
            this.checkQ_L1.Name = "checkQ_L1";
            this.checkQ_L1.Size = new System.Drawing.Size(66, 23);
            this.checkQ_L1.TabIndex = 5;
            this.checkQ_L1.Text = "Q_L1";
            this.checkQ_L1.UseVisualStyleBackColor = true;
            // 
            // checkQ_L2
            // 
            this.checkQ_L2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkQ_L2.AutoSize = true;
            this.checkQ_L2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkQ_L2.Location = new System.Drawing.Point(708, 33);
            this.checkQ_L2.Name = "checkQ_L2";
            this.checkQ_L2.Size = new System.Drawing.Size(66, 23);
            this.checkQ_L2.TabIndex = 5;
            this.checkQ_L2.Text = "Q_L2";
            this.checkQ_L2.UseVisualStyleBackColor = true;
            // 
            // checkQ_L3
            // 
            this.checkQ_L3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkQ_L3.AutoSize = true;
            this.checkQ_L3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkQ_L3.Location = new System.Drawing.Point(774, 33);
            this.checkQ_L3.Name = "checkQ_L3";
            this.checkQ_L3.Size = new System.Drawing.Size(66, 23);
            this.checkQ_L3.TabIndex = 5;
            this.checkQ_L3.Text = "Q_L3";
            this.checkQ_L3.UseVisualStyleBackColor = true;
            // 
            // Power
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.checkQ_L3);
            this.Controls.Add(this.checkQ_L2);
            this.Controls.Add(this.checkQ_L1);
            this.Controls.Add(this.checkQ);
            this.Controls.Add(this.checkP_L3);
            this.Controls.Add(this.checkP_L2);
            this.Controls.Add(this.checkP_L1);
            this.Controls.Add(this.checkP);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.MinutesTo);
            this.Controls.Add(this.HoursTo);
            this.Controls.Add(this.MinutesFrom);
            this.Controls.Add(this.HoursFrom);
            this.Controls.Add(this.DateTo);
            this.Controls.Add(this.DateFrom);
            this.Name = "Power";
            this.Size = new System.Drawing.Size(840, 600);
            this.Load += new System.EventHandler(this.Power_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DateFrom;
        private System.Windows.Forms.DateTimePicker DateTo;
        private System.Windows.Forms.ComboBox HoursFrom;
        private System.Windows.Forms.ComboBox HoursTo;
        private System.Windows.Forms.ComboBox MinutesFrom;
        private System.Windows.Forms.ComboBox MinutesTo;
        private System.Windows.Forms.Button buttonShow;
        private LiveCharts.WinForms.CartesianChart chart;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkP;
        private System.Windows.Forms.CheckBox checkP_L1;
        private System.Windows.Forms.CheckBox checkP_L2;
        private System.Windows.Forms.CheckBox checkP_L3;
        private System.Windows.Forms.CheckBox checkQ;
        private System.Windows.Forms.CheckBox checkQ_L1;
        private System.Windows.Forms.CheckBox checkQ_L2;
        private System.Windows.Forms.CheckBox checkQ_L3;
    }
}
