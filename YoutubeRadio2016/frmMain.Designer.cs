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
            this.volSlider = new NAudio.Gui.VolumeSlider();
            this.cmdMute = new System.Windows.Forms.Button();
            this.lblNextSong = new System.Windows.Forms.Label();
            this.tlTipPlayImmediately = new System.Windows.Forms.ToolTip(this.components);
            this.imgLMute = new System.Windows.Forms.ImageList(this.components);
            this.grpRepeat = new System.Windows.Forms.GroupBox();
            this.optRepeatOff = new System.Windows.Forms.RadioButton();
            this.optRepeatOne = new System.Windows.Forms.RadioButton();
            this.optRepeatAll = new System.Windows.Forms.RadioButton();
            this.prgLoadTracks = new System.Windows.Forms.ProgressBar();
            this.grpAutoplay = new System.Windows.Forms.GroupBox();
            this.optAutoplay_Off = new System.Windows.Forms.RadioButton();
            this.optAutoplay_Load = new System.Windows.Forms.RadioButton();
            this.optAutoplay_Play = new System.Windows.Forms.RadioButton();
            this.grpShuffle = new System.Windows.Forms.GroupBox();
            this.optShuffle_Off = new System.Windows.Forms.RadioButton();
            this.optShuffle_On = new System.Windows.Forms.RadioButton();
            this.lblCurrentPlaylist = new System.Windows.Forms.Label();
            this.tlStrip = new System.Windows.Forms.ToolStrip();
            this.tlsCmdYoutubeView = new System.Windows.Forms.ToolStripButton();
            this.tlsCmdNewPlaylist = new System.Windows.Forms.ToolStripButton();
            this.tlsCmdSaveList = new System.Windows.Forms.ToolStripButton();
            this.tlsCmdRemoveTrack = new System.Windows.Forms.ToolStripButton();
            this.tlsCmdClearPlaylist = new System.Windows.Forms.ToolStripButton();
            this.tlsCmdRemovePlaylist = new System.Windows.Forms.ToolStripButton();
            this.grpSavedPlaylists = new System.Windows.Forms.GroupBox();
            this.lstBoxPlaylists = new System.Windows.Forms.ListBox();
            this.txtPlaylistName = new System.Windows.Forms.TextBox();
            this.cmdRenamePlaylist = new System.Windows.Forms.Button();
            this.cmdAcceptRenaming = new System.Windows.Forms.Button();
            this.cmdCancelRenaming = new System.Windows.Forms.Button();
            this.ntfIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trkBDuration)).BeginInit();
            this.grpRepeat.SuspendLayout();
            this.grpAutoplay.SuspendLayout();
            this.grpShuffle.SuspendLayout();
            this.tlStrip.SuspendLayout();
            this.grpSavedPlaylists.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstVTracks
            // 
            this.lstVTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVTracks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTitle,
            this.clmDuration});
            this.lstVTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVTracks.FullRowSelect = true;
            this.lstVTracks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVTracks.HideSelection = false;
            this.lstVTracks.Location = new System.Drawing.Point(317, 101);
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
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(433, 35);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(596, 22);
            this.txtUrl.TabIndex = 0;
            this.txtUrl.Enter += new System.EventHandler(this.txtUrl_Enter);
            this.txtUrl.Leave += new System.EventHandler(this.txtUrl_Leave);
            // 
            // cmdAddTracks
            // 
            this.cmdAddTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddTracks.Location = new System.Drawing.Point(1035, 35);
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
            this.lblUrl.Location = new System.Drawing.Point(314, 38);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(68, 16);
            this.lblUrl.TabIndex = 3;
            this.lblUrl.Text = "Video-Url:";
            // 
            // cmdPlay
            // 
            this.cmdPlay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdPlay.Enabled = false;
            this.cmdPlay.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPlay.Location = new System.Drawing.Point(690, 645);
            this.cmdPlay.Name = "cmdPlay";
            this.cmdPlay.Size = new System.Drawing.Size(33, 36);
            this.cmdPlay.TabIndex = 8;
            this.cmdPlay.UseVisualStyleBackColor = true;
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // trkBDuration
            // 
            this.trkBDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkBDuration.LargeChange = 0;
            this.trkBDuration.Location = new System.Drawing.Point(320, 572);
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
            this.lblTrackPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrackPos.AutoSize = true;
            this.lblTrackPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackPos.Location = new System.Drawing.Point(317, 620);
            this.lblTrackPos.Name = "lblTrackPos";
            this.lblTrackPos.Size = new System.Drawing.Size(56, 16);
            this.lblTrackPos.TabIndex = 6;
            this.lblTrackPos.Text = "00:00:00";
            // 
            // lblTrackDuration
            // 
            this.lblTrackDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackDuration.AutoSize = true;
            this.lblTrackDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackDuration.Location = new System.Drawing.Point(1113, 620);
            this.lblTrackDuration.Name = "lblTrackDuration";
            this.lblTrackDuration.Size = new System.Drawing.Size(56, 16);
            this.lblTrackDuration.TabIndex = 7;
            this.lblTrackDuration.Text = "00:00:00";
            // 
            // cmdStop
            // 
            this.cmdStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdStop.Enabled = false;
            this.cmdStop.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStop.Image = ((System.Drawing.Image)(resources.GetObject("cmdStop.Image")));
            this.cmdStop.Location = new System.Drawing.Point(775, 645);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(33, 36);
            this.cmdStop.TabIndex = 9;
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdNextTrack
            // 
            this.cmdNextTrack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdNextTrack.Enabled = false;
            this.cmdNextTrack.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNextTrack.Image = ((System.Drawing.Image)(resources.GetObject("cmdNextTrack.Image")));
            this.cmdNextTrack.Location = new System.Drawing.Point(860, 645);
            this.cmdNextTrack.Name = "cmdNextTrack";
            this.cmdNextTrack.Size = new System.Drawing.Size(33, 36);
            this.cmdNextTrack.TabIndex = 10;
            this.cmdNextTrack.UseVisualStyleBackColor = true;
            this.cmdNextTrack.Click += new System.EventHandler(this.cmdNextTrack_Click);
            // 
            // cmdPreviousTrack
            // 
            this.cmdPreviousTrack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdPreviousTrack.Enabled = false;
            this.cmdPreviousTrack.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPreviousTrack.Image = ((System.Drawing.Image)(resources.GetObject("cmdPreviousTrack.Image")));
            this.cmdPreviousTrack.Location = new System.Drawing.Point(605, 645);
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
            // volSlider
            // 
            this.volSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.volSlider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.volSlider.Location = new System.Drawing.Point(359, 660);
            this.volSlider.Name = "volSlider";
            this.volSlider.Size = new System.Drawing.Size(95, 16);
            this.volSlider.TabIndex = 20;
            this.volSlider.VolumeChanged += new System.EventHandler(this.volSlider_VolumeChanged);
            // 
            // cmdMute
            // 
            this.cmdMute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdMute.Font = new System.Drawing.Font("Mistral", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMute.Location = new System.Drawing.Point(320, 652);
            this.cmdMute.Name = "cmdMute";
            this.cmdMute.Size = new System.Drawing.Size(29, 29);
            this.cmdMute.TabIndex = 21;
            this.cmdMute.UseVisualStyleBackColor = true;
            this.cmdMute.Click += new System.EventHandler(this.cmdMute_Click);
            // 
            // lblNextSong
            // 
            this.lblNextSong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNextSong.AutoEllipsis = true;
            this.lblNextSong.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNextSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextSong.Location = new System.Drawing.Point(320, 818);
            this.lblNextSong.Name = "lblNextSong";
            this.lblNextSong.Size = new System.Drawing.Size(864, 18);
            this.lblNextSong.TabIndex = 23;
            this.lblNextSong.Text = "Nächster Song:";
            this.lblNextSong.UseMnemonic = false;
            this.lblNextSong.Visible = false;
            // 
            // imgLMute
            // 
            this.imgLMute.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLMute.ImageStream")));
            this.imgLMute.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLMute.Images.SetKeyName(0, "MuteOff");
            this.imgLMute.Images.SetKeyName(1, "MuteOn");
            // 
            // grpRepeat
            // 
            this.grpRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpRepeat.Controls.Add(this.optRepeatOff);
            this.grpRepeat.Controls.Add(this.optRepeatOne);
            this.grpRepeat.Controls.Add(this.optRepeatAll);
            this.grpRepeat.Location = new System.Drawing.Point(320, 710);
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
            this.prgLoadTracks.Location = new System.Drawing.Point(602, 394);
            this.prgLoadTracks.Name = "prgLoadTracks";
            this.prgLoadTracks.Size = new System.Drawing.Size(288, 23);
            this.prgLoadTracks.TabIndex = 29;
            this.prgLoadTracks.Visible = false;
            // 
            // grpAutoplay
            // 
            this.grpAutoplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpAutoplay.Controls.Add(this.optAutoplay_Off);
            this.grpAutoplay.Controls.Add(this.optAutoplay_Load);
            this.grpAutoplay.Controls.Add(this.optAutoplay_Play);
            this.grpAutoplay.Location = new System.Drawing.Point(423, 710);
            this.grpAutoplay.Name = "grpAutoplay";
            this.grpAutoplay.Size = new System.Drawing.Size(129, 100);
            this.grpAutoplay.TabIndex = 27;
            this.grpAutoplay.TabStop = false;
            this.grpAutoplay.Text = "Autoplay";
            // 
            // optAutoplay_Off
            // 
            this.optAutoplay_Off.AutoSize = true;
            this.optAutoplay_Off.Checked = true;
            this.optAutoplay_Off.Location = new System.Drawing.Point(17, 65);
            this.optAutoplay_Off.Name = "optAutoplay_Off";
            this.optAutoplay_Off.Size = new System.Drawing.Size(43, 17);
            this.optAutoplay_Off.TabIndex = 2;
            this.optAutoplay_Off.TabStop = true;
            this.optAutoplay_Off.Text = "Aus";
            this.optAutoplay_Off.UseVisualStyleBackColor = true;
            this.optAutoplay_Off.CheckedChanged += new System.EventHandler(this.optAutoplay_CheckedChanged);
            // 
            // optAutoplay_Load
            // 
            this.optAutoplay_Load.AutoSize = true;
            this.optAutoplay_Load.Location = new System.Drawing.Point(17, 42);
            this.optAutoplay_Load.Name = "optAutoplay_Load";
            this.optAutoplay_Load.Size = new System.Drawing.Size(105, 17);
            this.optAutoplay_Load.TabIndex = 1;
            this.optAutoplay_Load.TabStop = true;
            this.optAutoplay_Load.Text = "In die Liste laden";
            this.optAutoplay_Load.UseVisualStyleBackColor = true;
            this.optAutoplay_Load.CheckedChanged += new System.EventHandler(this.optAutoplay_CheckedChanged);
            // 
            // optAutoplay_Play
            // 
            this.optAutoplay_Play.AutoSize = true;
            this.optAutoplay_Play.Location = new System.Drawing.Point(17, 19);
            this.optAutoplay_Play.Name = "optAutoplay_Play";
            this.optAutoplay_Play.Size = new System.Drawing.Size(90, 17);
            this.optAutoplay_Play.TabIndex = 0;
            this.optAutoplay_Play.TabStop = true;
            this.optAutoplay_Play.Text = "Nur abspielen";
            this.optAutoplay_Play.UseVisualStyleBackColor = true;
            this.optAutoplay_Play.CheckedChanged += new System.EventHandler(this.optAutoplay_CheckedChanged);
            // 
            // grpShuffle
            // 
            this.grpShuffle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpShuffle.Controls.Add(this.optShuffle_Off);
            this.grpShuffle.Controls.Add(this.optShuffle_On);
            this.grpShuffle.Location = new System.Drawing.Point(572, 710);
            this.grpShuffle.Name = "grpShuffle";
            this.grpShuffle.Size = new System.Drawing.Size(129, 100);
            this.grpShuffle.TabIndex = 28;
            this.grpShuffle.TabStop = false;
            this.grpShuffle.Text = "Zufallswiedergabe";
            // 
            // optShuffle_Off
            // 
            this.optShuffle_Off.AutoSize = true;
            this.optShuffle_Off.Checked = true;
            this.optShuffle_Off.Location = new System.Drawing.Point(17, 53);
            this.optShuffle_Off.Name = "optShuffle_Off";
            this.optShuffle_Off.Size = new System.Drawing.Size(43, 17);
            this.optShuffle_Off.TabIndex = 2;
            this.optShuffle_Off.TabStop = true;
            this.optShuffle_Off.Text = "Aus";
            this.optShuffle_Off.UseVisualStyleBackColor = true;
            this.optShuffle_Off.CheckedChanged += new System.EventHandler(this.optShuffle_CheckedChanged);
            // 
            // optShuffle_On
            // 
            this.optShuffle_On.AutoSize = true;
            this.optShuffle_On.Location = new System.Drawing.Point(17, 30);
            this.optShuffle_On.Name = "optShuffle_On";
            this.optShuffle_On.Size = new System.Drawing.Size(38, 17);
            this.optShuffle_On.TabIndex = 0;
            this.optShuffle_On.TabStop = true;
            this.optShuffle_On.Text = "An";
            this.optShuffle_On.UseVisualStyleBackColor = true;
            this.optShuffle_On.CheckedChanged += new System.EventHandler(this.optShuffle_CheckedChanged);
            // 
            // lblCurrentPlaylist
            // 
            this.lblCurrentPlaylist.AutoSize = true;
            this.lblCurrentPlaylist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPlaylist.Location = new System.Drawing.Point(317, 70);
            this.lblCurrentPlaylist.Name = "lblCurrentPlaylist";
            this.lblCurrentPlaylist.Size = new System.Drawing.Size(105, 16);
            this.lblCurrentPlaylist.TabIndex = 33;
            this.lblCurrentPlaylist.Text = "Aktuelle Playlist:";
            this.lblCurrentPlaylist.UseMnemonic = false;
            // 
            // tlStrip
            // 
            this.tlStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsCmdYoutubeView,
            this.tlsCmdNewPlaylist,
            this.tlsCmdSaveList,
            this.tlsCmdRemoveTrack,
            this.tlsCmdClearPlaylist,
            this.tlsCmdRemovePlaylist});
            this.tlStrip.Location = new System.Drawing.Point(0, 0);
            this.tlStrip.Name = "tlStrip";
            this.tlStrip.Size = new System.Drawing.Size(1182, 25);
            this.tlStrip.TabIndex = 35;
            this.tlStrip.Text = "toolStrip1";
            // 
            // tlsCmdYoutubeView
            // 
            this.tlsCmdYoutubeView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsCmdYoutubeView.Enabled = false;
            this.tlsCmdYoutubeView.Image = ((System.Drawing.Image)(resources.GetObject("tlsCmdYoutubeView.Image")));
            this.tlsCmdYoutubeView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsCmdYoutubeView.Name = "tlsCmdYoutubeView";
            this.tlsCmdYoutubeView.Size = new System.Drawing.Size(23, 22);
            this.tlsCmdYoutubeView.ToolTipText = "Auf Youtube ansehen";
            this.tlsCmdYoutubeView.Click += new System.EventHandler(this.tlsCmdYoutubeView_Click);
            // 
            // tlsCmdNewPlaylist
            // 
            this.tlsCmdNewPlaylist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsCmdNewPlaylist.Image = ((System.Drawing.Image)(resources.GetObject("tlsCmdNewPlaylist.Image")));
            this.tlsCmdNewPlaylist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsCmdNewPlaylist.Name = "tlsCmdNewPlaylist";
            this.tlsCmdNewPlaylist.Size = new System.Drawing.Size(23, 22);
            this.tlsCmdNewPlaylist.ToolTipText = "Neue Playlist";
            this.tlsCmdNewPlaylist.Click += new System.EventHandler(this.tlsCmdNewPlaylist_Click);
            // 
            // tlsCmdSaveList
            // 
            this.tlsCmdSaveList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsCmdSaveList.Enabled = false;
            this.tlsCmdSaveList.Image = ((System.Drawing.Image)(resources.GetObject("tlsCmdSaveList.Image")));
            this.tlsCmdSaveList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsCmdSaveList.Name = "tlsCmdSaveList";
            this.tlsCmdSaveList.Size = new System.Drawing.Size(23, 22);
            this.tlsCmdSaveList.ToolTipText = "Aktuelle Playlist speichern";
            this.tlsCmdSaveList.Click += new System.EventHandler(this.tlsCmdSaveList_Click);
            // 
            // tlsCmdRemoveTrack
            // 
            this.tlsCmdRemoveTrack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsCmdRemoveTrack.Enabled = false;
            this.tlsCmdRemoveTrack.Image = ((System.Drawing.Image)(resources.GetObject("tlsCmdRemoveTrack.Image")));
            this.tlsCmdRemoveTrack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsCmdRemoveTrack.Name = "tlsCmdRemoveTrack";
            this.tlsCmdRemoveTrack.Size = new System.Drawing.Size(23, 22);
            this.tlsCmdRemoveTrack.ToolTipText = "Ausgewählten Track löschen";
            this.tlsCmdRemoveTrack.Click += new System.EventHandler(this.tlsCmdRemoveTrack_Click);
            // 
            // tlsCmdClearPlaylist
            // 
            this.tlsCmdClearPlaylist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsCmdClearPlaylist.Enabled = false;
            this.tlsCmdClearPlaylist.Image = ((System.Drawing.Image)(resources.GetObject("tlsCmdClearPlaylist.Image")));
            this.tlsCmdClearPlaylist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsCmdClearPlaylist.Name = "tlsCmdClearPlaylist";
            this.tlsCmdClearPlaylist.Size = new System.Drawing.Size(23, 22);
            this.tlsCmdClearPlaylist.ToolTipText = "Aktuelle Playlist leeren";
            this.tlsCmdClearPlaylist.Click += new System.EventHandler(this.tlsCmdClearPlaylist_Click);
            // 
            // tlsCmdRemovePlaylist
            // 
            this.tlsCmdRemovePlaylist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsCmdRemovePlaylist.Enabled = false;
            this.tlsCmdRemovePlaylist.Image = ((System.Drawing.Image)(resources.GetObject("tlsCmdRemovePlaylist.Image")));
            this.tlsCmdRemovePlaylist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsCmdRemovePlaylist.Name = "tlsCmdRemovePlaylist";
            this.tlsCmdRemovePlaylist.Size = new System.Drawing.Size(23, 22);
            this.tlsCmdRemovePlaylist.ToolTipText = "Ausgewählte Playlist löschen";
            this.tlsCmdRemovePlaylist.Click += new System.EventHandler(this.tlsCmdRemovePlaylist_Click);
            // 
            // grpSavedPlaylists
            // 
            this.grpSavedPlaylists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpSavedPlaylists.Controls.Add(this.lstBoxPlaylists);
            this.grpSavedPlaylists.Location = new System.Drawing.Point(14, 31);
            this.grpSavedPlaylists.Name = "grpSavedPlaylists";
            this.grpSavedPlaylists.Size = new System.Drawing.Size(285, 811);
            this.grpSavedPlaylists.TabIndex = 37;
            this.grpSavedPlaylists.TabStop = false;
            this.grpSavedPlaylists.Text = "Gespeicherte Playlists:";
            // 
            // lstBoxPlaylists
            // 
            this.lstBoxPlaylists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstBoxPlaylists.FormattingEnabled = true;
            this.lstBoxPlaylists.Location = new System.Drawing.Point(11, 20);
            this.lstBoxPlaylists.Name = "lstBoxPlaylists";
            this.lstBoxPlaylists.Size = new System.Drawing.Size(262, 771);
            this.lstBoxPlaylists.TabIndex = 0;
            this.lstBoxPlaylists.SelectedIndexChanged += new System.EventHandler(this.lstBoxPlaylists_SelectedIndexChanged);
            this.lstBoxPlaylists.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstBoxPlaylists_MouseDoubleClick);
            // 
            // txtPlaylistName
            // 
            this.txtPlaylistName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlaylistName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaylistName.Location = new System.Drawing.Point(433, 67);
            this.txtPlaylistName.Name = "txtPlaylistName";
            this.txtPlaylistName.ReadOnly = true;
            this.txtPlaylistName.Size = new System.Drawing.Size(596, 22);
            this.txtPlaylistName.TabIndex = 38;
            this.txtPlaylistName.Text = "(Keine Playlist)";
            this.txtPlaylistName.Enter += new System.EventHandler(this.txtPlaylistName_Enter);
            this.txtPlaylistName.Leave += new System.EventHandler(this.txtPlaylistName_Leave);
            // 
            // cmdRenamePlaylist
            // 
            this.cmdRenamePlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRenamePlaylist.Enabled = false;
            this.cmdRenamePlaylist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRenamePlaylist.Location = new System.Drawing.Point(1035, 67);
            this.cmdRenamePlaylist.Name = "cmdRenamePlaylist";
            this.cmdRenamePlaylist.Size = new System.Drawing.Size(134, 23);
            this.cmdRenamePlaylist.TabIndex = 40;
            this.cmdRenamePlaylist.Text = "Umbenennen";
            this.cmdRenamePlaylist.UseVisualStyleBackColor = true;
            this.cmdRenamePlaylist.Click += new System.EventHandler(this.cmdRenamePlaylist_Click);
            // 
            // cmdAcceptRenaming
            // 
            this.cmdAcceptRenaming.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAcceptRenaming.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAcceptRenaming.Image = ((System.Drawing.Image)(resources.GetObject("cmdAcceptRenaming.Image")));
            this.cmdAcceptRenaming.Location = new System.Drawing.Point(976, 67);
            this.cmdAcceptRenaming.Name = "cmdAcceptRenaming";
            this.cmdAcceptRenaming.Size = new System.Drawing.Size(27, 23);
            this.cmdAcceptRenaming.TabIndex = 41;
            this.cmdAcceptRenaming.UseVisualStyleBackColor = true;
            this.cmdAcceptRenaming.Visible = false;
            this.cmdAcceptRenaming.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdCancelRenaming
            // 
            this.cmdCancelRenaming.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancelRenaming.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancelRenaming.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancelRenaming.Image")));
            this.cmdCancelRenaming.Location = new System.Drawing.Point(1002, 67);
            this.cmdCancelRenaming.Name = "cmdCancelRenaming";
            this.cmdCancelRenaming.Size = new System.Drawing.Size(27, 23);
            this.cmdCancelRenaming.TabIndex = 42;
            this.cmdCancelRenaming.UseVisualStyleBackColor = true;
            this.cmdCancelRenaming.Visible = false;
            this.cmdCancelRenaming.Click += new System.EventHandler(this.cmdCancelRenaming_Click);
            // 
            // ntfIcon
            // 
            this.ntfIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ntfIcon.BalloonTipText = "YouTubeRadio läuft im Hintergrund weiter";
            this.ntfIcon.BalloonTipTitle = "YoutubeRadio";
            this.ntfIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfIcon.Icon")));
            this.ntfIcon.Text = "YouTubeRadio";
            this.ntfIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ntfIcon_MouseClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 851);
            this.Controls.Add(this.cmdCancelRenaming);
            this.Controls.Add(this.cmdAcceptRenaming);
            this.Controls.Add(this.cmdRenamePlaylist);
            this.Controls.Add(this.txtPlaylistName);
            this.Controls.Add(this.grpSavedPlaylists);
            this.Controls.Add(this.tlStrip);
            this.Controls.Add(this.lblCurrentPlaylist);
            this.Controls.Add(this.grpShuffle);
            this.Controls.Add(this.grpAutoplay);
            this.Controls.Add(this.prgLoadTracks);
            this.Controls.Add(this.grpRepeat);
            this.Controls.Add(this.lblTrackPos);
            this.Controls.Add(this.lblNextSong);
            this.Controls.Add(this.cmdMute);
            this.Controls.Add(this.volSlider);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(920, 530);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YoutubeRadio - (Keine Playlist)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trkBDuration)).EndInit();
            this.grpRepeat.ResumeLayout(false);
            this.grpRepeat.PerformLayout();
            this.grpAutoplay.ResumeLayout(false);
            this.grpAutoplay.PerformLayout();
            this.grpShuffle.ResumeLayout(false);
            this.grpShuffle.PerformLayout();
            this.tlStrip.ResumeLayout(false);
            this.tlStrip.PerformLayout();
            this.grpSavedPlaylists.ResumeLayout(false);
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
        private NAudio.Gui.VolumeSlider volSlider;
        private System.Windows.Forms.Button cmdMute;
        private System.Windows.Forms.Label lblNextSong;
        private System.Windows.Forms.ToolTip tlTipPlayImmediately;
        private System.Windows.Forms.ImageList imgLMute;
        private System.Windows.Forms.GroupBox grpRepeat;
        private System.Windows.Forms.RadioButton optRepeatOff;
        private System.Windows.Forms.RadioButton optRepeatOne;
        private System.Windows.Forms.RadioButton optRepeatAll;
        private System.Windows.Forms.ProgressBar prgLoadTracks;
        private System.Windows.Forms.GroupBox grpAutoplay;
        private System.Windows.Forms.RadioButton optAutoplay_Off;
        private System.Windows.Forms.RadioButton optAutoplay_Load;
        private System.Windows.Forms.RadioButton optAutoplay_Play;
        private System.Windows.Forms.GroupBox grpShuffle;
        private System.Windows.Forms.RadioButton optShuffle_Off;
        private System.Windows.Forms.RadioButton optShuffle_On;
        private System.Windows.Forms.Label lblCurrentPlaylist;
        private System.Windows.Forms.ToolStrip tlStrip;
        private System.Windows.Forms.ToolStripButton tlsCmdYoutubeView;
        private System.Windows.Forms.ToolStripButton tlsCmdNewPlaylist;
        private System.Windows.Forms.ToolStripButton tlsCmdSaveList;
        private System.Windows.Forms.ToolStripButton tlsCmdClearPlaylist;
        private System.Windows.Forms.ToolStripButton tlsCmdRemoveTrack;
        private System.Windows.Forms.ToolStripButton tlsCmdRemovePlaylist;
        private System.Windows.Forms.GroupBox grpSavedPlaylists;
        private System.Windows.Forms.ListBox lstBoxPlaylists;
        private System.Windows.Forms.TextBox txtPlaylistName;
        private System.Windows.Forms.Button cmdRenamePlaylist;
        private System.Windows.Forms.Button cmdAcceptRenaming;
        private System.Windows.Forms.Button cmdCancelRenaming;
        private System.Windows.Forms.NotifyIcon ntfIcon;
    }
}

