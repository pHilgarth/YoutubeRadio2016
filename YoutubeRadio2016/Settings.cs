using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace YoutubeRadio2016
{
    public class Settings
    {
        static string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "YouTubeRadio", "Settings");

        public bool Shuffle { get; set; }
        public float Volume { get; set; }
        public string FilepathLastUsedPlaylist { get; set; }
        public Autoplay Autoplay { get; set; }
        public Repeat Repeat { get; set; }
                
        public Settings()
        {
        }
        public Settings(bool shuffle, Autoplay autoplay, Repeat repeat, float volume)
        {
            Shuffle = shuffle;
            Autoplay = autoplay;
            Repeat = repeat;
            Volume = volume;
        }
        
        public static void SerializeSettings(Settings settings)
        {
            string filepath = Path.Combine(directory, "settings.xml");

            using (FileStream stream = new FileStream(filepath, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                serializer.Serialize(writer, settings);
            }
        }
        public static Settings DeserializeSettings(bool firstRun)
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
                if (!firstRun)
                {
                    MessageBox.Show(
                        "Die Datei \"settings\" wurde nicht gefunden! Die Einstellungen werden auf die Standardwerte gesetzt!",
                        "Datei nicht gefunden", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

                settings = new Settings(false, Autoplay.Off, Repeat.RepeatOff, 1);
            }

            return settings;
        }
    }
}
