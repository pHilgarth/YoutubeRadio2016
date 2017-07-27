using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace YoutubeRadio2016
{
    public static class PlaylistManager
    {
        public static string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "YoutubeRadio", "Playlists");

        public static List<Playlist> SavedPlaylists { get; set; }
        public static Playlist CurrentPlaylist { get; set; }

        private static char[] invalidChars = Path.GetInvalidFileNameChars();
        private static string[] blockedNames = { "(Neue Playlist)", "(Keine Playlist)" };
        
        public static void AddNewPlaylist(Playlist newPlaylist)
        {
            newPlaylist.Filename = newPlaylist.Name + ".xml";
            newPlaylist.Index = SavedPlaylists.Count;

            SavedPlaylists.Add(newPlaylist);
        }
        public static void RemovePlaylist(Playlist playlistToRemove)
        {
            SavedPlaylists.Remove(playlistToRemove);

            string filepath = Path.Combine(directory, playlistToRemove.Filename);

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
        public static void SerializePlaylist()
        {
            string filepath = Path.Combine(directory, CurrentPlaylist.Filename);
            List<string> videoUrls = CurrentPlaylist.SortedPlaylist.Select(x => x.VideoUrl).ToList();

            int lastIndex = CurrentPlaylist.SortedPlaylist.Count - 1;

            if(CurrentPlaylist.SortedPlaylist[lastIndex].IsAutoplayTrack)
            {
                videoUrls.RemoveAt(lastIndex);
            }

            using (FileStream stream = new FileStream(filepath, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                XmlSerializer serializer = new XmlSerializer(typeof(List<string>));

                serializer.Serialize(writer, videoUrls);
            }
        }
        public static void UpdateSavedPlaylist(Playlist playlistToUpdate)
        {
            SavedPlaylists[playlistToUpdate.Index].Filename = playlistToUpdate.Name + ".xml";
        }

        public static bool VerifyPlaylistName(string newPlaylistName, string currentPlaylistName = "(Neue Playlist)")
        {
            bool verified = false;

            if (string.IsNullOrWhiteSpace(newPlaylistName))
            {
                MessageBox.Show(
                    "Bitte geben Sie einen Namen ein!", "Namen eingeben", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (newPlaylistName.Any(x => invalidChars.Contains(x)))
            {
                MessageBox.Show(
                        "Der eingegebene Name enthält ungültige Zeichen. Bitte geben Sie einen anderen Namen ein!", "Ungültige Zeichen",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (blockedNames.Contains(newPlaylistName))
            {
                MessageBox.Show(
                    "Der eingegebene Name ist ungültig. Bitte geben Sie einen anderen Namen ein!", "Ungültiger Name",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (!currentPlaylistName.Equals(newPlaylistName) && SavedPlaylists.Find(x => x.Filename.Equals(newPlaylistName)) != null)
                {
                    MessageBox.Show(
                        "Der eingegebene Name existiert bereits. Bitte geben Sie einen anderen Namen ein!", "Name existiert bereits",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    verified = true;
                }
            }

            return verified;
        }
        public static List<string> DeserializeVideoUrls(string filepath)
        {
            List<string> videoUrls = new List<string>();

            StreamReader reader = new StreamReader(filepath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));

            videoUrls = (List<string>)serializer.Deserialize(reader);

            reader.Close();

            return videoUrls;
        }
    }
}
