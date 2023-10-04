namespace orderPayment2
{
    partial class MdiParent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuOrder = new System.Windows.Forms.MenuStrip();
            this.pOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pOSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuOrder
            // 
            this.menuOrder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pOSToolStripMenuItem});
            this.menuOrder.Location = new System.Drawing.Point(0, 0);
            this.menuOrder.Name = "menuOrder";
            this.menuOrder.Size = new System.Drawing.Size(1151, 24);
            this.menuOrder.TabIndex = 0;
            this.menuOrder.Text = "menuStrip1";
            // 
            // pOSToolStripMenuItem
            // 
            this.pOSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pOSToolStripMenuItem1,
            this.rEPORTToolStripMenuItem,
            this.eXITToolStripMenuItem});
            this.pOSToolStripMenuItem.Name = "pOSToolStripMenuItem";
            this.pOSToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.pOSToolStripMenuItem.Text = "POS";
            // 
            // pOSToolStripMenuItem1
            // 
            this.pOSToolStripMenuItem1.Name = "pOSToolStripMenuItem1";
            this.pOSToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.pOSToolStripMenuItem1.Text = "POS";
            this.pOSToolStripMenuItem1.Click += new System.EventHandler(this.pOSToolStripMenuItem1_Click);
            // 
            // rEPORTToolStripMenuItem
            // 
            this.rEPORTToolStripMenuItem.Name = "rEPORTToolStripMenuItem";
            this.rEPORTToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rEPORTToolStripMenuItem.Text = "REPORT";
            this.rEPORTToolStripMenuItem.Click += new System.EventHandler(this.rEPORTToolStripMenuItem_Click);
            // 
            // eXITToolStripMenuItem
            // 
            this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            this.eXITToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eXITToolStripMenuItem.Text = "EXIT";
            this.eXITToolStripMenuItem.Click += new System.EventHandler(this.eXITToolStripMenuItem_Click_1);
            // 
            // frm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 713);
            this.Controls.Add(this.menuOrder);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuOrder;
            this.Name = "frm3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS FORM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm3_FormClosed);
            this.Load += new System.EventHandler(this.frm3_Load);
            this.menuOrder.ResumeLayout(false);
            this.menuOrder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuOrder;
        private System.Windows.Forms.ToolStripMenuItem pOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pOSToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rEPORTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
    }
}