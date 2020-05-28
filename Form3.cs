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

        private void Form3_Load(object sender, EventArgs e)
        {
            this.CloseEnabled = false;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.BalloonTipText = "Closed";
            notifyIcon1.ShowBalloonTip(10000);
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = true;
            this.Visible = false;


            Form1 frm1 = new Form1();
            frm1.Visible = false;

            Form2 frm2 = new Form2();
            frm2.Visible = false;


            Form3 frm3 = new Form3();
            frm3.Visible = false;

        }
        private void data_pass_user()
        {


            string q;
            q = "select * from Tbl_connect_costumer ";

            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, value.conlite);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.status_pass_table = dda.Fill(dt);

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {


            data_pass_user();

            string q;
            SQLiteConnection con = new SQLiteConnection(value.conlite);
            q = "select * from Tbl_connect_costumer ";
            SQLiteDataAdapter da99 = new SQLiteDataAdapter(q, con);
            DataSet ds99 = new DataSet();
            da99.Fill(ds99);

            string statulogin = ds99.Tables[0].Rows[0][2].ToString();



            if ((Convert.ToInt32(value.status_pass_table) == 1) && (Convert.ToInt32(statulogin) == 2))
            {
                notifyIcon1.Visible = false;

               // timer1.Enabled = false;
                Form3 frm_pass = new Form3();
                frm_pass.Visible = true;

                Form1 frm1 = new Form1();

                frm1.Visible = false;

                Form2 frm2 = new Form2();
                frm2.Visible = false;




            }
            else

            {
                notifyIcon1.Visible = false;

                Form1 form1 = new Form1();
                form1.Visible = true;



                Form2 frm2 = new Form2();
                frm2.Visible = false;

                Form3 frm3 = new Form3();

                frm3.Visible = false;

            }

           // timer1.Enabled = false;




        }
    }
    }

