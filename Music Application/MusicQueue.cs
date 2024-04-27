using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Application
{
    internal class MusicQueue
    {
        public List<Music> musics { get; set; }
        public static MusicQueue _Instance;
        public static MusicQueue Instance
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new MusicQueue();
                return _Instance;
            }
            private set { }
        }
        public MusicQueue() 
        {
            musics = new List<Music>();
        }
        public void addMusic(Music music)
        {
            musics.Add(music);
        }
    }
}
