using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class CashierForm : Form
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=POS_System;Trusted_Connection=True;";
        public CashierForm()
        {
            InitializeComponent();
            textBoxCustomerPayment.TextChanged += textBoxCustomerPayment_TextChanged;
            FlowLayoutPanel flowLayoutPanelItems = new FlowLayoutPanel();
            flowLayoutPanelItems.Dock = DockStyle.Left;
            flowLayoutPanelItems.AutoScroll = true;
            flowLayoutPanelItems.Size = new Size(200, this.Height);

            this.Controls.Add(flowLayoutPanelItems);
            LoadItemsForCashier();
        }

        private List<Item> GetAllItemsFromDatabase()
        {
            List<Item> items = new List<Item>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ItemId, ItemName, Price, Stock, ImageData FROM Items";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Item item = new Item
                        {
                            ItemId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Quantity = reader.GetInt32(3),
                            ImageData = reader["ImageData"] as byte[]
                        };
                        items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving items: " + ex.Message);
                }
            }

            return items;
        }
        private void LoadItemsForCashier()
        {
            flowLayoutPanelItems.Controls.Clear();

            var items = GetAllItemsFromDatabase();

            foreach (var item in items)
            {
                Panel itemPanel = new Panel
                {
                    Size = new Size(150, 200),
                    Margin = new Padding(5)
                };

                PictureBox pictureBoxItem = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(150, 150),
                    Tag = item
                };

                if (item.ImageData != null)
                {
                    using (MemoryStream ms = new MemoryStream(item.ImageData))
                    {
                        pictureBoxItem.Image = Image.FromStream(ms);
                    }
                }

                pictureBoxItem.Click += (s, e) => AddItemToCart(item);

                Label labelItemName = new Label
                {
                    Text = item.Name,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true
                };

                itemPanel.Controls.Add(pictureBoxItem);
                itemPanel.Controls.Add(labelItemName);

                flowLayoutPanelItems.Controls.Add(itemPanel);
            }
        }

        private decimal totalPrice = 0;

        private void AddItemToCart(Item item)
        {
            if (item.Quantity <= 0)
            {
                MessageBox.Show($"Item '{item.Name}' is out of stock.");
                return;
            }

            var existingCartItemPanel = flowLayoutPanelCart.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Tag is Item cartItem && cartItem.ItemId == item.ItemId);

            if (existingCartItemPanel != null)
            {
                Label quantityLabel = existingCartItemPanel.Controls.OfType<Label>()
                    .FirstOrDefault(l => l.Name == "labelQuantity");

                int currentQuantity = int.Parse(quantityLabel.Text);

                if (currentQuantity < item.Quantity)
                {
                    currentQuantity++;
                    totalPrice += item.Price;
                    quantityLabel.Text = currentQuantity.ToString();
                    UpdateTotalPrice();
                }
                else
                {
                    MessageBox.Show($"Cannot add more of '{item.Name}'. Maximum stock reached.");
                }
            }
            else
            {
                Panel cartItemPanel = new Panel
                {
                    Size = new Size(350, 50),
                    Margin = new Padding(5),
                    Tag = item
                };

                Label labelItemName = new Label
                {
                    Text = item.Name,
                    Size = new Size(120, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                Label labelItemPrice = new Label
                {
                    Text = "Rs." + item.Price.ToString("0.00"),
                    Size = new Size(60, 25),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label labelQuantity = new Label
                {
                    Name = "labelQuantity",
                    Text = "1",
                    Size = new Size(30, 25),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button buttonRemove = new Button
                {
                    Text = "Remove",
                    Size = new Size(60, 25),
                    AutoSize = false
                };

                buttonRemove.Click += (s, e) =>
                {
                    int currentQuantity = int.Parse(labelQuantity.Text);
                    if (currentQuantity > 1)
                    {
                        currentQuantity--;
                        labelQuantity.Text = currentQuantity.ToString();
                        totalPrice -= item.Price;
                        UpdateTotalPrice();
                    }
                    else
                    {
                        flowLayoutPanelCart.Controls.Remove(cartItemPanel);
                        totalPrice -= item.Price;
                        UpdateTotalPrice();
                    }
                };

                cartItemPanel.Controls.Add(labelItemName);
                cartItemPanel.Controls.Add(labelItemPrice);
                cartItemPanel.Controls.Add(labelQuantity);
                cartItemPanel.Controls.Add(buttonRemove);

                labelItemName.Location = new Point(5, 10);
                labelItemPrice.Location = new Point(130, 10);
                labelQuantity.Location = new Point(200, 10);
                buttonRemove.Location = new Point(250, 10);

                flowLayoutPanelCart.Controls.Add(cartItemPanel);

                totalPrice += item.Price;
            }

            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            textBoxTotalAmount.Text = totalPrice.ToString("0.00");
        }

        private void RemoveSelectedItem(Panel selectedItemPanel, decimal itemPrice)
        {
            flowLayoutPanelCart.Controls.Remove(selectedItemPanel);
            totalPrice -= itemPrice;
            textBoxTotalAmount.Text = totalPrice.ToString("C");
        }


        private void UpdateItemStockAfterSale(int itemId, int quantitySold)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Items SET Stock = Stock - @QuantitySold WHERE ItemID = @ItemID AND Stock >= @QuantitySold";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemID", itemId);
                cmd.Parameters.AddWithValue("@QuantitySold", quantitySold);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected == 0)
                {
                    MessageBox.Show($"Not enough stock for item {itemId}. Sale cannot be completed.");
                }
            }
        }
        private void buttonCheckout_Click(object sender, EventArgs e)
        {
            Checkout();
        }

        private void Checkout()
        {
            if (flowLayoutPanelCart.Controls.Count > 0)
            {
                decimal totalAmount = totalPrice;
                decimal changeGiven = 0;

                if (!decimal.TryParse(textBoxCustomerPayment.Text, out decimal customerMoney) || customerMoney < totalAmount)
                {
                    MessageBox.Show("Not enough payment provided.");
                    return;
                }

                changeGiven = customerMoney - totalAmount;
                foreach (Panel cartItemPanel in flowLayoutPanelCart.Controls)
                {
                    Item item = (Item)cartItemPanel.Tag;
                    Label quantityLabel = cartItemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "labelQuantity");
                    int quantitySold = int.Parse(quantityLabel.Text);

                    UpdateItemStockAfterSale(item.ItemId, quantitySold);
                }
                InsertSale(totalAmount, changeGiven);
                MessageBox.Show("Checkout successful!");
                ClearCart();
            }
            else
            {
                MessageBox.Show("No items in the cart to checkout.");
            }
        }

        private void ClearCart()
        {
            flowLayoutPanelCart.Controls.Clear();
            totalPrice = 0;
            UpdateTotalPrice();
            textBoxCustomerPayment.Clear();
            textBoxChange.Text = "0.00";
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            flowLayoutPanelCart.Controls.Clear();
            textBoxCustomerPayment.Clear();
            textBoxChange.Clear();
            totalPrice = 0;
            textBoxTotalAmount.Text = "0.00";
        }

        private void textBoxCustomerPayment_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBoxCustomerPayment.Text, out decimal customerMoney))
            {
                if (customerMoney >= totalPrice)
                {
                    decimal change = customerMoney - totalPrice;
                    textBoxChange.Text = change.ToString("0.00");
                }
                else
                {
                    textBoxChange.Text = "Not enough money!";
                }
            }
            else
            {
                textBoxChange.Text = "Enter valid amount!";
            }
        }
        private void OpenAdminForm()
        {
            AdminForm adminForm = new AdminForm(this);
            adminForm.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            this.Close();
        }

        private void buttonPrintRecipt_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(PrintReceipt);
            printDoc.Print();
        }

        private void PrintReceipt(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 12);
            float fontHeight = font.GetHeight();

            int startX = 10;
            int startY = 10;
            int offset = 40;

            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            string currentTime = DateTime.Now.ToString("hh:mm tt");

            graphics.DrawString("Shop Receipt", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY);
            offset += (int)fontHeight + 10;

            graphics.DrawString($"Date: {currentDate}", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString($"Time: {currentTime}", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontHeight + 20;

            foreach (Panel cartItemPanel in flowLayoutPanelCart.Controls)
            {
                Item item = (Item)cartItemPanel.Tag;
                Label quantityLabel = cartItemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "labelQuantity");

                int quantity = int.Parse(quantityLabel.Text);
                string productLine = $"{item.Name} x{quantity} @ Rs.{item.Price} = ${quantity * item.Price}";

                graphics.DrawString(productLine, font, new SolidBrush(Color.Black), startX, startY + offset);
                offset += (int)fontHeight + 5;
            }

            offset += 20;
            graphics.DrawString($"Total: Rs.{totalPrice.ToString("0.00")}", font, new SolidBrush(Color.Black), startX, startY + offset);
        }
        private void InsertSale(decimal totalAmount, decimal changeGiven)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Sales (SaleDate, SaleTime, CashierID, TotalAmount, ChangeGiven) VALUES (@SaleDate, @SaleTime, @CashierID, @TotalAmount, @ChangeGiven)";
                SqlCommand cmd = new SqlCommand(query, conn);

                int cashierID = UserSession.UserID;
                cmd.Parameters.AddWithValue("@CashierID", cashierID);

                cmd.Parameters.AddWithValue("@SaleDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@SaleTime", DateTime.Now.TimeOfDay);

                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                cmd.Parameters.AddWithValue("@ChangeGiven", changeGiven);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting sale: " + ex.Message);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
