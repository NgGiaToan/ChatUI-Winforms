using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics.Tracing;

namespace Chat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=TOAN\\SQLEXPRESS;Initial Catalog=Userdata;Integrated Security=True");
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "File anh|*.jpg; *.png;|All File|*.*";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(openFile.FileName);
                label10.Text= openFile.FileName;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
                {
                    int x = check(textBox1.Text);
                    if (x == 0)
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand("INSERT INTO UserDTB VALUES(@Username, @Password, @Phonenumber, @Linkimg, @Fullname)", conn);
                        command.Parameters.AddWithValue("@Username", textBox1.Text);
                        command.Parameters.AddWithValue("@Password", textBox2.Text);
                        command.Parameters.AddWithValue("@Phonenumber", textBox4.Text);
                        command.Parameters.AddWithValue("@Linkimg", label10.Text);
                        command.Parameters.AddWithValue("@Fullname", textBox3.Text);
                        command.ExecuteReader();
                        conn.Close();
                        MessageBox.Show("Successfully");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        pictureBox2.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("datontai");
                    }
                }
                else
                {
                    MessageBox.Show("Fill textbox");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            int check(string x)
            {
                conn.Open();
                string query = "select count(*) from UserDTB where Username='" + x + "'";
                SqlCommand command = new SqlCommand(query, conn);
                int v = (int)command.ExecuteScalar();
                conn.Close();
                return v;
            }

        }
    }
}
