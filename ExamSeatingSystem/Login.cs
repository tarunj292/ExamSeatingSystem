using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamSeatingSystem
{
    public partial class Login : Form
    {
        //private const string ConnectionString = "Data Source=TARUNJOSHI\\SQLEXPRESS;Initial Catalog=ExamCell;Integrated Security=True;";
        private const string ConnectionString = "Data Source=SHORT-FEET\\SQLEXPRESS;Initial Catalog=ExamCell;Integrated Security=True;";

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
            string username = textBox2.Text;
            string password = textBox1.Text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT user_pass FROM Users WHERE user_name = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
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

        }
    }

    public static class Shared
    {
        public static Boolean OpenSignUp = false;
    }

}
