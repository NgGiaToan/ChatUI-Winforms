using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class UserControl2 : UserControl
    {

        public UserControl2()
        {
            InitializeComponent();
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
                    this.BackColor = System.Drawing.Color.FromArgb(210, 220, 240);
                    label1.ForeColor = System.Drawing.Color.Black;
                    richTextBox1.BackColor = System.Drawing.Color.FromArgb(210, 220, 240);
                    richTextBox1.ForeColor = System.Drawing.Color.Black;
                    richTextBox1.BackColor = System.Drawing.Color.FromArgb(210, 220, 240);
                    richTextBox1.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    this.BackColor = System.Drawing.Color.FromArgb(40, 42, 56);
                    label1.ForeColor = SystemColors.Control;
                    richTextBox1.BackColor = System.Drawing.Color.FromArgb(40, 42, 56);
                    richTextBox1.ForeColor = SystemColors.Control;
                    richTextBox1.BackColor = System.Drawing.Color.FromArgb(40, 42, 56);
                    richTextBox1.ForeColor = SystemColors.Control;
                }
            }
        }

        private string _timebox;
        public string TimeBox
        {
            get { return _timebox;}
            set { _timebox = value; label1.Text = value;}
        }

        private string _img;
        public string Img
        {
            get { return _img; }
            set { 
                _img = value;
                if (value==null ||value.Length < 1)
                {
                    pictureBox1.Visible = false;
                }
                else
                {
                    
                    if (value.Contains("icon"))
                    {
                        this.Size = new Size(this.Width, 80);
                        pictureBox1.ImageLocation = value;
                        pictureBox1.Size = new Size(this.Height-30, this.Height-30);
                        pictureBox1.Location = new Point(15, 15);
                        label1.Location = new Point(this.Width - 60,  65);
                    }
                    else
                    {
                        
                        this.Size = new Size(this.Width, pictureBox1.Height + 15);
                        pictureBox1.ImageLocation = value;
                        label1.Location = new Point(this.Width - 60, pictureBox1.Height - 5);
                    }
                    
                }
                
            }
        }

        private string _vd;
        public string VD
        {
            get { return _vd; }
            set { 
                _vd = value; 
                if (value == null || value.Length < 1)
                {
                    axWindowsMediaPlayer1.Visible = false;
                }
                else
                {
                    this.Size = new Size(this.Width, axWindowsMediaPlayer1.Height + 15);
                    axWindowsMediaPlayer1.URL = value;
                    label1.Location = new Point(this.Width - 60, axWindowsMediaPlayer1.Height-5);
                }
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                if (value == null || value.Length < 1)
                {
                    richTextBox1.Visible = false;
                }
                else
                {
                    richTextBox1.Text = value;
                    int textheight = richTextBox1.Height + 15;
                    int textlocation = richTextBox1.Height - 5;
                    int textsize = richTextBox1.Height;
                    
                    if (value.Length >= 35) {
                        int n = richTextBox1.Text.Length / 35;
                        
                        for (int i = 0; i < n; i++)
                        {
                            textheight = textheight + richTextBox1.Height;
                            textlocation = textlocation + richTextBox1.Height;
                            textsize = textsize + richTextBox1.Height;
                        }
                        richTextBox1.Size = new Size(richTextBox1.Width, textsize);
                    }
                    
                    this.Size = new Size(this.Width, textheight);
                    label1.Location = new Point(this.Width - 60, textlocation);
                }

            }
        }

        private string _ischat;
        public string IsChat
        {
            get { return _ischat; }
            set
            {
                _ischat = value;
                if (value == "True")
                {
                    
                    if (Img == null || Img.Length < 1)
                    {
                        
                        this.Size = new Size(36,36);
                        axWindowsMediaPlayer1.Location = new Point(0, 0);
                        axWindowsMediaPlayer1.Size = this.Size;
                        label1.Visible = false;
                        richTextBox1.Visible = false;
                        pictureBox1.Visible = false;
                    }
                    else
                    {
                        if (Img.Contains("icon"))
                        {
                           
                            this.Size = new Size(20, 20);
                            pictureBox1.Location = new Point(0, 0);
                            pictureBox1.Size = this.Size;
                            this.BackColor = System.Drawing.Color.Transparent;
                            label1.Visible = false;
                            richTextBox1.Visible = false;
                            axWindowsMediaPlayer1.Visible = false;
                        }
                        else
                        {
                            
                            this.Size = new Size(36, 36);
                            pictureBox1.Location = new Point(0, 0);
                            pictureBox1.Size = this.Size;
                            label1.Visible = false;
                            richTextBox1.Visible = false;
                            axWindowsMediaPlayer1.Visible = false;
                        }
                        
                    }
                }
            }
        }

        private string _data;
        public string Data
        {
            get { return _data; }
            set { 
                _data = value; 
                if (value == "True")
                {
                    if(Img == null || Img.Length < 1)
                    {

                        this.Size = new Size(70, 70);
                        axWindowsMediaPlayer1.Location = new Point(0, 0);
                        axWindowsMediaPlayer1.Size = this.Size;
                        label1.Visible = false;
                        richTextBox1.Visible = false;
                        pictureBox1.Visible = false;
                    }
                    else
                    {
                        
                            this.Size = new Size(70, 70);
                            pictureBox1.Location = new Point(0, 0);
                            pictureBox1.Size = this.Size;
                            label1.Visible = false;
                            richTextBox1.Visible = false;
                            axWindowsMediaPlayer1.Visible = false;
                        
                 
                    }
                }
            }
        }

        private string _find;

        public string Find
        {
            get { return _find;  }
            set { 
                _find = value;
                if (value!="") 
                {   
                    
                    int startIndex = 0;
                    while (startIndex < value.Length)
                    {
                        int wordStartIndex = richTextBox1.Text.IndexOf(value);
                        if (wordStartIndex != -1)
                        {
                            richTextBox1.SelectionStart = wordStartIndex;
                            richTextBox1.SelectionLength = value.Length;
                            richTextBox1.SelectionColor = System.Drawing.Color.Yellow;
                        }
                        else
                            break;
                        startIndex += wordStartIndex + value.Length;
                    }
                }
                else
                {
                    richTextBox1.SelectionStart = 0;
                    richTextBox1.SelectAll();
                    richTextBox1.SelectionColor = System.Drawing.Color.Transparent;
                }
            }
        }
        private void UserControl2_Load(object sender, EventArgs e)
        {
            
            richTextBox1.WordWrap = true;
            richTextBox1.MaxLength = 200;
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
