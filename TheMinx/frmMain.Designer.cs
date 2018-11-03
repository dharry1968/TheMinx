namespace TheMinx
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsText1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtVicBal = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtNswBal = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtQldBal = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBetVic = new System.Windows.Forms.CheckBox();
            this.chkBetNsw = new System.Windows.Forms.CheckBox();
            this.chkBetQld = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkBestPrice = new System.Windows.Forms.CheckBox();
            this.lblNextRace = new System.Windows.Forms.Label();
            this.cmbVenue = new System.Windows.Forms.ComboBox();
            this.todaysVenueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.todaysVenue = new TheMinx.TodaysVenue();
            this.dgRace = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.todaysVenueTableAdapter = new TheMinx.TodaysVenueTableAdapters.TodaysVenueTableAdapter();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRace = new System.Windows.Forms.ComboBox();
            this.btnNextToGo = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnLoadRatings = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnLoadData = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtBetOffset = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtStatus = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.lblRaceTime = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnExport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtStopWin = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtStopLoss = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.chkBetting = new System.Windows.Forms.CheckBox();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.txtBetTo = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtBetPct = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnBetNow = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDeleteToday = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtTotalPct = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnTestAutoBet = new System.Windows.Forms.Button();
            this.btnUpdScratch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnUpdResults = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnBetPct = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.btnResults = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtNext5 = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cmbBetType = new System.Windows.Forms.ComboBox();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.pgProgress = new Telerik.WinControls.UI.RadProgressBar();
            this.txtTotalBal = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.todaysVenueBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.todaysVenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsText1,
            this.txtVicBal,
            this.txtNswBal,
            this.txtQldBal,
            this.txtTotalBal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 649);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1302, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsText1
            // 
            this.tsText1.Name = "tsText1";
            this.tsText1.Size = new System.Drawing.Size(0, 17);
            // 
            // txtVicBal
            // 
            this.txtVicBal.Name = "txtVicBal";
            this.txtVicBal.Size = new System.Drawing.Size(47, 17);
            this.txtVicBal.Text = "VIC Bal:";
            // 
            // txtNswBal
            // 
            this.txtNswBal.Name = "txtNswBal";
            this.txtNswBal.Size = new System.Drawing.Size(55, 17);
            this.txtNswBal.Text = "NSW Bal:";
            // 
            // txtQldBal
            // 
            this.txtQldBal.Name = "txtQldBal";
            this.txtQldBal.Size = new System.Drawing.Size(52, 17);
            this.txtQldBal.Text = "QLD Bal:";
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(1211, 619);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(79, 21);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(963, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Bet On";
            // 
            // chkBetVic
            // 
            this.chkBetVic.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBetVic.AutoSize = true;
            this.chkBetVic.BackColor = System.Drawing.Color.Red;
            this.chkBetVic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBetVic.ForeColor = System.Drawing.Color.White;
            this.chkBetVic.Location = new System.Drawing.Point(966, 32);
            this.chkBetVic.Name = "chkBetVic";
            this.chkBetVic.Size = new System.Drawing.Size(41, 23);
            this.chkBetVic.TabIndex = 7;
            this.chkBetVic.Text = "VIC ";
            this.chkBetVic.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkBetVic.UseVisualStyleBackColor = false;
            // 
            // chkBetNsw
            // 
            this.chkBetNsw.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBetNsw.AutoSize = true;
            this.chkBetNsw.BackColor = System.Drawing.Color.Red;
            this.chkBetNsw.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBetNsw.ForeColor = System.Drawing.Color.White;
            this.chkBetNsw.Location = new System.Drawing.Point(1007, 32);
            this.chkBetNsw.Name = "chkBetNsw";
            this.chkBetNsw.Size = new System.Drawing.Size(46, 23);
            this.chkBetNsw.TabIndex = 8;
            this.chkBetNsw.Text = "NSW";
            this.chkBetNsw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkBetNsw.UseVisualStyleBackColor = false;
            // 
            // chkBetQld
            // 
            this.chkBetQld.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBetQld.AutoSize = true;
            this.chkBetQld.BackColor = System.Drawing.Color.Red;
            this.chkBetQld.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBetQld.ForeColor = System.Drawing.Color.White;
            this.chkBetQld.Location = new System.Drawing.Point(1057, 32);
            this.chkBetQld.Name = "chkBetQld";
            this.chkBetQld.Size = new System.Drawing.Size(42, 23);
            this.chkBetQld.TabIndex = 9;
            this.chkBetQld.Text = "QLD";
            this.chkBetQld.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBetQld.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1099, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Or";
            // 
            // chkBestPrice
            // 
            this.chkBestPrice.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBestPrice.AutoSize = true;
            this.chkBestPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBestPrice.ForeColor = System.Drawing.Color.White;
            this.chkBestPrice.Location = new System.Drawing.Point(1102, 32);
            this.chkBestPrice.Name = "chkBestPrice";
            this.chkBestPrice.Size = new System.Drawing.Size(75, 23);
            this.chkBestPrice.TabIndex = 11;
            this.chkBestPrice.Text = "Best Price";
            this.chkBestPrice.UseVisualStyleBackColor = true;
            // 
            // lblNextRace
            // 
            this.lblNextRace.AutoSize = true;
            this.lblNextRace.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextRace.Location = new System.Drawing.Point(12, 619);
            this.lblNextRace.Name = "lblNextRace";
            this.lblNextRace.Size = new System.Drawing.Size(120, 26);
            this.lblNextRace.TabIndex = 12;
            this.lblNextRace.Text = "Next Race:";
            // 
            // cmbVenue
            // 
            this.cmbVenue.DataSource = this.todaysVenueBindingSource;
            this.cmbVenue.DisplayMember = "Venue";
            this.cmbVenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVenue.FormattingEnabled = true;
            this.cmbVenue.Location = new System.Drawing.Point(66, 82);
            this.cmbVenue.Name = "cmbVenue";
            this.cmbVenue.Size = new System.Drawing.Size(167, 28);
            this.cmbVenue.TabIndex = 14;
            this.cmbVenue.ValueMember = "Venue";
            this.cmbVenue.SelectedIndexChanged += new System.EventHandler(this.cmbVenue_SelectedIndexChanged);
            // 
            // todaysVenueBindingSource
            // 
            this.todaysVenueBindingSource.DataMember = "TodaysVenue";
            this.todaysVenueBindingSource.DataSource = this.todaysVenue;
            // 
            // todaysVenue
            // 
            this.todaysVenue.DataSetName = "TodaysVenue";
            this.todaysVenue.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dgRace
            // 
            this.dgRace.AllowUserToAddRows = false;
            this.dgRace.AllowUserToDeleteRows = false;
            this.dgRace.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgRace.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgRace.ColumnHeadersHeight = 25;
            this.dgRace.Location = new System.Drawing.Point(12, 116);
            this.dgRace.Name = "dgRace";
            this.dgRace.ReadOnly = true;
            this.dgRace.Size = new System.Drawing.Size(1278, 264);
            this.dgRace.StateNormal.HeaderRow.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dgRace.StateNormal.HeaderRow.Content.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgRace.TabIndex = 15;
            // 
            // todaysVenueTableAdapter
            // 
            this.todaysVenueTableAdapter.ClearBeforeFill = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Track:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(250, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Race:";
            // 
            // cmbRace
            // 
            this.cmbRace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRace.FormattingEnabled = true;
            this.cmbRace.Location = new System.Drawing.Point(307, 82);
            this.cmbRace.Name = "cmbRace";
            this.cmbRace.Size = new System.Drawing.Size(49, 28);
            this.cmbRace.TabIndex = 18;
            this.cmbRace.SelectedIndexChanged += new System.EventHandler(this.cmbRace_SelectedIndexChanged);
            // 
            // btnNextToGo
            // 
            this.btnNextToGo.Location = new System.Drawing.Point(362, 82);
            this.btnNextToGo.Name = "btnNextToGo";
            this.btnNextToGo.Size = new System.Drawing.Size(94, 28);
            this.btnNextToGo.TabIndex = 19;
            this.btnNextToGo.Values.Text = "Next To Go";
            this.btnNextToGo.Click += new System.EventHandler(this.btnNextToGo_Click);
            // 
            // btnLoadRatings
            // 
            this.btnLoadRatings.Location = new System.Drawing.Point(128, 12);
            this.btnLoadRatings.Name = "btnLoadRatings";
            this.btnLoadRatings.Size = new System.Drawing.Size(92, 43);
            this.btnLoadRatings.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLoadRatings.TabIndex = 20;
            this.btnLoadRatings.Values.Text = "Load Ratings";
            this.btnLoadRatings.Click += new System.EventHandler(this.btnLoadRatings_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(13, 12);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(109, 43);
            this.btnLoadData.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoadData.TabIndex = 21;
            this.btnLoadData.Values.Text = "Load Todays Data";
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // txtBetOffset
            // 
            this.txtBetOffset.Location = new System.Drawing.Point(1207, 88);
            this.txtBetOffset.Name = "txtBetOffset";
            this.txtBetOffset.Size = new System.Drawing.Size(30, 20);
            this.txtBetOffset.TabIndex = 22;
            this.txtBetOffset.Text = "0";
            this.txtBetOffset.TextChanged += new System.EventHandler(this.txtBetOffset_TextChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(1105, 88);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(96, 20);
            this.kryptonLabel1.TabIndex = 23;
            this.kryptonLabel1.Values.Text = "Race Bet Offset:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(1243, 88);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(28, 20);
            this.kryptonLabel2.TabIndex = 24;
            this.kryptonLabel2.Values.Text = "sec";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(12, 431);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(941, 185);
            this.txtStatus.TabIndex = 25;
            this.txtStatus.Text = "";
            // 
            // lblRaceTime
            // 
            this.lblRaceTime.Location = new System.Drawing.Point(462, 85);
            this.lblRaceTime.Name = "lblRaceTime";
            this.lblRaceTime.Size = new System.Drawing.Size(92, 23);
            this.lblRaceTime.StateNormal.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaceTime.TabIndex = 26;
            this.lblRaceTime.Values.Text = "Start Time:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(226, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(97, 43);
            this.btnExport.StateCommon.Back.Color1 = System.Drawing.Color.Red;
            this.btnExport.TabIndex = 27;
            this.btnExport.Values.Text = "Export Races";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtStopWin
            // 
            this.txtStopWin.Location = new System.Drawing.Point(1035, 61);
            this.txtStopWin.Name = "txtStopWin";
            this.txtStopWin.Size = new System.Drawing.Size(40, 20);
            this.txtStopWin.TabIndex = 28;
            this.txtStopWin.Text = "0";
            this.txtStopWin.TextChanged += new System.EventHandler(this.txtStopWin_TextChanged);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(959, 61);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel3.TabIndex = 29;
            this.kryptonLabel3.Values.Text = "Stop Win $:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(957, 88);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel4.TabIndex = 30;
            this.kryptonLabel4.Values.Text = "Stop Loss $:";
            // 
            // txtStopLoss
            // 
            this.txtStopLoss.Location = new System.Drawing.Point(1035, 88);
            this.txtStopLoss.Name = "txtStopLoss";
            this.txtStopLoss.Size = new System.Drawing.Size(40, 20);
            this.txtStopLoss.TabIndex = 31;
            this.txtStopLoss.Text = "0";
            this.txtStopLoss.TextChanged += new System.EventHandler(this.txtStopLoss_TextChanged);
            // 
            // chkBetting
            // 
            this.chkBetting.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBetting.AutoSize = true;
            this.chkBetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBetting.ForeColor = System.Drawing.Color.White;
            this.chkBetting.Location = new System.Drawing.Point(1102, 61);
            this.chkBetting.Name = "chkBetting";
            this.chkBetting.Size = new System.Drawing.Size(110, 23);
            this.chkBetting.TabIndex = 32;
            this.chkBetting.Text = "Betting Disabled";
            this.chkBetting.UseVisualStyleBackColor = true;
            this.chkBetting.CheckedChanged += new System.EventHandler(this.chkBetting_CheckedChanged);
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2010Black;
            // 
            // txtBetTo
            // 
            this.txtBetTo.Location = new System.Drawing.Point(916, 61);
            this.txtBetTo.Name = "txtBetTo";
            this.txtBetTo.Size = new System.Drawing.Size(37, 20);
            this.txtBetTo.TabIndex = 36;
            this.txtBetTo.Text = "100";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(830, 61);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.kryptonLabel5.Size = new System.Drawing.Size(83, 20);
            this.kryptonLabel5.TabIndex = 37;
            this.kryptonLabel5.Values.Text = "Bet To Win: $";
            // 
            // txtBetPct
            // 
            this.txtBetPct.Location = new System.Drawing.Point(916, 85);
            this.txtBetPct.Name = "txtBetPct";
            this.txtBetPct.Size = new System.Drawing.Size(37, 20);
            this.txtBetPct.TabIndex = 38;
            this.txtBetPct.Text = "90";
            this.txtBetPct.TextChanged += new System.EventHandler(this.txtBetPct_TextChanged);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(852, 85);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel6.TabIndex = 39;
            this.kryptonLabel6.Values.Text = "Bet at: %";
            // 
            // btnBetNow
            // 
            this.btnBetNow.Location = new System.Drawing.Point(710, 67);
            this.btnBetNow.Name = "btnBetNow";
            this.btnBetNow.Size = new System.Drawing.Size(114, 43);
            this.btnBetNow.StateNormal.Back.Color1 = System.Drawing.Color.Brown;
            this.btnBetNow.StateNormal.Back.Color2 = System.Drawing.Color.Yellow;
            this.btnBetNow.TabIndex = 41;
            this.btnBetNow.Values.Text = "Bet Now";
            this.btnBetNow.Click += new System.EventHandler(this.btnBetNow_Click);
            // 
            // btnDeleteToday
            // 
            this.btnDeleteToday.Location = new System.Drawing.Point(329, 11);
            this.btnDeleteToday.Name = "btnDeleteToday";
            this.btnDeleteToday.Size = new System.Drawing.Size(126, 44);
            this.btnDeleteToday.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnDeleteToday.TabIndex = 42;
            this.btnDeleteToday.Values.Text = "Delete Todays Data";
            this.btnDeleteToday.Click += new System.EventHandler(this.btnDeleteToday_Click);
            // 
            // txtTotalPct
            // 
            this.txtTotalPct.Location = new System.Drawing.Point(471, 386);
            this.txtTotalPct.Name = "txtTotalPct";
            this.txtTotalPct.Size = new System.Drawing.Size(55, 20);
            this.txtTotalPct.TabIndex = 43;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(423, 386);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(51, 20);
            this.kryptonLabel7.TabIndex = 44;
            this.kryptonLabel7.Values.Text = "Total %";
            // 
            // btnTestAutoBet
            // 
            this.btnTestAutoBet.Location = new System.Drawing.Point(542, 61);
            this.btnTestAutoBet.Name = "btnTestAutoBet";
            this.btnTestAutoBet.Size = new System.Drawing.Size(104, 26);
            this.btnTestAutoBet.TabIndex = 45;
            this.btnTestAutoBet.Text = "Test Auto Bet";
            this.btnTestAutoBet.UseVisualStyleBackColor = true;
            this.btnTestAutoBet.Visible = false;
            this.btnTestAutoBet.Click += new System.EventHandler(this.btnTestAutoBet_Click);
            // 
            // btnUpdScratch
            // 
            this.btnUpdScratch.Location = new System.Drawing.Point(462, 11);
            this.btnUpdScratch.Name = "btnUpdScratch";
            this.btnUpdScratch.Size = new System.Drawing.Size(119, 44);
            this.btnUpdScratch.StateCommon.Back.Color1 = System.Drawing.Color.Yellow;
            this.btnUpdScratch.TabIndex = 46;
            this.btnUpdScratch.Values.Text = "Update Scratchings";
            this.btnUpdScratch.Click += new System.EventHandler(this.btnUpdScratch_Click);
            // 
            // btnUpdResults
            // 
            this.btnUpdResults.Location = new System.Drawing.Point(588, 11);
            this.btnUpdResults.Name = "btnUpdResults";
            this.btnUpdResults.Size = new System.Drawing.Size(118, 44);
            this.btnUpdResults.StateCommon.Back.Color1 = System.Drawing.Color.Violet;
            this.btnUpdResults.TabIndex = 47;
            this.btnUpdResults.Values.Text = "Update All Results";
            this.btnUpdResults.Click += new System.EventHandler(this.btnUpdResults_Click);
            // 
            // btnBetPct
            // 
            this.btnBetPct.Checked = true;
            this.btnBetPct.Location = new System.Drawing.Point(846, 11);
            this.btnBetPct.Name = "btnBetPct";
            this.btnBetPct.Size = new System.Drawing.Size(107, 44);
            this.btnBetPct.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBetPct.TabIndex = 48;
            this.btnBetPct.Values.Text = "Bet %";
            this.btnBetPct.Click += new System.EventHandler(this.btnBetPct_Click);
            // 
            // btnResults
            // 
            this.btnResults.Location = new System.Drawing.Point(710, 11);
            this.btnResults.Name = "btnResults";
            this.btnResults.Size = new System.Drawing.Size(114, 44);
            this.btnResults.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnResults.TabIndex = 49;
            this.btnResults.Values.Text = "Results";
            this.btnResults.Click += new System.EventHandler(this.btnResults_Click);
            // 
            // txtNext5
            // 
            this.txtNext5.Location = new System.Drawing.Point(966, 431);
            this.txtNext5.Name = "txtNext5";
            this.txtNext5.Size = new System.Drawing.Size(324, 185);
            this.txtNext5.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNext5.TabIndex = 50;
            this.txtNext5.Text = "";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(966, 399);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(123, 26);
            this.kryptonLabel8.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel8.TabIndex = 51;
            this.kryptonLabel8.Values.Text = "Next Races:";
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(13, 399);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(119, 26);
            this.kryptonLabel9.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel9.TabIndex = 52;
            this.kryptonLabel9.Values.Text = "Processing:";
            // 
            // cmbBetType
            // 
            this.cmbBetType.AutoCompleteCustomSource.AddRange(new string[] {
            "Win Only",
            "Each Way",
            "Place Only"});
            this.cmbBetType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBetType.FormattingEnabled = true;
            this.cmbBetType.Items.AddRange(new object[] {
            "Win Only",
            "Each Way",
            "Place Only"});
            this.cmbBetType.Location = new System.Drawing.Point(661, 386);
            this.cmbBetType.Name = "cmbBetType";
            this.cmbBetType.Size = new System.Drawing.Size(121, 28);
            this.cmbBetType.TabIndex = 53;
            this.cmbBetType.Text = "Win Only";
            this.cmbBetType.SelectedIndexChanged += new System.EventHandler(this.cmbBetType_SelectedIndexChanged);
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(556, 386);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(99, 26);
            this.kryptonLabel10.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel10.TabIndex = 54;
            this.kryptonLabel10.Values.Text = "Bet Type:";
            // 
            // pgProgress
            // 
            this.pgProgress.Dash = true;
            this.pgProgress.Location = new System.Drawing.Point(525, 231);
            this.pgProgress.Name = "pgProgress";
            this.pgProgress.SeparatorColor1 = System.Drawing.Color.Yellow;
            this.pgProgress.SeparatorColor2 = System.Drawing.Color.Gold;
            this.pgProgress.SeparatorWidth = 6;
            this.pgProgress.Size = new System.Drawing.Size(237, 30);
            this.pgProgress.StepWidth = 12;
            this.pgProgress.SweepAngle = 210;
            this.pgProgress.TabIndex = 56;
            this.pgProgress.Text = "radProgressBar1";
            this.pgProgress.Visible = false;
            // 
            // txtTotalBal
            // 
            this.txtTotalBal.Name = "txtTotalBal";
            this.txtTotalBal.Size = new System.Drawing.Size(56, 17);
            this.txtTotalBal.Text = "Total Bal:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 671);
            this.Controls.Add(this.pgProgress);
            this.Controls.Add(this.kryptonLabel10);
            this.Controls.Add(this.cmbBetType);
            this.Controls.Add(this.kryptonLabel9);
            this.Controls.Add(this.kryptonLabel8);
            this.Controls.Add(this.btnResults);
            this.Controls.Add(this.btnBetPct);
            this.Controls.Add(this.btnUpdResults);
            this.Controls.Add(this.btnUpdScratch);
            this.Controls.Add(this.btnTestAutoBet);
            this.Controls.Add(this.txtTotalPct);
            this.Controls.Add(this.kryptonLabel7);
            this.Controls.Add(this.btnDeleteToday);
            this.Controls.Add(this.btnBetNow);
            this.Controls.Add(this.kryptonLabel6);
            this.Controls.Add(this.txtBetPct);
            this.Controls.Add(this.kryptonLabel5);
            this.Controls.Add(this.txtBetTo);
            this.Controls.Add(this.chkBetting);
            this.Controls.Add(this.txtStopLoss);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.txtStopWin);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblRaceTime);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.txtBetOffset);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.btnLoadRatings);
            this.Controls.Add(this.btnNextToGo);
            this.Controls.Add(this.cmbRace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbVenue);
            this.Controls.Add(this.lblNextRace);
            this.Controls.Add(this.chkBestPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkBetQld);
            this.Controls.Add(this.chkBetNsw);
            this.Controls.Add(this.chkBetVic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgRace);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtNext5);
            this.Name = "frmMain";
            this.Text = "The Minx - Main Menu";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.todaysVenueBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.todaysVenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

//        private ProgressControls.ProgressIndicator pgProgress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsText1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ToolStripStatusLabel txtVicBal;
        private System.Windows.Forms.ToolStripStatusLabel txtNswBal;
        private System.Windows.Forms.ToolStripStatusLabel txtQldBal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBetVic;
        private System.Windows.Forms.CheckBox chkBetNsw;
        private System.Windows.Forms.CheckBox chkBetQld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBestPrice;
        private System.Windows.Forms.Label lblNextRace;
        private System.Windows.Forms.ComboBox cmbVenue;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgRace;
        private TodaysVenue todaysVenue;
        private System.Windows.Forms.BindingSource todaysVenueBindingSource;
        private TodaysVenueTableAdapters.TodaysVenueTableAdapter todaysVenueTableAdapter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRace;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnNextToGo;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLoadRatings;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLoadData;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtBetOffset;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        public ComponentFactory.Krypton.Toolkit.KryptonRichTextBox txtStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblRaceTime;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExport;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtStopWin;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtStopLoss;
        private System.Windows.Forms.CheckBox chkBetting;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtBetTo;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtBetPct;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBetNow;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeleteToday;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtTotalPct;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private System.Windows.Forms.Button btnTestAutoBet;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpdScratch;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpdResults;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton btnBetPct;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnResults;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox txtNext5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private System.Windows.Forms.ComboBox cmbBetType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private Telerik.WinControls.UI.RadProgressBar pgProgress;
        private System.Windows.Forms.ToolStripStatusLabel txtTotalBal;
    }
}

