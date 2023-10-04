using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X500;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace OrderPayment
{
    public partial class MdiChildren : Form
    {
        public MdiChildren()
        {
            
            InitializeComponent();
        }
        MySqlConnection myconn;
        string mycon = "Server = localhost; Database = inventory1; Uid = root; Pwd =;";
        MySqlCommand mycmd;
        MySqlCommand mycom;
        MySqlDataReader rdr;


        private void frmOrder_Load(object sender, EventArgs e)
        {
            //loadProduct(lvOrder);
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            //MessageBox.Show("Connected to MySQL Server", "Connection Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtQuantity.Enabled = false;
            loadProduct(cboProdID);
            this.CenterToParent();


        }

        private void cboProdID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM product WHERE prod_id = '" + cboProdID.Text + "'";
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            mycmd = new MySqlCommand(sql, myconn);
            rdr = mycmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    txtDesc.Text = rdr[1].ToString();
                    txtPrice.Text = rdr[2].ToString();
                    txtAvail.Text = rdr[3].ToString();
                }
            }
            myconn.Close();
            myconn.Dispose();
            rdr.Close();
            txtQuantity.Enabled = true;

        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void loadProduct(ComboBox cboProdID)
        {
            string sql = "SELECT * FROM product";
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            mycmd = new MySqlCommand(sql, myconn);
            mycmd.ExecuteNonQuery();
            rdr = mycmd.ExecuteReader();
            while (rdr.Read())
            {
                cboProdID.Items.Add(rdr["prod_id"].ToString());
            }
            myconn.Close();
            myconn.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text) || Convert.ToInt32(txtQuantity.Text) <= 0)
            {
                MessageBox.Show("The Quantity should not be zero!", "Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!stockCheck(cboProdID.Text.ToString(), Convert.ToInt32(txtQuantity.Text)))
            {
                MessageBox.Show("Insufficient Stock!", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ListViewItem lvItem = lvOrder.Items.Add(cboProdID.Text);
                lvItem.SubItems.Add(txtDesc.Text);
                lvItem.SubItems.Add(txtPrice.Text);
                lvItem.SubItems.Add(txtQuantity.Text);

                int qt1 = int.Parse(txtQuantity.Text);
                int qt2 = int.Parse(txtPrice.Text);

                int total = qt1 * qt2;
                lvItem.SubItems.Add(total.ToString());

                CalculateTotalAmount();

                defaultSet();
            }

        }
        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (lvOrder.Items.Count < 0)
            {
                MessageBox.Show("Please Fill In Your Cart!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string info = "====================================\n\n" +
                          "\t CHECKOUT INFORMATION \n\n" +
                          "====================================\n\n";
            MySqlConnection myconn = new MySqlConnection(mycon);
            myconn.Open();

            try
            {

                for (int i = 0; i < lvOrder.Items.Count; i++)
                {
                    ListViewItem itm = lvOrder.Items[i];
                    int quantity = int.Parse(itm.SubItems[3].Text);
                    string productId = itm.Text;
                    decimal price = decimal.Parse(itm.SubItems[2].Text);
                    decimal itemTotal = price * quantity;


                    // Check if the quantity is available in stock
                    string checkQuery = "SELECT stocks FROM product WHERE prod_id='" + productId + "'";
                    mycmd = new MySqlCommand(checkQuery, myconn);
                    int currStock = Convert.ToInt32(mycmd.ExecuteScalar());

                    if (currStock < quantity)
                    {
                        // Insufficient Stock
                        MessageBox.Show("Insufficient stock for product with ID: " + productId, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // exit
                    }

                    // Insert the order into tblout
                    string insertQuery = "INSERT INTO tblout (Product_id, price, qty) VALUES ('" + productId + "', " + itm.SubItems[2].Text + ", " + quantity + ")";
                    mycmd = new MySqlCommand(insertQuery, myconn);
                    mycmd.ExecuteNonQuery();

                    // Update the stock quantity
                    string updateStockQuery = "UPDATE product SET stocks=stocks-" + quantity + " WHERE prod_id='" + productId + "'";
                    mycom = new MySqlCommand(updateStockQuery, myconn);
                    mycom.ExecuteNonQuery();
                    info += itm.SubItems[0].Text + "  :   " + itm.SubItems[1].Text + "  :   " + itm.SubItems[3].Text +
                           "x =  " + itm.SubItems[2].Text + " \n\n ";
                }


                info += "====================================\n\n\n\n" +
                                 "TOTAL AMOUNT : " + txtTotal.Text + "\n\n\n" +
                                 "\t THANK YOU FOR YOUR PURCHASE!";
                MessageBox.Show(info, "Order Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                defaultSet();
                lvOrder.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotalAmount()
        {
            int totalAmount = 0;

            foreach (ListViewItem item in lvOrder.Items)
            {
                int amount;
                if (int.TryParse(item.SubItems[4].Text, out amount))
                {
                    totalAmount += amount;
                }
            }

            txtTotal.Text = totalAmount.ToString();
        }

        private bool stockCheck(string prodId, int quantity)
        {
            string sql = "SELECT stocks FROM product WHERE prod_id = '" + prodId + "'";
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            mycmd = new MySqlCommand(sql, myconn);
            rdr = mycmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    int availStock = int.Parse(rdr["stocks"].ToString());
                    rdr.Close();
                    myconn.Close();

                    // Check if there is enough stock for the selected product
                    return quantity <= availStock;
                }
            }

            rdr.Close();
            myconn.Close();
            return false; // Product not found, assume insufficient stock
        }

        void UpdateOrderAndStock(string productId, int quantity, string price)
        {
            // Insert the order into tblout
            string insertOrderQuery = "INSERT INTO tblout (Product_id, price, qty) VALUES ('" + productId + "', " + price + ", " + quantity + ")";
            mycmd = new MySqlCommand(insertOrderQuery, myconn);
            mycmd.ExecuteNonQuery();

            // Update the stock quantity
            string updateStockQuery = "UPDATE product SET stocks=stocks-" + quantity + " WHERE prod_id='" + productId + "'";
            mycom = new MySqlCommand(updateStockQuery, myconn);
            mycom.ExecuteNonQuery();
        }
        private void defaultSet()
        {
            cboProdID.SelectedIndex = -1;
            txtDesc.Text = null;
            txtQuantity.Text = null;
            txtPrice.Text = null;
            txtAvail.Text = null;
            txtQuantity.Text = null;
            txtQuantity.Enabled = false;
        }

        private void frmOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        /*private void loadProduct(ListView listView)
           {

        string sql = "SELECT * FROM product ORDER by prod_id";   
        myconn = new MySqlConnection(mycon);
        myconn.Open();
        mycmd = new MySqlCommand(sql, myconn);
        rdr = mycmd.ExecuteReader();
        listView.Items.Clear();

        if (rdr.HasRows)
        {
        while (rdr.Read())
        {

        listView.Items.Add(rdr.GetString(0));
        listView.Items[listView.Items.Count - 1].SubItems.Add(rdr[1].ToString());
        listView.Items[listView.Items.Count - 1].SubItems.Add(rdr[2].ToString());
        listView.Items[listView.Items.Count - 1].SubItems.Add(rdr[3].ToString());

        }
       }   
    }*/

    }
}
