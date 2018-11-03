using System;


namespace TheMinx
{
    public partial class Results_Report : Telerik.WinControls.UI.RadForm
    {
        public Results_Report()
        {
            InitializeComponent();
        }

        private void Results_Report_Load(object sender, EventArgs e)
        {


        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            rptTest rpt = new rptTest();
            rpt.ReportParameters["RaceDate"].Value = radDateTime.Value.ToString("yyyyMMdd");
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
            reportViewer1.Refresh();
        }

    }
}
