namespace TheMinx
{
    partial class frmBetResults
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Results_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BetResults = new TheMinx.BetResults();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.calDateTime = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.btnLoadData = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.chkBetOnly = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.Results_ReportTableAdapter = new TheMinx.BetResultsTableAdapters.Results_ReportTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Results_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BetResults)).BeginInit();
            this.SuspendLayout();
            // 
            // Results_ReportBindingSource
            // 
            this.Results_ReportBindingSource.DataMember = "Results_Report";
            this.Results_ReportBindingSource.DataSource = this.BetResults;
            // 
            // BetResults
            // 
            this.BetResults.DataSetName = "BetResults";
            this.BetResults.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource2.Name = "BetResutls";
            reportDataSource2.Value = this.Results_ReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TheMinx.rptBetResults.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 62);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1480, 534);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Race Results";
            // 
            // calDateTime
            // 
            this.calDateTime.CalendarTodayDate = new System.DateTime(2014, 3, 9, 0, 0, 0, 0);
            this.calDateTime.Location = new System.Drawing.Point(230, 17);
            this.calDateTime.Name = "calDateTime";
            this.calDateTime.Size = new System.Drawing.Size(150, 21);
            this.calDateTime.TabIndex = 3;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(408, 16);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(90, 25);
            this.btnLoadData.TabIndex = 4;
            this.btnLoadData.Values.Text = "Load Data";
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // chkBetOnly
            // 
            this.chkBetOnly.Checked = true;
            this.chkBetOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBetOnly.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkBetOnly.Location = new System.Drawing.Point(514, 18);
            this.chkBetOnly.Name = "chkBetOnly";
            this.chkBetOnly.Size = new System.Drawing.Size(104, 20);
            this.chkBetOnly.TabIndex = 5;
            this.chkBetOnly.Text = "Bet Races Only";
            this.chkBetOnly.Values.Text = "Bet Races Only";
            // 
            // Results_ReportTableAdapter
            // 
            this.Results_ReportTableAdapter.ClearBeforeFill = true;
            // 
            // frmBetResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 596);
            this.Controls.Add(this.chkBetOnly);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.calDateTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmBetResults";
            this.Text = "frmBetResults";
            this.Load += new System.EventHandler(this.frmBetResults_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Results_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BetResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Results_ReportBindingSource;
        private BetResults BetResults;
        private BetResultsTableAdapters.Results_ReportTableAdapter Results_ReportTableAdapter;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker calDateTime;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLoadData;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkBetOnly;
    }
}