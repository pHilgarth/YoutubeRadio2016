using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace YoutubeRadio2016
{
    public class Settings
    {
        static string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "YouTubeRadio");

        public bool SaveList { get; set; }
        public bool Shuffle { get; set; }
        public float Volume { get; set; }
        public Autoplay Autoplay { get; set; }
        public Repeat Repeat { get; set; }
                
        public Settings()
        {
        }
        public Settings(bool saveList, bool shuffle, Autoplay autoplay, Repeat repeat, float volume)
        {
            SaveList = saveList;
            Shuffle = shuffle;
            Autoplay = autoplay;
            Repeat = repeat;
            Volume = volume;
        }

        public static void SerializeSettingsAndPlaylist(Settings settings, List<string> videoUrls)
        {
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
                    XmlSerializer serializer = new XmlSerializer(typeof(List<string>));

                    serializer.Serialize(writer, videoUrls);
                }
            }
        }

        public static List<string> DeserializePlaylist()
        {
            List<string> videoUrls = new List<string>();
            string filepath = Path.Combine(directory, "playlist.xml");

            if (File.Exists(filepath))
            {
                var reader = new StreamReader(filepath);
                var serializer = new XmlSerializer(typeof(List<string>));

                videoUrls = (List<string>)serializer.Deserialize(reader);

                reader.Close();
            }

            return videoUrls;
        }
        public static Settings DeserializeSettings()
        {
            string filepath = Path.Combine(directory, "settings.xml");

            Settings settings = null;

            if (File.Exists(filepath))
            {
                var reader = new StreamReader(filepath);
                var serializer = new XmlSerializer(typeof(Settings));

                try
                {
                    settings = (Settings)serializer.Deserialize(reader);
                }
                catch(InvalidOperationException)
                {
                    MessageBox.Show(
                        "Die Datei \"settings\" ist fehlerhaft und konnte nicht geladen werden! Die Einstellungen werden auf die Standardwerte gesetzt!",
                        "Datei fehlerhaft", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

                reader.Close();
            }
            else
            {
                MessageBox.Show(
                    "Die Datei \"settings\" wurde nicht gefunden! Die Einstellungen werden auf die Standardwerte gesetzt!",
                    "Datei nicht gefunden", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                settings = new Settings(true, false, Autoplay.Off, Repeat.RepeatOff, 1);
            }

            return settings;
        }
    }
}
