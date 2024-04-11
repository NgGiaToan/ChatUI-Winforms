using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Chat
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void RoundedPB(PictureBox sender)
        {
            System.Drawing.Drawing2D.GraphicsPath obj = new System.Drawing.Drawing2D.GraphicsPath();
            obj.AddEllipse(0, 0, sender.Width, sender.Height);
            Region rg = new Region(obj);
            sender.Region = rg;
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (value == "True")
                {
                    this.BackColor = System.Drawing.Color.FromArgb(200, 210, 230);
                    label1.ForeColor = System.Drawing.Color.Black;
                    label2.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    this.BackColor = System.Drawing.Color.FromArgb(48, 50, 64);
                    label1.ForeColor = SystemColors.Control;
                    label2.ForeColor = SystemColors.Control;
                }
            }
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { 
                _title = value; 
                label1.Text = value;
            }
        }

        private string _img;
        public string Img
        {
            get { return _img; }
            set {
                    _img = value; 
                try
                {
                    pictureBox1.Load(value);
                    RoundedPB(pictureBox1);
                }
                catch 
                {
                    
                }
            }
            
        }
        
    }

}
