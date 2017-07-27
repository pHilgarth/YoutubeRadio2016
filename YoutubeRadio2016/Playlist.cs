using System.Collections.Generic;

namespace YoutubeRadio2016
{
    public class Playlist
    {
        public Playlist()
        {
            VideoUrls = new List<string>();
            SortedPlaylist = new List<AudioTrack>();
            Index = -1;
        }
        public Playlist(string playlistName)
        {
            VideoUrls = new List<string>();
            SortedPlaylist = new List<AudioTrack>();
            Index = -1;
            Name = playlistName;
        }

        public int Index { get; set; }
        public string Filename { get; set; } //the name the list is saved as
        public string Name { get; set; } //the current name of the list, might differ from Filename, e. g. after renaming the playlist
        public AudioTrack CurrentTrack { get; set; }
        public List<AudioTrack> SortedPlaylist { get; }
        public List<AudioTrack> ShuffledPlaylist { get; set; }
        public List<AudioTrack> UnplayedTracks { get; set; }
        public List<string> VideoUrls { get; set; }

        public void AddAudioTrack(AudioTrack trackToAdd)
        {
            SortedPlaylist.Add(trackToAdd);
        }   //OK
        public void ClearAudioTracks()
        {
            SortedPlaylist.Clear();

            if(ShuffledPlaylist != null)
            {
                ShuffledPlaylist.Clear();
                UnplayedTracks.Clear();
            }
        }   //OK
        public void RemoveAudioTrack(AudioTrack trackToRemove, bool shuffle)
        {
            RemoveTrackFromAllTracksList(trackToRemove);

            if (shuffle)
            {
                bool trackFound = false;

                RemoveTrackFromUnplayedTracksList(trackToRemove, ref trackFound);

                if (!trackFound)
                {
                    RemoveTrackFromShuffledPlaylist(trackToRemove);
                }
            }
        }   //OK

        private void RemoveTrackFromAllTracksList(AudioTrack trackToRemove)
        {
            SortedPlaylist.RemoveAt(trackToRemove.IndexSortedList);

            for (int index = trackToRemove.IndexSortedList; index < SortedPlaylist.Count; index++)
            {
                SortedPlaylist[index].IndexSortedList--;
            }
        }   //OK
        private void RemoveTrackFromShuffledPlaylist(AudioTrack trackToRemove)
        {
            ShuffledPlaylist.RemoveAt(trackToRemove.IndexShuffledList);

            for (int indexToUpdate = trackToRemove.IndexShuffledList; indexToUpdate < ShuffledPlaylist.Count; indexToUpdate++)
            {
                AudioTrack trackToUpdate = ShuffledPlaylist[indexToUpdate];

                trackToUpdate.IndexShuffledList--;
            }
        }   //OK
        private void RemoveTrackFromUnplayedTracksList(AudioTrack trackToRemove, ref bool trackFound)
        {
            for (int index = 0; index < UnplayedTracks.Count; index++)
            {
                AudioTrack track = UnplayedTracks[index];

                if (trackToRemove.Equals(track))
                {
                    UnplayedTracks.RemoveAt(index);

                    trackFound = true;

                    break;
                }
            }
        }   //OK
    }   //OK
}
