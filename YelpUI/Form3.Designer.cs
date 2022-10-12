
namespace YelpUI
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartCheckIns = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnCheckIn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartCheckIns)).BeginInit();
            this.SuspendLayout();
            // 
            // chartCheckIns
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.Name = "ChartArea1";
            this.chartCheckIns.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartCheckIns.Legends.Add(legend1);
            this.chartCheckIns.Location = new System.Drawing.Point(-1, 0);
            this.chartCheckIns.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartCheckIns.Name = "chartCheckIns";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Number Of Check ins";
            this.chartCheckIns.Series.Add(series1);
            this.chartCheckIns.Size = new System.Drawing.Size(716, 358);
            this.chartCheckIns.TabIndex = 0;
            this.chartCheckIns.Text = "chart1";
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Location = new System.Drawing.Point(518, 252);
            this.btnCheckIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(116, 60);
            this.btnCheckIn.TabIndex = 1;
            this.btnCheckIn.Text = "Check In";
            this.btnCheckIn.UseVisualStyleBackColor = true;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 360);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.chartCheckIns);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form3";
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.chartCheckIns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataVisualization.Charting.Chart chartCheckIns;
        private System.Windows.Forms.Button btnCheckIn;
    }
}