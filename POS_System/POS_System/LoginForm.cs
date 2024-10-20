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

namespace POS_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            // Call method to validate credentials
            if (ValidateCredentials(username, password, out int userId))
            {
                // Set the UserSession values
                UserSession.UserID = userId; // Store the logged-in user's ID
                UserSession.Username = username; // Store the logged-in user's username

                // Open different forms based on user role
                string role = GetUserRole(username);

                if (role == "Admin")
                {
                    MessageBox.Show("Welcome, Admin!");
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                    this.Hide();
                }
                else if (role == "Cashier")
                {
                    MessageBox.Show("Welcome, Cashier!");
                    CashierForm cashierForm = new CashierForm();
                    cashierForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Unknown role.");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        // Method to validate the user credentials
        private bool ValidateCredentials(string username, string password, out int userId)
        {
            bool isValid = false;
            userId = -1; // Initialize userId to an invalid state
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=POS_System;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Query to check if the username and password match
                    string query = "SELECT UserID FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", password); // In real-world apps, use hashed passwords

                        // Get the UserID if valid
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            userId = (int)result; // Assign the valid UserID
                            isValid = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return isValid;
        }


        // Method to get the user's role
        private string GetUserRole(string username)
        {
            string role = string.Empty;
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=POS_System;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Role FROM Users WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            role = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return role;
        }
    

    }
}
