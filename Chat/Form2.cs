using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace Chat
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=TOAN\\SQLEXPRESS;Initial Catalog=Userdata;Integrated Security=True");
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM UserDTB WHERE Username = 'nguyengiatoan'", conn);
                DataTable tb = new DataTable();
                da.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                    foreach (DataRow row in tb.Rows)
                    {
                        if (textBox1.Text == "nguyengiatoan" && textBox2.Text == row["Password"].ToString())
                        {
                            Form3 newForm = new Form3();
                            newForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password.");
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        string randomCode;
        public static string to;
        private void label2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            randomCode = (rand.Next(999999)).ToString();
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress("giatoantestemail@gmail.com");
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mail.From.Address, "ayzl vgzl cizu piid");
            smtp.Host = "smtp.gmail.com";

            //recipient
            mail.To.Add(new MailAddress("giatoan2003.py@gmail.com"));
            mail.IsBodyHtml = true;
            mail.Subject = "New Password";
            mail.Body = "your reset code is " + randomCode ;

            smtp.Send(mail);
            MessageBox.Show("Email sent successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SqlConnection conn = new SqlConnection("Data Source=TOAN\\SQLEXPRESS;Initial Catalog=Userdata;Integrated Security=True");
            conn.Open();
            SqlCommand command = new SqlCommand("UPDATE UserDTB SET Password = @Password WHERE Username = @Username", conn);
            command.Parameters.AddWithValue("@Password", randomCode.ToString());
            command.Parameters.AddWithValue("@Username", "nguyengiatoan");
            command.ExecuteReader();
            conn.Close();

        }
    }
}
