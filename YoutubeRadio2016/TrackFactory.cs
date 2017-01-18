using HtmlAgilityPack;
using Jint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace YoutubeRadio2016
{
    public class TrackFactory
    {
        public TrackFactory()
        {
            Player = new AudioPlayer(this);
        }

        public AudioPlayer Player { get; set; }
        public List<AudioTrack> AllAudioTracks { get; set; }
        
        public void CreateAudioTrack(out bool loadingSuccessfull, bool autoplayTrack, bool shuffle, int trackIndex, string videoURL)
        {
            var doc = new HtmlWeb().Load(videoURL);

            try
            {
                string title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                string endIndicatorTitle = " - YouTube";
                title = title.Substring(0, title.Length - endIndicatorTitle.Length);
                title = HttpUtility.HtmlDecode(title);

                string durationCode = doc.DocumentNode.SelectSingleNode("//meta[@itemprop='duration']").Attributes["content"].Value;
                TimeSpan duration = XmlConvert.ToTimeSpan(durationCode);
                long durationTicks = duration.Ticks;

                string script = doc.DocumentNode.SelectNodes("//script").Select(x => x.InnerHtml).SingleOrDefault(x => x.StartsWith("var ytplayer"));
                string startIndicator = "ytplayer.config = ";
                string endIndicator = ";ytplayer.load = function";
                int startIndex = script.IndexOf(startIndicator) + startIndicator.Length;
                int endIndex = script.IndexOf(endIndicator);
                string json = script.Substring(startIndex, endIndex - startIndex);
                dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json);
                string formats;

                if(((IDictionary<string, object>)config.args).ContainsKey("adaptive_fmts"))
                {
                    formats = config.args.adaptive_fmts;

                    var streamObject = formats.Split(',')
                                        .Select(HttpUtility.ParseQueryString)
                                        .Where(x => x["type"]
                                        .Contains("audio/mp4"))
                                        .SingleOrDefault();
                    var audioUrl = WebUtility.UrlDecode(streamObject["url"]);
                    var signature = streamObject["s"];

                    var track = new AudioTrack(autoplayTrack, trackIndex, audioUrl, title, videoURL, durationTicks);

                    if (!string.IsNullOrWhiteSpace(signature))
                    {
                        track.Scrambled = true;
                        signature = DecryptSignature(track.VideoUrl, signature);

                        track.AudioUrl += "&signature=" + signature;
                    }

                    AllAudioTracks.Add(track);

                    if (shuffle)
                    {
                        Player.UnplayedTracks.Add(track);
                    }

                    loadingSuccessfull = true;
                }
                else
                {
                    MessageBox.Show("Das Video enthält keine Definition für \"adaptive_fmts\".\nEs kann nicht auf die Audiospur zugegriffen werden!", "Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    loadingSuccessfull = false;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //just for testing
                loadingSuccessfull = false;
            }
        }
        public List<ListViewItem> LoadTracks(out bool allTracksLoaded, bool autoplayTrack, bool shuffle, int lstVTracksCount, List<string> videoURLs, ProgressBar prgLoadTracks)
        {
            int trackIndex = lstVTracksCount;
            List<ListViewItem> itemsToAdd = new List<ListViewItem>();

            allTracksLoaded = true;

            foreach (string url in videoURLs)
            {
                bool loadingSuccessfull;

                CreateAudioTrack(out loadingSuccessfull, autoplayTrack, shuffle, trackIndex, url);

                if (loadingSuccessfull)
                {
                    int lastIndex = AllAudioTracks.Count - 1;
                    AudioTrack createdTrack = AllAudioTracks[lastIndex];

                    TimeSpan duration = TimeSpan.FromTicks(createdTrack.Duration);
                    string durationString = duration.ToString("T");
                    string title = createdTrack.Title;

                    if (autoplayTrack)
                    {
                        title = "AUTOPLAY: " + title;
                    }

                    string[] subItems = { title, durationString };

                    itemsToAdd.Add(new ListViewItem(subItems));

                    trackIndex++;
                }
                else
                {
                    allTracksLoaded = false;
                }

                prgLoadTracks.Value++;
            }

            return itemsToAdd;
        }
        public List<string> GetVideoURLs(string videoURL)
        {
            List<string> videoUrls = new List<string>();

            if (videoURL.Contains("&list="))
            {
                GetVideoURLs_Playlist(videoURL, ref videoUrls);
            }
            else
            {
                videoUrls.Add(videoURL);
            }

            return videoUrls;
        }

        private void GetVideoURLs_Playlist(string videoURL, ref List<string> videoURLs)
        {
            var document = new HtmlWeb().Load(videoURL);

            try
            {
                string playlistLength = document.DocumentNode.SelectSingleNode("//span[@id='playlist-length']").InnerText;

                if (MessageBox.Show(
                    "Das eingefügte Video befindet sich in einer youtube-Playlist mit insgesamt " + playlistLength +
                    ".\nMöchten Sie die gesamte Playlist in das Prgoramm laden?", "Playlist gefunden",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    videoURLs = LoadPlaylist(document);
                }
                else
                {
                    videoURLs.Add(videoURL);
                }
            }
            catch (Exception ex)//if "playlist-length" was not found, it's a youtube-Mix with 25 tracks
            {
                //MessageBox.Show(ex.Message); //just for testing

                if (MessageBox.Show(
                    "Das eingefügte Video befindet sich in einem \"youtube-Mix\" mit 25 Videos.\n" +
                    "Möchten Sie den ganzen Mix in das Programm laden?", "Playlist gefunden",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    videoURLs = LoadPlaylist(document);
                }
                else
                {
                    videoURLs.Add(videoURL);
                }
            }
        }
        private string DecryptSignature(string videoUrl, string encodedSignature)
        {
            WebClient client = new WebClient();
            string videoHtml = client.DownloadString(videoUrl);

            string playerScriptUrlTemplate = "https://s.ytimg.com/yts/jsbin/player-{0}/base.js";
            string functionPatternTemplate = @"#NAME#=function\([^)]+\)\{.*?\};";
            string helperObjectPatternTemplate = @"var #NAME#={.*?};";

            Regex playerVersionRegex = new Regex(@"player-(?<PlayerVersion>[\w\d\-]+)\/base\.js");
            Regex functionNameRegex = new Regex(@"\.sig\|\|(?<FunctionName>[a-zA-Z0-9$]+)\(");
            Regex helperObjectNameRegex = new Regex(@";(?<ObjectName>[$A-Za-z0-9]+)\.");

            string playerVersion = playerVersionRegex.Match(videoHtml).Groups["PlayerVersion"].Value;

            string playerScriptUrl = string.Format(playerScriptUrlTemplate, playerVersion);
            string playerScriptHtml = client.DownloadString(playerScriptUrl);

            string functionName = functionNameRegex.Match(playerScriptHtml).Groups["FunctionName"].Value;
            string function = Regex.Match(playerScriptHtml, functionPatternTemplate.Replace("#NAME#", functionName)).Value;

            string helperObjectName = helperObjectNameRegex.Match(function).Groups["ObjectName"].Value;
            string helperObject = Regex.Match(playerScriptHtml, helperObjectPatternTemplate.Replace("#NAME#", helperObjectName), RegexOptions.Singleline).Value;

            Engine engine = new Engine();
            Engine decoderScript = engine.Execute(helperObject).Execute(function);
            var decodedSignature = decoderScript.Invoke(functionName, encodedSignature).ToString();

            return decodedSignature;
        }
        private List<string> LoadPlaylist(HtmlAgilityPack.HtmlDocument document)
        {
            bool moreVideosAvailable = true;
            int videoIndex = 0;
            List<string> videoUrls = new List<string>();

            do
            {
                try
                {
                    var xpath = "//li[@data-index='" + videoIndex + "']";
                    var videoID = document.DocumentNode.SelectSingleNode(xpath).Attributes["data-video-id"].Value;
                    var videoUrl = "https://www.youtube.com/watch?v=" + videoID;

                    videoUrls.Add(videoUrl);
                    videoIndex++;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message); //just for testing!
                    moreVideosAvailable = false;
                }
            }
            while (moreVideosAvailable);

            return videoUrls;
        }
    }
}
