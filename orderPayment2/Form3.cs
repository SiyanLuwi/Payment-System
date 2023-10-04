using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using OrderPayment;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace orderPayment2
{
    public partial class MdiParent : Form
    {
        public MdiParent()
        {
            InitializeComponent();
        }

        private void pOSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MdiChildren frmOrder = new MdiChildren();
            frmOrder.Show();
        }

        private void frm3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void rEPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eXITToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frm3_Load(object sender, EventArgs e)
        {

        }
    }
}
