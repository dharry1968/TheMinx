using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMinx.Tatts.Data;
using TheMinx.Tatts.Data.Requests;
using TheMinx.Tatts.Data.Responses;
using TheMinx.wsTabAuth;
using TheMinx.wsTabRacing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing;

namespace TheMinx
{
    public class PriceUpdates
    {
        public string Venue { get; set; }
        public string RaceNum { get; set; }
        public DateTime MeetingDate { get; set; }
        public DataGridView dgRace { get; set; }
        public string RaceID { get; set; }
        public System.Timers.Timer pricetimer;

        public void UpdatePrices()
        {
            bool ok = true;
//            dgRace.CellFormatting += dgRace_CellFormatting;
            if (Globals.ViewingRace.ContainsValue(RaceID))
            {
                ok = true;
            }
            else
            {
                ok = false;
            }

            while (ok)
            {

                try
                {
                    dgRace.BeginInvoke((MethodInvoker)(delegate
                    {
                        dgRace.SuspendLayout();
                    }));
                }
                catch { }

                RacingServiceClient raceServ = new RacingServiceClient();
                wsTabRacing.apiMeta vMeta = new wsTabRacing.apiMeta();
                vMeta.usernamePasswordToken = Globals.DefaultAuth;
                vMeta.requestChannel = Globals.VicChan;
                vMeta.jurisdictionId = Globals.VicJury;
                vMeta.deviceId = Globals.DeviceID;
                vMeta.deviceIdSpecified = true;

                raceDetailsRequestV2 detReq = new raceDetailsRequestV2();
                raceDetailsResponseV2 detResp = new raceDetailsResponseV2();
                detReq.raceNumber = Convert.ToInt32(RaceNum);
                detReq.racingCode = "G";
                if (Venue == "SANDOWN") { Venue = "SANDOWN PARK"; }
                if (Venue == "MT GAMBIER") { Venue = "MOUNT GAMBIER"; }
                if (Venue.Contains("ELWICK")) { Venue = "HOBART"; }

                detReq.meetingName = Venue;
                detReq.meetingDate = MeetingDate.ToString("dd-MMM-yyyy");
                try
                {
                    detResp = raceServ.getRaceDetailsV2(vMeta, detReq);

                    raceDetail vDet = detResp.meeting.raceDetail;
                    raceSelectionDetail[] vRunners = vDet.raceSelections;
                    foreach (raceSelectionDetail runner in vRunners)
                    {
                        foreach (DataGridViewRow row in dgRace.Rows)
                        {
                            if ((row.Cells[1].Value.ToString().Equals(runner.runner.Replace("'", "")) && runner.barrierNumber != 0))
                            {
                                dgRace.BeginInvoke((MethodInvoker)(delegate
                                {
                                    row.Cells[6].Value = runner.toteWinPrice;
                                    row.Cells[7].Value = runner.totePlacePrice;
                                    UpdateBestPrice(row);
                                }));
                                break;
                            }
                        }
                    }
                }
                catch { }

                RacingServiceClient nraceServ = new RacingServiceClient();
                wsTabRacing.apiMeta nMeta = new wsTabRacing.apiMeta();
                nMeta.usernamePasswordToken = Globals.DefaultAuth;
                nMeta.requestChannel = Globals.NswChan;
                nMeta.jurisdictionId = Globals.NswJury;
                nMeta.deviceId = Globals.DeviceID;
                nMeta.deviceIdSpecified = true;

                raceDetailsRequestV2 ndetReq = new raceDetailsRequestV2();
                raceDetailsResponseV2 ndetResp = new raceDetailsResponseV2();
                ndetReq.raceNumber = Convert.ToInt32(RaceNum);
                ndetReq.racingCode = "G";
                if (Venue == "SANDOWN") { Venue = "SANDOWN PARK"; }
                if (Venue == "MT GAMBIER") { Venue = "MOUNT GAMBIER"; }
                if (Venue.Contains("ELWICK")) { Venue = "HOBART"; }

                ndetReq.meetingName = Venue;
                ndetReq.meetingDate = MeetingDate.ToString("dd-MMM-yyyy");
                try
                {
                    ndetResp = nraceServ.getRaceDetailsV2(nMeta, ndetReq);
                    raceDetail nDet = ndetResp.meeting.raceDetail;
                    raceSelectionDetail[] nRunners = nDet.raceSelections;
                    foreach (raceSelectionDetail runner in nRunners)
                    {
                        foreach (DataGridViewRow row in dgRace.Rows)
                        {
                            if ((row.Cells[1].Value.ToString().Equals(runner.runner.Replace("'", "")) && runner.barrierNumber != 0))
                            {
                                dgRace.BeginInvoke((MethodInvoker)(delegate
                                {
                                    row.Cells[8].Value = runner.toteWinPrice;
                                    row.Cells[9].Value = runner.totePlacePrice;
                                    UpdateBestPrice(row);
                                }));
                                break;
                            }
                        }
                    }
                }
                catch { }

                try
                {
                    string responseJSON = TattsUtils.CallAPI(RaceID, null, true, "GET");
                    DataResponse response = null;
                    response = JsonConvert.DeserializeObject<DataResponse>(responseJSON);
//                    if (!(response.Data[0].Meetings[0].Races[0].Status == "CLOSED") && !(response.Data[0].Meetings[0].Races[0].Status == "ABANDONED") && !(response.Data[0].Meetings[0].Races[0].Status == "PAYING"))
//                    {
                        foreach (TattsRunner runner in response.Data[0].Meetings[0].Races[0].Runners)
                        {
                            foreach (DataGridViewRow row in dgRace.Rows)
                            {
                                if ((row.Cells[1].Value.ToString().Equals(runner.RunnerName.Replace("'", ""))) && (runner.Scratched != true))
                                {
//                                    int rnum = Convert.ToInt32(runner.RunnerNumber - 1);
                                    double qWin = (double)runner.WinOdds;
                                    double qPlace = (double)runner.PlaceOdds;
                                    if (qWin > 1000) qWin = 0;
                                    if (qPlace > 1000) qPlace = 0;
                                    dgRace.BeginInvoke((MethodInvoker)(delegate
                                    {
                                        row.Cells[10].Value = qWin;
                                        row.Cells[11].Value = qPlace;
                                        UpdateBestPrice(row);
                                    }));
                                    break;
                                }
                            }
                        }
//                    }
//                    else
//                    {
//                        ok = false;
//                    }
                }
                catch { }


                try
                {
                    dgRace.BeginInvoke((MethodInvoker)(delegate
                    {
                        dgRace.Refresh();
                        dgRace.ResumeLayout(true);
                    }));
                }
                catch { }
            }

        }

        private void UpdateBestPrice(DataGridViewRow row)
        {
            double rowBestWin = 0.0;
            double rowBestPlace = 0.0;
            string resulted = "";
            try
            {
                resulted = row.Cells[18].Value.ToString();
            }
            catch { }
//            if (row.Cells[18].Value.ToString() == "0") row.Cells[18].Value = "";

            if (row.Cells[6].Value != null && row.Cells[6].Value != DBNull.Value)
            {
                if ((row.Cells[12].Value != DBNull.Value) && System.Convert.ToDouble(row.Cells[6].Value) > Convert.ToDouble(row.Cells[12].Value))
                {
                    rowBestWin = System.Convert.ToDouble(row.Cells[6].Value);
                    row.Cells[6].Style.BackColor = Color.LightGreen;
                    if (resulted != "")
                    {
                        row.Cells[6].Style.BackColor = Color.White;
//                        row.Cells[8].Style.BackColor = Color.White;
//                        row.Cells[10].Style.BackColor = Color.White;
                    }
                    else
                    {
                        row.Cells[8].Style.BackColor = Color.LightPink;
                        row.Cells[10].Style.BackColor = Color.LightPink;
                    }
                }
                else {row.Cells[6].Style.BackColor = Color.White; }

                if (row.Cells[12].Value != DBNull.Value && Convert.ToDouble(row.Cells[12].Value) == 0) row.Cells[6].Style.BackColor = Color.White;
            }


            if ((row.Cells[12].Value != DBNull.Value) && row.Cells[8].Value != null && row.Cells[8].Value != DBNull.Value)
            {
                if (System.Convert.ToDouble(row.Cells[8].Value) > Convert.ToDouble(row.Cells[12].Value))
                {
                    rowBestWin = System.Convert.ToDouble(row.Cells[8].Value);
                    row.Cells[8].Style.BackColor = Color.LightGreen;
//                    row.Cells[6].Style.BackColor = Color.White;
//                    row.Cells[10].Style.BackColor = Color.White;
                }
                else { row.Cells[8].Style.BackColor = Color.White; }

                if (row.Cells[12].Value != DBNull.Value && Convert.ToDouble(row.Cells[12].Value) == 0) row.Cells[8].Style.BackColor = Color.White;
            }
            

            if (row.Cells[10].Value != null && row.Cells[10].Value != DBNull.Value)
            {
                if ((row.Cells[12].Value != DBNull.Value) && System.Convert.ToDouble(row.Cells[10].Value) > Convert.ToDouble(row.Cells[12].Value))
                {
                    rowBestWin = System.Convert.ToDouble(row.Cells[10].Value);
                    row.Cells[10].Style.BackColor = Color.LightGreen;
//                    row.Cells[8].Style.BackColor = Color.White;
//                    row.Cells[6].Style.BackColor = Color.White;
                }
                else { row.Cells[10].Style.BackColor = Color.White; }

                if (row.Cells[12].Value != DBNull.Value && Convert.ToDouble(row.Cells[12].Value) == 0) row.Cells[10].Style.BackColor = Color.White;
            }
            

            if (row.Cells[7].Value != null && row.Cells[7].Value != DBNull.Value)
            {
                if (System.Convert.ToDouble(row.Cells[7].Value) > rowBestPlace)
                {
                    rowBestPlace = System.Convert.ToDouble(row.Cells[7].Value);
                }
            }
            

            if (row.Cells[9].Value != null && row.Cells[9].Value != DBNull.Value)
            {
                if (System.Convert.ToDouble(row.Cells[9].Value) > rowBestPlace)
                {
                    rowBestPlace = System.Convert.ToDouble(row.Cells[9].Value);
                }
            }
            

            if (row.Cells[11].Value != null && row.Cells[11].Value != DBNull.Value)
            {
                if (System.Convert.ToDouble(row.Cells[11].Value) > rowBestPlace)
                {
                    rowBestPlace = System.Convert.ToDouble(row.Cells[11].Value);
                }
            }
            

            row.Cells[3].Value = rowBestWin.ToString();
            row.Cells[4].Value = rowBestPlace.ToString();

            try
            {
                row.Cells[5].Value = Math.Round(100 / Convert.ToDouble(row.Cells[3].Value), 2);
            }
            catch
            {
                row.Cells[5].Value = "0";
            }

            if (row.Cells[5].Value.ToString() == "Infinity") row.Cells[5].Value = 0;
        }

        private void dgRace_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Font f = dgRace.DefaultCellStyle.Font;
            foreach (DataGridViewRow row in dgRace.Rows)
            {
                try
                {
                    if (Convert.ToInt32(row.Cells[18].Value) == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                    }
                }
                catch { }
                string stat = row.Cells[2].Value.ToString();
                if (stat == "SCRATCHED" || stat == "LATESCRATCHING")
                {
                    row.DefaultCellStyle.Font = new Font(f, FontStyle.Strikeout);
                    row.Cells[0].Style.ForeColor = Color.Red;
                    row.Cells[1].Style.ForeColor = Color.Red;
                    row.Cells[2].Style.ForeColor = Color.Red;
                    row.Cells[3].Style.ForeColor = Color.Red;
                    row.Cells[4].Style.ForeColor = Color.Red;
                    row.Cells[5].Style.ForeColor = Color.Red;
                    row.Cells[6].Style.ForeColor = Color.Red;
                    row.Cells[7].Style.ForeColor = Color.Red;
                    row.Cells[8].Style.ForeColor = Color.Red;
                    row.Cells[9].Style.ForeColor = Color.Red;
                    row.Cells[10].Style.ForeColor = Color.Red;
                    row.Cells[11].Style.ForeColor = Color.Red;
                    row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else
                {
                    row.DefaultCellStyle.Font = new Font(f, FontStyle.Regular);
                    row.Cells[0].Style.ForeColor = Color.Black;
                    row.Cells[1].Style.ForeColor = Color.Black;
                    row.Cells[2].Style.ForeColor = Color.Black;
                    row.Cells[3].Style.ForeColor = Color.Black;
                    row.Cells[4].Style.ForeColor = Color.Black;
                    row.Cells[5].Style.ForeColor = Color.Black;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    row.Cells[7].Style.ForeColor = Color.Black;
                    row.Cells[8].Style.ForeColor = Color.Black;
                    row.Cells[9].Style.ForeColor = Color.Black;
                    row.Cells[0].Style.ForeColor = Color.Black;
                    row.Cells[11].Style.ForeColor = Color.Black;
                    row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    row.Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                }
            }
            dgRace.CellFormatting -= dgRace_CellFormatting;
        }

    }
}
