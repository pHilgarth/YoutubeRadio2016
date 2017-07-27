using System;
using System.Windows.Forms;

namespace YoutubeRadio2016
{
    public partial class frmNewPlaylist : Form
    {
        frmMain formMain;

        public frmNewPlaylist(frmMain formMain)
        {
            this.formMain = formMain;

            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cmdCreate_Click(object sender, EventArgs e)
        {
            txtPlaylistName.Text = txtPlaylistName.Text.Trim();                       

            if (PlaylistManager.VerifyPlaylistName(txtPlaylistName.Text))
            {
                formMain.CreateNewPlaylist(txtPlaylistName.Text);
                Close();
            }
            else
            {                
                txtPlaylistName.SelectAll();
                txtPlaylistName.Focus();
            }
        }
    }
}
