using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music_Application
{
    public partial class ucMusicInQueue : UserControl
    {
        public Music music { get; set; }
        public ucMusicInQueue()
        {
            InitializeComponent();
        }
        public void load()
        {
            label1.Text = music.MusicTitle;
            label2.Text = music.Artist;
            pictureBox1.BackgroundImage = Image.FromFile(music.ImageURL);
        }
    }
}
