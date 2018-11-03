namespace TheMinx
{
    partial class Results_Report
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
            Telerik.Reporting.InstanceReportSource instanceReportSource1 = new Telerik.Reporting.InstanceReportSource();
            this.radDateTime = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnGo = new Telerik.WinControls.UI.RadButton();
            this.reportViewer1 = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.rptTest1 = new TheMinx.rptTest();
            ((System.ComponentModel.ISupportInitialize)(this.radDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptTest1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radDateTime
            // 
            this.radDateTime.Location = new System.Drawing.Point(711, 2);
            this.radDateTime.Name = "radDateTime";
            this.radDateTime.Size = new System.Drawing.Size(164, 20);
            this.radDateTime.TabIndex = 2;
            this.radDateTime.TabStop = false;
            this.radDateTime.Text = "Monday, 20 October 2014";
            this.radDateTime.Value = new System.DateTime(2014, 10, 20, 17, 47, 27, 993);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(590, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(115, 18);
            this.radLabel1.TabIndex = 3;
            this.radLabel1.Text = "Select Date to Report:";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(881, 0);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(64, 24);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Go";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.AutoSize = true;
            this.reportViewer1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("RaceDate", "20140910"));
            instanceReportSource1.ReportDocument = this.rptTest1;
            this.reportViewer1.ReportSource = instanceReportSource1;
            this.reportViewer1.Size = new System.Drawing.Size(1205, 584);
            this.reportViewer1.TabIndex = 1;
            // 
            // rptTest1
            // 
            this.rptTest1.Name = "rptTest";
            // 
            // Results_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 584);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radDateTime);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Results_Report";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Results Report";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.Results_Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptTest1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.ReportViewer.WinForms.ReportViewer reportViewer1;
        private rptTest rptTest1;
        private Telerik.WinControls.UI.RadDateTimePicker radDateTime;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton btnGo;
    }
}
