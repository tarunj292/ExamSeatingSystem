using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamSeatingSystem
{
    public partial class Login : Form
    {
        private const string ConnectionString = "Data Source=TARUNJOSHI\\SQLEXPRESS;Initial Catalog=ExamCell;Integrated Security=True;";
        public Login()
        {
            InitializeComponent();
            if (Shared.OpenSignUp)
            {
                panel3.Visible = false;
                panel2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginUser();
        }

        private void LoginUser()
        {
            string useremail = textBox2.Text;
            string password = textBox1.Text;
            if (string.IsNullOrWhiteSpace(useremail) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT user_pass FROM Users WHERE user_email = @Useremail";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Useremail", useremail);
                        connection.Open();
                        string hashedPassword = (string)command.ExecuteScalar();

                        if (hashedPassword != null && BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                        {
                            this.Hide();
                            AssignmentPage page = new AssignmentPage();
                            page.Closed += (s, args) => this.Close();
                            page.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox10.Text;
            string password = textBox4.Text;
            string cnfpassword = textBox3.Text;
            string email = textBox9.Text;

            if(!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            if(password.Length < 8)
            {
                MessageBox.Show("Password must be atleast 8 characters long");
                return;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(cnfpassword))
            {
                MessageBox.Show("Please enter username, email, and password.");
                return;
            }

            if (password != cnfpassword)
            {
                MessageBox.Show("Password and confirm password do not match.");
                return;
            }
            else
            {

                // Hash the password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO users (user_name, user_pass, user_email, user_role) VALUES (@Username, @PasswordHash, @Email, @UserRole)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@UserRole", "admin");                       
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            textBox10.Clear();
                            textBox9.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            /*this.Hide();
                            AssignmentPage page = new AssignmentPage();
                            page.Closed += (s, args) => this.Close();
                            page.ShowDialog();*/
                        }
                        else
                        {
                            MessageBox.Show("Failed to register user.");
                        }
                    }
                }
            }
        }

        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
            linkLabel1.Visible = false;
            label2.Text = "               Reset your password";
        }

        private void SendPasswordResetEmail(string email, string resetToken)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("tarunj292@gmail.com", "fsgo xwuk slpr gjwr"),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tarunj292@gmail.com"),
                Subject = "Password Reset",
                Body = $"Dear User,\n\nPlease click the following link to reset your password:\n\nhttps://example.com/reset-password?token={resetToken}\n\nThank you."
            };

            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string email = textBox5.Text;
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Generate a random password reset token (for demonstration purposes)
            string resetToken = Guid.NewGuid().ToString();

            // Send a password reset email to the user
            SendPasswordResetEmail(email, resetToken);
            MessageBox.Show("Password reset email has been sent to your email address.", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panel1.Visible = false;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = false;
            linkLabel1.Visible = true;
            label2.Text = "Please Enter your Username and Password";
        }
    }

    public static class Shared
    {
        public static Boolean OpenSignUp = false;
    }

}
