using HtmlAgilityPack;
using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace YoutubeRadio2016
{
    public class AudioPlayer
    {
        public AudioPlayer()
        {
        }
        
        public static MediaFoundationReader MediaReader { get; set; }
        public static WaveOutEvent WaveOut { get; set; }

        public static AudioTrack GetNextTrack(bool trackChangeByUserInput, Settings settings, string videoUrl, AudioTrack selectedTrack = null)
        {
            if (WaveOut != null)
            {
                WaveOut.Stop();
            }

            if (settings.Shuffle &&
                PlaylistManager.CurrentPlaylist.UnplayedTracks.Count == 0 && PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Count == 0)
            {
                PlaylistManager.CurrentPlaylist.UnplayedTracks.AddRange(PlaylistManager.CurrentPlaylist.SortedPlaylist);
            }

            AudioTrack nextTrack;

            if (settings.Autoplay != Autoplay.Off)
            {
                
                int trackIndex = PlaylistManager.CurrentPlaylist.SortedPlaylist.Count;

                nextTrack = TrackFactory.CreateAudioTrack(videoUrl, trackIndex, true);
            }
            else
            {
                if (trackChangeByUserInput)
                {
                    if (settings.Shuffle)
                    {
                        NextTrackByUserInput_Shuffle(out nextTrack, selectedTrack);
                    }
                    else
                    {
                        NextTrackByUserInput_Sorted(out nextTrack, selectedTrack);
                    }
                }
                else
                {
                    if (settings.Repeat == Repeat.RepeatOne)
                    {
                        nextTrack = PlaylistManager.CurrentPlaylist.CurrentTrack;
                    }
                    else
                    {
                        if (settings.Shuffle)
                        {
                            NextTrackAutomatic_Shuffle(out nextTrack, settings);
                        }
                        else
                        {
                            NextTrackAutomatic_Sorted(out nextTrack, settings);
                        }
                    }
                }
            }

            return nextTrack;
        }
        public static AudioTrack GetPreviousTrack(Settings settings, AudioTrack selectedTrack)
        {
            if (WaveOut != null)
            {
                WaveOut.Stop();
            }

            AudioTrack previousTrack;

            if (settings.Shuffle)
            {
                PreviousTrack_Shuffle(out previousTrack, selectedTrack);
            }
            else
            {
                PreviousTrack_Sorted(out previousTrack, selectedTrack);
            }

            return previousTrack;
        }

        public static void GetRandomTrack(out AudioTrack randomTrack)
        {
            Random random = new Random();
            int maxValue = PlaylistManager.CurrentPlaylist.UnplayedTracks.Count;
            int randomIndex = random.Next(0, maxValue);

            randomTrack = PlaylistManager.CurrentPlaylist.UnplayedTracks[randomIndex];
            PlaylistManager.CurrentPlaylist.UnplayedTracks.RemoveAt(randomIndex);            
        }
        public static void PlayTrack(AudioTrack trackToPlay, bool mute, float volume, out bool audioUrlUnaccessible)
        {
            audioUrlUnaccessible = false;

            try
            {
                bool scrambled = trackToPlay.Scrambled;
                string videoUrl = trackToPlay.VideoUrl;
                var doc = new HtmlWeb().Load(videoUrl);

                trackToPlay.AudioUrl = TrackFactory.GetAudioUrl(doc, videoUrl, ref scrambled);

                if (!string.IsNullOrEmpty(trackToPlay.AudioUrl))
                {
                    MediaReader = new MediaFoundationReader(trackToPlay.AudioUrl);
                    WaveOut = new WaveOutEvent();

                    WaveOut.Init(MediaReader);

                    if (mute)
                    {
                        WaveOut.Volume = 0;
                    }
                    else
                    {
                        WaveOut.Volume = volume;
                    }

                    WaveOut.Play();
                }
                else
                {
                    audioUrlUnaccessible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }       
        }
        
        private static void NextTrackAutomatic_Shuffle(out AudioTrack nextTrack, Settings settings)
        {
            int trackIndex = PlaylistManager.CurrentPlaylist.CurrentTrack.IndexShuffledList;
            int lastIndexShuffledList = PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Count - 1;            

            nextTrack = null;

            if (trackIndex != lastIndexShuffledList)
            {
                nextTrack = PlaylistManager.CurrentPlaylist.ShuffledPlaylist[trackIndex + 1];
            }
            else
            {
                if (PlaylistManager.CurrentPlaylist.UnplayedTracks.Count != 0)
                {
                    GetRandomTrack(out nextTrack);

                    PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Add(nextTrack);
                    nextTrack.IndexShuffledList = PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Count - 1;
                }
                else if (settings.Repeat == Repeat.RepeatAll)
                {
                    nextTrack = PlaylistManager.CurrentPlaylist.ShuffledPlaylist[0];
                }
            }
        }
        private static void NextTrackAutomatic_Sorted(out AudioTrack nextTrack, Settings settings)
        {
            int lastIndexSortedList = PlaylistManager.CurrentPlaylist.SortedPlaylist.Count - 1;
            int trackIndex = PlaylistManager.CurrentPlaylist.CurrentTrack.IndexSortedList;

            nextTrack = null;

            if (trackIndex != lastIndexSortedList)
            {
                nextTrack = PlaylistManager.CurrentPlaylist.SortedPlaylist[trackIndex + 1];
            }
            else if (settings.Repeat == Repeat.RepeatAll)
            {
                nextTrack = PlaylistManager.CurrentPlaylist.SortedPlaylist[0];
            }
        }
        private static void NextTrackByUserInput_Shuffle(out AudioTrack nextTrack, AudioTrack selectedTrack)
        {
            int trackIndex;
            int lastIndexShuffledList = PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Count - 1;
            
            if (selectedTrack != null)
            {
                trackIndex = selectedTrack.IndexShuffledList;
            }
            else
            {
                trackIndex = PlaylistManager.CurrentPlaylist.CurrentTrack.IndexShuffledList;
            }

            if (trackIndex != lastIndexShuffledList)
            {
                nextTrack = PlaylistManager.CurrentPlaylist.ShuffledPlaylist[trackIndex + 1];
            }
            else
            {
                if (PlaylistManager.CurrentPlaylist.UnplayedTracks.Count != 0)
                {
                    GetRandomTrack(out nextTrack);

                    PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Add(nextTrack);
                    nextTrack.IndexShuffledList = PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Count - 1;
                }
                else
                {
                    nextTrack = PlaylistManager.CurrentPlaylist.ShuffledPlaylist[0];
                }
            }
        }
        private static void NextTrackByUserInput_Sorted(out AudioTrack nextTrack, AudioTrack selectedTrack)
        {
            int trackIndex;
            int lastIndexAllTracks = PlaylistManager.CurrentPlaylist.SortedPlaylist.Count - 1;

            if (selectedTrack != null)
            {
                trackIndex = selectedTrack.IndexSortedList;
            }
            else
            {
                trackIndex = PlaylistManager.CurrentPlaylist.CurrentTrack.IndexSortedList;
            }

            if (trackIndex != lastIndexAllTracks)
            {
                nextTrack = PlaylistManager.CurrentPlaylist.SortedPlaylist[trackIndex + 1];
            }
            else
            {
                nextTrack = PlaylistManager.CurrentPlaylist.SortedPlaylist[0];
            }
        }
        private static void PreviousTrack_Shuffle(out AudioTrack previousTrack, AudioTrack selectedTrack)
        {
            int trackIndex = 0;

            if (selectedTrack != null)
            {
                trackIndex = selectedTrack.IndexShuffledList;
            }
            else
            {
                trackIndex = PlaylistManager.CurrentPlaylist.CurrentTrack.IndexShuffledList;
            }

            if (trackIndex > 0)
            {
                previousTrack = PlaylistManager.CurrentPlaylist.ShuffledPlaylist[trackIndex - 1];
            }
            else
            {
                GetRandomTrack(out previousTrack);

                PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Insert(0, previousTrack);

                for(int index = 0; index < PlaylistManager.CurrentPlaylist.ShuffledPlaylist.Count; index++)
                {
                    PlaylistManager.CurrentPlaylist.ShuffledPlaylist[index].IndexShuffledList = index;
                }
            }
        }
        private static void PreviousTrack_Sorted(out AudioTrack previousTrack, AudioTrack selectedTrack)
        {
            int trackIndex = 0;            

            if (selectedTrack != null)
            {
                trackIndex = selectedTrack.IndexSortedList;
            }
            else
            {
                trackIndex = PlaylistManager.CurrentPlaylist.CurrentTrack.IndexSortedList;
            }

            if (trackIndex != 0)
            {
                previousTrack = PlaylistManager.CurrentPlaylist.SortedPlaylist[trackIndex - 1];
            }
            else
            {
                int lastIndexAllTracks = PlaylistManager.CurrentPlaylist.SortedPlaylist.Count - 1;

                previousTrack = PlaylistManager.CurrentPlaylist.SortedPlaylist[lastIndexAllTracks];
            }
        }
    }
}
