using System;
using System.Windows.Forms;

namespace YoutubeRadio2016
{
    public partial class frmSettings : Form
    {
        Settings newSettings;

        public frmSettings(Settings settings)
        {
            InitializeComponent();

            newSettings = settings;
            CheckSettings(settings);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            ApplySettings();
            Close();
        }
        private void optAutoplay_CheckedChanged(object sender, EventArgs e)
        {
            if (!optAutoplay_Off.Checked)
            {
                optRepeatOff.Checked = true;
                optShuffleOff.Checked = true;
            }
        }
        private void optRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (!optRepeatOff.Checked)
            {
                optAutoplay_Off.Checked = true;

                if (optRepeatOne.Checked)
                {
                    optShuffleOff.Checked = true;
                }
            }
        }
        private void optShuffle_CheckedChanged(object sender, EventArgs e)
        {
            if (optShuffleOn.Checked)
            {
                optAutoplay_Off.Checked = true;

                if (optRepeatOne.Checked)
                {
                    optRepeatOff.Checked = true;
                }
            }
        }

        private void ApplyAutoplay()
        {
            if (optAutoplay_Play.Checked)
            {
                newSettings.Autoplay = Autoplay.Play;
            }
            else if (optAutoplay_Load.Checked)
            {
                newSettings.Autoplay = Autoplay.Load;
            }
            else
            {
                newSettings.Autoplay = Autoplay.Off;
            }
        }
        private void ApplyRepeat()
        {
            if (optRepeatAll.Checked)
            {
                newSettings.Repeat = Repeat.RepeatAll;
            }
            else if (optRepeatOne.Checked)
            {
                newSettings.Repeat = Repeat.RepeatOne;
            }
            else
            {
                newSettings.Repeat = Repeat.RepeatOff;
            }
        }
        private void ApplySaveList()
        {
            if (chkSavePlaylist.Checked)
            {
                newSettings.SaveList = true;
            }
            else
            {
                newSettings.SaveList = false;
            }
        }
        private void ApplySettings()
        {
            ApplyAutoplay();
            ApplyRepeat();
            ApplyShuffle();
            ApplySaveList();
        }
        private void ApplyShuffle()
        {
            if (optShuffleOn.Checked)
            {
                newSettings.Shuffle = true;
            }
            else
            {
                newSettings.Shuffle = false;
            }                        
        }
        private void CheckAutoplay(Autoplay autoplay)
        {
            if (autoplay == Autoplay.Play)
            {
                optAutoplay_Play.Checked = true;
            }
            else if (autoplay == Autoplay.Load)
            {
                optAutoplay_Load.Checked = true;
            }
            else
            {
                optAutoplay_Off.Checked = true;
            }
        }
        private void CheckRepeat(Repeat repeat)
        {
            if (repeat == Repeat.RepeatAll)
            {
                optRepeatAll.Checked = true;
            }
            else if (repeat == Repeat.RepeatOne)
            {
                optRepeatOne.Checked = true;
            }
            else
            {
                optRepeatOff.Checked = true;
            }
        }
        private void CheckSaveList(bool saveList)
        {
            if (saveList)
            {
                chkSavePlaylist.Checked = true;
            }
            else
            {
                chkSavePlaylist.Checked = false;
            }
        }
        private void CheckSettings(Settings settings)
        {
            CheckAutoplay(settings.Autoplay);

            if (settings.Autoplay != Autoplay.Off)
            {
                optRepeatOff.Checked = true;
                optShuffleOff.Checked = true;
            }
            else
            {
                CheckRepeat(settings.Repeat);
                CheckShuffle(settings.Shuffle);
            }

            CheckSaveList(settings.SaveList);
        }
        private void CheckShuffle(bool shuffle)
        {
            if (shuffle)
            {
                optShuffleOn.Checked = true;
            }
            else
            {
                optShuffleOff.Checked = true;
            }
        }
    }
}
