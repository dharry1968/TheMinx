using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.SQLite;
using Newtonsoft.Json;
using TheMinx.wsTabAuth;
using System.Data.SqlClient;
using System.Net;
using System.IO;

namespace TheMinx
{
    public partial class frmSettings : Form
    {
        Globals global = new Globals();

        private string ve = "false";
        private string ne = "false";
        private string qe = "false";

        public frmSettings()
        {
            InitializeComponent();
            global.dbCon = new SqlConnection(Globals.dbConString);
            global.dbCon.Open();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (global.dbCon.State == ConnectionState.Closed) global.dbCon.Open();
            if (txtVicAcc.Text == "" || txtVicPwd.Text == "") chkVicEnable.Checked = false;
            if (txtNswAcc.Text == "" || txtNswPwd.Text == "") chkNswEnable.Checked = false;
            if (txtQldAcc.Text == "" || txtQldPwd.Text == "") chkQldEnable.Checked = false;
            if (chkVicEnable.Checked) ve = "true"; else ve = "false";
            if (chkNswEnable.Checked) ne = "true"; else ne = "false";
            if (chkQldEnable.Checked) qe = "true"; else qe = "false";
            if (txtStopWin.Text == "") txtStopWin.Text = "0";
            if (txtStopLoss.Text == "") txtStopLoss.Text = "0";
            if (txtBetToWin.Text == "") txtBetToWin.Text = "0";
            if (txtBetPct.Text == "") txtBetPct.Text = "0";

            pgCheck.Visible = true;
            pgCheck.Start();
            if (chkVicEnable.Checked)
            {
                bool vicOk = CheckVic();
                if (!vicOk)
                {
                    pgCheck.Stop();
                    pgCheck.Visible = false;
                    MessageBox.Show("Your VIC account or password is incorrect. Please re-enter or disable VIC", "Error", MessageBoxButtons.OK);
                    return;
                }
            }

            if (chkNswEnable.Checked)
            {
                bool nswOk = CheckNsw();
                if (!nswOk)
                {
                    pgCheck.Stop();
                    pgCheck.Visible = false;
                    MessageBox.Show("Your NSW account or password is incorrect. Please re-enter or disable NSW", "Error", MessageBoxButtons.OK);
                    return;
                }
            }

            if (chkQldEnable.Checked)
            {
                bool qldOk = CheckQld();
                if (!qldOk)
                {
                    pgCheck.Stop();
                    pgCheck.Visible = false;
                    MessageBox.Show("Your QLD account or password is incorrect. Please re-enter or disable QLD", "Error", MessageBoxButtons.OK);
                    return;
                }
            }

            pgCheck.Stop();
            pgCheck.Visible = false;
//            string del = "delete from settings";
//            SQLiteCommand delcmd = new SQLiteCommand(del, global.dbCon);
//            delcmd.ExecuteNonQuery();
            string pct = "";
            if (chkPct.Checked == true)
            {
                pct = "Y";
            }
            else
            {
                pct = "N";
            }

            string useRatingBetAmt = "";

            if (this.chkUseRatingBetAmt.Checked)
            {
                useRatingBetAmt = "Y";
            }
            else
            {
                useRatingBetAmt = "N";
            }

            string upd = "update Settings set VicAcc = '" + txtVicAcc.Text + "', VicPwd = '" + txtVicPwd.Text + "', NswAcc = '" + txtNswAcc.Text + "', NswPwd = '" + txtNswPwd.Text + "', QldAcc = '" + txtQldAcc.Text + "', QldPwd = '" + txtQldPwd.Text + "', VicEnable = '" + ve + "', NswEnable = '" + ne + "', QldEnable = '" + qe + "', StopWin = " + Convert.ToInt32(txtStopWin.Text) + ", StopLoss = " + Convert.ToInt32(txtStopLoss.Text) + ", BetToWin = " + txtBetToWin.Text + ", PctOn = '" + pct + "', BetPct = " + Convert.ToInt32(txtBetPct.Text) + ", UseRatingBetAmt = '" + useRatingBetAmt + "'";
            SqlCommand cmdupd = new SqlCommand(upd, global.dbCon);
            try
            {
                cmdupd.ExecuteNonQuery();
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message.ToString());
            }
            global.dbCon.Close();

            if (txtVicAcc.Text != "")
            {
                Globals.VicAcc = Convert.ToInt32(txtVicAcc.Text);
                Globals.VicPwd = txtVicPwd.Text;
            }

            if (txtNswAcc.Text != "")
            {
                Globals.NswAcc = Convert.ToInt32(txtNswAcc.Text);
                Globals.NswPwd = txtVicPwd.Text;
            }

            if (txtQldAcc.Text != "")
            {
                Globals.QldAcc = Convert.ToInt32(txtQldAcc.Text);
                Globals.QldPwd = txtVicPwd.Text;
            }

            Globals.StopWin = Convert.ToInt32(txtStopWin.Text);
            Globals.StopLoss = Convert.ToInt32(txtStopLoss.Text);
            Globals.BetToWin = Convert.ToDouble(txtBetToWin.Text);
            Globals.PctOn = chkPct.Checked;
            Globals.BetPct = Convert.ToInt32(txtBetPct.Text);
            Globals.UseRatingBetAmt = this.chkUseRatingBetAmt.Checked;

            this.Close();

        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            string cmd = "select * from Settings";
            SqlCommand upd = new SqlCommand(cmd, global.dbCon);
            SqlDataReader reader = upd.ExecuteReader();
            while (reader.Read())
            {
                txtVicAcc.Text = reader["VicAcc"].ToString();
                txtVicPwd.Text = reader["VicPwd"].ToString();
                txtNswAcc.Text = reader["NswAcc"].ToString();
                txtNswPwd.Text = reader["NswPwd"].ToString();
                txtQldAcc.Text = reader["QldAcc"].ToString();
                txtQldPwd.Text = reader["QldPwd"].ToString();
                if (reader["VicEnable"].ToString() == "true") chkVicEnable.Checked = true;
                if (reader["NswEnable"].ToString() == "true") chkNswEnable.Checked = true;
                if (reader["QldEnable"].ToString() == "true") chkQldEnable.Checked = true;
                txtStopWin.Text = reader["StopWin"].ToString();
                txtStopLoss.Text = reader["StopLoss"].ToString();
                txtBetToWin.Text = reader["BetToWin"].ToString();
                if (reader["PctOn"].ToString() == "Y")
                {
                    chkPct.Checked = true;
                }
                else
                {
                    chkPct.Checked = false;
                }
                txtBetPct.Text = reader["BetPct"].ToString();

                if (reader["UseRatingBetAmt"].ToString() == "Y")
                {
                    this.chkUseRatingBetAmt.Checked = true;
                }
                else
                {
                    this.chkUseRatingBetAmt.Checked = false;
                }
            }
            global.dbCon.Close();
        }

        private bool CheckVic()
        {
            bool ok = false;
            try
            {
                wsTabAuth1.apiMeta meta = new wsTabAuth1.apiMeta();
                meta.deviceId = Globals.DeviceID;
                meta.requestChannel = Globals.VicChan;
                meta.jurisdictionId = Globals.VicJury;
                meta.deviceIdSpecified = true;
                wsTabAuth1.thirdPartyCustomerAuthenticateRequest vrequest = new wsTabAuth1.thirdPartyCustomerAuthenticateRequest();
                vrequest.accountId = Convert.ToInt32(txtVicAcc.Text);
                vrequest.accountPassword = txtVicPwd.Text;
                wsTabAuth1.thirdPartyAuthenticate auth = new wsTabAuth1.thirdPartyAuthenticate();
                wsTabAuth1.thirdPartyCustomerAuthenticateResponse resp = auth.authenticateAccount(meta, vrequest);
                Globals.VicAuth = resp.usernamePasswordToken;
                ok = true;
            }
            catch (Exception)
            {
                ok = false;
            }

            return ok;
        }

        private bool CheckNsw()
        {
            bool ok = false;
            try
            {
                wsTabAuth.apiMeta nmeta = new wsTabAuth.apiMeta();
                nmeta.deviceId = Globals.DeviceID;
                nmeta.requestChannel = Globals.NswChan;
                nmeta.jurisdictionId = Globals.NswJury;
                nmeta.deviceIdSpecified = true;
                thirdPartyCustomerAuthenticateRequest nrequest = new thirdPartyCustomerAuthenticateRequest();
                nrequest.accountId = Convert.ToInt32(txtNswAcc.Text);
                nrequest.accountPassword = txtNswPwd.Text;
                ThirdPartyCustomerAuthenticateServiceClient nauth = new ThirdPartyCustomerAuthenticateServiceClient();
                thirdPartyCustomerAuthenticateResponse nresp = nauth.authenticateAccount(nmeta, nrequest);
                Globals.NswAuth = nresp.usernamePasswordToken;
                ok = true;
            }
            catch (Exception)
            {
                ok = false;
            }

            return ok;
        }

        private bool CheckQld()
        {
            bool ok = false;
            try
            {
                LogonRequest request = new LogonRequest();
                request.Username = txtQldAcc.Text;
                request.Password = txtQldPwd.Text;
                request.DeviceName = "TheMinx";
                string requestJSON = JsonConvert.SerializeObject(request);
                string responseJSON = TattsUtils.CallAPI2("account/login/", requestJSON, true, "POST");
                LogonResponse response = null;
                if (!string.IsNullOrEmpty(responseJSON))
                {
                    response = JsonConvert.DeserializeObject<LogonResponse>(responseJSON, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        Formatting = Newtonsoft.Json.Formatting.None,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        FloatParseHandling = FloatParseHandling.Decimal,

                    });
                }

                if (response != null)
                {
                    if (response.Success)
                    {
                        Globals.QldAuth = response.CustomerSession.SessionId;
                        ok = true;
                    }
                    else
                    {
                        Globals.QldAuth = string.Empty;
                        ok = false;
                    }
                }
                else
                {
                    Globals.QldAuth = string.Empty;
                    ok = false;
                }
            }
            catch (Exception)
            {
                ok = false;
            }
            return ok;
        }

        private void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            Utils utils = new Utils();
            utils.CreateDatabase();
        }

    }
}
