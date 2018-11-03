using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheMinx
{
    public partial class frmBetResults : Form
    {
        public frmBetResults()
        {
            InitializeComponent();
        }

        private void frmBetResults_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(frmBetResults_Resize);
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            string dt = calDateTime.Value.ToString("yyyyMMdd");
            if (!chkBetOnly.Checked)
            {
                this.Results_ReportTableAdapter.Fill(this.BetResults.Results_Report, dt);
            }
            else
            {
                this.Results_ReportTableAdapter.FillBy(this.BetResults.Results_Report, dt);
            }

            this.reportViewer1.RefreshReport();
        }

        private void frmBetResults_Resize(object sender, System.EventArgs e)
        {
            reportViewer1.Width = this.Width - 40;
            reportViewer1.Height = this.Height - 125;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
