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
    public static class TrackFactory
    {
        public static string GetAudioUrl(HtmlAgilityPack.HtmlDocument doc, string videoUrl, ref bool scrambled)
        {
            string script = doc.DocumentNode.SelectNodes("//script").Select(x => x.InnerHtml).SingleOrDefault(x => x.StartsWith("var ytplayer"));
            string startIndicator = "ytplayer.config = ";
            string endIndicator = ";ytplayer.load = function";
            int startIndex = script.IndexOf(startIndicator) + startIndicator.Length;
            int endIndex = script.IndexOf(endIndicator);
            string json = script.Substring(startIndex, endIndex - startIndex);
            dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json);
            string audioUrl = null;

            if (((IDictionary<string, object>)config.args).ContainsKey("adaptive_fmts"))
            {
                string formats = config.args.adaptive_fmts;

                var streamObject = formats.Split(',')
                                    .Select(HttpUtility.ParseQueryString)
                                    .Where(x => x["type"]
                                    .Contains("audio/mp4"))
                                    .SingleOrDefault();
                audioUrl = WebUtility.UrlDecode(streamObject["url"]);
                var signature = streamObject["s"];

                if (!string.IsNullOrWhiteSpace(signature))
                {
                    scrambled = true;
                    signature = DecryptSignature(videoUrl, signature);

                    audioUrl += "&signature=" + signature;
                }
            }

            return audioUrl;
        }   //OK
        public static string GetVideoUrlAutoplayTrack(AudioTrack currentTrack)
        {
            var doc = new HtmlWeb().Load(currentTrack.VideoUrl);

            string videoID = doc.DocumentNode.SelectSingleNode("//div[@class='content-wrapper']/a").Attributes["href"].Value;
            string videoUrl = "https://www.youtube.com" + videoID;

            return videoUrl;            
        }   //OK
        public static AudioTrack CreateAudioTrack(string videoUrl, int trackIndex, bool autoplayTrack = false)
        {
            AudioTrack track = null;
            var doc = new HtmlWeb().Load(videoUrl);

            try
            {
                string title = doc.DocumentNode.SelectSingleNode("//title").InnerText;

                while (title == "YouTube")
                {
                    doc = new HtmlWeb().Load(videoUrl);
                    title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                }

                string endIndicatorTitle = " - YouTube";
                title = title.Substring(0, title.Length - endIndicatorTitle.Length);
                title = HttpUtility.HtmlDecode(title);

                string durationCode = doc.DocumentNode.SelectSingleNode("//meta[@itemprop='duration']").Attributes["content"].Value;
                TimeSpan duration = XmlConvert.ToTimeSpan(durationCode);
                long durationTicks = duration.Ticks;
                
                bool scrambled = false;

                string audioUrl = GetAudioUrl(doc, videoUrl, ref scrambled);

                if(!string.IsNullOrEmpty(audioUrl))
                {
                    track = new AudioTrack(autoplayTrack, trackIndex, audioUrl, title, videoUrl, durationTicks);

                    track.Scrambled = scrambled;
                }
                else
                {
                    MessageBox.Show("Das Video enthält keine Definition für \"adaptive_fmts\".\nEs kann nicht auf die Audiospur zugegriffen werden!", "Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //just for testing
            }

            return track;
        }   //OK
        public static List<string> GetVideoUrls(string videoURL)
        {
            List<string> videoUrls = new List<string>();

            if (videoURL.Contains("&list="))
            {
                GetVideoURLs_YouTubePlaylist(videoURL, ref videoUrls);
            }
            else
            {
                videoUrls.Add(videoURL);
            }

            return videoUrls;
        }   //OK

        private static void GetVideoURLs_YouTubePlaylist(string videoURL, ref List<string> videoURLs)
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
                    videoURLs = LoadYouTubePlaylist(document);
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
                    videoURLs = LoadYouTubePlaylist(document);
                }
                else
                {
                    videoURLs.Add(videoURL);
                }
            }
        }   //OK

        private static string DecryptSignature(string videoUrl, string encodedSignature)
        {
            WebClient client = new WebClient();
            string videoHtml = client.DownloadString(videoUrl);

            string playerScriptUrlTemplate = "https://s.ytimg.com/yts/jsbin/player-{0}/base.js";
            string functionPatternTemplate = @"#NAME#=function\([^)]+\)\{.*?\};";
            string helperObjectPatternTemplate = @"var #NAME#={.*?};";

            Regex playerVersionRegex = new Regex(@"player-(?<PlayerVersion>[\w\d\-\/]+)\/base\.js");
            Regex functionNameRegex = new Regex(@"\w\.set\(""signature"",\s*(?<FunctionName>[$A-Za-z0-9]+)");
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
        }   //OK
        private static List<string> LoadYouTubePlaylist(HtmlAgilityPack.HtmlDocument document)
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
        }   //OK
    }   //OK
}
