namespace YoutubeRadio2016
{
    public class AudioTrack
    {
        public AudioTrack()
        {
        }

        public AudioTrack(bool autoplayTrack, int index, string audioUrl, string title, string videoUrl, long duration)
        {
            IsAutoplayTrack = autoplayTrack;
            IndexSortedList = index;
            IndexShuffledList = -1;
            AudioUrl = audioUrl;
            Title = title;
            VideoUrl = videoUrl;
            Duration = duration;
        } 

        public bool IsAutoplayTrack { get; set; }
        public bool Scrambled { get; set; }
        public int IndexSortedList { get; set; }
        public int IndexShuffledList { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public string VideoUrl { get; set; }
        public long Duration { get; set; }
    }
}
