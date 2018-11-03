using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace TheMinx
{
    public partial class frmChooseDate : Form
    {
        Globals global = new Globals();
        public frmChooseDate()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime myDT = dtDate.Value.Date;
            string selDt = myDT.ToString("yyyyMMdd");
            string data = null;

            StringBuilder sb = new StringBuilder();
            global.dbCon = new SqlConnection(Globals.dbConString);
            global.dbCon.Open();
            //            string cmd = "select * from Bets where RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "'";
            string cmd = "select * from Bets where RaceDate = '" + selDt + "' order by Venue,Racenum,Box ASC";
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(cmd, global.dbCon);
            da.Fill(dt);
            foreach (DataColumn col in dt.Columns)
            {
                sb.Append(col.ColumnName + ',');
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(Environment.NewLine);

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string drow = row[i].ToString();
                    if (i == 16)
                    {
                        drow = drow.Replace(",", "-");
                    }
                    sb.Append(drow + ",");
                }

                sb.Append(Environment.NewLine);
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, sb.ToString());

            }

            this.Close();
            this.Dispose();
        }

        private void frmChooseDate_Load(object sender, EventArgs e)
        {

        }
    }
}
