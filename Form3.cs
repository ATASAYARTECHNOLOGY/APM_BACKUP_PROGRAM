using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideNavSample
{
    public partial class Form3 : OfficeForm
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void buttonX26_Click(object sender, EventArgs e)
        {
            string con = value.conlite;
            value.change_password = txt_pass_customer.Text;

            value.username_customer = txt_user_customer.Text;

            string q = "select * from Tbl_connect_costumer where username='" + value.username_customer + "' and  password='" + value.change_password + "' ";
            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, value.conlite);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.status_pass_table = dda.Fill(dt);
            if (value.status_pass_table == 1)
            {
                this.Visible = false;
                Form1 Form1 = new Form1();
                Form1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Kulancı adı ve ya şifre yanlış", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
