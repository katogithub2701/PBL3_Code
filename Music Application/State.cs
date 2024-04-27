using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Application
{
    internal class State
    {
        private static State _instance;
        public static State Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new State();
                }
                return _instance;
            }
        }
        public bool isPlaying {  get; set; } = false;
    }
}

