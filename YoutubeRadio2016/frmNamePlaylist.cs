using System;
using System.Windows.Forms;

namespace YoutubeRadio2016
{
    public partial class frmNamePlaylist : Form
    {
        bool nameConfirmed = false;
        frmMain formMain;

        public frmNamePlaylist(frmMain formMain)
        {
            this.formMain = formMain;

            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            string newPlaylistName = txtPlaylistName.Text;

            if (PlaylistManager.VerifyPlaylistName(newPlaylistName))
            {
                formMain.ApplyPlaylistName(txtPlaylistName.Text);

                nameConfirmed = true;

                Close();
            }
            else
            {
                txtPlaylistName.SelectAll();
                txtPlaylistName.Focus();
            }            
        }
        private void frmNamePlaylist_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!nameConfirmed)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Wenn Sie keinen Namen eintippen, wird die Playlist verworfen.\nSind Sie sicher?", "Playlist verwerfen",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if(dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
