using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Music_Application
{
    public partial class ucSong : UserControl
    {
        public Music music { get; set;}
        public WMPLib.WindowsMediaPlayer player { get; set;}
        public ucSong()
        {
            InitializeComponent();
        }
        public void load()
        {
            label1.Text = music.MusicTitle;
            label2.Text = music.Artist;
            pictureBox1.BackgroundImage = Image.FromFile(music.ImageURL);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MusicQueue.Instance.addMusic(music);
        }

    }
}
