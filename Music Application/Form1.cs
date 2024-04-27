using Bunifu.UI.WinForms;
using Music_Application.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Music_Application
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        DateTime lastUpdate = DateTime.MinValue;
        bool isPlaying = false;
        private int currentSongIndex = 0;
        public Form1()
        {
            InitializeComponent();
            loadMusic();
            Player.Instance.player.PlayStateChange += Player_PlayStateChange;
        }

        private void bunifuTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            bunifuPages1.SetPage(searchPage);
            wplayer.URL = "https://cdn.glitch.global/6f10d281-90ab-4fd9-b293-20a192c975a7/PhongDoTimEm-WrenEvans.mp3?v=1712988291368";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isPlaying && Player.Instance.player.currentMedia != null && Player.Instance.player.currentMedia.duration != 0)
            {
                double currentTime = Player.Instance.player.controls.currentPosition;
                double totalTime = Player.Instance.player.currentMedia.duration;

                double ratio = currentTime / totalTime;
                DateTime now = DateTime.Now;
                if ((now - lastUpdate).TotalMilliseconds > 1000)
                {
                    musicSlider.Value = (int)(ratio * 100);
                    lastUpdate = now;
                }
                updatecurrentLabel(currentTime);
            }
        }
        private void updatecurrentLabel(double currentTime)
        {
            TimeSpan currentTimeSpan = TimeSpan.FromSeconds(currentTime);
            string currentTimeString = currentTimeSpan.ToString(@"mm\:ss");

            currentLabel.Text = currentTimeString;
        }
        private void updatetotalLabel()
        {
            
            if (Player.Instance.player.currentMedia != null)
            {
                // Lấy tổng thời lượng của bài nhạc
                double totalDuration = Player.Instance.player.currentMedia.duration;

                // Chuyển đổi tổng thời lượng từ giây thành chuỗi định dạng hh:mm:ss
                TimeSpan durationTimeSpan = TimeSpan.FromSeconds(totalDuration);
                string durationString = durationTimeSpan.ToString(@"mm\:ss");

                // Gán giá trị cho totalLabel
                totalLabel.Text = durationString;
            }
        }

        private void musicSlider_Scroll(object sender, Utilities.BunifuSlider.BunifuHScrollBar.ScrollEventArgs e)
        {
            if (isPlaying)
            {
                double newPotition = Player.Instance.player.currentMedia.duration * musicSlider.Value / 100;
                if (Math.Abs(Player.Instance.player.controls.currentPosition - newPotition) > 0.05)
                {
                    Player.Instance.player.controls.currentPosition = newPotition;
                }
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            updatetotalLabel();
            //wplayer.URL = MusicQueue.Instance.musics.Dequeue().MusicURL;
            //if (isPlaying)
            //{
            //    wplayer.controls.pause();
            //    isPlaying = false;
            //    playButton.Image = Properties.Resources.wplay;
            //}
            //else
            //{
            //    wplayer.controls.play();
            //    isPlaying = true;
            //    timer1.Start();
            //    playButton.Image = Properties.Resources.wpause;
            //}
            //isPlaying = true;
            //timer1.Start();
            //Player.Instance.playMusic();
            PlaySong(MusicQueue.Instance.musics[currentSongIndex].MusicURL);
        }
        private void Player_PlayStateChange(int NewState)
        {
            // Khi trạng thái phát thay đổi, kiểm tra nếu bài hát kết thúc và chuyển sang bài tiếp theo
            if ((WMPPlayState)NewState == WMPPlayState.wmppsStopped)
            {
                currentSongIndex++;
                if (currentSongIndex >= MusicQueue.Instance.musics.Count)
                    currentSongIndex = 0;

                PlaySong(MusicQueue.Instance.musics[currentSongIndex].MusicURL);
            }
        }
        private void PlaySong(string filePath)
        {
            Player.Instance.player.URL = filePath;
            Player.Instance.player.controls.play();
        }
        private void musicSlider_MouseDown(object sender, MouseEventArgs e)
        {
            Player.Instance.player.controls.pause();
        }

        private void musicSlider_MouseUp(object sender, MouseEventArgs e)
        {
            Player.Instance.player.controls.play();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(homePage);
        }

        private void volumnButton_Click(object sender, EventArgs e)
        {
            volumnButton.BackgroundImage = Properties.Resources.wmute;
        }

        private void waitingButton_Click(object sender, EventArgs e)
        {
            int startX = 3;
            int startY = 32;
            int scale = 49;
            int countSong = 0;
            bunifuPages1.SetPage(queuePage);
            List<Music> list = MusicQueue.Instance.musics;
            foreach (Music music in list) 
            {
                ucMusicInQueue bar = new ucMusicInQueue();
                bar.music = music;
                bar.load();
                bar.Location = new Point(startX, startY + (countSong++) * scale);
                queuePage.Controls.Add(bar);
            }
        }
        List<Music> listMusic = new List<Music>();
        private void loadMusicToList()
        {
            string s = @"Data Source=DESKTOP-39DLQU7\SQLEXPRESS;Initial Catalog=PBL3;Integrated Security=true";
            SqlConnection sqlConnection = new SqlConnection(s);
            string query = "select * from Music";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            SqlDataReader r = sqlCommand.ExecuteReader();
            while (r.Read()) 
            {
                Music music = new Music()
                {
                    MusicID = Convert.ToInt32(r[0]),
                    MusicTitle = r["MusicTitle"].ToString(),
                    Artist = r["Artist"].ToString(),
                    Duration = Convert.ToInt32(r[4]),
                    ReleaseDate = Convert.ToDateTime(r[5]),
                    MusicURL = r["MusicURL"].ToString(),
                    ImageURL = r["ImageURL"].ToString()
                };
               listMusic.Add(music);
            }
            sqlConnection.Close();
        }
        private void loadMusic()
        {
            loadMusicToList();
            int startX = 3;
            int startY = 32;
            int scale = 49;
            int countSong = 0;
            foreach (Music music in listMusic) 
            {
                ucSong bar = new ucSong();   
                bar.music = music;
                bar.load();
                bar.Location = new Point(startX, startY + (countSong++)*scale);
                homePage.Controls.Add(bar);
            }
        }
    }
}
