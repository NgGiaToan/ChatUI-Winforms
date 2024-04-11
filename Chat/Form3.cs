using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading; 
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography.X509Certificates;

namespace Chat
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        SqlConnection conn = new SqlConnection("Data Source=TOAN\\SQLEXPRESS;Initial Catalog=Userdata;Integrated Security=True");

        public object ControlColection { get; private set; }

        private void RoundedPB(PictureBox sender)
        {
            System.Drawing.Drawing2D.GraphicsPath obj = new System.Drawing.Drawing2D.GraphicsPath();
            obj.AddEllipse(0, 0, sender.Width, sender.Height);
            Region rg = new Region(obj);
            sender.Region = rg;
        }
        

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (panel10.Visible == true)
            {
                panel10.Visible = false;
            }
            else
            {
                panel10.Visible = true;
            }
        }

        


        


        public void UserClick(object sender, EventArgs e)
        {
            UserControl1 control = (UserControl1)sender;
            label2.Text = control.Title;
            label3.Text = control.Title;
            pictureBox2.Load(control.Img);
            pictureBox3.Load(control.Img);
            ChatItem();
            ChatList();
        }
        private void UserItem()
        {
            flowLayoutPanel1.Controls.Clear();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [UserDTB] WHERE Username != 'nguyengiatoan'", conn);
            DataTable tb = new DataTable();
            da.Fill(tb);
            if (tb != null )
            {
                if (tb.Rows.Count > 0)
                {
                    UserControl1[] userControls1 = new UserControl1[tb.Rows.Count];
                    for (int i=0;i<1;i++)
                    {
                        foreach (DataRow row in tb.Rows)
                        {
                            userControls1[i] = new UserControl1();
                            userControls1[i].Color = (toggleDL1.Checked==true).ToString();
                            userControls1[i].Title = row["Fullname"].ToString();
                            userControls1[i].Img = row["Linkimg"].ToString();

                            if (userControls1[i].Name == label2.Text)
                            {
                                flowLayoutPanel1.Controls.Remove(userControls1[i]);
                            }
                            else
                            {
                                flowLayoutPanel1.Controls.Add(userControls1[i]);
                            }
                            userControls1[i].Click += new System.EventHandler(this.UserClick);
                        }
                    }
                }
            }
        }
        private void ChatItem()
        {
            flowLayoutPanel2.Controls.Clear();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM [Table] WHERE Username = '"+ label2.Text +"'", conn);
            DataTable tb2 = new DataTable();
            da2.Fill(tb2);
            if (tb2 != null)
            {
                if (tb2.Rows.Count > 0)
                {
                    UserControl2[] userControls2 = new UserControl2[tb2.Rows.Count];
                    for (int i = 0; i < 1; i++)
                    {
                        foreach (DataRow row in tb2.Rows)
                        {
                            userControls2[i] = new UserControl2();
                            userControls2[i].Color = (toggleDL1.Checked == true).ToString();
                            userControls2[i].Title = row["Title"].ToString();
                            userControls2[i].Img = row["Img"].ToString();
                            userControls2[i].VD = row["VD"].ToString();
                            userControls2[i].TimeBox = row["Time"].ToString();
                            
                            flowLayoutPanel2.Controls.Add(userControls2[i]);
                            
                        }
                    }
                }
            }
        }

        public void pictureBox25_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "File anh|*.jpg; *.png; *.mp4; *.mp3;|All File|*.*";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                
                if (openFile.FileName.Contains(".mp4"))
                {
                    label9.Text = null;
                    label10.Text = openFile.FileName;
                    
                }
                else
                {
                    label9.Text = openFile.FileName;
                    label10.Text = null;
                }
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time,@Title)", conn);
                command.Parameters.AddWithValue("@Username", label2.Text);
                command.Parameters.AddWithValue("@Img", label9.Text);
                command.Parameters.AddWithValue("@VD", label10.Text);
                command.Parameters.AddWithValue("@Time", DateTime.Now);
                command.Parameters.AddWithValue("@Title", "");
                command.ExecuteNonQuery();
                conn.Close();
                ChatItems();
                 
            }
            
        }
        private void ChatItems()
        {

            flowLayoutPanel4.Visible = true;
            UserControl2[] userControls2 = new UserControl2[10];
            for (int i = 0; i < 1; i++)
            {
                userControls2[i] = new UserControl2();
                userControls2[i].Img = label9.Text;
                userControls2[i].VD = label10.Text;
                userControls2[i].IsChat = "True";
                flowLayoutPanel4.Controls.Add(userControls2[i]);
            }
        }

        private void ChatList()
        {
            flowLayoutPanel3.Controls.Clear();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM [Table] WHERE Username = '" + label2.Text + "'", conn);
            DataTable tb2 = new DataTable();
            da2.Fill(tb2);
            if (tb2 != null)
            {
                if (tb2.Rows.Count > 0)
                {
                    UserControl2[] userControls2 = new UserControl2[tb2.Rows.Count];
                    for (int i = 0; i < 1; i++)
                    {
                        foreach (DataRow row in tb2.Rows)
                        {
                            userControls2[i] = new UserControl2();
                            userControls2[i].Color = (toggleDL1.Checked == true).ToString();
                            userControls2[i].Title = row["Title"].ToString();
                            userControls2[i].Img = row["Img"].ToString();
                            userControls2[i].VD = row["VD"].ToString();
                            userControls2[i].TimeBox = row["Time"].ToString();
                            userControls2[i].Data = "True";
                            if (!row["Img"].ToString().Contains("icon") && (row["Img"].ToString().Length >= 1 || row["VD"].ToString().Length >=1))
                            {
                                flowLayoutPanel3.Controls.Add(userControls2[i]);
                            }
                            else
                            { 
                                flowLayoutPanel3.Controls.Remove(userControls2[i]);
                            }

                        }
                    }
                }
            }
        }

        

        private void toggleDL1_CheckedChanged(object sender, EventArgs e)
        {
            UserItem();
            ChatItem();
            if (toggleDL1.Checked)
            {
                panel2.BackColor = Color.FromArgb(200, 210, 230);
                panel1.BackColor = Color.FromArgb(210, 220, 240);
                panel3.BackColor = Color.FromArgb(210, 220, 240);
                panel4.BackColor = Color.FromArgb(210, 220, 240);
                panel5.BackColor = Color.FromArgb(210, 220, 240);
                panel6.BackColor = Color.FromArgb(200,210,230);
                textBox1.BackColor = Color.FromArgb(210, 220, 240);
                textBox2.BackColor = Color.FromArgb(200, 210, 230);
                textBox1.ForeColor = System.Drawing.Color.Black;
                textBox2.ForeColor = System.Drawing.Color.Black;
                label1.ForeColor = System.Drawing.Color.Black;
                label2.ForeColor = System.Drawing.Color.Black;
                label3.ForeColor = System.Drawing.Color.Black;
                label4.ForeColor = System.Drawing.Color.Black;
                label5.ForeColor = System.Drawing.Color.Black;
                label6.ForeColor = System.Drawing.Color.Black;
                label7.ForeColor = System.Drawing.Color.Black;
                label8.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                panel2.BackColor = Color.FromArgb(48, 50, 64);
                panel1.BackColor = Color.FromArgb(40, 42, 56);
                panel3.BackColor = Color.FromArgb(40, 42, 56);
                panel4.BackColor = Color.FromArgb(40, 42, 56);
                panel5.BackColor = Color.FromArgb(40, 42, 56);
                panel6.BackColor = Color.FromArgb(48, 50, 64);
                textBox1.BackColor = Color.FromArgb(40, 42, 56);
                textBox2.BackColor = Color.FromArgb(48, 50, 64);
                textBox1.ForeColor = SystemColors.Control;
                textBox2.ForeColor = SystemColors.Control;
                label1.ForeColor = SystemColors.Control;
                label2.ForeColor = SystemColors.Control;
                label3.ForeColor = SystemColors.Control;
                label4.ForeColor = SystemColors.Control;
                label5.ForeColor = SystemColors.Control;
                label6.ForeColor = SystemColors.Control;
                label7.ForeColor = SystemColors.Control;
                label8.ForeColor = SystemColors.Control;
            }
            if (button1.Visible == true)
            {
                pictureBox4_Click(sender, e);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "English") 
            {
                label4.Text = "TODAY'S ACTIVE CHAT";
                label5.Text = "Theme";
                label6.Text = "Languge";
                label7.Text = "Img and Video";
                label8.Text = "Search";
            }
            else 
            {
                label4.Text = "Trò chuyện trực tuyến";
                label5.Text = "Chủ đề";
                label6.Text = "Ngon ngữ";
                label7.Text = "Ảnh và Video";
                label8.Text = "Tìm kiếm";
            }
            this.Controls.Clear();
            InitializeComponent();
            RightToLeftLayout = true;
        }


        private void pictureBox26_Click(object sender, EventArgs e)
        {
            panel10.Visible= false;
            if (flowLayoutPanel4.Visible == false && textBox2.Text != "Aa")
            {
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
                command.Parameters.AddWithValue("@Username", label2.Text);
                command.Parameters.AddWithValue("@Img", "");
                command.Parameters.AddWithValue("@VD", "");
                command.Parameters.AddWithValue("@Time", DateTime.Now);
                command.Parameters.AddWithValue("@Title", textBox1.Text);
                command.ExecuteReader();
                conn.Close();
                textBox1.Text = "";
                ChatItem();
            }
            else
            {
                flowLayoutPanel4.Controls.Clear();
                ChatItem();
                ChatList();
            }
            flowLayoutPanel4.Visible = false;
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon1.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD","");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title","");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon2.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();

        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon3.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon4.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon5.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon6.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon7.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon8.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon9.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon10.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon11.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon12.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon13.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon14.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon15.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\ICON\\icon16.png";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\IMG\\gif1.gif";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\IMG\\gif2.gif";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\IMG\\gif3.gif";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            label9.Text = "C:\\Users\\giato\\Downloads\\Workspace\\C#\\IMG\\gif4.gif";
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Table] VALUES(@Username, @Img, @VD, @Time, @Title)", conn);
            command.Parameters.AddWithValue("@Username", label2.Text);
            command.Parameters.AddWithValue("@Img", label9.Text);
            command.Parameters.AddWithValue("@VD", "");
            command.Parameters.AddWithValue("@Time", DateTime.Now);
            command.Parameters.AddWithValue("@Title", "");
            command.ExecuteReader();
            conn.Close();
            ChatItems();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
            if (textBox2.Text!="")
            {
                button1.Visible = true;
                flowLayoutPanel2.Controls.Clear();
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM [Table] WHERE Username = '" + label2.Text + "'", conn);
                DataTable tb2 = new DataTable();
                da2.Fill(tb2);
                if (tb2 != null)
                {
                    if (tb2.Rows.Count > 0)
                    {
                        UserControl2[] userControls2 = new UserControl2[tb2.Rows.Count];
                        for (int i = 0; i < 1; i++)
                        {
                            foreach (DataRow row in tb2.Rows)
                            {
                                userControls2[i] = new UserControl2();
                                userControls2[i].Color = (toggleDL1.Checked == true).ToString();
                                userControls2[i].Title = row["Title"].ToString();
                                userControls2[i].Img = row["Img"].ToString();
                                userControls2[i].VD = row["VD"].ToString();
                                userControls2[i].TimeBox = row["Time"].ToString();
                                userControls2[i].Find = textBox2.Text;

                                flowLayoutPanel2.Controls.Add(userControls2[i]);

                            }
                        }
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            UserItem();
            ChatItem();
            ChatList();
            UserClick(sender,e);
            RoundedPB(pictureBox1);
            RoundedPB(pictureBox2);
            RoundedPB(pictureBox3);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Aa")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Aa";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            if (textBox2.Text != "")
            {
                flowLayoutPanel2.Controls.Clear();
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM [Table] WHERE Username = '" + label2.Text + "'", conn);
                DataTable tb2 = new DataTable();
                da2.Fill(tb2);
                if (tb2 != null)
                {
                    if (tb2.Rows.Count > 0)
                    {
                        UserControl2[] userControls2 = new UserControl2[tb2.Rows.Count];
                        for (int i = 0; i < 1; i++)
                        {
                            foreach (DataRow row in tb2.Rows)
                            {
                                userControls2[i] = new UserControl2();
                                userControls2[i].Color = (toggleDL1.Checked == true).ToString();
                                userControls2[i].Title = row["Title"].ToString();
                                userControls2[i].Img = row["Img"].ToString();
                                userControls2[i].VD = row["VD"].ToString();
                                userControls2[i].TimeBox = row["Time"].ToString();
                                userControls2[i].Find = "";
                                flowLayoutPanel2.Controls.Add(userControls2[i]);
                            }
                        }
                    }
                }
            }
            textBox2.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void userControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
