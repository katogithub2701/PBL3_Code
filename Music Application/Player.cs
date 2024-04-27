using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using WMPLib;

namespace Music_Application
{
    internal class Player
    {
        private int currentSongIndex = 0;
        public WMPLib.WindowsMediaPlayer player {  get; set; }
        public Player()
        {
            player = new WMPLib.WindowsMediaPlayer();
            player.settings.volume = 30;
        }
        public static Player _Instance;
        public static Player Instance
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new Player();
                return _Instance;
            }
            private set { }
        }
        public void playMusic()
        {
            State.Instance.isPlaying = true;
            player.URL = MusicQueue.Instance.musics[currentSongIndex].MusicURL;
            player.controls.play();
        }
        private void Player_PlayStateChange(int NewState)
        {
            if (NewState == 8)
            {
                currentSongIndex++;
                if (currentSongIndex < MusicQueue.Instance.musics.Count)
                    playMusic();
            }
        }
        public void playingMusic()
        {
            player.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
        }
        public void pauseMusic()
        {

        }
    }
}
