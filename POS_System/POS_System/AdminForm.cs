using iTextSharp.text.pdf;
using iTextSharp.text;
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



namespace POS_System
{
    public partial class AdminForm : Form
    {

        private byte[] imageData;
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=POS_System;Trusted_Connection=True;";

        private CashierForm cashierForm;

        public AdminForm(CashierForm cashierFormInstance)
        {
            InitializeComponent();
            this.cashierForm = cashierFormInstance;
        }

        public AdminForm()
        {
            InitializeComponent();
            this.cashierForm = cashierForm;
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.comboBoxSaleID.SelectedIndexChanged += new System.EventHandler(this.comboBoxSaleID_SelectedIndexChanged);
            this.buttonDeleteSale.Click += new System.EventHandler(this.buttonDeleteSale_Click);
          
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxItemImage.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);

                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, (int)fs.Length);
                }

                this.imageData = imageData;
            }
        }

        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddItemName.Text) ||
        string.IsNullOrWhiteSpace(textBoxAddItemPrice.Text) ||
        string.IsNullOrWhiteSpace(textBoxAddItemQuantity.Text) ||
        imageData == null)
            {
                MessageBox.Show("Please fill in all fields and upload an image.");
                return;
            }

            string itemName = textBoxAddItemName.Text;
            decimal itemPrice;
            int itemQuantity;

            if (!decimal.TryParse(textBoxAddItemPrice.Text, out itemPrice) ||
                !int.TryParse(textBoxAddItemQuantity.Text, out itemQuantity))
            {
                MessageBox.Show("Invalid price or quantity.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Items (ItemName, Price, Stock, ImageData) VALUES (@ItemName, @Price, @Stock, @ImageData)";

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ItemName", itemName);
                    cmd.Parameters.AddWithValue("@Price", itemPrice);
                    cmd.Parameters.AddWithValue("@Stock", itemQuantity);
                    cmd.Parameters.AddWithValue("@ImageData", imageData);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item added successfully!");

                    textBoxAddItemName.Clear();
                    textBoxAddItemPrice.Clear();
                    textBoxAddItemQuantity.Clear();
                    pictureBoxItemImage.Image = null;
                    imageData = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding item: " + ex.Message);
                }
            }

        }

        private void comboBoxItemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedItemId = (int)comboBoxUpdateID.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Price, Quantity FROM Items WHERE ItemID = @ItemID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemID", selectedItemId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBoxUpdateItemName.Text = reader["Name"].ToString();
                    textBoxUpdateItemPrice.Text = reader["Price"].ToString();
                    textBoxUpdateItemQuantity.Text = reader["Quantity"].ToString();
                }
            }
        }

        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Username FROM Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                comboBoxSelectUser.DataSource = dataTable;
                comboBoxSelectUser.DisplayMember = "Username";
                comboBoxSelectUser.ValueMember = "Username";
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            string selectedUsername = comboBoxSelectUser.SelectedValue.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", selectedUsername);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("User deleted successfully!");
            LoadUsers();
        }

        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            if (comboBoxDeleteID.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            int selectedItemID;
            bool isParsed = int.TryParse(comboBoxDeleteID.SelectedItem.ToString(), out selectedItemID);

            if (!isParsed)
            {
                MessageBox.Show("Invalid item ID selected.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Items WHERE ItemID = @ItemID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemID", selectedItemID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Item deleted successfully!");
            LoadItemIdsIntoComboBox();
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string role = comboBoxRole.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@Username, @PasswordHash, @Role)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", password);
                cmd.Parameters.AddWithValue("@Role", role);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("User added successfully!");
            LoadUsers();
        }

        private void comboBoxDeleteID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDeleteID.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid item ID.");
                return;
            }

            int selectedItemID;
            bool isParsed = int.TryParse(comboBoxDeleteID.SelectedItem.ToString(), out selectedItemID);

            if (!isParsed)
            {
                MessageBox.Show("Invalid item ID selected.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ItemName, Stock FROM Items WHERE ItemID = @ItemID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemID", selectedItemID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBoxDeleteItemName.Text = reader["ItemName"].ToString();
                    textBoxDeleteItemQuantity.Text = reader["Stock"].ToString();
                }
                conn.Close();
            }
        }

        private void buttonRemoveUser_Click(object sender, EventArgs e)
        {
            string selectedUsername = comboBoxSelectUser.SelectedValue.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", selectedUsername);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("User deleted successfully!");
            LoadUsers();
        }

        private void comboBoxUpdateID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUpdateID.SelectedItem != null)
            {
                int selectedItemID = (int)comboBoxUpdateID.SelectedItem;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ItemName, Price, Stock FROM Items WHERE ItemID = @ItemID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ItemID", selectedItemID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBoxUpdateItemName.Text = reader["ItemName"].ToString();
                        textBoxUpdateItemPrice.Text = reader["Price"].ToString();
                        textBoxUpdateItemQuantity.Text = reader["Stock"].ToString();
                    }
                    conn.Close();
                }
            }
        }

        private void buttonUpdateItem_Click(object sender, EventArgs e)
        {
            if (comboBoxUpdateID.SelectedItem == null)
            {
                MessageBox.Show("Please select an item ID.");
                return;
            }

            int selectedItemID = (int)comboBoxUpdateID.SelectedItem;
            string itemName = textBoxUpdateItemName.Text;
            decimal itemPrice = decimal.Parse(textBoxUpdateItemPrice.Text);
            int itemStock = int.Parse(textBoxUpdateItemQuantity.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Items SET ItemName = @ItemName, Price = @Price, Stock = @Stock WHERE ItemID = @ItemID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@Price", itemPrice);
                cmd.Parameters.AddWithValue("@Stock", itemStock);
                cmd.Parameters.AddWithValue("@ItemID", selectedItemID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Item updated successfully!");
        }  

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadItemIdsIntoComboBox();
            LoadSaleIDs();
        }
        private void LoadItemIdsIntoComboBox()
        {
            string query = "SELECT ItemId FROM Items";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    comboBoxUpdateID.Items.Clear();
                    comboBoxDeleteID.Items.Clear();

                    while (reader.Read())
                    {
                        int itemId = reader.GetInt32(0);
                        comboBoxUpdateID.Items.Add(itemId);
                        comboBoxDeleteID.Items.Add(itemId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading item IDs: " + ex.Message);
                }
            }
        }

        private void buttonGenerateReport_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateTimePickerFrom.Value.Date;
            DateTime toDate = dateTimePickerTo.Value.Date;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT SaleID, SaleDate, SaleTime, CashierID, TotalAmount, ChangeGiven
        FROM Sales
        WHERE SaleDate BETWEEN @FromDate AND @ToDate
        ORDER BY SaleDate ASC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    StringBuilder report = new StringBuilder();
                    report.AppendLine("SaleID\tDate\tTime\tCashierID\tTotalAmount\tChangeGiven");

                    while (reader.Read())
                    {
                        int saleID = reader.GetInt32(0);
                        DateTime date = reader.GetDateTime(1);
                        TimeSpan time = reader.GetTimeSpan(2);
                        int cashierID = reader.GetInt32(3);
                        decimal totalAmount = reader.GetDecimal(4);
                        decimal changeGiven = reader.GetDecimal(5);

                        string totalAmountFormatted = "Rs. " + totalAmount.ToString("0.00");
                        string changeGivenFormatted = "Rs. " + changeGiven.ToString("0.00");

                        report.AppendLine($"{saleID}\t{date.ToShortDateString()}\t{time}\t{cashierID}\t{totalAmountFormatted}\t{changeGivenFormatted}");
                    }

                    MessageBox.Show("Report generated successfully. Click OK to download the PDF.", "Sales Report");

                    SaveReportAsPDF(report.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error generating report: " + ex.Message);
                }
            }
        }

        private void SaveReportAsPDF(string reportContent)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    pdfDoc.Add(new Paragraph(reportContent.Replace("\t", "    ")));

                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("Report saved successfully as PDF!");
            }
        }

        private void LoadSaleIDs()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SaleID FROM Sales";

                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBoxSaleID.Items.Add(reader.GetInt32(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Sale IDs: " + ex.Message);
                }
            }
        }

        private void comboBoxSaleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedSaleID = (int)comboBoxSaleID.SelectedItem;
            LoadSaleDetails(selectedSaleID);
        }

        private void LoadSaleDetails(int saleID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SaleDate, SaleTime, CashierID, TotalAmount, ChangeGiven FROM Sales WHERE SaleID = @SaleID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaleID", saleID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxSaleDate.Text = reader.GetDateTime(0).ToShortDateString();
                        textBoxSaleTime.Text = reader.GetTimeSpan(1).ToString();
                        textBoxCashierID.Text = reader.GetInt32(2).ToString();
                        textBoxTotalAmount.Text = reader.GetDecimal(3).ToString("C");
                        textBoxChangeGiven.Text = reader.GetDecimal(4).ToString("C");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading sale details: " + ex.Message);
                }
            }
        }

        private void buttonDeleteSale_Click(object sender, EventArgs e)
        {
            if (comboBoxSaleID.SelectedItem != null)
            {
                int selectedSaleID = (int)comboBoxSaleID.SelectedItem;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Sales WHERE SaleID = @SaleID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SaleID", selectedSaleID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sale data deleted successfully.");
                        comboBoxSaleID.Items.Clear();
                        LoadSaleIDs(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting sale data: " + ex.Message);
                    }
                }
            }
        }

        private void ClearSaleDetails()
        {
            textBoxSaleDate.Clear();
            textBoxSaleTime.Clear();
            textBoxCashierID.Clear();
            textBoxTotalAmount.Clear();
            textBoxChangeGiven.Clear();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
