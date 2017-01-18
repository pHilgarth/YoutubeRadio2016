using NAudio.Wave;
using System;
using System.Collections.Generic;

namespace YoutubeRadio2016
{
    public class AudioPlayer
    {
        public AudioPlayer(TrackFactory trackFactory)
        {
            ShuffledPlaylist = new List<AudioTrack>();
            UnplayedTracks = new List<AudioTrack>();
            TrackFactory = trackFactory;
        }

        public AudioTrack CurrentTrack { get; set; }
        public List<AudioTrack> AllAudioTracks { get; set; }
        public List<AudioTrack> ShuffledPlaylist { get; set; }
        public List<AudioTrack> UnplayedTracks { get; set; }
        public MediaFoundationReader MediaReader { get; set; }
        public TrackFactory TrackFactory { get; set; }
        public WaveOutEvent WaveOut { get; set; }

        public void GetNextTrack(ref AudioTrack nextTrack, bool buttonPressed, Settings settings, AudioTrack selectedTrack = null)
        {
            if (buttonPressed)
            {
                NextTrack_Manual(ref nextTrack, selectedTrack, settings);
            }
            else
            {
                NextTrack_Automatic(ref nextTrack, settings);
            }
        }
        public void GetPreviousTrack(ref AudioTrack previousTrack, AudioTrack selectedTrack, Settings settings)
        {
            if (settings.Shuffle)
            {
                PreviousTrack_Shuffle(ref previousTrack, selectedTrack);
            }
            else
            {
                PreviousTrack_Sorted(ref previousTrack, selectedTrack);
            }
        }
        public void GetRandomTrack(ref AudioTrack nextTrack)
        {
            Random random = new Random();
            int maxValue = UnplayedTracks.Count;
            int randomIndex = random.Next(0, maxValue);

            nextTrack = UnplayedTracks[randomIndex];
            UnplayedTracks.RemoveAt(randomIndex);
            ShuffledPlaylist.Add(nextTrack);
            nextTrack.IndexShuffledList = ShuffledPlaylist.Count - 1;
        }
        public void PlayTrack(AudioTrack trackToPlay, bool mute, float volume)
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
        public void FillUnplayedTracksList()
        {
            foreach (AudioTrack track in AllAudioTracks)
            {
                UnplayedTracks.Add(track);
            }
        }
        
        private void NextTrack_Automatic(ref AudioTrack nextTrack, Settings settings)
        {
            if (settings.Repeat == Repeat.RepeatOne)
            {
                nextTrack = CurrentTrack;
            }
            else
            {
                NextTrack_Automatic_NoRepeatOne(ref nextTrack, settings);
            }
        }
        private void NextTrack_Automatic_NoRepeatOne(ref AudioTrack nextTrack, Settings settings)
        {
            if (settings.Shuffle)
            {
                NextTrack_Automatic_Shuffle(ref nextTrack, settings);
            }
            else
            {
                NextTrack_Automatic_Sorted(ref nextTrack, settings);
            }
        }
        private void NextTrack_Automatic_Shuffle(ref AudioTrack nextTrack, Settings settings)
        {
            int lastIndexShuffledList = ShuffledPlaylist.Count - 1;
            int trackIndex = CurrentTrack.IndexShuffledList;

            if (trackIndex != lastIndexShuffledList)
            {
                nextTrack = ShuffledPlaylist[trackIndex + 1];
            }
            else
            {
                if (UnplayedTracks.Count != 0)
                {
                    GetRandomTrack(ref nextTrack);
                }
                else
                {
                    if (settings.Repeat == Repeat.RepeatAll)
                    {
                        FillUnplayedTracksList();
                        GetRandomTrack(ref nextTrack);
                    }
                }
            }
        }
        private void NextTrack_Automatic_Sorted(ref AudioTrack nextTrack, Settings settings)
        {
            int lastIndexSortedList = AllAudioTracks.Count - 1;
            int trackIndex = CurrentTrack.IndexSortedList;

            if (trackIndex != lastIndexSortedList)
            {
                nextTrack = AllAudioTracks[trackIndex + 1];
            }
            else
            {
                if (settings.Repeat == Repeat.RepeatAll)
                {
                    nextTrack = AllAudioTracks[0];
                }
            }
        }
        private void NextTrack_Manual(ref AudioTrack nextTrack, AudioTrack selectedTrack, Settings settings)
        {
            if (settings.Shuffle)
            {
                NextTrack_Manual_ShuffleRepeatAll(ref nextTrack, selectedTrack);
            }
            else
            {
                NextTrack_Manual_SortedRepeatAll(ref nextTrack, selectedTrack);
            }
        }
        private void NextTrack_Manual_ShuffleRepeatAll(ref AudioTrack nextTrack, AudioTrack selectedTrack)
        {
            int trackIndex;
            int lastIndexShuffledList = ShuffledPlaylist.Count - 1;
            
            if (selectedTrack != null)
            {
                trackIndex = selectedTrack.IndexShuffledList;
            }
            else
            {
                trackIndex = CurrentTrack.IndexShuffledList;
            }

            if (trackIndex != lastIndexShuffledList)
            {
                nextTrack = ShuffledPlaylist[trackIndex + 1];
            }
            else
            {
                if (UnplayedTracks.Count != 0)
                {
                    GetRandomTrack(ref nextTrack);
                }
                else
                {
                    FillUnplayedTracksList();
                    GetRandomTrack(ref nextTrack);
                }
            }
        }
        private void NextTrack_Manual_SortedRepeatAll(ref AudioTrack nextTrack, AudioTrack selectedTrack)
        {
            int trackIndex;
            int lastIndexAllTracks = AllAudioTracks.Count - 1;

            if (selectedTrack != null)
            {
                trackIndex = selectedTrack.IndexSortedList;
            }
            else
            {
                trackIndex = CurrentTrack.IndexSortedList;
            }

            if (trackIndex != lastIndexAllTracks)
            {
                nextTrack = AllAudioTracks[trackIndex + 1];
            }
            else
            {
                nextTrack = AllAudioTracks[0];
            }
        }
        private void PreviousTrack_Shuffle(ref AudioTrack previousTrack, AudioTrack selectedTrack)
        {
            int trackIndex;

            if (selectedTrack != null)
            {
                trackIndex = selectedTrack.IndexShuffledList;
            }
            else
            {
                trackIndex = CurrentTrack.IndexShuffledList;
            }

            if (trackIndex != 0)
            {
                previousTrack = ShuffledPlaylist[trackIndex - 1];
            }
            else
            {
                int lastIndexShuffledPlaylist = ShuffledPlaylist.Count - 1;

                previousTrack = ShuffledPlaylist[lastIndexShuffledPlaylist];
            }
        }
        private void PreviousTrack_Sorted(ref AudioTrack previousTrack, AudioTrack selectedTrack)
        {
            int trackIndex;
            int lastIndexAllTracks = AllAudioTracks.Count - 1;

            if (selectedTrack != null)
            {
                trackIndex = selectedTrack.IndexSortedList;
            }
            else
            {
                trackIndex = CurrentTrack.IndexSortedList;
            }

            if (trackIndex != 0)
            {
                previousTrack = AllAudioTracks[trackIndex - 1];
            }
            else
            {
                previousTrack = AllAudioTracks[lastIndexAllTracks];
            }
        }
    }
}
