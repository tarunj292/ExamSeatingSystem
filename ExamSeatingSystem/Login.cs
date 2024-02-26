using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamSeatingSystem
{
    public partial class Login : Form
    {
        private const string ConnectionString = "Data Source=31D-LAB3-45;Initial Catalog=ExamCell;Integrated Security=True"; // Update with your connection string
        public Login()
        {
            InitializeComponent();
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
                            MessageBox.Show("Login successful!");
                            HOME f = new HOME();
                            f.Show();
                            // Proceed to main application or whatever you want to do after successful login
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
            }
        }
    

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox10.Text;
            string password = textBox4.Text;
            string cnfpassword = textBox3.Text;
            string email = textBox9.Text;

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
                    string query = "INSERT INTO users (user_name, user_pass, user_email) VALUES (@Username, @PasswordHash, @Email)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        command.Parameters.AddWithValue("@Email", email);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User registered successfully!");
                            // Optionally, you can clear the textboxes after successful registration
                            textBox10.Clear();
                            textBox9.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            HOME hOME = new HOME();
                            hOME.Show();
                        }
                        else
                        {
                            MessageBox.Show("Failed to register user.");
                        }
                    }
                }
            }
        }
    }
}

