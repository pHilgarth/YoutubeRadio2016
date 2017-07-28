using HtmlAgilityPack;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace YoutubeRadio2016
{
    public partial class frmMain : Form
    {                
        bool currentPlaylistChanged = false;
        bool mute = false;
        bool muteButtonClicked = false;
        bool playbackStopped = true;
        bool trackBarSeeking = false;

        bool savingAborted;
        float volume;
        string videoUrlAutoplayTrack;
        AudioTrack currentTrack;
        AudioTrack selectedTrack;
        Image muteOnImage;
        Image muteOffImage;        
        Image pauseImage;
        Image playImage;
        MediaFoundationReader mediaReader;
        Playlist currentPlaylist;
        Settings settings;
        WaveOutEvent waveOut;    

        public frmMain()
        {
            InitializeComponent();
        }           
                
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            txtPlaylistName.Text = txtPlaylistName.Text.Trim();

            if (PlaylistManager.VerifyPlaylistName(txtPlaylistName.Text, currentPlaylist.Name))
            {
                string newPlaylistName = txtPlaylistName.Text;

                if (!currentPlaylist.Name.Equals(newPlaylistName))
                {
                    currentPlaylistChanged = true;

                    Text = "YoutubeRadio - " + newPlaylistName + "*";
                    txtPlaylistName.Text = newPlaylistName + "*";

                    if(currentPlaylist.Index != -1)
                    {
                        lstBoxPlaylists.Items[currentPlaylist.Index] = Path.GetFileNameWithoutExtension(currentPlaylist.Filename) + "*";
                    }
                }

                currentPlaylist.Name = newPlaylistName;                

                txtPlaylistName.ReadOnly = true;
                cmdAcceptRenaming.Visible = false;
                cmdCancelRenaming.Visible = false;
                txtUrl.Focus();
            }
            else
            {
                txtPlaylistName.SelectAll();
                txtPlaylistName.Focus();
            }

            UpdateButtons();
        }   
        private void cmdAddTracks_Click(object sender, EventArgs e)
        {
            string videoUrl = txtUrl.Text;

            if(videoUrl.StartsWith("www"))
            {
                videoUrl = "https://" + videoUrl;
            }

            if (!string.IsNullOrWhiteSpace(videoUrl) && videoUrl.StartsWith("https://www.youtube.com/watch?v="))
            {
                int lstVCountBefore = lstVTracks.Items.Count;

                if(currentPlaylist == null)
                {
                    PlaylistManager.CurrentPlaylist = new Playlist();
                    currentPlaylist = PlaylistManager.CurrentPlaylist;

                    txtPlaylistName.Text = "(Neue Playlist)";
                    Text = txtPlaylistName.Text;
                }

                List<string> videoUrls = TrackFactory.GetVideoUrls(videoUrl);
                currentPlaylist.VideoUrls.AddRange(videoUrls);

                LoadTracks(videoUrls);

                txtUrl.Text = "";

                if (lstVCountBefore != lstVTracks.Items.Count && !currentPlaylistChanged)
                {
                    currentPlaylistChanged = true;
                    txtPlaylistName.Text += "*";
                    Text += "*";
                    
                    var shortFilename = Path.GetFileNameWithoutExtension(currentPlaylist.Filename);

                    if (shortFilename != null)
                    {
                        var indexCurrentPlaylist = lstBoxPlaylists.Items.IndexOf(shortFilename);

                        lstBoxPlaylists.Items[indexCurrentPlaylist] += "*";
                    }
                }

                UpdateButtons();
            }
            else
            {
                MessageBox.Show(
                    "Bitte geben Sie einen youtube-Videolink an!", "Fehlerhafte Eingabe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            txtUrl.SelectAll();
            txtUrl.Focus();
        }
 
        private void cmdCancelRenaming_Click(object sender, EventArgs e)
        {
            if (!txtPlaylistName.ReadOnly)
            {
                txtPlaylistName.Text = currentPlaylist.Name;

                if(currentPlaylistChanged)
                {
                    txtPlaylistName.Text += "*";
                }
                txtPlaylistName.ReadOnly = true;
                cmdAcceptRenaming.Visible = false;
                cmdCancelRenaming.Visible = false;
            }
        }   

        private void cmdMute_Click(object sender, EventArgs e)
        {
            muteButtonClicked = true;
            mute = !mute;

            if (mute)
            {
                cmdMute.Image = muteOnImage;
                volSlider.Volume = 0;
            }
            else
            {
                cmdMute.Image = muteOffImage;
                volSlider.Volume = volume;
            }

            muteButtonClicked = false;
        }   
        private void cmdNextTrack_Click(object sender, EventArgs e)
        {
            if(selectedTrack != null && currentTrack != null)
            {
                selectedTrack = null;
                lstVTracks.SelectedItems[0].Selected = false;
            }

            tmrPlayTrack.Stop();
            trkBDuration.Enabled = false;

            ChangeTrack(AudioPlayer.GetNextTrack(true, settings, videoUrlAutoplayTrack));

            if (!playbackStopped)
            {
                PlayTrack(currentTrack);
            }

            if (settings.Autoplay != Autoplay.Off)
            {
                ShowAutoplayPreview();
            }
        }   
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            if (currentTrack != null)
            {
                if (waveOut != null)
                {
                    TogglePause();
                }
                else
                {
                    UpdateTrackbar();
                    PlayTrack(currentTrack);
                }
            }
            else
            {
                if (selectedTrack != null)
                {
                    currentPlaylist.CurrentTrack = selectedTrack;
                    currentTrack = currentPlaylist.CurrentTrack;
                    lstVTracks.SelectedItems[0].Selected = false;
                    selectedTrack = null;
                }
                else
                {
                    currentPlaylist.CurrentTrack = GetPlaylistsFirstTrack();
                    currentTrack = currentPlaylist.CurrentTrack;
                }

                ListViewItem trackItem = lstVTracks.Items[currentTrack.IndexSortedList];

                trackItem.ForeColor = Color.DarkBlue;
                trackItem.Font = new Font(trackItem.Font, FontStyle.Bold);

                UpdateTrackbar();
                PlayTrack(currentTrack);
                cmdPlay.Image = pauseImage;
            }

            if (settings.Autoplay != Autoplay.Off)
            {
                ShowAutoplayPreview();
            }

            playbackStopped = false;

            UpdateButtons();
        }   
        private void cmdPreviousTrack_Click(object sender, EventArgs e)
        {
            if (selectedTrack != null && currentTrack != null)
            {
                selectedTrack = null;
                lstVTracks.SelectedItems[0].Selected = false;
            }

            tmrPlayTrack.Stop();
            trkBDuration.Enabled = false;

            ChangeTrack(AudioPlayer.GetPreviousTrack(settings, selectedTrack));

            if (!playbackStopped)
            {
                PlayTrack(currentTrack);
            }

            if (settings.Autoplay != Autoplay.Off)
            {
                ShowAutoplayPreview();
            }
        }   
        private void cmdRenamePlaylist_Click(object sender, EventArgs e)
        {
            txtPlaylistName.Text = txtPlaylistName.Text.TrimEnd('*');

            cmdAcceptRenaming.Visible = true;
            cmdCancelRenaming.Visible = true;
            txtPlaylistName.ReadOnly = false;
            txtPlaylistName.Focus();
        }   
        private void cmdStop_Click(object sender, EventArgs e)
        {
            if(settings.Autoplay != Autoplay.Off)
            {
                lblNextSong.Visible = false;

                if(currentTrack != null && currentTrack.IsAutoplayTrack)
                {
                    int lastIndex = lstVTracks.Items.Count - 1;

                    if (settings.Autoplay == Autoplay.Play)
                    {
                        RemoveAutoplayTrack(lastIndex);
                    }
                    else
                    {
                        CarryAutoplayTrackIntoPlaylist(lastIndex);

                        ListViewItem autoplayTrack = lstVTracks.Items[lastIndex];

                        autoplayTrack.ForeColor = Color.DarkBlue;
                        autoplayTrack.Font = new Font(autoplayTrack.Font, FontStyle.Bold);
                    }
                }
            }

            StopPlayback();
        }   

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentPlaylist != null)
            {
                if (currentPlaylistChanged)
                {
                    DialogResult dialogResult = MessageBox.Show(
                    "Wollen Sie die Playlist speichern?", "Playlist speichern",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dialogResult == DialogResult.Yes)
                    {
                        tlsCmdSaveList_Click(sender, e);

                        if (savingAborted)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                settings.FilepathLastUsedPlaylist = Path.Combine(PlaylistManager.directory, currentPlaylist.Filename);
            }           

            settings.Volume = volume;

            Settings.SerializeSettings(settings);
        }   
        private void frmMain_Load(object sender, EventArgs e)
        {
            bool firstRun = false;

            string ytrDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "YoutubeRadio");

            if (!Directory.Exists(ytrDirectory))
            {
                firstRun = true;

                Directory.CreateDirectory(Path.Combine(ytrDirectory, "Playlists"));
                Directory.CreateDirectory(Path.Combine(ytrDirectory, "Settings"));
            }
            
            volume = volSlider.Volume;

            muteOffImage = imgLMute.Images[0];
            muteOnImage = imgLMute.Images[1];
            playImage = imgLPlayPause.Images[0];
            pauseImage = imgLPlayPause.Images[1];

            cmdMute.Image = muteOffImage;
            cmdPlay.Image = playImage;

            settings = Settings.DeserializeSettings(firstRun);
            CheckSettings(settings);

            PlaylistManager.SavedPlaylists = new List<Playlist>();
            GetPlaylists();
            GetCurrentPlaylist(settings.FilepathLastUsedPlaylist);

            if(currentPlaylist != null)
            {
                Text = "YoutubeRadio - " + currentPlaylist.Name;
                txtPlaylistName.Text = currentPlaylist.Name;
            }

            UpdateButtons();
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                ntfIcon.Visible = true; ;
                ntfIcon.ShowBalloonTip(500);

                Hide();
            }
            else if (FormWindowState.Normal == WindowState)
            {
                ntfIcon.Visible = false;
            }
        }

        private void lstBoxPlaylists_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstBoxPlaylists.SelectedIndex != -1)
            {
                if (currentPlaylistChanged)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "Wollen Sie die aktuelle Playlist speichern?", "Playlist speichern",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dialogResult == DialogResult.Yes)
                    {
                        tlsCmdSaveList_Click(sender, e);

                        if (savingAborted)
                        {
                            return;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        var shortFilename = Path.GetFileNameWithoutExtension(currentPlaylist.Filename);

                        if (shortFilename != null)
                        {
                            var indexCurrentPlaylist = lstBoxPlaylists.Items.IndexOf(shortFilename + "*");

                            lstBoxPlaylists.Items[indexCurrentPlaylist] = shortFilename;
                        }

                        currentPlaylistChanged = false;
                    }
                    else
                    {
                        lstBoxPlaylists.SelectedIndex = -1;
                        return;
                    }
                }

                if (currentPlaylist != null)
                {
                    StopPlayback();
                    ClearListViewTracks();
                }

                string playlistName = lstBoxPlaylists.Items[lstBoxPlaylists.SelectedIndex].ToString();
                string filepath = Path.Combine(PlaylistManager.directory, playlistName + ".xml");

                GetCurrentPlaylist(filepath);

                Text = "YoutubeRadio - " + playlistName;
                txtPlaylistName.Text = playlistName;

                lstBoxPlaylists.SelectedIndex = -1;

                UpdateButtons();
            }
        }   
        private void lstBoxPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            tlsCmdRemovePlaylist.Enabled = CanRemovePlaylist();
        }   

        private void lstVTracks_KeyUp(object sender, KeyEventArgs e)
        {
            if (selectedTrack != null && e.KeyCode == Keys.Delete)
            {
                tlsCmdRemoveTrack_Click(sender, e);
            }
        }   
        private void lstVTracks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (currentPlaylist != null)
            {
                currentPlaylist.ShuffledPlaylist.Clear();
                currentPlaylist.UnplayedTracks.Clear();
            }

            playbackStopped = false;

            if (selectedTrack != null)
            {
                if (waveOut != null)
                {
                    waveOut.Stop();
                }

                if (currentTrack != null && !currentTrack.Equals(selectedTrack))
                {
                    if (currentTrack.IsAutoplayTrack)
                    {
                        if (settings.Autoplay == Autoplay.Play)
                        {
                            RemoveAutoplayTrack(currentTrack.IndexSortedList);
                        }
                        else
                        {
                            CarryAutoplayTrackIntoPlaylist(currentTrack.IndexSortedList);
                        }
                    }
                }

                ChangeTrack(selectedTrack);
                PlayTrack(currentTrack);

                if (settings.Autoplay != Autoplay.Off)
                {
                    ShowAutoplayPreview();
                }

                UpdateButtons();
            }
        }
        private void lstVTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVTracks.SelectedItems.Count == 0)
            {
                selectedTrack = null;
            }
            else
            {
                selectedTrack = currentPlaylist.SortedPlaylist[lstVTracks.SelectedItems[0].Index];
            }

            UpdateButtons();
        }

        private void ntfIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();

            WindowState = FormWindowState.Normal;
        }

        private void optAutoplay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton senderButton = sender as RadioButton;

            if (senderButton.Checked == true)
            {
                return;
            }

            if (!optAutoplay_Off.Checked)
            {
                if (optAutoplay_Play.Checked)
                {
                    settings.Autoplay = Autoplay.Play;
                }
                else
                {
                    settings.Autoplay = Autoplay.Load;                    
                }

                optRepeatOff.Checked = true;
                optShuffle_Off.Checked = true;


                if (waveOut != null && string.IsNullOrEmpty(videoUrlAutoplayTrack))
                {
                    ShowAutoplayPreview();
                }
            }
            else
            {
                settings.Autoplay = Autoplay.Off;
                videoUrlAutoplayTrack = null;
                lblNextSong.Visible = false;
            }

            UpdateButtons();
        }   
        private void optRepeat_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton senderButton = sender as RadioButton;

            if(senderButton.Checked == true)
            {
                return;
            }

            if(optRepeatOff.Checked)
            {
                settings.Repeat = Repeat.RepeatOff;
            }
            else
            {
                if (optRepeatAll.Checked)
                {
                    if (currentTrack != null && currentTrack.IsAutoplayTrack)
                    {
                        if (settings.Autoplay == Autoplay.Load ||
                            MessageBox.Show("Wollen Sie den aktuellen autoplayTrack in die Liste übernehmen?", "Track übernehmen",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            CarryAutoplayTrackIntoPlaylist(lstVTracks.Items.Count - 1);

                            ListViewItem autoplayTrack = lstVTracks.Items[lstVTracks.Items.Count - 1];

                            autoplayTrack.ForeColor = Color.DarkBlue;
                            autoplayTrack.Font = new Font(autoplayTrack.Font, FontStyle.Bold);
                        }
                        else
                        {
                            RemoveAutoplayTrack(lstVTracks.Items.Count - 1);

                            if (waveOut != null)
                            {
                                waveOut.Stop();
                            }
                            
                            tmrPlayTrack.Stop();
                            cmdPlay_Click(sender, e);
                        }                        
                    }

                    settings.Repeat = Repeat.RepeatAll; 
                }
                else
                {
                    if(currentTrack != null && currentTrack.IsAutoplayTrack)
                    {
                        CarryAutoplayTrackIntoPlaylist(lstVTracks.Items.Count - 1);

                        ListViewItem autoplayTrack = lstVTracks.Items[lstVTracks.Items.Count - 1];

                        autoplayTrack.ForeColor = Color.DarkBlue;
                        autoplayTrack.Font = new Font(autoplayTrack.Font, FontStyle.Bold);
                    }                    

                    optShuffle_Off.Checked = true;
                    settings.Repeat = Repeat.RepeatOne;
                }

                optAutoplay_Off.Checked = true;
            }
        }   
        private void optShuffle_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton senderButton = sender as RadioButton;

            if (senderButton.Checked == true)
            {
                return;
            }

            if (optShuffle_On.Checked)
            {
                if (currentTrack != null && currentTrack.IsAutoplayTrack)
                {
                    if (settings.Autoplay == Autoplay.Load ||
                        MessageBox.Show("Wollen Sie den aktuellen autoplayTrack in die Liste übernehmen?", "Track übernehmen",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CarryAutoplayTrackIntoPlaylist(lstVTracks.Items.Count - 1);

                        ListViewItem autoplayTrack = lstVTracks.Items[lstVTracks.Items.Count - 1];

                        autoplayTrack.ForeColor = Color.DarkBlue;
                        autoplayTrack.Font = new Font(autoplayTrack.Font, FontStyle.Bold);
                    }
                    else
                    {
                        RemoveAutoplayTrack(lstVTracks.Items.Count - 1);

                        if (waveOut != null)
                        {
                            waveOut.Stop();
                        }

                        tmrPlayTrack.Stop();

                        cmdPlay_Click(sender, e);
                    }
                }

                settings.Shuffle = true;
                optAutoplay_Off.Checked = true;

                if (optRepeatOne.Checked)
                {
                    optRepeatOff.Checked = true;
                }
            }
            else
            {
                settings.Shuffle = false;

                if(currentPlaylist != null)
                {
                    currentPlaylist.UnplayedTracks.Clear();
                    currentPlaylist.ShuffledPlaylist.Clear();

                    foreach (AudioTrack track in currentPlaylist.SortedPlaylist)
                    {
                        track.IndexShuffledList = -1;
                    }
                }                
            }
        }
    
        private void tlsCmdClearPlaylist_Click(object sender, EventArgs e)
        {            
            DialogResult dialogResult;

            dialogResult = MessageBox.Show(
                    "Wollen Sie die Playlist wirklich leeren?", "Playlist leeren",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {
                StopPlayback();
                ClearListViewTracks();
                currentPlaylist.ClearAudioTracks();
                currentPlaylist.VideoUrls.Clear();

                if(!currentPlaylistChanged)
                {
                    currentPlaylistChanged = true;
                    txtPlaylistName.Text += "*";
                    Text += "*";

                    int indexListName = lstBoxPlaylists.Items.IndexOf(currentPlaylist.Name);

                    if (indexListName != -1)
                    {
                        lstBoxPlaylists.Items[indexListName] += "*";
                    }
                }
            }

            UpdateButtons();
        }   
        private void tlsCmdNewPlaylist_Click(object sender, EventArgs e)
        {
            if(currentPlaylistChanged)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Wollen Sie die aktuelle Playlist speichern?", "Playlist speichern",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if(dialogResult == DialogResult.Yes)
                {
                    tlsCmdSaveList_Click(sender, e);

                    if(savingAborted)
                    {
                        return;
                    }
                }
                else if(dialogResult == DialogResult.No)
                {
                    var shortFilename = Path.GetFileNameWithoutExtension(currentPlaylist.Filename);

                    if (shortFilename != null)
                    {
                        var indexCurrentPlaylist = lstBoxPlaylists.Items.IndexOf(shortFilename + "*");

                        lstBoxPlaylists.Items[indexCurrentPlaylist] = shortFilename;
                    }

                    currentPlaylistChanged = false;
                }
                else
                {
                    return;
                }
            }

            if (!playbackStopped)
            {
                StopPlayback();
                ClearListViewTracks();
            }

            frmNewPlaylist formNewPlaylist = new frmNewPlaylist(this);

            formNewPlaylist.ShowDialog();
        }
        private void tlsCmdRemovePlaylist_Click(object sender, EventArgs e)
        {
            Playlist playlistToRemove = PlaylistManager.SavedPlaylists[lstBoxPlaylists.SelectedIndex];           

            DialogResult dialogResult = MessageBox.Show(
                    "Wollen Sie die Playlist \"" + Path.GetFileNameWithoutExtension(playlistToRemove.Filename) + "\" wirklich dauerhaft entfernen?",
                    "Playlist löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {
                if((playlistToRemove).Equals(currentPlaylist))
                {
                    StopPlayback();                                       
                    ClearListViewTracks();                    

                    Text = "YoutubeRadio - (Keine Playlist)";
                    txtPlaylistName.Text = "(Keine Playlist)";

                    settings.FilepathLastUsedPlaylist = null;
                    currentPlaylistChanged = false;
                    PlaylistManager.CurrentPlaylist = null;
                    currentPlaylist = null;
                }

                PlaylistManager.RemovePlaylist(playlistToRemove);

                lstBoxPlaylists.Items.RemoveAt(lstBoxPlaylists.SelectedIndex);
            }

            UpdateButtons();

            txtUrl.Focus();
        }   
        private void tlsCmdRemoveTrack_Click(object sender, EventArgs e)
        {
            currentPlaylist.RemoveAudioTrack(selectedTrack, settings.Shuffle);

            if(currentTrack != null && selectedTrack.Equals(currentTrack))
            {
                StopPlayback();

                if (settings.Autoplay != Autoplay.Off)
                {
                    lblNextSong.Visible = false;
                }

                currentPlaylist.CurrentTrack = null;
                currentTrack = null;
            }

            lstVTracks.Items.RemoveAt(selectedTrack.IndexSortedList);

            if (!currentPlaylistChanged)
            {
                currentPlaylistChanged = true;
                txtPlaylistName.Text += "*";
                Text += "*";

                var shortFilename = Path.GetFileNameWithoutExtension(currentPlaylist.Filename);

                if (shortFilename != null)
                {
                    var indexCurrentPlaylist = lstBoxPlaylists.Items.IndexOf(shortFilename);

                    lstBoxPlaylists.Items[indexCurrentPlaylist] += "*";
                }
            }

            UpdateButtons();

            txtUrl.Focus();
        }  
        private void tlsCmdSaveList_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(currentPlaylist.Name))
            {
                frmNamePlaylist formNamePlaylist = new frmNamePlaylist(this);

                formNamePlaylist.ShowDialog();

                if (!PlaylistManager.VerifyPlaylistName(currentPlaylist.Name))
                {
                    savingAborted = true;
                    return;
                }

                savingAborted = false;
            }

            UpdatePlaylist();

            PlaylistManager.SerializePlaylist();

            //MessageBox.Show(
            //    "Die Playlist wurde erfolgreich gespeichert!", "Playlist gespeichert!",
            //    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            currentPlaylistChanged = false;
            txtPlaylistName.Text = currentPlaylist.Name;
            Text = "YoutubeRadio - " + currentPlaylist.Name;

            UpdateButtons();
            txtUrl.Focus();
        }  
        private void tlsCmdYoutubeView_Click(object sender, EventArgs e)
        {
            if (selectedTrack != null && currentTrack == null)
            {
                Process.Start(selectedTrack.VideoUrl);
            }
            else if (currentTrack != null)
            {
                if (waveOut != null)
                {
                    TogglePause();
                }

                TimeSpan trackPosition = TimeSpan.FromSeconds(trkBDuration.Value);
                string adjustedVideoUrl = currentTrack.VideoUrl + "#t=" + trackPosition.Minutes + "m" + trackPosition.Seconds + "s";
                Process.Start(adjustedVideoUrl);
            }
        }   

        private void tmrPlayTrack_Tick(object sender, EventArgs e)
        {
            if (waveOut.PlaybackState == PlaybackState.Stopped)
            {
                tmrPlayTrack.Stop();

                ChangeTrack(AudioPlayer.GetNextTrack(false, settings, videoUrlAutoplayTrack));

                if (!playbackStopped)
                {
                    PlayTrack(currentTrack);

                    if (settings.Autoplay != Autoplay.Off)
                    {
                        ShowAutoplayPreview();
                    }
                }
            }
            else if(!trackBarSeeking)
            {
                TimeSpan trackPosition = mediaReader.CurrentTime;
                var trackPositionSeconds = (int)trackPosition.TotalSeconds;
                string posString = trackPosition.ToString(@"hh\:mm\:ss");

                if (trkBDuration.Maximum >= trackPositionSeconds)
                {
                    trkBDuration.Value = trackPositionSeconds;
                }

                lblTrackPos.Text = posString;
            }
        }

        private void trkBDuration_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentTrack != null)
            {
                trackBarSeeking = true;

                GetMousePositionTrackbar(e.X);
            }
        }   
        private void trkBDuration_MouseMove(object sender, MouseEventArgs e)
        {
            if (trackBarSeeking)
            {
                GetMousePositionTrackbar(e.X);
            }
        } 
        private void trkBDuration_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentTrack != null)
            {
                mediaReader.CurrentTime = TimeSpan.FromSeconds(trkBDuration.Value);

                trackBarSeeking = false;
            }
        }  
        private void trkBDuration_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan position = TimeSpan.FromSeconds(trkBDuration.Value);

            lblTrackPos.Text = position.ToString("T");
        }

        private void txtPlaylistName_Enter(object sender, EventArgs e)
        {
            AcceptButton = cmdAcceptRenaming;
        }
        private void txtPlaylistName_Leave(object sender, EventArgs e)
        {
            if (ActiveControl != cmdAcceptRenaming)
            {
                cmdCancelRenaming_Click(sender, e);
            }

            AcceptButton = null;
        }
        private void txtUrl_Enter(object sender, EventArgs e)
        {
            AcceptButton = cmdAddTracks;
        }
        private void txtUrl_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void volSlider_VolumeChanged(object sender, EventArgs e)
        {
            if (!mute)
            {
                volume = volSlider.Volume;
                settings.Volume = volume;

                if (waveOut != null)
                {
                    waveOut.Volume = volume;
                }
            }
            else
            {
                if (muteButtonClicked && waveOut != null)
                {
                    waveOut.Volume = 0;
                }
                else if (!muteButtonClicked)
                {
                    mute = false;
                    volume = volSlider.Volume;
                    cmdMute.Image = muteOffImage;

                    if (waveOut != null)
                    {
                        waveOut.Volume = volume;
                    }
                }
            }
        }
        
        
           
        public void CreateNewPlaylist(string playlistName)
        {
            PlaylistManager.CurrentPlaylist = new Playlist(playlistName);
            currentPlaylist = PlaylistManager.CurrentPlaylist;
            currentPlaylistChanged = true;

            Text = "YoutubeRadio - " + playlistName + "*";
            txtPlaylistName.Text = playlistName + "*";
            
            UpdateButtons();
        }
        public void ApplyPlaylistName(string playlistName)
        {
            currentPlaylist.Name = playlistName;

            txtPlaylistName.Text = playlistName;            
        }  
        public void StopPlayback()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }

            cmdPlay.Image = playImage;
            tmrPlayTrack.Stop();
            trkBDuration.Maximum = 0;
            lblTrackDuration.Text = "00:00:00";
            lblTrackPos.Text = "00:00:00";
            AudioPlayer.WaveOut = null;
            AudioPlayer.MediaReader = null;
            waveOut = null;
            mediaReader = null;
            playbackStopped = true;

            UpdateButtons();
        }

        private void AddNewAutoplayTrack(AudioTrack pickedTrack)
        {
            currentPlaylist.AddAudioTrack(pickedTrack);
            lstVTracks.Items.Add(CreateListViewItem(pickedTrack));            
        }  

        private void CarryAutoplayTrackIntoPlaylist(int lastIndex)
        {
            string title =  currentTrack.Title;
            TimeSpan duration = TimeSpan.FromTicks(currentTrack.Duration);
            string durationString = duration.ToString("T");
            string[] subItems = { title, durationString };

            lstVTracks.Items.RemoveAt(lastIndex);
            lstVTracks.Items.Add(new ListViewItem(subItems));

            currentTrack.IsAutoplayTrack = false;

            if(!currentPlaylistChanged)
            {
                currentPlaylistChanged = true;
                txtPlaylistName.Text += "*";
                Text += "*";
            }

            UpdateButtons();
        }   
        private void ChangeTrack(AudioTrack pickedTrack)
        {
            if (currentTrack != null && currentTrack.Equals(pickedTrack))
            {
                UpdateTrackbar();
                return;
            }

            if (currentTrack != null && currentTrack.IsAutoplayTrack)
            {
                if (settings.Autoplay == Autoplay.Load)
                {
                    CarryAutoplayTrackIntoPlaylist(lstVTracks.Items.Count - 1);
                }
                else if (settings.Autoplay == Autoplay.Play || settings.Autoplay == Autoplay.Off)
                {
                    RemoveAutoplayTrack(currentTrack.IndexSortedList);

                    if (pickedTrack.IsAutoplayTrack)
                    {
                        pickedTrack.IndexSortedList--;
                    }
                }
            }

            if (pickedTrack != null)
            {
                if (pickedTrack.IsAutoplayTrack &&
                    (currentTrack != null && !currentTrack.Equals(pickedTrack) || currentTrack == null))
                {
                    AddNewAutoplayTrack(pickedTrack);
                }

                SwitchTrackHighlighting(pickedTrack);

                ListViewItem itemAfterChange = lstVTracks.Items[pickedTrack.IndexSortedList];

                lstVTracks.Items[itemAfterChange.Index].EnsureVisible();
                currentPlaylist.CurrentTrack = pickedTrack;
                currentTrack = currentPlaylist.CurrentTrack;
                UpdateTrackbar();
            }
            else
            {
                StopPlayback();

                ListViewItem lastPlayedTrack = lstVTracks.Items[currentTrack.IndexSortedList];
                
                lastPlayedTrack.ForeColor = Color.FromName("WindowText");
                lastPlayedTrack.Font = new Font(lastPlayedTrack.Font, FontStyle.Regular);

                currentPlaylist.CurrentTrack = GetPlaylistsFirstTrack();
                currentTrack = currentPlaylist.CurrentTrack;

                ListViewItem firstTrack = lstVTracks.Items[currentTrack.IndexSortedList];

                firstTrack.ForeColor = Color.DarkBlue;
                firstTrack.Font = new Font(firstTrack.Font, FontStyle.Bold);
            }
        }  
        private void CheckSettings(Settings settings)
        {
            if (settings.Autoplay != Autoplay.Off)
            {
                if (settings.Autoplay == Autoplay.Play)
                {
                    optAutoplay_Play.Checked = true;
                }
                else
                {
                    optAutoplay_Load.Checked = true;
                }
            }
            else
            {
                optAutoplay_Off.Checked = true;

                if (settings.Repeat == Repeat.RepeatAll)
                {
                    optRepeatAll.Checked = true;
                }
                else if (settings.Repeat == Repeat.RepeatOne)
                {
                    optRepeatOne.Checked = true;
                }

                if (settings.Shuffle)
                {
                    optShuffle_On.Checked = true;
                }
            }

            if (!mute)
            {
                volSlider.Volume = settings.Volume;
            }
        }   
        private void ClearListViewTracks()
        {
            lstVTracks.Items.Clear();

            selectedTrack = null;
            currentPlaylist.CurrentTrack = null;
            currentTrack = null;

            lblTrackDuration.Text = "00:00:00";
            lblTrackPos.Text = "00:00:00";
            trkBDuration.Enabled = false;
            trkBDuration.Maximum = 0;
            trkBDuration.Value = 0;

            UpdateButtons();
        }   

        private void GetPlaylists()
        {
            string[] playlists = Directory.GetFiles(PlaylistManager.directory);

            foreach(string playlistName in playlists)
            {
                string filepath = Path.Combine(PlaylistManager.directory, playlistName);
                string shortFilename = Path.GetFileNameWithoutExtension(playlistName);

                Playlist newPlaylist = new Playlist(shortFilename);
                newPlaylist.VideoUrls = PlaylistManager.DeserializeVideoUrls(filepath);

                PlaylistManager.AddNewPlaylist(newPlaylist);
                lstBoxPlaylists.Items.Add(shortFilename);
            }
        }

        private void GetMousePositionTrackbar(int mouseX)
        {
            double value = ((double)mouseX / trkBDuration.Width) * trkBDuration.Maximum;
            value = Math.Round(value);

            if (value < 0)
            {
                value = 0;
            }
            else if (value > trkBDuration.Maximum)
            {
                value = trkBDuration.Maximum;
            }

            trkBDuration.Value = (int)value;
        }  
        private void GetCurrentPlaylist(string filepath)
        {
            string filename = Path.GetFileName(filepath);

            if (!string.IsNullOrEmpty(filepath) && File.Exists(filepath))
            {
                PlaylistManager.CurrentPlaylist = PlaylistManager.SavedPlaylists.Find(x => x.Filename == filename);
                currentPlaylist = PlaylistManager.CurrentPlaylist;

                LoadTracks(currentPlaylist.VideoUrls);
            }
            else if(!string.IsNullOrEmpty(filepath) && !File.Exists(filepath))
            {
                string shortFilename = Path.GetFileNameWithoutExtension(filepath);

                DialogResult dialogResult = MessageBox.Show(
                    "Die Playlist \"" + shortFilename + "\" wurde nicht gefunden!\n" +
                    "Möchten Sie die Playlist aus der Liste entfernen?", "Playlist nicht gefunden",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                if(dialogResult == DialogResult.Yes)
                {
                    Playlist playlistToRemove = PlaylistManager.SavedPlaylists.Find(x => x.Filename == filename);

                    PlaylistManager.RemovePlaylist(playlistToRemove);

                    int indexPlaylistToRemove = lstBoxPlaylists.Items.IndexOf(shortFilename);

                    lstBoxPlaylists.Items.RemoveAt(playlistToRemove.Index);

                    Text = "YoutubeRadio - (Keine Playlist)";
                    txtPlaylistName.Text = "(Keine Playlist)";

                    settings.FilepathLastUsedPlaylist = null;
                    currentPlaylistChanged = false;
                    PlaylistManager.CurrentPlaylist = null;
                    currentPlaylist = null;
                }
            }

            UpdateButtons();
        }        

        private void LoadTracks(List<string> videoUrls)
        {
            List<ListViewItem> itemsToAdd = new List<ListViewItem>();

            prgLoadTracks.Visible = true;
            prgLoadTracks.Maximum = videoUrls.Count;
            Enabled = false;

            foreach (string videoUrl in videoUrls)
            {
                ListViewItem itemToAdd = CreateAudioTrack(videoUrl, lstVTracks.Items.Count);

                if(itemToAdd != null)
                {
                    itemsToAdd.Add(itemToAdd);
                }
            }

            if (videoUrls.Count != itemsToAdd.Count)
            {
                MessageBox.Show("Einige Videos konnten nicht geladen werden!", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            prgLoadTracks.Visible = false;
            prgLoadTracks.Maximum = 0;
            Enabled = true;

            lstVTracks.Items.AddRange(itemsToAdd.ToArray());
        }
        private void PlayTrack(AudioTrack trackToPlay)
        {
            try
            {
                bool audioUrlUnaccessible;

                AudioPlayer.PlayTrack(trackToPlay, mute, volume, out audioUrlUnaccessible);

                mediaReader = AudioPlayer.MediaReader;
                waveOut = AudioPlayer.WaveOut;

                if (audioUrlUnaccessible)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "Die Tonspur dieses Videos ist zurzeit nicht erreichbar. Wollen Sie das Video entfernen?", "Tonspur nicht erreichbar",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dialogResult == DialogResult.Yes)
                    {
                        currentPlaylist.RemoveAudioTrack(currentTrack, settings.Shuffle);
                    }

                    ChangeTrack(AudioPlayer.GetNextTrack(false, settings, videoUrlAutoplayTrack));
                }
                else
                {
                    tmrPlayTrack.Start();
                    cmdPlay.Image = pauseImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Beim Abspielen des Songs ist ein unerwarteter Fehler aufgetreten!\n\n" + ex.Message, "Unerwarteter Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                if (lstVTracks.Items.Count > 1)
                {
                    ChangeTrack(AudioPlayer.GetNextTrack(false, settings, videoUrlAutoplayTrack));
                }
                else
                {
                    StopPlayback();
                }
            }

            UpdateButtons();
        }

        private void RemoveAutoplayTrack(int trackIndex)
        {
            currentPlaylist.RemoveAudioTrack(currentTrack, settings.Shuffle);

            lstVTracks.Items.RemoveAt(trackIndex);

            UpdateButtons();

            currentPlaylist.CurrentTrack = null;
            currentTrack = null;
        }

        private void ShowAutoplayPreview()
        {
            videoUrlAutoplayTrack = TrackFactory.GetVideoUrlAutoplayTrack(currentTrack);

            while (videoUrlAutoplayTrack == null)
            {
                videoUrlAutoplayTrack = TrackFactory.GetVideoUrlAutoplayTrack(currentTrack);
            }

            var doc = new HtmlWeb().Load(videoUrlAutoplayTrack);
            string title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
            string endIndicatorTitle = " - YouTube";

            while (title == "YouTube")
            {
                doc = new HtmlWeb().Load(videoUrlAutoplayTrack);
                title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
            }

            title = title.Substring(0, title.Length - endIndicatorTitle.Length);
            title = HttpUtility.HtmlDecode(title);

            lblNextSong.Text = "Nächstes Lied: " + title;

            lblNextSong.Visible = true;
        }
        private void SwitchTrackHighlighting(AudioTrack trackAfterChange)
        {
            if (currentTrack != null && !currentTrack.Equals(trackAfterChange))
            {
                ListViewItem itemBeforeChange = lstVTracks.Items[currentTrack.IndexSortedList];

                itemBeforeChange.ForeColor = Color.FromName("WindowText");
                itemBeforeChange.Font = new Font(itemBeforeChange.Font, FontStyle.Regular);
            }

            ListViewItem itemAfterChange = lstVTracks.Items[trackAfterChange.IndexSortedList];

            if (trackAfterChange.IsAutoplayTrack)
            {
                itemAfterChange.ForeColor = Color.DarkGreen;
                itemAfterChange.Font = new Font(itemAfterChange.Font, FontStyle.Bold);
            }
            else
            {
                if (playbackStopped)
                {
                    itemAfterChange.Selected = true;
                }
                else
                {
                    itemAfterChange.ForeColor = Color.DarkBlue;
                    itemAfterChange.Font = new Font(itemAfterChange.Font, FontStyle.Bold);
                }
            }
        }

        private void TogglePause()
        {
            PlaybackState playbackState = waveOut.PlaybackState;

            if (playbackState == PlaybackState.Playing)
            {
                cmdPlay.Image = playImage;
                waveOut.Pause();
                tmrPlayTrack.Stop();
            }
            else if (playbackState == PlaybackState.Paused)
            {
                cmdPlay.Image = pauseImage;
                waveOut.Play();
                tmrPlayTrack.Start();
            }
        }

        private void UpdateButtons()
        {
            tlsCmdYoutubeView.Enabled = YoutubeViewAvailable();
            tlsCmdSaveList.Enabled = CanSaveList();
            tlsCmdRemoveTrack.Enabled = CanRemoveTrack();
            tlsCmdClearPlaylist.Enabled = CanClearPlaylist();
            tlsCmdRemovePlaylist.Enabled = CanRemovePlaylist();
            cmdRenamePlaylist.Enabled = CanRenamePlaylist();
            cmdPreviousTrack.Enabled = PreviousTrackAvailable();
            cmdPlay.Enabled = CanPlayTrack();
            cmdStop.Enabled = CanStopPlayback();
            cmdNextTrack.Enabled = NextTrackAvailable();
        }
        private void UpdatePlaylist()
        {
            if(currentPlaylist.Index == -1)
            {
                PlaylistManager.AddNewPlaylist(currentPlaylist);
                lstBoxPlaylists.Items.Add(currentPlaylist.Name);
            }
            else
            {
                PlaylistManager.UpdateSavedPlaylist(currentPlaylist);
                lstBoxPlaylists.Items[currentPlaylist.Index] = currentPlaylist.Name;
            }

            string filepath = Path.Combine(PlaylistManager.directory, currentPlaylist.Filename);

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
        private void UpdateTrackbar()
        {
            TimeSpan duration = TimeSpan.FromTicks(currentTrack.Duration);
            string durationString = duration.ToString("T");

            lblTrackDuration.Text = durationString;
            lblTrackPos.Text = "00:00:00";
            trkBDuration.Enabled = true;
            trkBDuration.Maximum = Convert.ToInt32(duration.TotalSeconds);
            trkBDuration.Value = trkBDuration.Minimum;
        }



        private bool CanClearPlaylist()
        {
            return currentPlaylist != null && currentPlaylist.SortedPlaylist.Count != 0;
        }
        private bool CanPlayTrack()
        {
            return currentPlaylist != null && currentPlaylist.SortedPlaylist.Count > 0;
        }
        private bool CanRemovePlaylist()
        {
            return lstBoxPlaylists.SelectedIndex != -1;
        }
        private bool CanRemoveTrack()
        {
            return (selectedTrack != null);
        }
        private bool CanRenamePlaylist()
        {
            return currentPlaylist != null;
        }
        private bool CanSaveList()
        {
            return currentPlaylistChanged;
        }
        private bool CanStopPlayback()
        {
            return !playbackStopped;
        }
        private ListViewItem CreateAudioTrack(string videoUrl, int trackIndex, bool isAutoplayTrack = false)
        {
            AudioTrack track = TrackFactory.CreateAudioTrack(videoUrl, trackIndex, isAutoplayTrack);
            ListViewItem itemToAdd = null;

            if (track != null)
            {
                currentPlaylist.AddAudioTrack(track);

                if (settings.Shuffle)
                {
                    currentPlaylist.UnplayedTracks.Add(track);
                }

                itemToAdd = CreateListViewItem(track);

                prgLoadTracks.Value++;
            }

            return itemToAdd;
        }
        private ListViewItem CreateListViewItem(AudioTrack createdTrack)
        {
            TimeSpan duration = TimeSpan.FromTicks(createdTrack.Duration);
            string durationString = duration.ToString("T");
            string title = createdTrack.Title;

            if (createdTrack.IsAutoplayTrack)
            {
                title = "AUTOPLAY: " + title;
            }

            return new ListViewItem(new[] { title, durationString });          
        }

        private AudioTrack GetPlaylistsFirstTrack()
        {
            AudioTrack trackToPlay;

            if (settings.Shuffle)
            {
                if(currentPlaylist.ShuffledPlaylist.Count == 0)
                {
                    if (currentPlaylist.UnplayedTracks.Count == 0)
                    {
                        currentPlaylist.UnplayedTracks.AddRange(currentPlaylist.SortedPlaylist);
                    }

                    AudioPlayer.GetRandomTrack(out trackToPlay);
                }
                else
                {
                    trackToPlay = currentPlaylist.ShuffledPlaylist[0];
                }
            }
            else
            {
                trackToPlay = currentPlaylist.SortedPlaylist[0];
            }

            return trackToPlay;
        }
                
        private bool NextTrackAvailable()
        {
            if (currentPlaylist == null || currentPlaylist.SortedPlaylist.Count == 0)
            {
                return false;
            }

            if (currentPlaylist.SortedPlaylist.Count == 1 && (settings.Autoplay == Autoplay.Off || playbackStopped))
            {
                return false;
            }

            return true;
        }

        private bool PreviousTrackAvailable()
        {

            if (currentPlaylist == null || currentPlaylist.SortedPlaylist.Count < 2)
            {
                return false;
            }

            if (settings.Autoplay != Autoplay.Off && !playbackStopped)
            {
                return false;
            }

            return true;
        }

        private bool YoutubeViewAvailable()
        {
            return currentTrack != null || selectedTrack != null;
        }
    }
}
