namespace YoutubeRadio2016
{
    partial class frmSettings
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
            this.grpAutoplay = new System.Windows.Forms.GroupBox();
            this.optAutoplayOff = new System.Windows.Forms.RadioButton();
            this.optAutoplayLoad = new System.Windows.Forms.RadioButton();
            this.optAutoplayPlay = new System.Windows.Forms.RadioButton();
            this.grpRepeat = new System.Windows.Forms.GroupBox();
            this.optRepeatOff = new System.Windows.Forms.RadioButton();
            this.optRepeatOne = new System.Windows.Forms.RadioButton();
            this.optRepeatAll = new System.Windows.Forms.RadioButton();
            this.grpShuffle = new System.Windows.Forms.GroupBox();
            this.optShuffleOff = new System.Windows.Forms.RadioButton();
            this.optShuffleOn = new System.Windows.Forms.RadioButton();
            this.grpPlaybackSettings = new System.Windows.Forms.GroupBox();
            this.grpAppSettings = new System.Windows.Forms.GroupBox();
            this.chkSavePlaylist = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.grpAutoplay.SuspendLayout();
            this.grpRepeat.SuspendLayout();
            this.grpShuffle.SuspendLayout();
            this.grpPlaybackSettings.SuspendLayout();
            this.grpAppSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAutoplay
            // 
            this.grpAutoplay.Controls.Add(this.optAutoplayOff);
            this.grpAutoplay.Controls.Add(this.optAutoplayLoad);
            this.grpAutoplay.Controls.Add(this.optAutoplayPlay);
            this.grpAutoplay.Location = new System.Drawing.Point(15, 30);
            this.grpAutoplay.Name = "grpAutoplay";
            this.grpAutoplay.Size = new System.Drawing.Size(129, 90);
            this.grpAutoplay.TabIndex = 9;
            this.grpAutoplay.TabStop = false;
            this.grpAutoplay.Text = "Autoplay";
            // 
            // optAutoplayOff
            // 
            this.optAutoplayOff.AutoSize = true;
            this.optAutoplayOff.Checked = true;
            this.optAutoplayOff.Location = new System.Drawing.Point(8, 60);
            this.optAutoplayOff.Name = "optAutoplayOff";
            this.optAutoplayOff.Size = new System.Drawing.Size(43, 17);
            this.optAutoplayOff.TabIndex = 2;
            this.optAutoplayOff.TabStop = true;
            this.optAutoplayOff.Text = "Aus";
            this.optAutoplayOff.UseVisualStyleBackColor = true;
            this.optAutoplayOff.CheckedChanged += new System.EventHandler(this.optAutoplay_CheckedChanged);
            // 
            // optAutoplayLoad
            // 
            this.optAutoplayLoad.AutoSize = true;
            this.optAutoplayLoad.Location = new System.Drawing.Point(8, 37);
            this.optAutoplayLoad.Name = "optAutoplayLoad";
            this.optAutoplayLoad.Size = new System.Drawing.Size(115, 17);
            this.optAutoplayLoad.TabIndex = 1;
            this.optAutoplayLoad.Text = "In die Playlist laden";
            this.optAutoplayLoad.UseVisualStyleBackColor = true;
            this.optAutoplayLoad.CheckedChanged += new System.EventHandler(this.optAutoplay_CheckedChanged);
            // 
            // optAutoplayPlay
            // 
            this.optAutoplayPlay.AutoSize = true;
            this.optAutoplayPlay.Location = new System.Drawing.Point(8, 14);
            this.optAutoplayPlay.Name = "optAutoplayPlay";
            this.optAutoplayPlay.Size = new System.Drawing.Size(90, 17);
            this.optAutoplayPlay.TabIndex = 0;
            this.optAutoplayPlay.Text = "Nur abspielen";
            this.optAutoplayPlay.UseVisualStyleBackColor = true;
            this.optAutoplayPlay.CheckedChanged += new System.EventHandler(this.optAutoplay_CheckedChanged);
            // 
            // grpRepeat
            // 
            this.grpRepeat.Controls.Add(this.optRepeatOff);
            this.grpRepeat.Controls.Add(this.optRepeatOne);
            this.grpRepeat.Controls.Add(this.optRepeatAll);
            this.grpRepeat.Location = new System.Drawing.Point(162, 30);
            this.grpRepeat.Name = "grpRepeat";
            this.grpRepeat.Size = new System.Drawing.Size(113, 90);
            this.grpRepeat.TabIndex = 7;
            this.grpRepeat.TabStop = false;
            this.grpRepeat.Text = "Repeat";
            // 
            // optRepeatOff
            // 
            this.optRepeatOff.AutoSize = true;
            this.optRepeatOff.Checked = true;
            this.optRepeatOff.Location = new System.Drawing.Point(8, 60);
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
            this.optRepeatOne.Location = new System.Drawing.Point(8, 37);
            this.optRepeatOne.Name = "optRepeatOne";
            this.optRepeatOne.Size = new System.Drawing.Size(91, 17);
            this.optRepeatOne.TabIndex = 1;
            this.optRepeatOne.Text = "Aktuelles Lied";
            this.optRepeatOne.UseVisualStyleBackColor = true;
            this.optRepeatOne.CheckedChanged += new System.EventHandler(this.optRepeat_CheckedChanged);
            // 
            // optRepeatAll
            // 
            this.optRepeatAll.AutoSize = true;
            this.optRepeatAll.Location = new System.Drawing.Point(8, 14);
            this.optRepeatAll.Name = "optRepeatAll";
            this.optRepeatAll.Size = new System.Drawing.Size(78, 17);
            this.optRepeatAll.TabIndex = 0;
            this.optRepeatAll.Text = "GanzeListe";
            this.optRepeatAll.UseVisualStyleBackColor = true;
            this.optRepeatAll.CheckedChanged += new System.EventHandler(this.optRepeat_CheckedChanged);
            // 
            // grpShuffle
            // 
            this.grpShuffle.Controls.Add(this.optShuffleOff);
            this.grpShuffle.Controls.Add(this.optShuffleOn);
            this.grpShuffle.Location = new System.Drawing.Point(293, 30);
            this.grpShuffle.Name = "grpShuffle";
            this.grpShuffle.Size = new System.Drawing.Size(123, 90);
            this.grpShuffle.TabIndex = 10;
            this.grpShuffle.TabStop = false;
            this.grpShuffle.Text = "Zufallswiedergabe";
            // 
            // optShuffleOff
            // 
            this.optShuffleOff.AutoSize = true;
            this.optShuffleOff.Checked = true;
            this.optShuffleOff.Location = new System.Drawing.Point(8, 37);
            this.optShuffleOff.Name = "optShuffleOff";
            this.optShuffleOff.Size = new System.Drawing.Size(43, 17);
            this.optShuffleOff.TabIndex = 2;
            this.optShuffleOff.TabStop = true;
            this.optShuffleOff.Text = "Aus";
            this.optShuffleOff.UseVisualStyleBackColor = true;
            this.optShuffleOff.CheckedChanged += new System.EventHandler(this.optShuffle_CheckedChanged);
            // 
            // optShuffleOn
            // 
            this.optShuffleOn.AutoSize = true;
            this.optShuffleOn.Location = new System.Drawing.Point(8, 14);
            this.optShuffleOn.Name = "optShuffleOn";
            this.optShuffleOn.Size = new System.Drawing.Size(38, 17);
            this.optShuffleOn.TabIndex = 0;
            this.optShuffleOn.Text = "An";
            this.optShuffleOn.UseVisualStyleBackColor = true;
            this.optShuffleOn.CheckedChanged += new System.EventHandler(this.optShuffle_CheckedChanged);
            // 
            // grpPlaybackSettings
            // 
            this.grpPlaybackSettings.Controls.Add(this.grpAutoplay);
            this.grpPlaybackSettings.Controls.Add(this.grpRepeat);
            this.grpPlaybackSettings.Controls.Add(this.grpShuffle);
            this.grpPlaybackSettings.Location = new System.Drawing.Point(12, 12);
            this.grpPlaybackSettings.Name = "grpPlaybackSettings";
            this.grpPlaybackSettings.Size = new System.Drawing.Size(472, 150);
            this.grpPlaybackSettings.TabIndex = 10;
            this.grpPlaybackSettings.TabStop = false;
            this.grpPlaybackSettings.Text = "Wiedergabeeinstellungen";
            // 
            // grpAppSettings
            // 
            this.grpAppSettings.Controls.Add(this.chkSavePlaylist);
            this.grpAppSettings.Location = new System.Drawing.Point(12, 181);
            this.grpAppSettings.Name = "grpAppSettings";
            this.grpAppSettings.Size = new System.Drawing.Size(472, 73);
            this.grpAppSettings.TabIndex = 11;
            this.grpAppSettings.TabStop = false;
            this.grpAppSettings.Text = "Programmeinstellungen";
            // 
            // chkSavePlaylist
            // 
            this.chkSavePlaylist.AutoSize = true;
            this.chkSavePlaylist.Checked = true;
            this.chkSavePlaylist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSavePlaylist.Location = new System.Drawing.Point(15, 30);
            this.chkSavePlaylist.Name = "chkSavePlaylist";
            this.chkSavePlaylist.Size = new System.Drawing.Size(178, 17);
            this.chkSavePlaylist.TabIndex = 0;
            this.chkSavePlaylist.Text = "Playlist beim Beenden speichern";
            this.chkSavePlaylist.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(12, 270);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(409, 270);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 13;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 308);
            this.ControlBox = false;
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpAppSettings);
            this.Controls.Add(this.grpPlaybackSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSettings";
            this.Text = "Einstellungen";
            this.grpAutoplay.ResumeLayout(false);
            this.grpAutoplay.PerformLayout();
            this.grpRepeat.ResumeLayout(false);
            this.grpRepeat.PerformLayout();
            this.grpShuffle.ResumeLayout(false);
            this.grpShuffle.PerformLayout();
            this.grpPlaybackSettings.ResumeLayout(false);
            this.grpAppSettings.ResumeLayout(false);
            this.grpAppSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAutoplay;
        private System.Windows.Forms.RadioButton optAutoplayOff;
        private System.Windows.Forms.RadioButton optAutoplayLoad;
        private System.Windows.Forms.RadioButton optAutoplayPlay;
        private System.Windows.Forms.GroupBox grpRepeat;
        private System.Windows.Forms.RadioButton optRepeatOff;
        private System.Windows.Forms.RadioButton optRepeatOne;
        private System.Windows.Forms.RadioButton optRepeatAll;
        private System.Windows.Forms.GroupBox grpShuffle;
        private System.Windows.Forms.RadioButton optShuffleOff;
        private System.Windows.Forms.RadioButton optShuffleOn;
        private System.Windows.Forms.GroupBox grpPlaybackSettings;
        private System.Windows.Forms.GroupBox grpAppSettings;
        private System.Windows.Forms.CheckBox chkSavePlaylist;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
    }
}