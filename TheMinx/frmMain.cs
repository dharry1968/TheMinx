using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.IO;
using System.Data.SqlClient;

namespace TheMinx
{
    public partial class frmMain : Form
    {
        Utils utils = new Utils();
        Globals global = new Globals();
        System.Timers.Timer TokenTimer, LiveTimer, ResultsTimer, BalanceTimer;
        Thread ntg = null;
        About about = new About();

        static frmMain()
        {
            Telerik.WinControls.RadTypeResolver.Instance.ResolveTypesInCurrentAssembly = true;
        }

        public frmMain()
        {
            InitializeComponent();
            
            about.Show();
            Globals.DataLoaded = false;
            this.FormClosing += frmMain_FormClosing;

            TokenTimer = new System.Timers.Timer(900000);
            TokenTimer.AutoReset = true;
            TokenTimer.Elapsed += new ElapsedEventHandler(TokenTimer_Elapsed);
            TokenTimer.Start();

            ResultsTimer = new System.Timers.Timer(180000);
            ResultsTimer.AutoReset = true;
            ResultsTimer.Elapsed += new ElapsedEventHandler(ResultsTimer_Elapsed);
//            ResultsTimer.Start();

            BalanceTimer = new System.Timers.Timer(300000);
            BalanceTimer.AutoReset = true;
            BalanceTimer.Elapsed += new ElapsedEventHandler(BalanceTimer_Elapsed);
            BalanceTimer.Start();

            utils.LoadDefaults();
            utils.Login();
            utils.GetBalances();
            utils.ntgLabel = lblNextRace;
            utils.dgRace = dgRace;
            utils.txtStatus = txtStatus;
            utils.ntgRacestxt = txtNext5;

            //utils.AutoBet(!this.tglPrice.Checked, Convert.ToInt32(cmbRace.SelectedValue.ToString()), raceID, Globals.UseRatingBetAmt, this.chkBetVic.Checked, this.chkBetNsw.Checked, this.chkBetQld.Checked, Convert.ToDouble(this.txtBetPct.Text), Convert.ToDouble(this.txtBetTo.Text), this.chkBestPrice.Checked);

            ntg = new Thread(new ThreadStart(utils.UpdateNextToGo));
            txtStopWin.Text = Globals.StopWin.ToString();
            txtStopLoss.Text = Globals.StopLoss.ToString();
            txtBetTo.Text = Globals.BetToWin.ToString();
            if (!Globals.UseRatingBetAmt)
            {
                btnBetPct.Checked = true;
                btnBetPct.Text = "Bet %";
            }
            else
            {
                btnBetPct.Checked = false;
                btnBetPct.Text = "Bet Ratings";
            }
            dgRace.CellValueChanged += dgRace_CellValueChanged;
        }
 
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.todaysVenueTableAdapter.Fill(this.todaysVenue._TodaysVenue);
            cmbVenue.SelectedIndexChanged += cmbVenue_SelectedIndexChanged;
            cmbVenue_SelectedIndexChanged(null, null);
            chkBestPrice.Checked = true;
            chkBestPrice.BackColor = Color.Green;
            chkBetNsw.Checked = false;
            chkBetNsw.BackColor = Color.Red;
            chkBetVic.Checked = false;
            chkBetVic.BackColor = Color.Red;
            chkBetQld.Checked = false;
            chkBetQld.BackColor = Color.Red;
            chkBestPrice.CheckedChanged += chkBestPrice_CheckedChanged;
            chkBetNsw.CheckedChanged += chkBetNsw_CheckedChanged;
            chkBetQld.CheckedChanged += chkBetQld_CheckedChanged;
            chkBetVic.CheckedChanged += chkBetVic_CheckedChanged;

            if (!Globals.VicEnable)
            {
                chkBetVic.Enabled = false;
                chkBetVic.BackColor = Color.DarkGray;
                chkBetVic.CheckedChanged -= chkBetVic_CheckedChanged;
            }

            if (!Globals.NswEnable)
            {
                chkBetNsw.Enabled = false;
                chkBetNsw.BackColor = Color.DarkGray;
                chkBetNsw.CheckedChanged -= chkBetNsw_CheckedChanged;
            }

            if (!Globals.QldEnable)
            {
                chkBetQld.Enabled = false;
                chkBetQld.BackColor = Color.DarkGray;
                chkBetQld.CheckedChanged -= chkBetQld_CheckedChanged;
            }

            Globals.offset = Convert.ToInt32(txtBetOffset.Text);
            Globals.BetType = "WN";

            chkBetting.Checked = false;
            chkBetting.BackColor = Color.Red;
            chkBetting.CheckedChanged += chkBetting_CheckedChanged;

            utils.pctOn = this.btnBetPct.Checked;
            utils.useRatingBetAmount = Globals.UseRatingBetAmt;
            utils.betOnVic = this.chkBetVic.Checked;
            utils.betOnNsw = this.chkBetNsw.Checked;
            utils.betOnQld = this.chkBetQld.Checked;
            utils.betPct = Convert.ToDouble(this.txtBetPct.Text);
            utils.betToWin = Convert.ToDouble(this.txtBetTo.Text);
            utils.betOnAll = this.chkBestPrice.Checked;
            about.Close();
        }

        private void frmMain_FormClosing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void TokenTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            utils.Login();
        }

        public void BalanceTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            utils.GetBalances();
            txtQldBal.Text = string.Format("QLD Bal: {0:C}", Globals.QldBal);
            txtNswBal.Text = string.Format("NSW Bal: {0:C}", Globals.NswBal);
            txtVicBal.Text = string.Format("VIC Bal: {0:C}", Globals.VicBal);
            txtTotalBal.Text = string.Format("Total Bal: {0:C}", Globals.TotalBal);
        }


        private void btnLoadData_Click(object sender, EventArgs e)
        {
            pgProgress.Visible = true;
//            pgProgress.showtext = true;
            pgProgress.Text = "Loading Data, Please wait.";
            Thread th = null;
            utils.prog = pgProgress;
            utils.ts = tsText1;
//            pgProgress.Start();
            th = new Thread(new ThreadStart(utils.LoadData));
            th.IsBackground = true;
            th.Start();
            
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
            txtBetTo.Text = Globals.BetToWin.ToString();
            if (Globals.PctOn)
            {
                btnBetPct.Checked = true;
            }
            else
            {
                btnBetPct.Checked = false;
            }
            txtBetPct.Text = Globals.BetPct.ToString();
            txtStopWin.Text = Globals.StopWin.ToString();
            txtStopLoss.Text = Globals.StopLoss.ToString();
        }

        private void chkBetting_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBetting.Checked)
            {
                chkBetting.BackColor = Color.Green;
                chkBetting.Text = "Betting Enabled";
                Globals.BettingEnabled = true;
            }
            else
            {
                chkBetting.BackColor = Color.Red;
                chkBetting.Text = "Betting Disabled";
                Globals.BettingEnabled = false;
            }
        }

        #region Update Check Bet
        private void chkBestPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBestPrice.Checked)
            {
                chkBestPrice.Checked = true;
                chkBestPrice.BackColor = Color.Green;
                if (chkBetNsw.Enabled)
                {
                    chkBetNsw.Checked = false;
                    chkBetNsw.BackColor = Color.Red;
                }
                if (chkBetVic.Enabled)
                {
                    chkBetVic.Checked = false;
                    chkBetVic.BackColor = Color.Red;
                }
                if (chkBetQld.Enabled)
                {
                    chkBetQld.Checked = false;
                    chkBetQld.BackColor = Color.Red;
                }
            }
            else
            {
                chkBestPrice.Checked = false;
                chkBestPrice.BackColor = Color.Red;
            }
        }

        private void chkBetVic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBetVic.Checked)
            {
                chkBestPrice.Checked = false;
                chkBestPrice.BackColor = Color.Red;
                chkBetVic.Checked = true;
                chkBetVic.BackColor = Color.Green;
            }
            else
            {
                chkBetVic.Checked = false;
                chkBetVic.BackColor = Color.Red;
            }
        }

        private void chkBetNsw_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBetNsw.Checked)
            {
                chkBestPrice.Checked = false;
                chkBestPrice.BackColor = Color.Red;
                chkBetNsw.Checked = true;
                chkBetNsw.BackColor = Color.Green;
            }
            else
            {
                chkBetNsw.Checked = false;
                chkBetNsw.BackColor = Color.Red;
            }
        }

        private void chkBetQld_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBetQld.Checked)
            {
                chkBestPrice.Checked = false;
                chkBestPrice.BackColor = Color.Red;
                chkBetQld.Checked = true;
                chkBetQld.BackColor = Color.Green;
            }
            else
            {
                chkBetQld.Checked = false;
                chkBetQld.BackColor = Color.Red;
            }
        }

        #endregion

        private void frmMain_Shown(object sender, EventArgs e)
        {
            CheckTodaysData();
            txtQldBal.Text = string.Format("QLD Bal: {0:C}", Globals.QldBal);
            txtNswBal.Text = string.Format("NSW Bal: {0:C}", Globals.NswBal);
            txtVicBal.Text = string.Format("VIC Bal: {0:C}", Globals.VicBal);
            txtTotalBal.Text = string.Format("Total Bal: {0:C}", Globals.TotalBal);

            cmbRace.SelectedValue = 1;
            cmbRace_SelectedIndexChanged(null, null);
            try
            {
                dgRace.Columns[0].Width = 40;   //Box
                dgRace.Columns[1].Width = 140;   //Dog
                dgRace.Columns[2].Width = 85;   //Status
                dgRace.Columns[3].Width = 70;   //Best Win
                dgRace.Columns[4].Width = 80;   //Best Place
                dgRace.Columns[5].Width = 70;   //Best Pct
                dgRace.Columns[6].Width = 70;   //Vic Win
                dgRace.Columns[7].Width = 80;   //Vic Place
                dgRace.Columns[8].Width = 70;   //Nsw Win
                dgRace.Columns[9].Width = 80;   //Nsw Place
                dgRace.Columns[10].Width = 90;   //Qld Win
                dgRace.Columns[11].Width = 80;  //Qld Place
                dgRace.Columns[12].Width = 70;  //My Price
                dgRace.Columns[13].Width = 70;  //Bet $
                dgRace.Columns[14].Width = 70;  //Tip

                //dgRace.ReadOnly = false;

                foreach (DataGridViewColumn c in dgRace.Columns)
                {
//                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                //dgRace.Columns[14].ReadOnly = false;
            }
            catch { }
        }

        public void CheckTodaysData()
        {
            Globals.TodayLoaded = true;
            Globals.DataLoaded = false;
            string dt = DateTime.Now.ToString("yyyyMMdd");
            global.dbCon = new SqlConnection(Globals.dbConString);
            global.dbCon.Open();
            string cmd = "select count(*) from LivePrices where RaceDate = '" + dt + "'";
            SqlCommand scmd = new SqlCommand(cmd, global.dbCon);
            int count = System.Convert.ToInt32(scmd.ExecuteScalar());
            if (count == 0)
            {
                Globals.TodayLoaded = false;
                if (DateTime.Now.TimeOfDay < System.TimeSpan.Parse("00:10:00"))
                    MessageBox.Show("Not all scratchings may be compelte yet. It is advised to load data after 10.30am", "Warning...");

                DialogResult result = MessageBox.Show("Today's race data has not been loaded. Do you want to load it now?", "Data", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    pgProgress.Visible = true;
//                    pgProgress.ShowText = true;
                    pgProgress.Text = "Loading Data, Please wait.";
                    Thread th = null;
                    utils.prog = pgProgress;
                    utils.ts = tsText1;
//                    pgProgress.Start();
                    th = new Thread(new ThreadStart(utils.LoadData));
                    th.IsBackground = true;
                    th.Start();
                }
            }
            else
            {
                tsText1.Text = "Today's Data Loaded";
                Globals.DataLoaded = true;
            }
            ntg.Start();            
        }

        private void btnLoadRatings_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            OpenFileDialog filePath = new OpenFileDialog();
            if (filePath.ShowDialog() == DialogResult.OK)
            {
                s = filePath.FileName;
            }

            utils.RatingsFile = s;
            pgProgress.Visible = true;
//            pgProgress.ShowText = true;
            pgProgress.Text = "Loading Ratings, Please wait.";
            Thread th = null;
            utils.prog = pgProgress;
//            pgProgress.Start();
            th = new Thread(new ThreadStart(utils.LoadRatings));
            th.Start();
        }

        public void cmbVenue_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dt = DateTime.Now.ToString("yyyyMMdd");
            global.dbCon = new SqlConnection(Globals.dbConString);
            global.dbCon.Open();
            string cmd = "select distinct RaceNum from LivePrices where RaceDate = '" + dt + "' and Venue = '" + cmbVenue.SelectedValue + "' order by RaceNum";
            SqlDataAdapter da = new SqlDataAdapter(cmd, global.dbCon);
            DataTable table = new DataTable();
            da.Fill(table);
            cmbRace.DataSource = table;
            cmbRace.DisplayMember = "RaceNum";
            cmbRace.ValueMember = "RaceNum";
//            cmbRace_SelectedIndexChanged(null, null);
        }

        public void cmbRace_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = "";
            string racenum = "0";
            if (cmbRace.SelectedValue != null)
            {
                if (cmbRace.SelectedValue.ToString() == "System.Data.DataRowView")
                {
                    racenum = "1";
                }
                else
                {
                    racenum = cmbRace.SelectedValue.ToString();
                }

                id = cmbVenue.SelectedValue.ToString() + racenum;
            }

            if ((id != Globals.PreviousIndex) && (Globals.PreviousIndex != null))
            {
                Globals.ViewingRace.Remove(Globals.PreviousIndex);
            }
            

            try
            {
                string venue = cmbVenue.SelectedValue.ToString();
                racenum = cmbRace.SelectedValue.ToString();

                if (racenum == "System.Data.DataRowView") racenum = "1";
                string dt = DateTime.Now.ToString("yyyyMMdd");
                global.dbCon = new SqlConnection(Globals.dbConString);
                global.dbCon.Open();
                string cmd = "select Box, Runner as 'Dog', Status, BestWin as 'BEST WIN', BestPlace as 'BEST PLACE', BestPct as 'BEST %', VicPrice as 'VIC WIN', VicPlace as 'VIC PLACE', NswPrice as 'NSW WIN', NswPlace as 'NSW PLACE', QldPrice as 'QLD WIN', QldPlace as 'QLD PLACE', MyPrice as 'My Price', BetAmt as 'Bet $', Tip, RaceID, MeetingCode, QLDMeetingCode, Resulted from LivePrices where RaceDate = '" + dt + "' and Venue = '" + venue + "' and RaceNum = '" + racenum + "' order by Box";
                SqlDataAdapter da = new SqlDataAdapter(cmd, global.dbCon);
                DataTable table = new DataTable();
                da.Fill(table);
                dgRace.DataSource = table;
                cmd = "select RaceTime from LivePrices where  RaceDate = '" + dt + "' and Venue = '" + venue + "' and RaceNum = '" + racenum + "'";
                SqlCommand upd = new SqlCommand(cmd, global.dbCon);
                SqlDataReader reader = upd.ExecuteReader();
                while (reader.Read())
                {
                    lblRaceTime.Text = "Race Time: " + reader["RaceTime"].ToString();
                }
                global.dbCon.Close();
                dgRace.Columns[0].Width = 40;   //Box
                dgRace.Columns[1].Width = 140;   //Dog
                dgRace.Columns[2].Width = 85;   //Status
                dgRace.Columns[3].Width = 70;   //Best Win
                dgRace.Columns[4].Width = 80;   //Best Place
                dgRace.Columns[5].Width = 70;   //Best Pct
                dgRace.Columns[6].Width = 70;   //Vic Win
                dgRace.Columns[7].Width = 80;   //Vic Place
                dgRace.Columns[8].Width = 70;   //Nsw Win
                dgRace.Columns[9].Width = 80;   //Nsw Place
                dgRace.Columns[10].Width = 90;   //Qld Win
                dgRace.Columns[11].Width = 80;  //Qld Place
                dgRace.Columns[12].Width = 70;  //My Price
                dgRace.Columns[13].Width = 70;  //Bet $
                dgRace.Columns[14].Width = 70;  //Tip
                dgRace.Columns[15].Visible = false;
                dgRace.Columns[15].Width = 0;
                dgRace.Columns[16].Width = 0;
                dgRace.Columns[16].Visible = false;
                dgRace.Columns[17].Width = 0;
                dgRace.Columns[17].Visible = false;
                dgRace.Columns[18].Width = 70;

                //dgRace.ReadOnly = false;

                foreach (DataGridViewColumn c in dgRace.Columns)
                {
                    //c.ReadOnly = true;
                }

                //dgRace.Columns[14].ReadOnly = false;

                string raceID = "";
                global.dbCon = new SqlConnection(Globals.dbConString);
                global.dbCon.Open();
                cmd = "select QldID, VicID from LivePrices where Venue = '" + venue + "' and RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "' and RaceNum = '" + racenum + "'";
                SqlCommand updcmd = new SqlCommand(cmd, global.dbCon);
                reader = updcmd.ExecuteReader();
                while (reader.Read())
                {
                    raceID = reader["QldID"].ToString();
                }

                if (raceID == "") raceID = reader["VicID"].ToString();
                
                global.dbCon.Close();
                Globals.PreviousIndex = venue + racenum;
                if (!Globals.ViewingRace.ContainsValue(raceID))
                {
                    Globals.ViewingRace.Add(id, raceID);
                }
                utils.DisplayRaceDetail(venue, DateTime.Now, racenum, raceID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void btnNextToGo_Click(object sender, EventArgs e)
        {
            if (Globals.ntgVenue != null)
            {
                if (Globals.ntgVenue == "MOUNT GAMBIER") Globals.ntgVenue = "MT GAMBIER";
                cmbVenue.SelectedValue = Globals.ntgVenue;
                cmbRace.SelectedIndex = Convert.ToInt32(Globals.ntgRace) - 1;
                cmbRace.SelectedValue = Globals.ntgRace;
                string raceID = "";
                global.dbCon = new SqlConnection(Globals.dbConString);
                global.dbCon.Open();
                string cmd = "select QldID from LivePrices where Venue = '" + cmbVenue.SelectedValue.ToString() + "' and RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "' and RaceNum = '" + cmbRace.SelectedValue.ToString() + "'";
                SqlCommand updcmd = new SqlCommand(cmd, global.dbCon);
                SqlDataReader reader = updcmd.ExecuteReader();
                while (reader.Read())
                {
                    raceID = reader["QldID"].ToString();
                }
                global.dbCon.Close();
                utils.DisplayRaceDetail(cmbVenue.SelectedValue.ToString(), DateTime.Now, cmbRace.SelectedValue.ToString(), raceID);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string dt = DateTime.Now.ToString("yyyyMMdd");
            global.dbCon.Open();
            string cmd = "select Venue as 'TRACK', RaceDate as 'DATE', RaceNum as 'RACE', RaceTime as 'JUMP', Box as 'BOX', Runner as 'DOG', MyPrice as 'PRICE', BetAmt as 'BET', Tip as 'TIP' from LivePrices where RaceDate = '" + dt + "' order by Venue,RaceNum,Box";
            SqlDataAdapter da = new SqlDataAdapter(cmd, global.dbCon);
            DataTable table = new DataTable();
            da.Fill(table);

            SaveFileDialog file = new SaveFileDialog();
            file.FileName = "RaceDetails.csv";
            file.Filter = "CSV|*.csv";

            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (DataRow row in table.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(file.FileName, sb.ToString());
            }

            global.dbCon.Close();
        }

        private void ResultsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Thread th = null;
            th = new Thread(new ThreadStart(utils.UpdateResults));
            th.IsBackground = true;
            th.Start();
        }

        private void btnDeleteToday_Click(object sender, EventArgs e)
        {
            ResultsTimer.Stop();

            if (global.dbCon.State != ConnectionState.Open) global.dbCon.Open();
            string upd = "delete from liveprices where racedate = '" + DateTime.Now.ToString("yyyyMMdd") + "'";
            SqlCommand cmdupd = new SqlCommand(upd, global.dbCon);
            cmdupd.ExecuteNonQuery();
            global.dbCon.Close();
            MessageBox.Show("Todays prices have been deleted. Please reload to continue racing today.", "Information", MessageBoxButtons.OK);
        }

        private void dgRace_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
                txtTotalPct.Text = CellSum().ToString();
        }

        private double CellSum()
        {
            double sum = 0;
            for (int i = 0; i < dgRace.Rows.Count; ++i)
            {
                double d = 0;
                try
                {
                    Double.TryParse(dgRace.Rows[i].Cells[5].Value.ToString(), out d);
                }
                catch
                {
                    d = 0;
                }
                sum += d;
            }
            return sum;
        }

        private void btnTestAutoBet_Click(object sender, EventArgs e)
        {
            string raceID = "";
            global.dbCon = new SqlConnection(Globals.dbConString);
            global.dbCon.Open();
            string cmd = "select QldID from LivePrices where Venue = '" + cmbVenue.SelectedValue.ToString() + "' and RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "' and RaceNum = '" + cmbRace.SelectedValue.ToString() + "'";
            SqlCommand updcmd = new SqlCommand(cmd, global.dbCon);
            SqlDataReader reader = updcmd.ExecuteReader();
            while (reader.Read())
            {
                raceID = reader["QldID"].ToString();
            }
            global.dbCon.Close();
            utils.AutoBet(!this.btnBetPct.Checked, Convert.ToInt32(cmbRace.SelectedValue.ToString()), raceID, Globals.UseRatingBetAmt, this.chkBetVic.Checked, this.chkBetNsw.Checked, this.chkBetQld.Checked, Convert.ToDouble(this.txtBetPct.Text), Convert.ToDouble(this.txtBetTo.Text), this.chkBestPrice.Checked);
        }

        private void tglPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (btnBetPct.Checked)
            {
                Globals.UseRatingBetAmt = true;
            }
            else
            {
                Globals.UseRatingBetAmt = false;
            }
        }

        private void btnBetNow_Click(object sender, EventArgs e)
        {
            string raceID = "";
            global.dbCon = new SqlConnection(Globals.dbConString);
            global.dbCon.Open();
            string cmd = "select QldID from LivePrices where Venue = '" + cmbVenue.SelectedValue.ToString() + "' and RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "' and RaceNum = '" + cmbRace.SelectedValue.ToString() + "'";
            SqlCommand updcmd = new SqlCommand(cmd, global.dbCon);
            SqlDataReader reader = updcmd.ExecuteReader();
            while (reader.Read())
            {
                raceID = reader["QldID"].ToString();
            }
            if (Globals.CurrentRace.ContainsKey("Autobet_" + raceID))
            {
            }

            global.dbCon.Close();
            utils.AutoBet(!this.btnBetPct.Checked, Convert.ToInt32(cmbRace.SelectedValue.ToString()), raceID, Globals.UseRatingBetAmt, this.chkBetVic.Checked, this.chkBetNsw.Checked, this.chkBetQld.Checked, Convert.ToDouble(this.txtBetPct.Text), Convert.ToDouble(this.txtBetTo.Text), this.chkBestPrice.Checked);
        }

        private void btnUpdScratch_Click(object sender, EventArgs e)
        {
            pgProgress.Visible = true;
//            pgProgress.ShowText = true;
            pgProgress.Text = "Updating Scratchings, Please wait";
//            pgProgress.Start();
            Thread th = null;
            utils.prog = pgProgress;
            utils.ts = tsText1;
//            pgProgress.Start();
            th = new Thread(new ThreadStart(utils.UpdateScratchings));
            th.IsBackground = true;
            th.Start();
        }

        private void btnUpdResults_Click(object sender, EventArgs e)
        {
            utils.UpdateResults();
        }

        private void btnBetPct_Click(object sender, EventArgs e)
        {
            if (btnBetPct.Checked)
            {
                btnBetPct.Text = "Bet %";
                Globals.PctOn = true;
                Console.WriteLine("Use Rating :"+Globals.PctOn.ToString());
            }
            else
            {
                btnBetPct.Text = "Bet Ratings";
                Globals.PctOn = false;
                Console.WriteLine("Use Rating:" + Globals.PctOn.ToString());
            }
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            Results_Report rpt = new Results_Report();
            rpt.Show();

//            frmChooseDate export = new frmChooseDate();
//            export.Show();
            
        }

        private void txtBetOffset_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.offset = Convert.ToInt32(txtBetOffset.Text);
            }
            catch { }
        }

        private void txtBetPct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.BetPct = Convert.ToDouble(txtBetPct.Text);
            }
            catch { }
        }

        private void cmbBetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBetType.Text == "Win Only") Globals.BetType = "WN";
            if (cmbBetType.Text == "Each Way") Globals.BetType = "EW";
            if (cmbBetType.Text == "Place Only") Globals.BetType = "PL";
        }

        private void txtStopWin_TextChanged(object sender, EventArgs e)
        {
            Globals.StopWin = Convert.ToDecimal(txtStopWin.Text);
        }

        private void txtStopLoss_TextChanged(object sender, EventArgs e)
        {
            Globals.StopLoss = Convert.ToDecimal(txtStopLoss.Text);
        }

    }
}


