namespace YoutubeRadio2016
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lstVTracks = new System.Windows.Forms.ListView();
            this.clmTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.cmdAddTracks = new System.Windows.Forms.Button();
            this.lblUrl = new System.Windows.Forms.Label();
            this.cmdPlay = new System.Windows.Forms.Button();
            this.trkBDuration = new System.Windows.Forms.TrackBar();
            this.tmrPlayTrack = new System.Windows.Forms.Timer(this.components);
            this.lblTrackPos = new System.Windows.Forms.Label();
            this.lblTrackDuration = new System.Windows.Forms.Label();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdNextTrack = new System.Windows.Forms.Button();
            this.cmdPreviousTrack = new System.Windows.Forms.Button();
            this.imgLPlayPause = new System.Windows.Forms.ImageList(this.components);
            this.chkShuffle = new System.Windows.Forms.CheckBox();
            this.cmdRemoveTrack = new System.Windows.Forms.Button();
            this.cmdClearList = new System.Windows.Forms.Button();
            this.volSlider = new NAudio.Gui.VolumeSlider();
            this.cmdMute = new System.Windows.Forms.Button();
            this.chkAutoplay = new System.Windows.Forms.CheckBox();
            this.lblNextSong = new System.Windows.Forms.Label();
            this.cmdPlayAutoplayTrack = new System.Windows.Forms.Button();
            this.tlTipPlayImmediately = new System.Windows.Forms.ToolTip(this.components);
            this.cmdYoutubeView = new System.Windows.Forms.Button();
            this.imgLMute = new System.Windows.Forms.ImageList(this.components);
            this.mnuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.grpRepeat = new System.Windows.Forms.GroupBox();
            this.optRepeatOff = new System.Windows.Forms.RadioButton();
            this.optRepeatOne = new System.Windows.Forms.RadioButton();
            this.optRepeatAll = new System.Windows.Forms.RadioButton();
            this.prgLoadTracks = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.trkBDuration)).BeginInit();
            this.mnuStrip.SuspendLayout();
            this.grpRepeat.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstVTracks
            // 
            this.lstVTracks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTitle,
            this.clmDuration});
            this.lstVTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVTracks.FullRowSelect = true;
            this.lstVTracks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVTracks.HideSelection = false;
            this.lstVTracks.Location = new System.Drawing.Point(12, 111);
            this.lstVTracks.MultiSelect = false;
            this.lstVTracks.Name = "lstVTracks";
            this.lstVTracks.Size = new System.Drawing.Size(853, 465);
            this.lstVTracks.TabIndex = 4;
            this.lstVTracks.UseCompatibleStateImageBehavior = false;
            this.lstVTracks.View = System.Windows.Forms.View.Details;
            this.lstVTracks.SelectedIndexChanged += new System.EventHandler(this.lstVTracks_SelectedIndexChanged);
            this.lstVTracks.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstVTracks_KeyUp);
            this.lstVTracks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstVTracks_MouseDoubleClick);
            // 
            // clmTitle
            // 
            this.clmTitle.Text = "Titel";
            this.clmTitle.Width = 704;
            // 
            // clmDuration
            // 
            this.clmDuration.Text = "Dauer";
            this.clmDuration.Width = 115;
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(86, 38);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(585, 22);
            this.txtUrl.TabIndex = 0;
            this.txtUrl.Enter += new System.EventHandler(this.txtUrl_Enter);
            this.txtUrl.Leave += new System.EventHandler(this.txtUrl_Leave);
            // 
            // cmdAddTracks
            // 
            this.cmdAddTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddTracks.Location = new System.Drawing.Point(702, 38);
            this.cmdAddTracks.Name = "cmdAddTracks";
            this.cmdAddTracks.Size = new System.Drawing.Size(134, 23);
            this.cmdAddTracks.TabIndex = 1;
            this.cmdAddTracks.Text = "Hinzufügen";
            this.cmdAddTracks.UseVisualStyleBackColor = true;
            this.cmdAddTracks.Click += new System.EventHandler(this.cmdAddTracks_Click);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrl.Location = new System.Drawing.Point(12, 41);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(68, 16);
            this.lblUrl.TabIndex = 3;
            this.lblUrl.Text = "Video-Url:";
            // 
            // cmdPlay
            // 
            this.cmdPlay.Enabled = false;
            this.cmdPlay.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPlay.Location = new System.Drawing.Point(385, 655);
            this.cmdPlay.Name = "cmdPlay";
            this.cmdPlay.Size = new System.Drawing.Size(33, 36);
            this.cmdPlay.TabIndex = 8;
            this.cmdPlay.UseVisualStyleBackColor = true;
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // trkBDuration
            // 
            this.trkBDuration.LargeChange = 0;
            this.trkBDuration.Location = new System.Drawing.Point(15, 582);
            this.trkBDuration.Name = "trkBDuration";
            this.trkBDuration.Size = new System.Drawing.Size(850, 45);
            this.trkBDuration.SmallChange = 0;
            this.trkBDuration.TabIndex = 5;
            this.trkBDuration.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trkBDuration.ValueChanged += new System.EventHandler(this.trkBDuration_ValueChanged);
            this.trkBDuration.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trkBDuration_MouseDown);
            this.trkBDuration.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trkBDuration_MouseMove);
            this.trkBDuration.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trkBDuration_MouseUp);
            // 
            // tmrPlayTrack
            // 
            this.tmrPlayTrack.Tick += new System.EventHandler(this.tmrPlayTrack_Tick);
            // 
            // lblTrackPos
            // 
            this.lblTrackPos.AutoSize = true;
            this.lblTrackPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackPos.Location = new System.Drawing.Point(12, 630);
            this.lblTrackPos.Name = "lblTrackPos";
            this.lblTrackPos.Size = new System.Drawing.Size(56, 16);
            this.lblTrackPos.TabIndex = 6;
            this.lblTrackPos.Text = "00:00:00";
            // 
            // lblTrackDuration
            // 
            this.lblTrackDuration.AutoSize = true;
            this.lblTrackDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackDuration.Location = new System.Drawing.Point(808, 630);
            this.lblTrackDuration.Name = "lblTrackDuration";
            this.lblTrackDuration.Size = new System.Drawing.Size(56, 16);
            this.lblTrackDuration.TabIndex = 7;
            this.lblTrackDuration.Text = "00:00:00";
            // 
            // cmdStop
            // 
            this.cmdStop.Enabled = false;
            this.cmdStop.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStop.Image = ((System.Drawing.Image)(resources.GetObject("cmdStop.Image")));
            this.cmdStop.Location = new System.Drawing.Point(470, 655);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(33, 36);
            this.cmdStop.TabIndex = 9;
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdNextTrack
            // 
            this.cmdNextTrack.Enabled = false;
            this.cmdNextTrack.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNextTrack.Image = ((System.Drawing.Image)(resources.GetObject("cmdNextTrack.Image")));
            this.cmdNextTrack.Location = new System.Drawing.Point(555, 655);
            this.cmdNextTrack.Name = "cmdNextTrack";
            this.cmdNextTrack.Size = new System.Drawing.Size(33, 36);
            this.cmdNextTrack.TabIndex = 10;
            this.cmdNextTrack.UseVisualStyleBackColor = true;
            this.cmdNextTrack.Click += new System.EventHandler(this.cmdNextTrack_Click);
            // 
            // cmdPreviousTrack
            // 
            this.cmdPreviousTrack.Enabled = false;
            this.cmdPreviousTrack.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPreviousTrack.Image = ((System.Drawing.Image)(resources.GetObject("cmdPreviousTrack.Image")));
            this.cmdPreviousTrack.Location = new System.Drawing.Point(300, 655);
            this.cmdPreviousTrack.Name = "cmdPreviousTrack";
            this.cmdPreviousTrack.Size = new System.Drawing.Size(33, 36);
            this.cmdPreviousTrack.TabIndex = 7;
            this.cmdPreviousTrack.UseVisualStyleBackColor = true;
            this.cmdPreviousTrack.Click += new System.EventHandler(this.cmdPreviousTrack_Click);
            // 
            // imgLPlayPause
            // 
            this.imgLPlayPause.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLPlayPause.ImageStream")));
            this.imgLPlayPause.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLPlayPause.Images.SetKeyName(0, "ytrPlay.png");
            this.imgLPlayPause.Images.SetKeyName(1, "ytrPause.png");
            // 
            // chkShuffle
            // 
            this.chkShuffle.AutoSize = true;
            this.chkShuffle.Location = new System.Drawing.Point(118, 659);
            this.chkShuffle.Name = "chkShuffle";
            this.chkShuffle.Size = new System.Drawing.Size(112, 17);
            this.chkShuffle.TabIndex = 6;
            this.chkShuffle.Text = "Zufallswiedergabe";
            this.chkShuffle.UseVisualStyleBackColor = true;
            this.chkShuffle.CheckedChanged += new System.EventHandler(this.chkShuffle_CheckedChanged);
            // 
            // cmdRemoveTrack
            // 
            this.cmdRemoveTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemoveTrack.Image = ((System.Drawing.Image)(resources.GetObject("cmdRemoveTrack.Image")));
            this.cmdRemoveTrack.Location = new System.Drawing.Point(840, 82);
            this.cmdRemoveTrack.Name = "cmdRemoveTrack";
            this.cmdRemoveTrack.Size = new System.Drawing.Size(24, 23);
            this.cmdRemoveTrack.TabIndex = 2;
            this.tlTipPlayImmediately.SetToolTip(this.cmdRemoveTrack, "Ausgewählten Track löschen");
            this.cmdRemoveTrack.UseVisualStyleBackColor = true;
            this.cmdRemoveTrack.Click += new System.EventHandler(this.cmdRemoveTrack_Click);
            // 
            // cmdClearList
            // 
            this.cmdClearList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClearList.Image = ((System.Drawing.Image)(resources.GetObject("cmdClearList.Image")));
            this.cmdClearList.Location = new System.Drawing.Point(810, 82);
            this.cmdClearList.Name = "cmdClearList";
            this.cmdClearList.Size = new System.Drawing.Size(24, 23);
            this.cmdClearList.TabIndex = 3;
            this.tlTipPlayImmediately.SetToolTip(this.cmdClearList, "Liste leeren");
            this.cmdClearList.UseVisualStyleBackColor = true;
            this.cmdClearList.Click += new System.EventHandler(this.cmdClearList_Click);
            // 
            // volSlider
            // 
            this.volSlider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.volSlider.Location = new System.Drawing.Point(769, 667);
            this.volSlider.Name = "volSlider";
            this.volSlider.Size = new System.Drawing.Size(95, 16);
            this.volSlider.TabIndex = 20;
            this.volSlider.VolumeChanged += new System.EventHandler(this.volSlider_VolumeChanged);
            // 
            // cmdMute
            // 
            this.cmdMute.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMute.Location = new System.Drawing.Point(730, 659);
            this.cmdMute.Name = "cmdMute";
            this.cmdMute.Size = new System.Drawing.Size(29, 29);
            this.cmdMute.TabIndex = 21;
            this.cmdMute.UseVisualStyleBackColor = true;
            this.cmdMute.Click += new System.EventHandler(this.cmdMute_Click);
            // 
            // chkAutoplay
            // 
            this.chkAutoplay.AutoSize = true;
            this.chkAutoplay.Location = new System.Drawing.Point(118, 701);
            this.chkAutoplay.Name = "chkAutoplay";
            this.chkAutoplay.Size = new System.Drawing.Size(67, 17);
            this.chkAutoplay.TabIndex = 22;
            this.chkAutoplay.Text = "Autoplay";
            this.chkAutoplay.UseVisualStyleBackColor = true;
            this.chkAutoplay.CheckedChanged += new System.EventHandler(this.chkAutoplay_CheckedChanged);
            // 
            // lblNextSong
            // 
            this.lblNextSong.AutoEllipsis = true;
            this.lblNextSong.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNextSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextSong.Location = new System.Drawing.Point(144, 739);
            this.lblNextSong.Name = "lblNextSong";
            this.lblNextSong.Size = new System.Drawing.Size(720, 18);
            this.lblNextSong.TabIndex = 23;
            this.lblNextSong.Text = "Nächster Song:";
            this.lblNextSong.UseMnemonic = false;
            this.lblNextSong.Visible = false;
            // 
            // cmdPlayAutoplayTrack
            // 
            this.cmdPlayAutoplayTrack.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPlayAutoplayTrack.Image = ((System.Drawing.Image)(resources.GetObject("cmdPlayAutoplayTrack.Image")));
            this.cmdPlayAutoplayTrack.Location = new System.Drawing.Point(118, 735);
            this.cmdPlayAutoplayTrack.Name = "cmdPlayAutoplayTrack";
            this.cmdPlayAutoplayTrack.Size = new System.Drawing.Size(20, 22);
            this.cmdPlayAutoplayTrack.TabIndex = 24;
            this.tlTipPlayImmediately.SetToolTip(this.cmdPlayAutoplayTrack, "Sofort abspielen");
            this.cmdPlayAutoplayTrack.UseVisualStyleBackColor = true;
            this.cmdPlayAutoplayTrack.Visible = false;
            this.cmdPlayAutoplayTrack.Click += new System.EventHandler(this.cmdPlayAutoplayTrack_Click);
            // 
            // cmdYoutubeView
            // 
            this.cmdYoutubeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdYoutubeView.Image = ((System.Drawing.Image)(resources.GetObject("cmdYoutubeView.Image")));
            this.cmdYoutubeView.Location = new System.Drawing.Point(15, 74);
            this.cmdYoutubeView.Name = "cmdYoutubeView";
            this.cmdYoutubeView.Size = new System.Drawing.Size(33, 31);
            this.cmdYoutubeView.TabIndex = 28;
            this.tlTipPlayImmediately.SetToolTip(this.cmdYoutubeView, "Auf Youtube ansehen");
            this.cmdYoutubeView.UseVisualStyleBackColor = true;
            this.cmdYoutubeView.Click += new System.EventHandler(this.cmdYoutubeView_Click);
            // 
            // imgLMute
            // 
            this.imgLMute.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLMute.ImageStream")));
            this.imgLMute.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLMute.Images.SetKeyName(0, "MuteOff");
            this.imgLMute.Images.SetKeyName(1, "MuteOn");
            // 
            // mnuStrip
            // 
            this.mnuStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mnuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMenu});
            this.mnuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuStrip.Name = "mnuStrip";
            this.mnuStrip.Size = new System.Drawing.Size(882, 24);
            this.mnuStrip.TabIndex = 25;
            this.mnuStrip.Text = "menuStrip1";
            // 
            // mnuMenu
            // 
            this.mnuMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings});
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Size = new System.Drawing.Size(50, 20);
            this.mnuMenu.Text = "Menü";
            // 
            // mnuSettings
            // 
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(154, 22);
            this.mnuSettings.Text = "Einstellungen...";
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // grpRepeat
            // 
            this.grpRepeat.Controls.Add(this.optRepeatOff);
            this.grpRepeat.Controls.Add(this.optRepeatOne);
            this.grpRepeat.Controls.Add(this.optRepeatAll);
            this.grpRepeat.Location = new System.Drawing.Point(15, 655);
            this.grpRepeat.Name = "grpRepeat";
            this.grpRepeat.Size = new System.Drawing.Size(83, 100);
            this.grpRepeat.TabIndex = 26;
            this.grpRepeat.TabStop = false;
            this.grpRepeat.Text = "Repeat";
            // 
            // optRepeatOff
            // 
            this.optRepeatOff.AutoSize = true;
            this.optRepeatOff.Checked = true;
            this.optRepeatOff.Location = new System.Drawing.Point(17, 65);
            this.optRepeatOff.Name = "optRepeatOff";
            this.optRepeatOff.Size = new System.Drawing.Size(43, 17);
            this.optRepeatOff.TabIndex = 2;
            this.optRepeatOff.TabStop = true;
            this.optRepeatOff.Text = "Aus";
            this.optRepeatOff.UseVisualStyleBackColor = true;
            this.optRepeatOff.CheckedChanged += new System.EventHandler(this.optRepeat_CheckedChanged);
            // 
            // optRepeatOne
            // 
            this.optRepeatOne.AutoSize = true;
            this.optRepeatOne.Location = new System.Drawing.Point(17, 42);
            this.optRepeatOne.Name = "optRepeatOne";
            this.optRepeatOne.Size = new System.Drawing.Size(63, 17);
            this.optRepeatOne.TabIndex = 1;
            this.optRepeatOne.TabStop = true;
            this.optRepeatOne.Text = "Ein Lied";
            this.optRepeatOne.UseVisualStyleBackColor = true;
            this.optRepeatOne.CheckedChanged += new System.EventHandler(this.optRepeat_CheckedChanged);
            // 
            // optRepeatAll
            // 
            this.optRepeatAll.AutoSize = true;
            this.optRepeatAll.Location = new System.Drawing.Point(17, 19);
            this.optRepeatAll.Name = "optRepeatAll";
            this.optRepeatAll.Size = new System.Drawing.Size(47, 17);
            this.optRepeatAll.TabIndex = 0;
            this.optRepeatAll.TabStop = true;
            this.optRepeatAll.Text = "Liste";
            this.optRepeatAll.UseVisualStyleBackColor = true;
            this.optRepeatAll.CheckedChanged += new System.EventHandler(this.optRepeat_CheckedChanged);
            // 
            // prgLoadTracks
            // 
            this.prgLoadTracks.Location = new System.Drawing.Point(297, 404);
            this.prgLoadTracks.Name = "prgLoadTracks";
            this.prgLoadTracks.Size = new System.Drawing.Size(288, 23);
            this.prgLoadTracks.TabIndex = 29;
            this.prgLoadTracks.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 772);
            this.Controls.Add(this.prgLoadTracks);
            this.Controls.Add(this.cmdYoutubeView);
            this.Controls.Add(this.grpRepeat);
            this.Controls.Add(this.lblTrackPos);
            this.Controls.Add(this.cmdPlayAutoplayTrack);
            this.Controls.Add(this.lblNextSong);
            this.Controls.Add(this.chkAutoplay);
            this.Controls.Add(this.cmdMute);
            this.Controls.Add(this.volSlider);
            this.Controls.Add(this.cmdClearList);
            this.Controls.Add(this.cmdRemoveTrack);
            this.Controls.Add(this.chkShuffle);
            this.Controls.Add(this.cmdPreviousTrack);
            this.Controls.Add(this.cmdNextTrack);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.lblTrackDuration);
            this.Controls.Add(this.trkBDuration);
            this.Controls.Add(this.cmdPlay);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.cmdAddTracks);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lstVTracks);
            this.Controls.Add(this.mnuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuStrip;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YoutubeRadio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkBDuration)).EndInit();
            this.mnuStrip.ResumeLayout(false);
            this.mnuStrip.PerformLayout();
            this.grpRepeat.ResumeLayout(false);
            this.grpRepeat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstVTracks;
        private System.Windows.Forms.ColumnHeader clmTitle;
        private System.Windows.Forms.ColumnHeader clmDuration;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button cmdAddTracks;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button cmdPlay;
        private System.Windows.Forms.TrackBar trkBDuration;
        private System.Windows.Forms.Timer tmrPlayTrack;
        private System.Windows.Forms.Label lblTrackPos;
        private System.Windows.Forms.Label lblTrackDuration;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Button cmdNextTrack;
        private System.Windows.Forms.Button cmdPreviousTrack;
        private System.Windows.Forms.ImageList imgLPlayPause;
        private System.Windows.Forms.CheckBox chkShuffle;
        private System.Windows.Forms.Button cmdRemoveTrack;
        private System.Windows.Forms.Button cmdClearList;
        private NAudio.Gui.VolumeSlider volSlider;
        private System.Windows.Forms.Button cmdMute;
        private System.Windows.Forms.CheckBox chkAutoplay;
        private System.Windows.Forms.Label lblNextSong;
        private System.Windows.Forms.Button cmdPlayAutoplayTrack;
        private System.Windows.Forms.ToolTip tlTipPlayImmediately;
        private System.Windows.Forms.ImageList imgLMute;
        private System.Windows.Forms.MenuStrip mnuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.GroupBox grpRepeat;
        private System.Windows.Forms.RadioButton optRepeatOff;
        private System.Windows.Forms.RadioButton optRepeatOne;
        private System.Windows.Forms.RadioButton optRepeatAll;
        private System.Windows.Forms.Button cmdYoutubeView;
        private System.Windows.Forms.ProgressBar prgLoadTracks;
    }
}

