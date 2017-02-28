using HtmlAgilityPack;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Windows.Forms;

namespace YoutubeRadio2016
{
    public enum Autoplay { Play, Load, Off }
    public enum Repeat { RepeatOne, RepeatAll, RepeatOff }
    public enum NextSongConditions { ShuffleRepeatOne, ShuffleRepeatAll, ShuffleOnly, RepeatOne, RepeatAll, Default }

    public partial class frmMain : Form
    {
        float volume;
        string videoURLAutoplayTrack;
        bool mute = false;
        bool muteButtonClicked = false;
        bool trackBarSeeking = false;
        AudioPlayer player;
        AudioTrack currentTrack;
        AudioTrack selectedTrack;       
        Image muteOffImage;
        Image muteOnImage;
        Image pauseImage;
        Image playImage;
        List<AudioTrack> allAudioTracks;
        Settings settings;
        TrackFactory trackFactory = new TrackFactory();

        public frmMain()
        {
            InitializeComponent();
        }

        private void cmdAddTracks_Click(object sender, EventArgs e)
        {
            string videoUrl = txtUrl.Text;

            if (!string.IsNullOrEmpty(videoUrl) && videoUrl.StartsWith("https://www.youtube.com/watch?v="))
            {
                AddTracks(videoUrl);
            }
            else
            {
                MessageBox.Show(
                    "Bitte geben Sie einen youtube-Videolink an!", "Fehlerhafte Eingabe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                txtUrl.SelectAll();
                txtUrl.Focus();
            }
        }
        private void cmdClearList_Click(object sender, EventArgs e)
        {
            if (lstVTracks.Items.Count > 0 &&  MessageBox.Show(
                "Wollen Sie wirklich die komplette Playlist leeren?", "Playlist leeren",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                StopPlayback();

                allAudioTracks.Clear();
                player.ShuffledPlaylist.Clear();
                player.UnplayedTracks.Clear();
                lstVTracks.Items.Clear();

                currentTrack = null;
                selectedTrack = null;
                player.CurrentTrack = null;

                lblTrackDuration.Text = "00:00:00";
                lblTrackPos.Text = "00:00:00";
                trkBDuration.Enabled = false;
                trkBDuration.Maximum = 0;
                trkBDuration.Value = 0;

                cmdPreviousTrack.Enabled = false;
                cmdPlay.Enabled = false;
                cmdStop.Enabled = false;
                cmdNextTrack.Enabled = false;
            }

            txtUrl.Focus();
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
            if(lstVTracks.SelectedItems.Count != 0 && currentTrack != null)
            {
                selectedTrack = null;
                lstVTracks.SelectedItems[0].Selected = false;
            }

            ChangeTrack(true, false);
        }
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            if (currentTrack != null)
            {
                CmdPlay_CurrentTrackIsNotNull();
            }
            else
            {
                CmdPlay_Click_CurrentTrackIsNull();                

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
        }
        private void cmdPlayAutoplayTrack_Click(object sender, EventArgs e)
        {
            PlayNextAutoplayTrack();
        }
        private void cmdPreviousTrack_Click(object sender, EventArgs e)
        {
            if (lstVTracks.SelectedItems.Count != 0 && currentTrack != null)
            {
                selectedTrack = null;
                lstVTracks.SelectedItems[0].Selected = false;
            }

            ChangeTrack(true, true);
        }
        private void cmdRemoveTrack_Click(object sender, EventArgs e)
        {
            if (selectedTrack != null)
            {
                RemoveTrack(selectedTrack);
            }

            if(settings.Autoplay != Autoplay.Off)
            {
                cmdPlayAutoplayTrack.Visible = false;
                lblNextSong.Visible = false;
            }

            txtUrl.Focus();
        }
        private void cmdStop_Click(object sender, EventArgs e)
        {
            if(settings.Autoplay != Autoplay.Off)
            {
                lblNextSong.Visible = false;
                cmdPlayAutoplayTrack.Visible = false;

                if(currentTrack != null && currentTrack.IsAutoplayTrack)
                {
                    int lastIndex = lstVTracks.Items.Count - 1;

                    AddOrRemoveAutoplayTrack(lastIndex);

                    if(settings.Autoplay == Autoplay.Load)
                    {
                        ListViewItem autoplayTrack = lstVTracks.Items[lastIndex];

                        autoplayTrack.ForeColor = Color.DarkBlue;
                        autoplayTrack.Font = new Font(autoplayTrack.Font, FontStyle.Bold);
                    }
                }
            }

            StopPlayback();
        }
        private void cmdYoutubeView_Click(object sender, EventArgs e)
        {
            if (selectedTrack != null)
            {
                if (player.WaveOut != null)
                {
                    Play_PauseTrack();
                }

                Process.Start(selectedTrack.VideoUrl);
            }
            else if (currentTrack != null)
            {
                if (player.WaveOut != null)
                {
                    Play_PauseTrack();
                }

                Process.Start(currentTrack.VideoUrl);
            }
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.Volume = volume;

            List<string> videoUrls = new List<string>();

            foreach(AudioTrack track in allAudioTracks)
            {
                videoUrls.Add(track.VideoUrl);
            }

            Settings.SerializeSettingsAndPlaylist(settings, videoUrls);
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            player = trackFactory.Player;            
            volume = volSlider.Volume;

            muteOffImage = imgLMute.Images[0];
            muteOnImage = imgLMute.Images[1];
            playImage = imgLPlayPause.Images[0];
            pauseImage = imgLPlayPause.Images[1];

            cmdMute.Image = muteOffImage;
            cmdPlay.Image = playImage;

            settings = Settings.DeserializeSettings();
            CheckSettings(settings);

            trackFactory.AllAudioTracks = new List<AudioTrack>();

            GetPlaylist();

            player.AllAudioTracks = trackFactory.AllAudioTracks;
            allAudioTracks = trackFactory.AllAudioTracks;

            if(allAudioTracks.Count != 0)
            {
                cmdPlay.Enabled = true;
            }
        }
        private void lstVTracks_KeyUp(object sender, KeyEventArgs e)
        {
            if (selectedTrack != null && e.KeyCode == Keys.Delete)
            {
                cmdRemoveTrack_Click(sender, e);
            }
        }
        private void lstVTracks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (selectedTrack != null)
            {
                WaveOutEvent waveOut = player.WaveOut;

                if (waveOut != null)
                {
                    waveOut.Stop();
                }

                if(currentTrack != null && currentTrack.IsAutoplayTrack)
                {
                    int lastIndex = lstVTracks.Items.Count - 1;

                    AddOrRemoveAutoplayTrack(lastIndex);
                }

                ChangeToSelectedTrack();
                UpdateTrackbar();
                PlayTrack(currentTrack);

                if (settings.Autoplay != Autoplay.Off)
                {
                    ShowAutoplayPreview();
                }
            }
        }
        private void lstVTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVTracks.SelectedItems.Count == 0)
            {
                if (currentTrack == null)
                {
                    cmdPreviousTrack.Enabled = false;
                    cmdNextTrack.Enabled = false;
                    cmdStop.Enabled = false;
                }

                selectedTrack = null;
            }
            else
            {
                int trackIndex = lstVTracks.SelectedItems[0].Index;

                selectedTrack = allAudioTracks[trackIndex];
            }

            UpdateButtons();
        }
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            frmSettings formSettings = new frmSettings(settings);

            formSettings.ShowDialog();            
            CheckSettings(settings);
        }
        private void optAutoplay_CheckedChanged(object sender, EventArgs e)
        {
            if (!optAutoplay_Off.Checked)
            {
                if (optAutoplay_Play.Checked)
                {
                    settings.Autoplay = Autoplay.Play;

                    optShuffle_Off.Checked = true;
                    optRepeatOff.Checked = true;
                }
                else
                {
                    settings.Autoplay = Autoplay.Load;

                    optShuffle_Off.Checked = true;
                    optRepeatOff.Checked = true;
                }

                if (player.WaveOut != null)
                {
                    ShowAutoplayPreview();
                }
            }
            else
            {
                settings.Autoplay = Autoplay.Off;

                lblNextSong.Visible = false;
                cmdPlayAutoplayTrack.Visible = false;
            }
        }
        private void optRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if(optRepeatOff.Checked)
            {
                settings.Repeat = Repeat.RepeatOff;
            }
            else
            {
                optAutoplay_Off.Checked = true;

                if (optRepeatAll.Checked)
                {
                    settings.Repeat = Repeat.RepeatAll; 
                }
                else
                {
                    optShuffle_Off.Checked = true;
                    settings.Repeat = Repeat.RepeatOne;
                }
            }
        }
        private void optShuffle_CheckedChanged(object sender, EventArgs e)
        {
            if (optShuffle_On.Checked)
            {
                settings.Shuffle = true;
                optAutoplay_Off.Checked = true;

                if (optRepeatOne.Checked)
                {
                    optRepeatOff.Checked = true;
                }

                player.FillUnplayedTracksList();
            }
            else
            {
                settings.Shuffle = false;

                player.UnplayedTracks.Clear();
                player.ShuffledPlaylist.Clear();

                foreach (AudioTrack track in allAudioTracks)
                {
                    track.IndexShuffledList = -1; //indicates there's no ShuffledList respectively it's empty
                }
            }
        }
        private void tmrPlayTrack_Tick(object sender, EventArgs e)
        {
            if (player.WaveOut.PlaybackState == PlaybackState.Stopped)
            {
                tmrPlayTrack.Stop();

                if (settings.Autoplay != Autoplay.Off)
                {
                    cmdPlayAutoplayTrack_Click(sender, e);
                }
                else
                {
                    if (lstVTracks.Items.Count == 1)
                    {
                        if (settings.Repeat != Repeat.RepeatOff)
                        {
                            UpdateTrackbar();

                            bool audioUrlUnaccessible;

                            player.PlayTrack(currentTrack, mute, volume, out audioUrlUnaccessible);

                            if(audioUrlUnaccessible)
                            {
                                DialogResult dialogResult = MessageBox.Show(
                                    "Die Tonspur dieses Videos ist zurzeit nicht erreichbar. Wollen Sie das Video entfernen?", "Tonspur nicht erreichbar",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    RemoveTrack(currentTrack);
                                }

                                StopPlayback();
                            }
                        }
                        else
                        {
                            StopPlayback();
                        }
                    }
                    else
                    {
                        ChangeTrack(false, false);
                    }
                }
            }
            else if(!trackBarSeeking)
            {
                TimeSpan trackPosition = player.MediaReader.CurrentTime;
                var trackPositionSeconds = (int)trackPosition.TotalSeconds;
                string posString = trackPosition.ToString(@"hh\:mm\:ss");

                if(trkBDuration.Maximum >= trackPositionSeconds)
                    trkBDuration.Value = trackPositionSeconds;

                lblTrackPos.Text = posString;
            }
        }
        private void trkBDuration_MouseDown(object sender, MouseEventArgs e)
        {
            trackBarSeeking = true;

            GetMousePositionTrackbar(e.X);
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
            player.MediaReader.CurrentTime = TimeSpan.FromSeconds(trkBDuration.Value);

            trackBarSeeking = false;
        }
        private void trkBDuration_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan position = TimeSpan.FromSeconds(trkBDuration.Value);

            lblTrackPos.Text = position.ToString("T");
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
            WaveOutEvent waveOut = player.WaveOut;

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

        private void AddTracks(string videoUrl, bool autoplayTrack = false)
        {
            try
            {
                List<string> videoUrls = trackFactory.GetVideoUrls(videoUrl);

                LoadTracks(videoUrls, autoplayTrack);

                txtUrl.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Beim Hinzufügen des Videos ist ein unerwarteter Fehler aufgetreten!\n\n" + ex.Message,
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUrl.SelectAll();
                txtUrl.Focus();
            }


            if (lstVTracks.Items.Count > 0)
            {
                cmdPlay.Enabled = true;

                if (lstVTracks.Items.Count > 1 && currentTrack != null)
                {
                    cmdPreviousTrack.Enabled = true;
                    cmdNextTrack.Enabled = true;
                }
            }

            txtUrl.Focus();
        }
        private void ChangeToSelectedTrack()
        {
            if (currentTrack != null)
            {
                int trackIndexBeforeChange = currentTrack.IndexSortedList;
                ListViewItem itemBeforeChange = lstVTracks.Items[trackIndexBeforeChange];

                itemBeforeChange.ForeColor = Color.FromName("WindowText");
                itemBeforeChange.Font = new Font(itemBeforeChange.Font, FontStyle.Regular);
            }

            player.CurrentTrack = selectedTrack;
            currentTrack = player.CurrentTrack;
            selectedTrack = null;
            lstVTracks.SelectedItems[0].Selected = false;

            int trackIndexAfterChange = currentTrack.IndexSortedList;
            ListViewItem itemAfterChange = lstVTracks.Items[currentTrack.IndexSortedList];

            itemAfterChange.ForeColor = Color.DarkBlue;
            itemAfterChange.Font = new Font(itemAfterChange.Font, FontStyle.Bold);
        }
        private void ChangeTrack(bool buttonPressed, bool previousTrack)
        {
            if (player.WaveOut != null)
            {
                player.WaveOut.Stop();
            }

            tmrPlayTrack.Stop();
            trkBDuration.Enabled = false;

            AudioTrack nextTrack;

            GetNextTrackToPlay(buttonPressed, previousTrack, out nextTrack);

            if (nextTrack != null)
            {
                int trackIndex = nextTrack.IndexSortedList;
                ListViewItem itemAfterChange = lstVTracks.Items[trackIndex];

                if (currentTrack != null)
                {
                    itemAfterChange.ForeColor = Color.DarkBlue;
                    itemAfterChange.Font = new Font(itemAfterChange.Font, FontStyle.Bold);

                    if (!currentTrack.IsAutoplayTrack)
                    {
                        ListViewItem itemBeforeChange = lstVTracks.Items[currentTrack.IndexSortedList];

                        itemBeforeChange.ForeColor = Color.FromName("WindowText");
                        itemBeforeChange.Font = new Font(itemBeforeChange.Font, FontStyle.Regular);
                    }
                    else
                    {
                        int lastIndex = lstVTracks.Items.Count - 1;

                        AddOrRemoveAutoplayTrack(lastIndex);
                    }

                    player.CurrentTrack = allAudioTracks[trackIndex];
                    currentTrack = player.CurrentTrack;

                    UpdateTrackbar();

                    if (currentTrack != null)
                    {
                        PlayTrack(currentTrack);
                    }

                    if (settings.Autoplay != Autoplay.Off)
                    {
                        ShowAutoplayPreview();
                    }
                }
                else
                {
                    itemAfterChange.Selected = true;
                }

                lstVTracks.Items[itemAfterChange.Index].EnsureVisible();
            }
            else
            {
                StopPlayback();
            }
        }
        private void AddOrRemoveAutoplayTrack(int lastIndex)
        {
            if(settings.Autoplay == Autoplay.Play && currentTrack.IsAutoplayTrack)
            {
                RemoveTrack(currentTrack);

                lastIndex--;
            }
            else if(currentTrack.IsAutoplayTrack)
            {
                string title = currentTrack.Title;
                TimeSpan duration = TimeSpan.FromTicks(currentTrack.Duration);
                string durationString = duration.ToString("T");
                string[] subItems = { title, durationString };

                lstVTracks.Items.RemoveAt(lastIndex);
                lstVTracks.Items.Add(new ListViewItem(subItems));

                currentTrack.IsAutoplayTrack = false;
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
        private void CmdPlay_CurrentTrackIsNotNull()
        {
            if (player.WaveOut != null)
            {
                Play_PauseTrack();
            }
            else
            {
                if (selectedTrack != null)
                {
                    ChangeToSelectedTrack();
                }

                UpdateTrackbar();
                PlayTrack(currentTrack);                               
            }
        }
        private void CmdPlay_Click_CurrentTrackIsNull()
        {
            if (selectedTrack != null)
            {
                player.CurrentTrack = selectedTrack;
                currentTrack = player.CurrentTrack;
                lstVTracks.Items[selectedTrack.IndexSortedList].Selected = false;
                selectedTrack = null;
            }
            else
            {
                GetPlaylistsFirstTrack();

                cmdStop.Enabled = true;
            }

            UpdateButtons();
        }
        private void DoAutoplayOperations()
        {
            try
            {
                int lastIndex = lstVTracks.Items.Count - 1;

                AddOrRemoveAutoplayTrack(lastIndex);

                AddTracks(videoURLAutoplayTrack, true);

                lastIndex = lstVTracks.Items.Count - 1;

                if(currentTrack != null)
                {
                    int trackIndexBeforeChange = currentTrack.IndexSortedList;
                    ListViewItem itemBeforeChange = lstVTracks.Items[trackIndexBeforeChange];

                    itemBeforeChange.ForeColor = Color.FromName("WindowText");
                    itemBeforeChange.Font = new Font(itemBeforeChange.Font, FontStyle.Regular);
                }

                ListViewItem autoplayTrackItem = lstVTracks.Items[lastIndex];

                autoplayTrackItem.ForeColor = Color.DarkGreen;
                autoplayTrackItem.Font = new Font(autoplayTrackItem.Font, FontStyle.Bold);

                player.CurrentTrack = allAudioTracks[lastIndex];
                currentTrack = player.CurrentTrack;

                UpdateTrackbar();
                ShowAutoplayPreview();
                PlayTrack(currentTrack);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es ist ein unerwarteter Fehler aufgetreten!\n\n" + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void GetNextTrackToPlay(bool buttonPressed, bool previousTrack, out AudioTrack nextTrack)
        {
            nextTrack = null;

            if (buttonPressed)
            {
                if (previousTrack)
                {
                    player.GetPreviousTrack(ref nextTrack, selectedTrack, settings);
                }
                else
                {
                    player.GetNextTrack(ref nextTrack, true, settings, selectedTrack);
                }
            }
            else
            {
                player.GetNextTrack(ref nextTrack, false, settings);
            }
        }
        private void GetPlaylist()
        {
            List<string> videoUrls = Settings.DeserializePlaylist();

            LoadTracks(videoUrls, false);
        }
        private void GetPlaylistsFirstTrack()
        {
            if (settings.Shuffle)
            {
                AudioTrack trackToPlay = null;

                player.GetRandomTrack(ref trackToPlay);
                player.CurrentTrack = trackToPlay;
                currentTrack = player.CurrentTrack;
            }
            else
            {
                player.CurrentTrack = allAudioTracks[0];
                currentTrack = player.CurrentTrack;
            }
        }        
        private void LoadTracks(List<string> videoUrls, bool autoplayTrack)
        {
            bool allTracksLoaded;
            bool shuffle = settings.Shuffle;
            int lstVTracksCount = lstVTracks.Items.Count;

            prgLoadTracks.Visible = true;
            prgLoadTracks.Maximum = videoUrls.Count;
            Enabled = false;
            
            List<ListViewItem> itemsToAdd = trackFactory.LoadTracks(out allTracksLoaded, autoplayTrack, shuffle, lstVTracksCount, videoUrls, prgLoadTracks);

            Enabled = true;
            prgLoadTracks.Value = 0;
            prgLoadTracks.Visible = false;

            foreach (ListViewItem item in itemsToAdd)
            {
                lstVTracks.Items.Add(item);
            }

            if (!allTracksLoaded)
            {
                MessageBox.Show(
                    "Einige Videos sind derzeit nicht verfügbar und wurden daher nicht in den Player geladen!", "Videos nicht verfügbar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void Play_PauseTrack()
        {
            PlaybackState playbackState = player.WaveOut.PlaybackState;

            if (playbackState == PlaybackState.Playing)
            {
                cmdPlay.Image = playImage;
                player.WaveOut.Pause();
                tmrPlayTrack.Stop();
            }
            else if (playbackState == PlaybackState.Paused)
            {
                cmdPlay.Image = pauseImage;
                player.WaveOut.Play();
                tmrPlayTrack.Start();
            }
        }
        private void PlayNextAutoplayTrack()
        {
            player.WaveOut.Stop();
            tmrPlayTrack.Stop();
            trkBDuration.Enabled = false;
            DoAutoplayOperations();
        }
        private void PlayTrack(AudioTrack trackToPlay)
        {
            try
            {
                bool audioUrlUnaccessible;

                player.PlayTrack(trackToPlay, mute, volume, out audioUrlUnaccessible);

                if (audioUrlUnaccessible)
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "Die Tonspur dieses Videos ist zurzeit nicht erreichbar. Wollen Sie das Video entfernen?", "Tonspur nicht erreichbar",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dialogResult == DialogResult.Yes)
                    {
                        RemoveTrack(currentTrack);
                    }

                    ChangeTrack(false, false);
                }
                else
                {
                    tmrPlayTrack.Start();
                    cmdStop.Enabled = true;
                    cmdPlay.Image = pauseImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Beim Abspielen des Songs ist ein unerwarteter Fehler aufgetreten!\n\n" + ex.Message, "Unerwarteter Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                if (settings.Autoplay != Autoplay.Off)
                {
                    PlayNextAutoplayTrack();
                }
                else if(lstVTracks.Items.Count > 1)
                {
                    if(settings.Repeat == Repeat.RepeatOne)
                    {
                        ChangeTrack(true, false);
                    }
                    else
                    {
                        ChangeTrack(false, false);
                    }                    
                }
                else
                {
                    StopPlayback();
                }
            }
        }
        private void RemoveTrack(AudioTrack trackToRemove)
        {
            if (currentTrack != null && trackToRemove.Equals(currentTrack))
            {
                StopPlayback();

                player.CurrentTrack = null;
                currentTrack = null;
            }

            RemoveTrackFromAllTracksList(trackToRemove);

            if (settings.Shuffle)
            {
                bool trackFound = false;

                RemoveTrackFromUnplayedTracksList(ref trackFound);

                if (!trackFound)
                {
                    RemoveTrackFromShuffledPlaylist();
                }
            }

            lstVTracks.Items.RemoveAt(trackToRemove.IndexSortedList);

            UpdateButtons();
        }
        private void RemoveTrackFromAllTracksList(AudioTrack trackToRemove)
        {
            int trackToRemoveIndex = trackToRemove.IndexSortedList;

            allAudioTracks.RemoveAt(trackToRemoveIndex);

            for (int index = trackToRemoveIndex; index < allAudioTracks.Count; index++)
            {
                AudioTrack track = allAudioTracks[index];

                track.IndexSortedList--;
            }
        }
        private void RemoveTrackFromShuffledPlaylist()
        {
            List<AudioTrack> shuffledPlaylist = player.ShuffledPlaylist;

            int selectedTrackIndex = selectedTrack.IndexShuffledList;

            shuffledPlaylist.RemoveAt(selectedTrackIndex);

            for (int indexToUpdate = selectedTrackIndex; indexToUpdate < shuffledPlaylist.Count; indexToUpdate++)
            {
                AudioTrack trackToUpdate = shuffledPlaylist[indexToUpdate];

                trackToUpdate.IndexShuffledList--;
            }
        }
        private void RemoveTrackFromUnplayedTracksList(ref bool trackFound)
        {
            List<AudioTrack> unplayedTracks = player.UnplayedTracks;

            for (int index = 0; index < unplayedTracks.Count; index++)
            {
                AudioTrack track = unplayedTracks[index];

                if (selectedTrack.Equals(track))
                {
                    unplayedTracks.RemoveAt(index);

                    trackFound = true;
                    break;
                }
            }
        }
        private void ShowAutoplayPreview()
        {
            videoURLAutoplayTrack = GetVideoUrlAutoplayTrack();

            var doc = new HtmlWeb().Load(videoURLAutoplayTrack);
            string title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
            string endIndicatorTitle = " - YouTube";

            title = title.Substring(0, title.Length - endIndicatorTitle.Length);
            title = HttpUtility.HtmlDecode(title);

            lblNextSong.Text = "Nächstes Lied: " + title;

            lblNextSong.Visible = true;
            cmdPlayAutoplayTrack.Visible = true;
        }
        private void StopPlayback()
        {
            if(player.WaveOut != null)
            {
                player.WaveOut.Stop();
            }

            cmdPlay.Image = playImage;
            tmrPlayTrack.Stop();
            trkBDuration.Maximum = 0;
            lblTrackDuration.Text = "00:00:00";
            lblTrackPos.Text = "00:00:00";
            player.WaveOut = null;
            player.MediaReader = null;
        }
        private void UpdateButtons()
        {
            if (lstVTracks.Items.Count == 1 || currentTrack == null)
            {
                cmdPreviousTrack.Enabled = false;
                cmdNextTrack.Enabled = false;
            }
            else
            {
                cmdPreviousTrack.Enabled = true;
                cmdNextTrack.Enabled = true;                
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
        private string GetVideoUrlAutoplayTrack()
        {
            var doc = new HtmlWeb().Load(currentTrack.VideoUrl);

            string videoID = doc.DocumentNode.SelectSingleNode("//div[@class='content-wrapper']/a").Attributes["href"].Value;
            string videoUrl = "https://www.youtube.com" + videoID;

            return videoUrl;
        }
    }
}
