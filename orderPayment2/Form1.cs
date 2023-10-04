using MySql.Data.MySqlClient;
using OrderPayment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace orderPayment2
{
    public partial class frmOrder2 : Form
    {
        public frmOrder2()
        {
            InitializeComponent();
        }
        MySqlConnection myconn;
        string mycon = "Server = localhost; Database = inventory1; Uid = root; Pwd =;";
        MySqlCommand mycmd;
        MySqlCommand mycom;
        MySqlDataReader rdr;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tbl_users WHERE user_name = '" + txtUser.Text + "' AND user_pass = '" + txtPass.Text + "'";
            myconn = new MySqlConnection(mycon);
            
            try
            {
                myconn.Open();
                mycmd = new MySqlCommand(sql, myconn);
                rdr = mycmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    MessageBox.Show("Login Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MdiParent mid = new MdiParent();
                    mid.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Login Credentials",  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPass.Clear();
            txtUser.Clear();
        }
    }
}
