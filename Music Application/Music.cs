using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Application
{
    public class Music
    {
        public int MusicID {  get; set; }
        public string MusicTitle { get; set; }
        public string Artist { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string MusicURL { get; set; }
        public string ImageURL { get; set; }
        public Music()
        {
            MusicID = 0;
            MusicTitle = string.Empty;
            Artist = string.Empty;
            Duration = 0;
            ReleaseDate = DateTime.MinValue;
            MusicURL = string.Empty;
            ImageURL = string.Empty;
        }
    }
}
