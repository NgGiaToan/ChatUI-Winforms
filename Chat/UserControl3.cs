using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
        }

        private string _img;
        public string Img
        {
            get { return _img; }
            set
            {
                _img = value;
                pictureBox1.ImageLocation = value;
                if (value == null || value.Length < 1)
                {
                    pictureBox1.Visible = false;
                }

            }
        }

        private string _vd;
        public string VD
        {
            get { return _vd; }
            set
            {
                _vd = value;
                axWindowsMediaPlayer1.URL = value;
                if (value == null || value.Length < 1)
                {
                    axWindowsMediaPlayer1.Visible = false;
                }
            }
        }
    }
}
