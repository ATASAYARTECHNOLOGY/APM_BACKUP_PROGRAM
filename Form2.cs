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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Visible = false;
            data_pass_user();
            if (Convert.ToInt32(value.status_pass_table) == 0)
            {

                timer1.Enabled = false;
                Form1 Form1 = new Form1();
                Form1.ShowDialog();


            }
            else
             if (Convert.ToInt32(value.status_pass_table) == 1)
            {
                string q;
                SQLiteConnection con = new SQLiteConnection(value.conlite);
                q = "select * from Tbl_connect_costumer ";
                SQLiteDataAdapter da99 = new SQLiteDataAdapter(q, con);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);

                string statulogin = ds99.Tables[0].Rows[0][2].ToString();
                if ((Convert.ToInt32(value.status_pass_table) == 1) && (Convert.ToInt32(statulogin) == 2))
                {

                    timer1.Enabled = false;
                    Form3 frm_pass = new Form3();
                    frm_pass.ShowDialog();



                }
                else
            
                {
                    timer1.Enabled = false;
                    Form1 Form1 = new Form1();
                    Form1.ShowDialog();

                }

            }


            timer1.Enabled = false;
            this.Visible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void data_pass_user()
        {

            int status = 2;
            string q;
            //q = "select [status] from Tbl_connect_costumer where( status='" + status + "')";
            q = "select [status] from Tbl_connect_costumer ";

            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, value.conlite);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.status_pass_table = dda.Fill(dt);

        }
        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
