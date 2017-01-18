using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace YoutubeRadio2016
{
    public class Settings
    {
        public bool Autoplay { get; set; }
        public bool SaveList { get; set; }
        public bool Shuffle { get; set; }
        public AutoplaySettings AutoplaySettings { get; set; }
        public Repeat Repeat { get; set; }

        public Settings()
        {
        }
        public Settings(bool autoplay, bool saveList, bool shuffle, AutoplaySettings autoplaySettings, Repeat repeat)
        {
            Autoplay = autoplay;
            SaveList = saveList;
            Shuffle = shuffle;
            AutoplaySettings = autoplaySettings;
            Repeat = repeat;
        }

        public static void SerializeSettingsAndPlaylist(Settings settings, List<AudioTrack> playlist)
        {
            string directory = @"C:\Users\Public\Documents\YoutubeRadio";
            string filepath = Path.Combine(directory, "settings.xml");

            Directory.CreateDirectory(directory);

            using (FileStream stream = new FileStream(filepath, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                serializer.Serialize(writer, settings);
            }

            if (settings.SaveList)
            {
                filepath = Path.Combine(directory, "playlist.xml");

                Directory.CreateDirectory(directory);

                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    StreamWriter writer = new StreamWriter(stream);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<AudioTrack>));

                    serializer.Serialize(writer, playlist);
                }
            }
        }

        public static List<AudioTrack> DeserializePlaylist()
        {
            List<AudioTrack> playlist = new List<AudioTrack>();
            string filepath = @"C:\Users\Public\Documents\YoutubeRadio\playlist.xml";

            if (File.Exists(filepath))
            {
                var reader = new StreamReader(filepath);
                var serializer = new XmlSerializer(typeof(List<AudioTrack>));

                playlist = (List<AudioTrack>)serializer.Deserialize(reader);

                reader.Close();
            }

            return playlist;
        }
        public static Settings DeserializeSettings()
        {
            string filepath = @"C:\Users\Public\Documents\YoutubeRadio\settings.xml";

            Settings settings = null;

            if (File.Exists(filepath))
            {
                var reader = new StreamReader(filepath);
                var serializer = new XmlSerializer(typeof(Settings));

                settings = (Settings)serializer.Deserialize(reader);

                reader.Close();
            }
            else
            {
                MessageBox.Show(
                    "Die Datei \"settings\" wurde nicht gefunden! Die Einstellungen werden auf die Standardwerte gesetzt!",
                    "Datei nicht gefunden", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                settings = new Settings(false, true, false, AutoplaySettings.Load, Repeat.RepeatOff);
            }

            return settings;
        }
    }
}
