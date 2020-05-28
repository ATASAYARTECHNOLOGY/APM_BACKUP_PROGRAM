using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.IO;
using System.Data.Sql;
using System.Net.Mail;
using System.Net;
using System.IO.Compression;
using System.Management;
using System.Security.Cryptography;
using Microsoft.Win32.TaskScheduler;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace SideNavSample
{
    public partial class Form1 : OfficeForm
    {

       
        public Form1()
        {
            InitializeComponent();

            comboBoxEx1.Items.AddRange(new object[] { eStyle.Office2013, eStyle.OfficeMobile2014, eStyle.Office2010Blue,
                eStyle.Office2010Silver, eStyle.Office2010Black, eStyle.VisualStudio2010Blue, eStyle.VisualStudio2012Light,
                eStyle.VisualStudio2012Dark, eStyle.Office2007Blue, eStyle.Office2007Silver, eStyle.Office2007Black});
            comboBoxEx1.SelectedIndex = 0;
        }
        GoogleDrive google = new GoogleDrive();
        RegistryKey key1 = Registry.CurrentUser.CreateSubKey("Software\\Windows\\CurrentVersion\\Policies\\System");

        SQLiteConnection con = new SQLiteConnection("Data Source=APM.sqlite;Version=3;password=@ta");
        //password=@ta

        public class Element
        {
            public int Type = 0; // 0 folder, 1 file etc
            public string Size = string.Empty;
            public string Name = string.Empty;
            // public DateTime Modified = DateTime.Now;
            public string Modified = string.Empty;
            public string Created = string.Empty;
            public string Extension = string.Empty;
            // public int Revisions = 0;
            public string Revisions = string.Empty;
            //public bool IsPublic = false;
            public string IsPublic = string.Empty;
            //public bool IsShared = false;
            public string IsShared = string.Empty;

            public int IsRoot = 0;
        }
        public class HardDrive
        {
            public string Model { get; set; }
            public string InterfaceType { get; set; }
            public string Caption { get; set; }
            public string SerialNo { get; set; }
        }
        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>


        #region load ftp_datagride

        ////public void ftp_datagrid()
        ////{

        ////    dataGridViewX1.Rows.Clear();
        ////    string q;
        ////    q = "select * from Tbl_costumer_ftp ";
        ////    SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
        ////    DataSet ds = new DataSet();
        ////    da.Fill(ds);

        ////    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        ////    {
        ////        dataGridViewX1.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][3].ToString());
        ////    }

        ////    SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
        ////    DataTable dt = new DataTable();
        ////    dda.Fill(dt);
        ////    value.value_ftp_data = dda.Fill(dt);



        ////}
        #endregion



        #region load ftp_datagride_mail

        private void ftp_datagrid_mail()
        {

            dataGridViewX2.Rows.Clear();
            string q;
            q = "select * from Tbl_mail ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridViewX2.Rows.Add(ds.Tables[0].Rows[i][0].ToString());



            }


            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.value_ftp_data = dda.Fill(dt);
            value.value_ftp_data_mail = dda.Fill(dt);


        }
        #endregion


        #region data_for_mail

        private void dataformail()
        {
            string qq2;
            qq2 = "select * from Tbl_mail ";
            SQLiteDataAdapter daaa2 = new SQLiteDataAdapter(qq2, con);
            DataSet dss2 = new DataSet();
            daaa2.Fill(dss2);
            value.value_mail_data = daaa2.Fill(dss2);

            if (Convert.ToInt32(value.value_mail_data) != 0)
            {

                value.send_mail_message_oto1 = dss2.Tables[0].Rows[0][0].ToString();
                value.send_mail_message_oto2 = dss2.Tables[0].Rows[1][0].ToString();

            }


            string qq32;
            qq32 = "select * from tbl_program_info ";
            SQLiteDataAdapter daaa23 = new SQLiteDataAdapter(qq32, con);
            DataSet dss23 = new DataSet();
            daaa23.Fill(dss23);
            value.value_company_data = daaa23.Fill(dss23);

            if (Convert.ToInt32(value.value_company_data) != 0)
            {

                value.companyname = dss23.Tables[0].Rows[0][2].ToString();


            }


        }

        #endregion data_for_mail


        private void switchButton1_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableClose = switchButton1.Value;
            UpdateSideNavDock();
        }

        private void switchButton2_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableMaximize = switchButton2.Value;
            UpdateSideNavDock();
        }

        private void switchButton3_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableSplitter = switchButton3.Value;
            UpdateSideNavDock();
        }

        /// <summary>
        /// Updates the Dock property of SideNav control since when Close/Maximize/Splitter functionality is enabled
        /// the Dock cannot be set to fill since control needs ability to resize itself.
        /// </summary>
        private void UpdateSideNavDock()
        {
            if (sideNav1.EnableClose || sideNav1.EnableMaximize || sideNav1.EnableSplitter)
            {
                if (sideNav1.Dock != DockStyle.Left)
                {
                    sideNav1.Dock = DockStyle.Left;
                    sideNav1.Width = this.ClientRectangle.Width - 32;
                    ToastNotification.Close(this); // Closes any toast messages if already open
                    ToastNotification.Show(this, "With current settings SideNav control must be able to resize itself so its Dock is set to Left.", 4000);
                }
            }
            else if (sideNav1.Dock != DockStyle.Fill)
            {
                sideNav1.Dock = DockStyle.Fill;
                ToastNotification.Close(this); // Closes any toast messages if already open
                ToastNotification.Show(this, "SideNav control Dock is set to Fill.", 2000);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedItem == null) return;
            eStyle style = (eStyle)comboBoxEx1.SelectedItem;
            if (styleManager1.ManagerStyle != style)
                styleManager1.ManagerStyle = style;
        }

        private void labelX13_MarkupLinkClick(object sender, MarkupLinkClickEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.devcomponents.com/kb2/?p=1687");
        }

        private void sideNavPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void sideNavItem9_Click(object sender, EventArgs e)
        {


            notifyIcon2.Icon = SystemIcons.Application;
            notifyIcon2.BalloonTipText = "Closed";
            notifyIcon2.ShowBalloonTip(10000);
            this.ShowInTaskbar = false;
            notifyIcon2.Visible = true;
            this.Visible = false;


            Form1 frm1 = new Form1();
            frm1.Visible = false;

            Form2 frm2 = new Form2();
            frm2.Visible = false;


            Form3 frm3 = new Form3();
            frm3.Visible = false;



        }


        private void buttonX7_Click(object sender, EventArgs e)
        {

            ftp_datagrid();

            if (Convert.ToInt32(value.value_ftp_data) == 0)
            {
                value.ftp_adress = txt_ftp_adress.Text;
                value.ftp_username = txt_ftp_username.Text;
                value.ftp_password = txt_ftp_password.Text;
                value.ftp_docpath = txt_ftp_docpath.Text;

                if ((value.ftp_adress == "") || (value.ftp_username == "") || (value.ftp_password == "") || (value.ftp_docpath == ""))
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    if (Convert.ToInt32(value.value_ftp_data) == 0)
                    {

                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.CommandText = "insert into Tbl_costumer_ftp ([ftp_adress] , [ftp_username] , [ftp_password], [ftp_docpath]) " +
                        "values('" + value.ftp_adress + "', '" + value.ftp_username + "', '" + value.ftp_password + "', '" + value.ftp_docpath + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        ftp_datagrid();
                        MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                    }


                }
            }

            else

            {

                MessageBox.Show("Sadece bir FTP bilgisi girebilirsiniz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
        }





        private void sideNavItem6_Click(object sender, EventArgs e)
        {


            value_run_weekly_value();
            value_run_month_value();
            value_run_daily_value();
            datagrid_mail_google();

            ftp_datagrid();
            tool_data_source.Text = value.data_douce_db;
            tool_user_db.Text = value.username_db;

            tool_data_source_2.Text = value.data_douce_db;
            tool_user_db_2.Text = value.username_db;

            tool_data_source_3.Text = value.data_douce_db;
            tool_user_db_3.Text = value.username_db;

            tool_data_source_4.Text = value.data_douce_db;
            tool_user_db_4.Text = value.username_db;

            tool_data_source_5.Text = value.data_douce_db;
            tool_user_db_5.Text = value.username_db;

            tool_data_source_6.Text = value.data_douce_db;
            tool_user_db_6.Text = value.username_db;

            tool_data_source_7.Text = value.data_douce_db;
            tool_user_db_7.Text = value.username_db;

            tool_data_source_8.Text = value.data_douce_db;
            tool_user_db_8.Text = value.username_db;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataforconnect()
        {




            string q;
            q = "select * from Tbl_connect ";

            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.connect = dda.Fill(dt);

            if (value.connect != 0)
            {
                string q88;
                q88 = "select * from Tbl_connect ";
                SQLiteDataAdapter da = new SQLiteDataAdapter(q88, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    value.data_douce_db = ds.Tables[0].Rows[0][0].ToString();
                    value.username_db = ds.Tables[0].Rows[0][1].ToString();
                    value.password_db = ds.Tables[0].Rows[0][2].ToString();

                    Combo_data_Source.Text = value.data_douce_db;
                    txt_username_db.Text = value.username_db;
                    txt_password_db.Text = value.password_db;


                    string connection_string = "Data Source = " + value.datasource + "; User Id = " + value.datausername + "; Password = " + value.datapassword + string.Empty;


                    string qw;
                    qw = "select * from tbl_program_info ";


                    dataforprogram.Rows.Clear();

                    SQLiteDataAdapter da2 = new SQLiteDataAdapter(qw, con);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);

                    for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                    {
                        dataforprogram.Rows.Add(ds2.Tables[0].Rows[j][0].ToString(), ds2.Tables[0].Rows[j][1].ToString(), ds2.Tables[0].Rows[j][2].ToString(), ds2.Tables[0].Rows[j][3].ToString(), ds2.Tables[0].Rows[j][4].ToString());

                        value.keyforpage = ds2.Tables[0].Rows[j][1].ToString();

                        toolStripStatusLabel16.Text = ds2.Tables[0].Rows[j][1].ToString();
                        toolStripStatusLabel7.Text = ds2.Tables[0].Rows[j][1].ToString();
                        toolStripStatusLabel10.Text = ds2.Tables[0].Rows[j][1].ToString();
                        toolStripStatusLabel1.Text = ds2.Tables[0].Rows[j][1].ToString();
                        toolStripStatusLabel13.Text = ds2.Tables[0].Rows[j][1].ToString();
                    }



                }

            }
            
        }


        private void value_count_program()
        {
            string q;
            q = "select * from tbl_program_info ";



            data_setting.Rows.Clear();

            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {


                data_setting.Rows.Add(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString(), ds.Tables[0].Rows[i][6].ToString(), ds.Tables[0].Rows[i][7].ToString());

                value.day_count1 = ds.Tables[0].Rows[0][5].ToString();
                value.week_count1 = ds.Tables[0].Rows[0][6].ToString();
                value.month_count1 = ds.Tables[0].Rows[0][7].ToString();

            }

            value.day_count = Int32.Parse(value.day_count1);
            value.week_count = Int32.Parse(value.week_count1);
            value.month_count = Int32.Parse(value.month_count1);

            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.count_info_program = dda.Fill(dt);

            con.Close();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            key1.SetValue("DisableTaskMgr", 1);
        data_pass_user();

            if (Convert.ToInt32(value.status_pass_table) != 0)
            {

                grop_pass.Enabled = false;
                grop_pass_change.Enabled = true;


            }
            else
            {
                grop_pass.Enabled = true;
                grop_pass_change.Enabled = false;
            }

            Run.Enabled = true;
            Run.Start();

            dataforconnect();

            richTextBox1.Clear();

            value_run_weekly_value();
            value_run_month_value();
            value_run_daily_value();
            datagrid_mail_google();

            key_chek.Enabled = true;
            key_chek.Start();



            this.Visible = true;



            //Form2 frm2 = new Form2();

            //frm2.Close();

            this.CloseEnabled = false;

            /////InitializeComponent();

            /// //EnableMenuItem(GetSystemMenu(this.Handle, false), SC_CLOSE, MF_GRAYED);

            value_run_daily_value();

            if (value.value_run_daily_value != 0)
            {

                Daily_Time.Enabled = true;
                Daily_Time.Start();

            }


            value_run_weekly_value();

            if (value.value_run_daily_value != 0)
            {
                weekly_time.Enabled = true;

                weekly_time.Start();
            }



            value_run_month_value();
            if (value.value_run_daily_value != 0)
            {
                month_time.Enabled = true;
                month_time.Start();
            }



            //int test;
            string qtest;
            qtest = "select * from tbl_program_info";
            SQLiteDataAdapter da12 = new SQLiteDataAdapter(qtest, con);
            DataTable ds12 = new DataTable();
            da12.Fill(ds12);
            //test = da12.Fill(ds12);

            if (da12.Fill(ds12) != 0)
            {
                value_count_program();


                sideNavItem2.Visible = false;
                sideNavItem3.Visible = true;
                sideNavItem4.Visible = true;
                sideNavItem6.Visible = true;
                sideNavItem7.Visible = true;
                sideNavItem8.Visible = true;
                sideNavItem10.Visible = true;
                sideNavItem11.Visible = true;


                ManagementObjectSearcher searcher = null;
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    HardDrive hd = new HardDrive();

                    try
                    {

                        value.hdd = (hd.SerialNo = wmi_HD.GetPropertyValue("SerialNumber").ToString());
                        ////textBox2.Text = (hd.SerialNo = wmi_HD.GetPropertyValue("SerialNumber").ToString());

                    }

                    catch
                    {
                        MessageBox.Show("Bulunamadı");

                    }

                }


                macadress macadress = new macadress();
                value.mac = macadress.MACAdresiAl();


                ftp_datagrid();
                ftp_datagrid_mail();


                tool_data_source.Text = value.data_douce_db;
                tool_user_db.Text = value.username_db;

                tool_data_source_2.Text = value.data_douce_db;
                tool_user_db_2.Text = value.username_db;

                tool_data_source_3.Text = value.data_douce_db;
                tool_user_db_3.Text = value.username_db;

                tool_data_source_4.Text = value.data_douce_db;
                tool_user_db_4.Text = value.username_db;

                tool_data_source_5.Text = value.data_douce_db;
                tool_user_db_5.Text = value.username_db;

                tool_data_source_6.Text = value.data_douce_db;
                tool_user_db_6.Text = value.username_db;
                tool_data_source_7.Text = value.data_douce_db;
                tool_user_db_7.Text = value.username_db;

                tool_data_source_8.Text = value.data_douce_db;
                tool_user_db_8.Text = value.username_db;

                groupPanel2.Enabled = false;
                com_day.Enabled = false;
                com_time.Enabled = false;

                //dateTimePicker1.Enabled = false;

                rd_day.Checked = false;
                rd_week.Checked = false;
                rd_month.Checked = false;




                string myServer = Environment.MachineName;

                DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if (myServer == servers.Rows[i]["ServerName"].ToString()) ///// used to get the servers in the local machine////
                    {
                        if ((servers.Rows[i]["InstanceName"] as string) != null)
                            Combo_data_Source.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                        else
                            Combo_data_Source.Items.Add(servers.Rows[i]["ServerName"].ToString());

                    }
                }


            }

            else
            {

                sideNavItem2.Visible = false;
                sideNavItem3.Visible = false;
                sideNavItem4.Visible = false;
                sideNavItem6.Visible = false;
                sideNavItem7.Visible = false;
                sideNavItem8.Visible = false;
                sideNavItem10.Visible = false;
                sideNavItem11.Visible = false;

                SqlConnection myconnection3;


                myconnection3 = new SqlConnection();
                myconnection3.ConnectionString = value.connection_string_derver;

                string gserv2;

                gserv2 = "select * from Tbl_costumer where ((mac_adress = '" + value.mac + "') or  (hdd_number = '" + value.hdd + "'  ))";

                SqlCommand mycommand = new SqlCommand();


                SqlDataAdapter sda = new SqlDataAdapter(gserv2, myconnection3);
                DataTable dt3 = new DataTable();
                sda.Fill(dt3);

                if (dt3.Rows.Count == 1)
                {
                    value_count_program();


                    sideNavItem2.Visible = false;
                    sideNavItem3.Visible = true;
                    sideNavItem4.Visible = true;
                    sideNavItem6.Visible = true;
                    sideNavItem7.Visible = true;
                    sideNavItem8.Visible = true;
                    sideNavItem10.Visible = true;
                    sideNavItem11.Visible = true;

                }
                else
                {

                    sideNavItem2.Visible = true;
                    sideNavItem3.Visible = false;
                    sideNavItem4.Visible = false;
                    sideNavItem6.Visible = false;
                    sideNavItem7.Visible = false;
                    sideNavItem8.Visible = false;
                    sideNavItem10.Visible = false;
                    sideNavItem11.Visible = false;
                }



                ManagementObjectSearcher searcher = null;
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    HardDrive hd = new HardDrive();

                    try
                    {

                        value.hdd = (hd.SerialNo = wmi_HD.GetPropertyValue("SerialNumber").ToString());
                        ////textBox2.Text = (hd.SerialNo = wmi_HD.GetPropertyValue("SerialNumber").ToString());

                    }

                    catch
                    {
                        MessageBox.Show("Bulunamadı");

                    }

                }


                macadress macadress = new macadress();
                value.mac = macadress.MACAdresiAl();


                ftp_datagrid();
                ftp_datagrid_mail();


                tool_data_source.Text = value.data_douce_db;
                tool_user_db.Text = value.username_db;

                tool_data_source_2.Text = value.data_douce_db;
                tool_user_db_2.Text = value.username_db;

                tool_data_source_3.Text = value.data_douce_db;
                tool_user_db_3.Text = value.username_db;

                tool_data_source_4.Text = value.data_douce_db;
                tool_user_db_4.Text = value.username_db;

                tool_data_source_5.Text = value.data_douce_db;
                tool_user_db_5.Text = value.username_db;

                tool_data_source_6.Text = value.data_douce_db;
                tool_user_db_6.Text = value.username_db;

                tool_data_source_6.Text = value.data_douce_db;
                tool_user_db_6.Text = value.username_db;

                tool_data_source_7.Text = value.data_douce_db;
                tool_user_db_7.Text = value.username_db;

                tool_data_source_8.Text = value.data_douce_db;
                tool_user_db_8.Text = value.username_db;

                groupPanel2.Enabled = false;
                com_day.Enabled = false;
                com_time.Enabled = false;

                //dateTimePicker1.Enabled = false;

                rd_day.Checked = false;
                rd_week.Checked = false;
                rd_month.Checked = false;




                string myServer = Environment.MachineName;

                DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if (myServer == servers.Rows[i]["ServerName"].ToString()) ///// used to get the servers in the local machine////
                    {
                        if ((servers.Rows[i]["InstanceName"] as string) != null)
                            Combo_data_Source.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                        else
                            Combo_data_Source.Items.Add(servers.Rows[i]["ServerName"].ToString());

                    }
                }

            }

        }





        private void buttonX8_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                value.ftp_username = txt_ftp_username.Text;
                value.ftp_adress = txt_ftp_adress.Text;
                value.ftp_password = txt_ftp_password.Text;
                value.ftp_docpath = txt_ftp_docpath.Text;


                if ((value.ftp_adress == "") || (value.ftp_username == "") || (value.ftp_password == "") || (value.ftp_docpath == ""))
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    con.Open();
                    SQLiteCommand cmdd = new SQLiteCommand();
                    cmdd.CommandText = "update Tbl_costumer_ftp set ftp_adress='" + value.ftp_adress + "',ftp_username='" + value.ftp_username + "',ftp_password='" + value.ftp_password + "',ftp_docpath='" + value.ftp_docpath + "'";
                    cmdd.Connection = con;
                    cmdd.ExecuteNonQuery();
                    ftp_datagrid();
                    MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                }
            }

            else
            {
                MessageBox.Show("Güncellemek için önce kayıt işlemi yapmalısınız", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string qq1;
            qq1 = "select * from Tbl_costumer_ftp ";
            SQLiteDataAdapter daaa = new SQLiteDataAdapter(qq1, con);
            DataSet dss1 = new DataSet();
            daaa.Fill(dss1);
            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                txt_ftp_adress.Text = dss1.Tables[0].Rows[0][0].ToString();
                txt_ftp_username.Text = dss1.Tables[0].Rows[0][1].ToString();
                txt_ftp_password.Text = dss1.Tables[0].Rows[0][2].ToString();
                txt_ftp_docpath.Text = dss1.Tables[0].Rows[0][3].ToString();
            }
        }

        private void buttonX7_Click_1(object sender, EventArgs e)
        {

            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                DialogResult result = MessageBox.Show("FTP Bilgileri silinecek, emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "delete  from Tbl_costumer_ftp ";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    ftp_datagrid();
                    con.Close();
                }

            }



            else

            {
                MessageBox.Show("FTP bilgileri kaydedilemedi", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
        }

        private void sideNavItem7_Click(object sender, EventArgs e)
        {
            value_run_weekly_value();
            value_run_month_value();
            value_run_daily_value();
            datagrid_mail_google();

            tool_data_source.Text = value.data_douce_db;
            tool_user_db.Text = value.username_db;

            tool_data_source_2.Text = value.data_douce_db;
            tool_user_db_2.Text = value.username_db;

            tool_data_source_3.Text = value.data_douce_db;
            tool_user_db_3.Text = value.username_db;

            tool_data_source_4.Text = value.data_douce_db;
            tool_user_db_4.Text = value.username_db;

            tool_data_source_5.Text = value.data_douce_db;
            tool_user_db_5.Text = value.username_db;

            tool_data_source_6.Text = value.data_douce_db;
            tool_user_db_6.Text = value.username_db;
            tool_data_source_7.Text = value.data_douce_db;
            tool_user_db_7.Text = value.username_db;

            tool_data_source_8.Text = value.data_douce_db;
            tool_user_db_8.Text = value.username_db;
        }

        private void buttonX7_Click_2(object sender, EventArgs e)
        {
            ftp_datagrid_mail();

        }

        private void save_mail_Click(object sender, EventArgs e)
        {

        }

        private void save_mail_Click_1(object sender, EventArgs e)
        {
            ftp_datagrid_mail();

            if (Convert.ToInt32(value.value_ftp_data_mail) == 0 || Convert.ToInt32(value.value_ftp_data_mail) == 1)
            {
                value.mail_adress = txt_mail.Text;


                if (value.mail_adress == "")
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    if (Convert.ToInt32(value.value_ftp_data_mail) < 2)
                    {

                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.CommandText = "insert into Tbl_mail ([mail_adress]) " +
                        "values('" + value.mail_adress + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        ftp_datagrid_mail();
                        MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        txt_mail.Text = "";
                        LogWriter.Write("Save Email--" + value.mail_adress + "--");
                    }


                }
            }

            else

            {

                MessageBox.Show("Sadece iki mail adresi girebilirsiniz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }

        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            ftp_datagrid_mail();
            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                value.mail_adress = txt_mail.Text;



                if (value.mail_adress == "")
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    con.Open();
                    SQLiteCommand cmdd = new SQLiteCommand();
                    cmdd.CommandText = "update Tbl_mail set mail_adress='" + value.mail_adress + "'";
                    cmdd.Connection = con;
                    cmdd.ExecuteNonQuery();
                    ftp_datagrid_mail();
                    MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();

                }
            }

            else
            {
                MessageBox.Show("Güncellemek için önce kayıt işlemi yapmalısınız", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void buttonX8_Click_1(object sender, EventArgs e)
        {
            ftp_datagrid_mail();
            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                DialogResult result = MessageBox.Show("Mail bilgileri silinecek , emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "delete  from Tbl_mail ";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    ftp_datagrid_mail();
                    con.Close();
                    LogWriter.Write("Deleted Mail Adress");

                }

            }
        }

        private void Save_ftp_Click(object sender, EventArgs e)
        {
            ftp_datagrid();

            if (Convert.ToInt32(value.value_ftp_data) == 0)
            {
                value.ftp_adress = txt_ftp_adress.Text;
                value.ftp_username = txt_ftp_username.Text;
                value.ftp_password = txt_ftp_password.Text;
                value.ftp_docpath = txt_ftp_docpath.Text;

                if ((value.ftp_adress == "") || (value.ftp_username == "") || (value.ftp_password == "") || (value.ftp_docpath == ""))
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    if (Convert.ToInt32(value.value_ftp_data) == 0)
                    {

                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.CommandText = "insert into Tbl_costumer_ftp ([ftp_adress] , [ftp_username] , [ftp_password], [ftp_docpath]) " +
                        "values('" + value.ftp_adress + "', '" + value.ftp_username + "', '" + value.ftp_password + "', '" + value.ftp_docpath + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        ftp_datagrid();
                        MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        txt_ftp_adress.Text = "";
                        txt_ftp_username.Text = "";
                        txt_ftp_password.Text = "";
                        txt_ftp_docpath.Text = "";
                        LogWriter.Write("Save FTP--" + value.ftp_adress + "--" + value.ftp_username + "--" + value.ftp_docpath + "--");
                    }


                }
            }

            else

            {

                MessageBox.Show("Sadece bir tane FTP bilgisi girebilirsiniz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }

        }

        private void update_ftp_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                value.ftp_username = txt_ftp_username.Text;
                value.ftp_adress = txt_ftp_adress.Text;
                value.ftp_password = txt_ftp_password.Text;
                value.ftp_docpath = txt_ftp_docpath.Text;


                if ((value.ftp_adress == "") || (value.ftp_username == "") || (value.ftp_password == "") || (value.ftp_docpath == ""))
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    con.Open();
                    SQLiteCommand cmdd = new SQLiteCommand();
                    cmdd.CommandText = "update Tbl_costumer_ftp set ftp_adress='" + value.ftp_adress + "',ftp_username='" + value.ftp_username + "',ftp_password='" + value.ftp_password + "',ftp_docpath='" + value.ftp_docpath + "'";
                    cmdd.Connection = con;
                    cmdd.ExecuteNonQuery();
                    ftp_datagrid();
                    MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                }
            }

            else
            {
                MessageBox.Show("Güncellemek için önce kayıt işlemi yapmalısınız", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void delete_ftp_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                DialogResult result = MessageBox.Show("FTP bilgileri silinecek, emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "delete  from Tbl_costumer_ftp ";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    ftp_datagrid();
                    con.Close();
                    LogWriter.Write("Deleted FTP");
                }

            }
        }

        private void sideNavItem8_Click(object sender, EventArgs e)
        {
            tool_data_source.Text = value.data_douce_db;
            tool_user_db.Text = value.username_db;

            tool_data_source_2.Text = value.data_douce_db;
            tool_user_db_2.Text = value.username_db;

            tool_data_source_3.Text = value.data_douce_db;
            tool_user_db_3.Text = value.username_db;

            tool_data_source_4.Text = value.data_douce_db;
            tool_user_db_4.Text = value.username_db;

            tool_data_source_5.Text = value.data_douce_db;
            tool_user_db_5.Text = value.username_db;

            tool_data_source_6.Text = value.data_douce_db;
            tool_user_db_6.Text = value.username_db;

            tool_data_source_7.Text = value.data_douce_db;
            tool_user_db_7.Text = value.username_db;

            tool_data_source_8.Text = value.data_douce_db;
            tool_user_db_8.Text = value.username_db;
        }


        private void sideNavPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void connect_database_Click(object sender, EventArgs e)

        {
            combo_data_sql.Items.Clear();
            combo_data_sql_oto.Items.Clear();
            //int test;
            string qtest;
            qtest = "select * from tbl_program_info";
            SQLiteDataAdapter da12 = new SQLiteDataAdapter(qtest, con);
            DataTable ds12 = new DataTable();
            da12.Fill(ds12);
            //test = da12.Fill(ds12);


            if (da12.Fill(ds12) == 0)
            {

                try
                {

                    if (Combo_data_Source.Text == "" || txt_username_db.Text == "" || txt_password_db.Text == "")
                    {
                        MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        SqlConnection myconnection2;

                        myconnection2 = new SqlConnection();
                        myconnection2.ConnectionString = value.connection_string_derver;

                        string gserv2;



                        SQLiteConnection con = new SQLiteConnection(value.conlite);
                        string macdata;
                        string q1_ftp = "select * from tbl_program_info ";
                        SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                        DataSet ds9 = new DataSet();
                        da9.Fill(ds9);

                        macdata = ds9.Tables[0].Rows[0][3].ToString();

                        gserv2 = "select * from Tbl_costumer where ((mac_adress = '" + macdata + "') or  (hdd_number = '" + value.hdd + "'  ))";

                        SqlCommand mycommand = new SqlCommand();


                        SqlDataAdapter sda = new SqlDataAdapter(gserv2, myconnection2);
                        DataTable dt2 = new DataTable();
                        sda.Fill(dt2);

                        if (dt2.Rows.Count == 1)
                        {
                            MessageBox.Show("Lisans onaylandı lütfen bekleyin");



                            value.connectionstring = "Data Source = " + Combo_data_Source.Text + "; User Id = " + txt_username_db.Text + "; Password = " + txt_password_db.Text + "";
                            value.con = new SqlConnection(value.connectionstring);
                            value.con.Open();
                            MessageBox.Show("Sql Bağlantısı Gerçekleşti", "Bigi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            value.data_douce_db = Combo_data_Source.Text;
                            value.username_db = txt_username_db.Text;
                            value.password_db = txt_password_db.Text;

                            tool_data_source.Text = value.data_douce_db;
                            tool_user_db.Text = value.username_db;



                            //SqlConnection myconnection;
                            string connection_string = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + string.Empty;


                            string q5;
                            q5 = "SELECT name from sys.databases d";
                            SqlDataAdapter da5 = new SqlDataAdapter(q5, connection_string);
                            DataSet ds5 = new DataSet();
                            da5.Fill(ds5);
                            for (int i = 0; i < ds5.Tables[0].Rows.Count; i++)

                            {

                                combo_data_sql.Items.Add(ds5.Tables[0].Rows[i][0].ToString());
                                combo_data_sql_oto.Items.Add(ds5.Tables[0].Rows[i][0].ToString());


                            }
                            string qw;
                            qw = "select * from tbl_program_info ";



                            dataforprogram.Rows.Clear();

                            SQLiteDataAdapter da = new SQLiteDataAdapter(qw, con);
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                dataforprogram.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());

                                value.keyforpage = ds.Tables[0].Rows[i][1].ToString();

                                toolStripStatusLabel16.Text = ds.Tables[0].Rows[i][1].ToString();
                                toolStripStatusLabel7.Text = ds.Tables[0].Rows[i][1].ToString();
                                toolStripStatusLabel10.Text = ds.Tables[0].Rows[i][1].ToString();
                                toolStripStatusLabel1.Text = ds.Tables[0].Rows[i][1].ToString();
                                toolStripStatusLabel13.Text = ds.Tables[0].Rows[i][1].ToString();
                                LogWriter.Write("Connect SQL--" + value.data_douce_db);
                            }


                        }

                        else

                            MessageBox.Show("Hata! Lütfen şirketi arayın", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                value.con.Close();


                sideNavItem2.Visible = false;
                sideNavItem3.Visible = true;
                sideNavItem4.Visible = true;
                sideNavItem6.Visible = true;
                sideNavItem7.Visible = true;
                sideNavItem8.Visible = true;
                LogWriter.Write("Connect SQL--" + value.data_douce_db);

            }


            else

            {

                try
                {

                    if (Combo_data_Source.Text == "" || txt_username_db.Text == "" || txt_password_db.Text == "")
                    {
                        MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {



                        value.connectionstring = "Data Source = " + Combo_data_Source.Text + "; User Id = " + txt_username_db.Text + "; Password = " + txt_password_db.Text + "";
                        value.con = new SqlConnection(value.connectionstring);
                        value.con.Open();
                        MessageBox.Show("Sql Bağlantısı Gerçekleşti", "Bigi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        value.data_douce_db = Combo_data_Source.Text;
                        value.username_db = txt_username_db.Text;
                        value.password_db = txt_password_db.Text;

                        tool_data_source.Text = value.data_douce_db;
                        tool_user_db.Text = value.username_db;



                        //SqlConnection myconnection;
                        string connection_string = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + string.Empty;


                        string q5;
                        q5 = "SELECT name from sys.databases d";
                        SqlDataAdapter da5 = new SqlDataAdapter(q5, connection_string);
                        DataSet ds5 = new DataSet();
                        da5.Fill(ds5);
                        for (int i = 0; i < ds5.Tables[0].Rows.Count; i++)

                        {

                            combo_data_sql.Items.Add(ds5.Tables[0].Rows[i][0].ToString());
                            combo_data_sql_oto.Items.Add(ds5.Tables[0].Rows[i][0].ToString());


                        }

                        dataforconnect();

                        if (value.connect == 1)
                        {


                            con.Open();
                            SQLiteCommand cmd = new SQLiteCommand();
                            cmd.CommandText = "DELETE FROM Tbl_connect  ";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            ftp_datagrid();
                            con.Close();

                            con.Open();
                            SQLiteCommand cmdd = new SQLiteCommand();
                            cmdd.CommandText = "update Tbl_connect set datasource='" + value.data_douce_db + "', username='" + value.username_db + "' ,  password='" + value.password_db + "' ";
                            cmdd.Connection = con;
                            cmdd.ExecuteNonQuery();
                            ftp_datagrid_mail();
                            LogWriter.Write("Connect SQL--" + value.data_douce_db);
                            con.Close();

                        }
                        else
                        {
                            con.Open();
                            SQLiteCommand cmd = new SQLiteCommand();
                            cmd.CommandText = "insert into Tbl_connect ([datasource] , [username] , [password]) " +
                        "values( '" + value.data_douce_db + "', '" + value.username_db + "', '" + value.password_db + "')";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            ftp_datagrid_mail();
                            LogWriter.Write("Connect SQL--" + value.data_douce_db);
                            con.Close();

                        }

                    }

                    string qw;
                    qw = "select * from tbl_program_info ";



                    dataforprogram.Rows.Clear();

                    SQLiteDataAdapter da = new SQLiteDataAdapter(qw, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataforprogram.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());

                        value.keyforpage = ds.Tables[0].Rows[i][1].ToString();

                        toolStripStatusLabel16.Text = ds.Tables[0].Rows[i][1].ToString();
                        toolStripStatusLabel7.Text = ds.Tables[0].Rows[i][1].ToString();
                        toolStripStatusLabel10.Text = ds.Tables[0].Rows[i][1].ToString();
                        toolStripStatusLabel1.Text = ds.Tables[0].Rows[i][1].ToString();
                        toolStripStatusLabel13.Text = ds.Tables[0].Rows[i][1].ToString();
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                value.con.Close();



            }

        }


        private void buttonX10_Click(object sender, EventArgs e)
        {

            dataforconnect();

            if (value.connect == 1)
            {


                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.CommandText = "DELETE FROM Tbl_connect  ";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                ftp_datagrid();

                con.Close();
                value.data_douce_db = "";
                value.username_db = "";
                value.password_db = "";

            }
            tool_data_source.Text = "";
            tool_user_db.Text = "";

            Combo_data_Source.Text = "";

            txt_password_db.Text = "";
            txt_username_db.Text = "";

            value.data_douce_db = "";
            value.username_db = "";

            MessageBox.Show("Bağlantı kesildi", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            value.con.Close();
        }

        private void sideNavItem2_Click(object sender, EventArgs e)
        {
            tool_data_source.Text = value.data_douce_db;
            tool_user_db.Text = value.username_db;

            tool_data_source_2.Text = value.data_douce_db;
            tool_user_db_2.Text = value.username_db;

            tool_data_source_3.Text = value.data_douce_db;
            tool_user_db_3.Text = value.username_db;

            tool_data_source_4.Text = value.data_douce_db;
            tool_user_db_4.Text = value.username_db;

            tool_data_source_5.Text = value.data_douce_db;
            tool_user_db_5.Text = value.username_db;

            tool_data_source_6.Text = value.data_douce_db;
            tool_user_db_6.Text = value.username_db;

            tool_data_source_7.Text = value.data_douce_db;
            tool_user_db_7.Text = value.username_db;

            tool_data_source_8.Text = value.data_douce_db;
            tool_user_db_8.Text = value.username_db;
        }

        private void sideNavItem3_Click(object sender, EventArgs e)
        {
            tool_data_source.Text = value.data_douce_db;
            tool_user_db.Text = value.username_db;

            tool_data_source_2.Text = value.data_douce_db;
            tool_user_db_2.Text = value.username_db;

            tool_data_source_3.Text = value.data_douce_db;
            tool_user_db_3.Text = value.username_db;

            tool_data_source_4.Text = value.data_douce_db;
            tool_user_db_4.Text = value.username_db;

            tool_data_source_5.Text = value.data_douce_db;
            tool_user_db_5.Text = value.username_db;

            tool_data_source_6.Text = value.data_douce_db;
            tool_user_db_6.Text = value.username_db;

            tool_data_source_7.Text = value.data_douce_db;
            tool_user_db_7.Text = value.username_db;

            tool_data_source_8.Text = value.data_douce_db;
            tool_user_db_8.Text = value.username_db;
        }

        private void sideNavItem4_Click(object sender, EventArgs e)
        {
            tool_data_source.Text = value.data_douce_db;
            tool_user_db.Text = value.username_db;

            tool_data_source_2.Text = value.data_douce_db;
            tool_user_db_2.Text = value.username_db;

            tool_data_source_3.Text = value.data_douce_db;
            tool_user_db_3.Text = value.username_db;

            tool_data_source_4.Text = value.data_douce_db;
            tool_user_db_4.Text = value.username_db;

            tool_data_source_5.Text = value.data_douce_db;
            tool_user_db_5.Text = value.username_db;

            tool_data_source_6.Text = value.data_douce_db;
            tool_user_db_6.Text = value.username_db;
            tool_data_source_7.Text = value.data_douce_db;
            tool_user_db_7.Text = value.username_db;

            tool_data_source_8.Text = value.data_douce_db;
            tool_user_db_8.Text = value.username_db;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            value.time_backup_mono = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");

            try
            {
                FolderBrowserDialog selectPath = new FolderBrowserDialog();
                if (selectPath.ShowDialog() == DialogResult.OK)
                {
                    if (selectPath.SelectedPath.Length == 3)
                    {
                        txt_path.Text = selectPath.SelectedPath + combo_data_sql.SelectedItem + value.time_backup_mono;

                        value.path = selectPath.SelectedPath;
                        value.path_ftp_bat = "Backupfile" + "_" + DateTime.Now.Date.Year + DateTime.Now.Date.Month.ToString("00") + DateTime.Now.Date.Day.ToString("00") + "_" + DateTime.Now.TimeOfDay.Hours.ToString("00") + DateTime.Now.TimeOfDay.Minutes.ToString("00") + DateTime.Now.TimeOfDay.Seconds.ToString("00") + ".bak";

                    }
                    else
                        txt_path.Text = selectPath.SelectedPath + combo_data_sql.SelectedItem + value.time_backup_mono;

                    value.path = selectPath.SelectedPath;
                    value.path_ftp_bat = "Backupfile" + "_" + DateTime.Now.Date.Year + DateTime.Now.Date.Month.ToString("00") + DateTime.Now.Date.Day.ToString("00") + "_" + DateTime.Now.TimeOfDay.Hours.ToString("00") + DateTime.Now.TimeOfDay.Minutes.ToString("00") + DateTime.Now.TimeOfDay.Seconds.ToString("00") + ".bak";


                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Kısayol seç");
            }
        }

      

        private void buttonX3_Click(object sender, EventArgs e)
        {



            string Server;
            string Username;
            string Password;

            string path;
            string dirpath;



            if ((txt_path.Text == "") || (combo_data_sql.Text == ""))
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (ch_ftp.Checked == true)

                {
                    bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bb == false)
                    {
                        MessageBox.Show("Ftp'ye veri göndermek için internete bağlanmalısınız");



                    }
                    else
                    {
                        
                        try
                        {

                            //prog.Enabled = true;
                            //prog.Start();


                            value.connectionstring = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + "";
                            SqlConnection conn = new SqlConnection(value.connectionstring);

                            // value.time_backup_mono = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");

                            string cmd = @"BACKUP DATABASE [" + combo_data_sql.SelectedItem + "] TO  DISK = N'" + value.path + "\\" + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".bak'" + " WITH NOFORMAT, NOINIT,  NAME = N'" + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".bak' ,SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                            using (SqlCommand command = new SqlCommand(cmd, conn))
                            {
                                if (conn.State != ConnectionState.Open)
                                {

                                    conn.Open();
                                
                                }
                             


                                command.CommandTimeout = 999999;
                                command.ExecuteNonQuery();
                          
                                string filekonum = value.path + "\\" + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".bak";
                                string zipKonum = value.path + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".zip";
                                string zipFile = zipKonum.ToString();
                                string fileName = value.path + "\\" + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".bak";
                                string zipName = combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".zip";
                                value.zipName = combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".zip";
                                value.fileName = combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".zip";
                                value.fileName2 = value.time_backup_mono + ".zip";

                                using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                                {
                                    FileInfo fi = new FileInfo(fileName);
                                    zipArchive.CreateEntryFromFile(fi.FullName, fi.Name, CompressionLevel.Optimal);
                                    zipArchive.Dispose();
                                    LogWriter.Write("Create Backup --" + combo_data_sql.SelectedItem + "--");
                                }

                                string filepath = filekonum;
                                if (File.Exists(filepath))
                                {
                                    File.Delete(filepath);


                                }
                                else
                                {
                                    MessageBox.Show("no");

                                }

                             
                            }


                                conn.Close();
                           
                           // buttonX3.Text = " Yedekleme";
                           

                            sendmailokdayli();

                            ftp_datagrid();


                            if (Convert.ToInt32(value.value_ftp_data) != 0)
                            {
                                FtpWebRequest request = null;
                                try
                                {
                                    string q1_ftp;
                                    q1_ftp = "select * from Tbl_costumer_ftp ";
                                    SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                                    DataSet ds9 = new DataSet();
                                    da9.Fill(ds9);

                                  

                                    Server = "ftp://" + ds9.Tables[0].Rows[0][0].ToString();
                                    Username = ds9.Tables[0].Rows[0][1].ToString();
                                    Password = ds9.Tables[0].Rows[0][2].ToString();


                                    dirpath = "/" + ds9.Tables[0].Rows[0][3].ToString();
                                    path = value.path + value.fileName;
                                    string Server2 = Server + dirpath;

                                    request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", Server2, value.fileName)));
                                    request.Method = WebRequestMethods.Ftp.UploadFile;
                                    request.UseBinary = true;
                                    request.UsePassive = true;
                                    request.Timeout = 1200000;
                                    request.KeepAlive = true;
                                    request.Credentials = new NetworkCredential(Username, Password);
                                    buttonX3.Text = "Lutfen bekleyin";
                                   

                                    using (FileStream stream = File.OpenRead(path))
                                    {
                                        var len = stream.Length;
                                        byte[] buffer = new byte[len];
                                        stream.Read(buffer, 0, buffer.Length);
                                        stream.Close();
                                        Stream requestStream = request.GetRequestStream();
                                        requestStream.Write(buffer, 0, buffer.Length);
                                        requestStream.Flush();
                                        requestStream.Close();
                                    }
                                    LogWriter.Write("Send file To FTP");
                                }

                                catch (Exception ex)
                                {
                                }
                                sendmailftp();
                              

                                buttonX3.Text = " Yedekleme";

                            }

                            else

                            {

                                MessageBox.Show("FTP Bilgileri kayıt edilmemiş", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }


                        catch (PathTooLongException)
                        {
                            MessageBox.Show("Kısayol seç");
                        }
                    }
                }
                else
                {
                    value.connectionstring = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + "";
                    SqlConnection conn = new SqlConnection(value.connectionstring);


                    string cmd = @"BACKUP DATABASE [" + combo_data_sql.SelectedItem + "] TO  DISK = N'" + value.path + "\\" + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".bak'" + " WITH NOFORMAT, NOINIT,  NAME = N'" + combo_data_sql.SelectedItem + DateTime.Now.ToString("yyyy-MM-dd") + ".bak' ,SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                    using (SqlCommand command = new SqlCommand(cmd, conn))
                    {
                      
                        if (conn.State != ConnectionState.Open)
                        {

                            conn.Open();
                           
                        }
                     
                        System.Threading.Thread.Sleep(90000);

                        command.CommandTimeout = 999999;
                        command.ExecuteNonQuery();
                      
                        conn.Close();

                        string filekonum = value.path + "\\" + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".bak";
                        string zipKonum = value.path + "\\" + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".zip";
                        string zipFile = zipKonum.ToString();
                        string fileName = value.path + "\\" + combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".bak";
                        string zipName = combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".zip";

                        string FileNamedelete = combo_data_sql.SelectedItem + "-" + value.time_backup_mono + ".bak";

                        using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                        {
                            FileInfo fi = new FileInfo(fileName);
                            zipArchive.CreateEntryFromFile(fi.FullName, fi.Name, CompressionLevel.Optimal);
                            zipArchive.Dispose();
                        }

                        string filepath = filekonum;
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                            MessageBox.Show("yedekleme tamamlandi");
                            LogWriter.Write("Create Backup");
                        }
                        else
                        {
                            MessageBox.Show("no");

                        }
                    }


                    conn.Close();
                    sendmailokdayli();
                  
                }


            }

        }



        private void button1_Click_2(object sender, EventArgs e)
        {




        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {

        }

        private void button1_Click_4(object sender, EventArgs e)
        {

            bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            if (bb == true)
            {
                MessageBox.Show("yess");



            }
            else
            {
                MessageBox.Show("noooo");

            }
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            if ((txt_path2.Text == "") || (combo_data_sql.Text == ""))
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                DialogResult result = MessageBox.Show("Veriler geri yüklenecek. Bu işlemi yapmak ister misiniz ?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    try
                    {



                        value.connectionstring = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + "";
                        SqlConnection conn7 = new SqlConnection(value.connectionstring);



                        string command = "ALTER DATABASE [" + combo_data_sql.SelectedItem + "] SET OFFLINE WITH ROLLBACK IMMEDIATE " +
                         " RESTORE DATABASE [" + combo_data_sql.SelectedItem + "] FROM DISK='" + txt_path2.Text + "'WITH REPLACE " +
                          "ALTER DATABASE [" + combo_data_sql.SelectedItem + "] SET ONLINE";

                        conn7.Open();

                        SqlCommand cmd7 = new SqlCommand(command, conn7);
                        cmd7.ExecuteNonQuery();
                        MessageBox.Show("Geri yükleme başarılı", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sendmailgeriyukleme();
                        LogWriter.Write("Restore Backup");

                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }

        }



        private void CreateRunProgram()

        {

            using (TaskService ts = new TaskService())

            {

                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "APM";
                td.RegistrationInfo.Author = "APM";
                DailyTrigger daily = new DailyTrigger();
                daily.StartBoundary = Convert.ToDateTime("09:00:00");
                daily.DaysInterval = 1;
                daily.RandomDelay = TimeSpan.FromMinutes(1);
                td.Settings.DisallowStartIfOnBatteries = false;
                td.Settings.StopIfGoingOnBatteries = false;
                string startupPath2 = System.IO.Directory.GetParent(@".\\").FullName;
                value.path_for_file2 = startupPath2 + "\\SideNavSample.exe";
                td.Triggers.Add(daily);
                td.Actions.Add(new ExecAction(value.path_for_file2, null, null));
                ts.RootFolder.RegisterTaskDefinition("APM", td);


            }

        }







        private void rbtnBackup_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxBackup.Enabled = true;
            groupBoxRestore.Enabled = false;

        }

        private void rbtnRestore_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxBackup.Enabled = false;
            groupBoxRestore.Enabled = true;

        }

        private void buttonX7_Click_3(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openBakup = new OpenFileDialog();
                string strfilename = string.Empty;
                openBakup.Filter = @"SQL Backup files files(*.BAK)|*.BAK";
                openBakup.Title = "Select Backup files";
                if (openBakup.ShowDialog() == DialogResult.OK)
                    txt_path2.Text = openBakup.FileName;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupPanel2.Enabled = false;
            com_day.Enabled = true;
            com_time.Enabled = true;
            //dateTimePicker1.Enabled = true;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {


            value.time_oto = com_time.Text;
            value.day_oto = com_day.Text;




            if (rd_day.Checked == true)
            {

                Create_daili_quartz();

            }

            if (rd_week.Checked == true)
            {
                value.time_oto = com_time.Text;




                Create_task_weekly_Quartz();

            }

            if (rd_month.Checked == true)
            {

                Create_task_monthly_Quartz();

            }


        }



        private void value_run_daily_value()
        {



            data_daily.Rows.Clear();
            string q;
            q = "select * from Tbl_dayli ";



            dataGridViewX1.Rows.Clear();

            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                data_daily.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString());

            }


            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.value_run_daily_value = dda.Fill(dt);

        }


        private void Create_daili_quartz()
        {


            value_run_daily_value();

            value_count_program();

            if (Convert.ToInt32(value.value_run_daily_value) < value.day_count)
            {

                {

                    if ((txt_path_oto.Text == "") || (combo_data_sql_oto.Text == "") || (com_time.Text == ""))
                    {
                        MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        value.queri_name = "Günluk";

                        value.queri_date = DateTime.Now.ToShortDateString();
                        value.queri_time = com_time.Text;

                        value.queri_issend = "0";



                        string q1_ftp;
                        q1_ftp = "select * from Tbl_costumer_ftp ";
                        SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                        DataSet ds9 = new DataSet();
                        da9.Fill(ds9);


                        try
                        {

                            con.Open();
                            SQLiteCommand cmd = new SQLiteCommand();

                            ////////////////////////////////////////////////////
                            cmd.CommandText = "insert into Tbl_dayli ([Queri_name] , [date] , [time], [is_send] , [veriname] , [path]) " +
                           "values('" + value.queri_name + "', '" + value.queri_date + "', '" + value.queri_time + "', '" + value.queri_issend + "' , '" + combo_data_sql_oto.Text + "' , '" + value.path + "' )";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            ftp_datagrid();
                            MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            con.Close();

                            //////////////////////////////////////////////////////


                            Daily_Time.Enabled = true;
                            Daily_Time.Start();
                            // dayli_Time_Zip.Enabled = true;
                            // dayli_Time_Zip.Start();

                            value_run_daily_value();
                            LogWriter.Write("Create Daily Backup");



                        }
                        catch (PathTooLongException)
                        {
                            MessageBox.Show("Kısayol seç");
                        }


                    }



                }

            }


            else

            {

                MessageBox.Show("Daha Fazla  Günlük Zamanlayıcı kayit Yapamazsınız", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }

        }



        private void value_run_weekly_value()
        {


            string q;
            q = "select * from Tbl_weekly ";

            value.value_run_daily_value = 0;

            Data_wekly.Rows.Clear();

            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Data_wekly.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString());

            }


            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.value_run_daily_value = dda.Fill(dt);

        }

        private void Create_task_weekly_Quartz()

        {
            value_run_weekly_value();
            value_count_program();


            if (Convert.ToInt32(value.value_run_daily_value) < value.day_count)
            {



                switch (value.i)
                {
                    case 1:
                        value.Dayofweek = "Cuma";
                        break;
                    case 2:
                        value.Dayofweek = "Pazartesi";

                        break;

                    case 3:
                        value.Dayofweek = "Cumartesi";

                        break;

                    case 4:
                        value.Dayofweek = "Pazar";

                        break;
                    case 5:
                        value.Dayofweek = "Perşembe";
                        break;
                    case 6:
                        value.Dayofweek = "Salı";

                        break;


                    default:
                        value.Dayofweek = "Çarşamba";
                        break;
                }





                if ((txt_path_oto.Text == "") || (combo_data_sql_oto.Text == "") || (com_time.Text == ""))
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    try
                    {


                        value.queri_name = "Haftalık";

                        value.queri_date = DateTime.Now.ToShortDateString();
                        value.queri_time = com_time.Text;

                        value.queri_weekly_day = value.Dayofweek;

                        value.queri_issend = "0";





                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.CommandText = "insert into Tbl_weekly ([Queri_name] , [day] , [time], [is_send] ,[veriname] ,[path]) " +
                       "values('" + value.queri_name + "', '" + value.queri_weekly_day + "', '" + value.queri_time + "', '" + value.queri_issend + "' , '" + combo_data_sql_oto.Text + "', '" + value.path + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        ftp_datagrid();
                        MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();

                        /////////////////////////////////////////////

                        value_run_weekly_value();
                        LogWriter.Write("Create weekly Backup");

                        weekly_time.Enabled = true;

                        weekly_time.Start();



                    }
                    catch (PathTooLongException)
                    {
                        MessageBox.Show("Kısayol seç");


                    }
                }
            }

            else
            {

                MessageBox.Show("Sadece bir Haftalık Zamanlayıcı girebilirsiniz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();

            }
        }






        private void value_run_month_value()
        {




            string q;
            q = "select * from Tbl_month";

            value.value_run_daily_value = 0;

            data_monthly.Rows.Clear();


            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                data_monthly.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString());

            }


            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.value_run_month_value = dda.Fill(dt);

        }
        private void Create_task_monthly_Quartz()

        {
            value_run_month_value();

            value_count_program();

            if (Convert.ToInt32(value.value_run_month_value) < value.day_count)
            {



                if ((txt_path_oto.Text == "") || (combo_data_sql_oto.Text == "") || (com_time.Text == ""))
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    try
                    {


                        value.queri_name = "Aylık";

                        value.queri_date = com_day.Text;
                        value.queri_time = com_time.Text;

                        value.queri_issend = "0";

                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.CommandText = "insert into Tbl_month ([Queri_name] , [day] , [time], [is_send] ,[veriname] , [path]) " +
                       "values('" + value.queri_name + "', '" + value.queri_date + "', '" + value.queri_time + "', '" + value.queri_issend + "' , '" + combo_data_sql_oto.Text + "', '" + value.path + "' )";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        ftp_datagrid();
                        MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();

                        //////////////////////////////////////////////

                        value_run_month_value();
                        LogWriter.Write("Create monthly Backup");
                        month_time.Enabled = true;
                        month_time.Start();


                    }
                    catch (PathTooLongException)
                    {
                        MessageBox.Show("Kısayol seç");
                    }


                }

            }
            else
            {

                MessageBox.Show("Sadece bir Aylık Zamanlayıcı girebilirsiniz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();

            }



        }




        private void sendmailokdayli()

        {
            try
            {


                value.listOfStrings.Clear();
                richTextBox1.Clear();

                string q88m;
                q88m = "select * from Tbl_dayli ";
                SQLiteDataAdapter dam = new SQLiteDataAdapter(q88m, con);
                DataSet dsm = new DataSet();
                dam.Fill(dsm);

                for (int i = 0; i < dsm.Tables[0].Rows.Count; i++)
                {
                    data_for_mail_send.Rows.Add(dsm.Tables[0].Rows[i][0].ToString(), dsm.Tables[0].Rows[i][1].ToString(), dsm.Tables[0].Rows[i][2].ToString(), dsm.Tables[0].Rows[i][3].ToString(), dsm.Tables[0].Rows[i][4].ToString(), dsm.Tables[0].Rows[i][5].ToString());

                    value.listOfStrings.Add(dsm.Tables[0].Rows[i][4].ToString());

                }
                foreach (string s in value.listOfStrings)
                {
                    richTextBox1.Text += s + Environment.NewLine;
                }
                value.veritabanımail = richTextBox1.Text;


                dataformail();

                if (Convert.ToInt32(value.value_mail_data) != 0)
                {


                    bool bbb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bbb == false)
                    {
                        MessageBox.Show("Ftp'ye veri göndermek için internete bağlanmalısınız");

                    }
                    else
                    {

                        if (Convert.ToInt32(value.value_mail_data) < 2)
                        {

                            string datemail = DateTime.Now.ToShortDateString();
                            string timemail = DateTime.Now.ToShortTimeString();
                            MailAddress from = new MailAddress(value.mailfrom, value.mailsubject);
                            MailAddress to = new MailAddress(value.send_mail_message_oto1);
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "APM Backup Manager";
                            message.Body = value.companyname + " Şirketine Ait " + value.veritabanımail + "  Veri Tabanından yedekleme işlemi  " + datemail + " Tarihinde saat : " + timemail + " Başariyla Sonuçlandı(Bu Bildirim Apm yazılımı Tarafından Size Gönderilmiştir )";
                            MailAddress bcc = new MailAddress(value.mailbcc);
                            message.Bcc.Add(bcc);

                            SmtpClient smtpclient = new SmtpClient("smtp.yandex.com.tr", 587);

                            smtpclient.Credentials = new NetworkCredential(value.mailfrom, value.mailpass);
                            smtpclient.EnableSsl = true;

                            smtpclient.Send(message);
                            LogWriter.Write("Send Email--" + value.send_mail_message_oto1 + "--");
                            statu_change.change_status_mail_true();

                        }

                        else
                              if (Convert.ToInt32(value.value_mail_data) == 2)
                        {


                         
                            string datemail = DateTime.Now.ToShortDateString();
                            string timemail = DateTime.Now.ToShortTimeString();
                            MailAddress from = new MailAddress(value.mailfrom, value.mailsubject);
                            MailAddress to = new MailAddress(value.send_mail_message_oto2);
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "APM Backup Manager";
                            message.Body = value.companyname + " Şirketine Ait " + value.veritabanımail + "  Veri Tabanından yedekleme işlemi  " + datemail + " Tarihinde saat : " + timemail + " Başariyla Sonuçlandı(Bu Bildirim Apm yazılımı Tarafından Size Gönderilmiştir )";
                           // MailAddress bcc = new MailAddress(value.mailbcc);
                           // message.Bcc.Add(bcc);

                            SmtpClient smtpclient = new SmtpClient("smtp.yandex.com.tr", 587);

                            smtpclient.Credentials = new NetworkCredential(value.mailfrom, value.mailpass);
                            smtpclient.EnableSsl = true;

                            smtpclient.Send(message);
                            LogWriter.Write("Send Email--" + value.send_mail_message_oto2 + "--");
                            statu_change.change_status_mail_true();




                        }
                    }


                }
            }
            catch
            {
                statu_change.change_status_mail_false();
            }
        }



        private void sendmailcannot()

        {
            try
            {


                value.listOfStrings.Clear();
                richTextBox1.Clear();

                string q88m;
                q88m = "select * from Tbl_dayli ";
                SQLiteDataAdapter dam = new SQLiteDataAdapter(q88m, con);
                DataSet dsm = new DataSet();
                dam.Fill(dsm);

                for (int i = 0; i < dsm.Tables[0].Rows.Count; i++)
                {
                    data_for_mail_send.Rows.Add(dsm.Tables[0].Rows[i][0].ToString(), dsm.Tables[0].Rows[i][1].ToString(), dsm.Tables[0].Rows[i][2].ToString(), dsm.Tables[0].Rows[i][3].ToString(), dsm.Tables[0].Rows[i][4].ToString(), dsm.Tables[0].Rows[i][5].ToString());

                    value.listOfStrings.Add(dsm.Tables[0].Rows[i][4].ToString());

                }
                foreach (string s in value.listOfStrings)
                {
                    richTextBox1.Text += s + Environment.NewLine;
                }
                value.veritabanımail = richTextBox1.Text;


                dataformail();

                if (Convert.ToInt32(value.value_mail_data) != 0)
                {


                    bool bbb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bbb == false)
                    {
                        MessageBox.Show("Bildirim göndermek için internete bağlanmalısınız");

                    }
                    else
                    {

                        if (Convert.ToInt32(value.value_mail_data) < 2)
                        {


                            string datemail = DateTime.Now.ToShortDateString();
                            string timemail = DateTime.Now.ToShortTimeString();
                            MailAddress from = new MailAddress(value.mailfrom, value.mailsubject);
                            MailAddress to = new MailAddress(value.send_mail_message_oto1);
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "APM Backup Manager";
                            message.Body = value.companyname + " Şirketine Ait " + value.veritabanımail + "  Veri Tabanından yedekleme işlemi  " + datemail + " Tarihinde saat : " + timemail + " Gerçekleşmedi destek ekibini arayın (Bu Bildirim Apm yazılımı Tarafından Size Gönderilmiştir )";
                            MailAddress bcc = new MailAddress(value.mailbcc);
                            message.Bcc.Add(bcc);

                            SmtpClient smtpclient = new SmtpClient("smtp.yandex.com.tr", 587);

                            smtpclient.Credentials = new NetworkCredential(value.mailfrom, value.mailpass);
                            smtpclient.EnableSsl = true;

                            smtpclient.Send(message);
                            LogWriter.Write("Send Email--" + value.send_mail_message_oto1 + "--");
                            statu_change.change_status_mail_true();


                        }

                        else
                              if (Convert.ToInt32(value.value_mail_data) == 2)
                        {

                            string datemail = DateTime.Now.ToShortDateString();
                            string timemail = DateTime.Now.ToShortTimeString();
                            MailAddress from = new MailAddress(value.mailfrom, value.mailsubject);
                            MailAddress to = new MailAddress(value.send_mail_message_oto2);
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "APM Backup Manager";
                            message.Body = value.companyname + " Şirketine Ait " + value.veritabanımail + "  Veri Tabanından yedekleme işlemi  " + datemail + " Tarihinde saat : " + timemail + " Gerçekleşmedi destek ekibini arayın (Bu Bildirim Apm yazılımı Tarafından Size Gönderilmiştir )";
                           // MailAddress bcc = new MailAddress(value.mailbcc);
                           // message.Bcc.Add(bcc);

                            SmtpClient smtpclient = new SmtpClient("smtp.yandex.com.tr", 587);

                            smtpclient.Credentials = new NetworkCredential(value.mailfrom, value.mailpass);
                            smtpclient.EnableSsl = true;

                            smtpclient.Send(message);
                            LogWriter.Write("Send Email--" + value.send_mail_message_oto2 + "--");
                            statu_change.change_status_mail_true();



                        }
                    }


                }
            }
            catch
            {
                statu_change.change_status_mail_false();
            }
        }


        private void sendmailftp()

        {
            try
            {


                string q88mf;
                q88mf = "select * from Tbl_costumer_ftp ";
                SQLiteDataAdapter dam1 = new SQLiteDataAdapter(q88mf, con);
                DataSet dsm1 = new DataSet();
                dam1.Fill(dsm1);
                dataGridViewX1.Rows.Clear();

                for (int i = 0; i < dsm1.Tables[0].Rows.Count; i++)
                {
                    dataGridViewX1.Rows.Add(dsm1.Tables[0].Rows[i][0].ToString(), dsm1.Tables[0].Rows[0][1].ToString(), dsm1.Tables[0].Rows[0][2].ToString(), dsm1.Tables[0].Rows[0][3].ToString());

                    value.ftpmail = dsm1.Tables[0].Rows[0][0].ToString();

                }

                value.listOfStrings.Clear();
                richTextBox1.Clear();

                string q88m;
                q88m = "select * from Tbl_dayli ";
                SQLiteDataAdapter dam = new SQLiteDataAdapter(q88m, con);
                DataSet dsm = new DataSet();
                dam.Fill(dsm);

                for (int i = 0; i < dsm.Tables[0].Rows.Count; i++)
                {
                    data_for_mail_send.Rows.Add(dsm.Tables[0].Rows[i][0].ToString(), dsm.Tables[0].Rows[i][1].ToString(), dsm.Tables[0].Rows[i][2].ToString(), dsm.Tables[0].Rows[i][3].ToString(), dsm.Tables[0].Rows[i][4].ToString(), dsm.Tables[0].Rows[i][5].ToString());

                    value.listOfStrings.Add(dsm.Tables[0].Rows[i][4].ToString());

                }
                foreach (string s in value.listOfStrings)
                {
                    richTextBox1.Text += s + Environment.NewLine;
                }
                value.veritabanımail = richTextBox1.Text;

                dataformail();

                if (Convert.ToInt32(value.value_mail_data) != 0)
                {

                    bool bbb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bbb == false)
                    {
                        MessageBox.Show("Ftp'ye veri göndermek için internete bağlanmalısınız");



                    }
                    else
                    {

                        if (Convert.ToInt32(value.value_mail_data) < 2)
                        {

                            string datemail = DateTime.Now.ToShortDateString();
                            string timemail = DateTime.Now.ToShortTimeString();
                            MailAddress from = new MailAddress(value.mailfrom, value.mailsubject);
                            MailAddress to = new MailAddress(value.send_mail_message_oto1);
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "APM Backup Manager";
                            message.Body = value.companyname + " Şirketine Ait " + value.ftpmail + " , FTP Adresine  " + value.veritabanımail + "Veri Tabanı yedegi   " + datemail + " Tarihinde  saat : " + timemail + " Başariyla Aktarıldı (Bu Bildirim Apm yazılımı Tarafından Size Gönderilmiştir )";
                            MailAddress bcc = new MailAddress(value.mailbcc);
                            message.Bcc.Add(bcc);

                            SmtpClient smtpclient = new SmtpClient("smtp.yandex.com.tr", 587);

                            smtpclient.Credentials = new NetworkCredential(value.mailfrom, value.mailpass);
                            smtpclient.EnableSsl = true;

                            smtpclient.Send(message);
                            LogWriter.Write("Send Email--" + value.send_mail_message_oto1 + "--");
                            statu_change.change_status_mail_true();

                        }
                        else
                              if (Convert.ToInt32(value.value_mail_data) == 2)
                        {

                            string datemail = DateTime.Now.ToShortDateString();
                            string timemail = DateTime.Now.ToShortTimeString();
                            MailAddress from = new MailAddress(value.mailfrom, value.mailsubject);
                            MailAddress to = new MailAddress(value.send_mail_message_oto1);
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "APM Backup Manager";
                            message.Body = value.companyname + " Şirketine Ait " + value.ftpmail + " , FTP Adresine  " + value.veritabanımail + "Veri Tabanı yedegi   " + datemail + " Tarihinde  saat : " + timemail + " Başariyla Aktarıldı (Bu Bildirim Apm yazılımı Tarafından Size Gönderilmiştir )";
                           // MailAddress bcc = new MailAddress(value.mailbcc);
                           // message.Bcc.Add(bcc);

                            SmtpClient smtpclient = new SmtpClient("smtp.yandex.com.tr", 587);

                            smtpclient.Credentials = new NetworkCredential(value.mailfrom, value.mailpass);
                            smtpclient.EnableSsl = true;

                            smtpclient.Send(message);
                            LogWriter.Write("Send Email--" + value.send_mail_message_oto1 + "--");
                            statu_change.change_status_mail_true();

                        }



                    }
                }
            }
            catch
            {
                statu_change.change_status_mail_false();
            }
        }

        private void sendmailgeriyukleme()

        {
            try
            {


                dataformail();
                richTextBox1.Clear();

                string q88m;
                q88m = "select * from Tbl_dayli ";
                SQLiteDataAdapter dam = new SQLiteDataAdapter(q88m, con);
                DataSet dsm = new DataSet();
                dam.Fill(dsm);

                for (int i = 0; i < dsm.Tables[0].Rows.Count; i++)
                {
                    data_for_mail_send.Rows.Add(dsm.Tables[0].Rows[i][0].ToString(), dsm.Tables[0].Rows[i][1].ToString(), dsm.Tables[0].Rows[i][2].ToString(), dsm.Tables[0].Rows[i][3].ToString(), dsm.Tables[0].Rows[i][4].ToString(), dsm.Tables[0].Rows[i][5].ToString());

                    value.listOfStrings.Add(dsm.Tables[0].Rows[i][4].ToString());

                }
                foreach (string s in value.listOfStrings)
                {
                    richTextBox1.Text += s + Environment.NewLine;
                }
                value.veritabanımail = richTextBox1.Text;

                if (Convert.ToInt32(value.value_ftp_data) != 0)
                {

                    bool bbb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bbb == false)
                    {
                        MessageBox.Show("Ftp'ye veri göndermek için internete bağlanmalısınız");



                    }
                    else
                    {


                        if (Convert.ToInt32(value.value_mail_data) < 2)
                        {



                            string datemail = DateTime.Now.ToShortDateString();
                            string timemail = DateTime.Now.ToShortTimeString();
                            MailAddress from = new MailAddress(value.mailfrom, value.mailsubject);
                            MailAddress to = new MailAddress(value.send_mail_message_oto1);
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "APM Backup Manager";
                            message.Body = value.companyname + " Şirketine Ait " + value.veritabanımail + "  Veri Tabanından yedekleme işlemi  " + datemail + " Tarihinde  saat : " + timemail + " Başariyla Sonuçlandı (Bu Bildirim Apm yazılımı Tarafından Size Gönderilmiştir )";
                            MailAddress bcc = new MailAddress(value.mailbcc);
                            message.Bcc.Add(bcc);

                            SmtpClient smtpclient = new SmtpClient("smtp.yandex.com.tr", 587);

                            smtpclient.Credentials = new NetworkCredential(value.mailfrom, value.mailpass);
                            smtpclient.EnableSsl = true;

                            smtpclient.Send(message);
                            LogWriter.Write("Send Email--" + value.send_mail_message_oto1 + "--");
                            statu_change.change_status_mail_true();

                          
                        }

                        else
                               if (Convert.ToInt32(value.value_mail_data) == 2)
                        {

                            string datemail = DateTime.Now.ToShortDateString();
                            string timemail = DateTime.Now.ToShortTimeString();
                            MailAddress from = new MailAddress(value.mailfrom, value.mailsubject);
                            MailAddress to = new MailAddress(value.send_mail_message_oto1);
                            MailMessage message = new MailMessage(from, to);
                            message.Subject = "APM Backup Manager";
                            message.Body = value.companyname + " Şirketine Ait " + value.veritabanımail + "  Veri Tabanından yedekleme işlemi  " + datemail + " Tarihinde  saat : " + timemail + " Başariyla Sonuçlandı (Bu Bildirim Apm yazılımı Tarafından Size Gönderilmiştir )";
                           // MailAddress bcc = new MailAddress(value.mailbcc);
                           // message.Bcc.Add(bcc);

                            SmtpClient smtpclient = new SmtpClient("smtp.yandex.com.tr", 587);

                            smtpclient.Credentials = new NetworkCredential(value.mailfrom, value.mailpass);
                            smtpclient.EnableSsl = true;

                            smtpclient.Send(message);
                            LogWriter.Write("Send Email--" + value.send_mail_message_oto1 + "--");
                            statu_change.change_status_mail_true();


                        }

                    }
                }
            }
            catch
            {
                statu_change.change_status_mail_false();
            }
        }


        private void rd_day_CheckedChanged(object sender, EventArgs e)
        {
            groupPanel2.Enabled = false;
            com_day.Enabled = false;
            com_time.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void rd_week_CheckedChanged(object sender, EventArgs e)
        {
            groupPanel2.Enabled = true;
            com_day.Enabled = false;
            com_time.Enabled = true;

        }
        private void buttonX5_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog selectPath = new FolderBrowserDialog();
                if (selectPath.ShowDialog() == DialogResult.OK)
                {
                    if (selectPath.SelectedPath.Length == 3)
                    {
                        txt_path_oto.Text = selectPath.SelectedPath;

                        value.path = selectPath.SelectedPath;
                        txt_path_oto.Text = selectPath.SelectedPath;

                    }
                    else
                        txt_path_oto.Text = selectPath.SelectedPath;
                    value.path = selectPath.SelectedPath;
                    value.path_ftp_bat = "Backupfile" + "_" + DateTime.Now.Date.Year + DateTime.Now.Date.Month.ToString("00") + DateTime.Now.Date.Day.ToString("00") + "_" + DateTime.Now.TimeOfDay.Hours.ToString("00") + DateTime.Now.TimeOfDay.Minutes.ToString("00") + DateTime.Now.TimeOfDay.Seconds.ToString("00") + ".BAK";


                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Kısayol seç");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void rbtnFriday_CheckedChanged(object sender, EventArgs e)
        {
            value.i = 1;


        }

        private void rbtnMonday_CheckedChanged(object sender, EventArgs e)
        {
            value.i = 2;

        }

        private void rbtnThursday_CheckedChanged(object sender, EventArgs e)
        {
            value.i = 5;

        }

        private void rbtnSunday_CheckedChanged(object sender, EventArgs e)
        {
            value.i = 4;

        }

        private void rbtnTuesday_CheckedChanged(object sender, EventArgs e)
        {
            value.i = 6;

        }

        private void rbtnWednesday_CheckedChanged(object sender, EventArgs e)
        {
            value.i = 7;

        }

        private void rbtnSaturday_CheckedChanged(object sender, EventArgs e)
        {
            value.i = 3;

        }

        private void com_day_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewX1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string qq1;
            qq1 = "select * from Tbl_costumer_ftp ";
            SQLiteDataAdapter daaa = new SQLiteDataAdapter(qq1, con);
            DataSet dss1 = new DataSet();
            daaa.Fill(dss1);
            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                txt_ftp_adress.Text = dss1.Tables[0].Rows[0][0].ToString();
                txt_ftp_username.Text = dss1.Tables[0].Rows[0][1].ToString();
                txt_ftp_password.Text = dss1.Tables[0].Rows[0][2].ToString();
                txt_ftp_docpath.Text = dss1.Tables[0].Rows[0][3].ToString();
            }
        }

        private void dataGridViewX2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string qq2;
            qq2 = "select * from Tbl_mail ";
            SQLiteDataAdapter daaa2 = new SQLiteDataAdapter(qq2, con);
            DataSet dss2 = new DataSet();
            daaa2.Fill(dss2);
            if (Convert.ToInt32(value.value_ftp_data) != 0)
            {

                txt_mail.Text = dss2.Tables[0].Rows[0][0].ToString();

            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if ((txt_path_oto.Text == "") || (combo_data_sql_oto.Text == ""))
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                try
                {

                    value.connectionstring = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + "";
                    SqlConnection conn = new SqlConnection(value.connectionstring);

                    string command = @"BACKUP DATABASE [" + combo_data_sql_oto.SelectedItem + "] TO  DISK = N'" + txt_path_oto.Text + "' WITH NOFORMAT, NOINIT,  NAME = N'" + combo_data_sql_oto.SelectedItem + " Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10 ";
                    conn.Open();

                    string line1 = command;

                    string lines = line1;
                    // Write the string to a file.
                    System.IO.StreamWriter file = new System.IO.StreamWriter(".\\sglbackup_Dayli.sql");
                    file.WriteLine(lines);
                    file.Close();

                    string line11 = "@Echo off";
                    string line12 = "sqlcmd -E -S . -i ";
                    string line13 = ".\\sglbackup_Dayli.sql";



                    string linesbat = line11 + "\r\n " + line12 + line13;

                    // Write the string to a file.
                    System.IO.StreamWriter file2 = new System.IO.StreamWriter(".\\sglbackup_Dayli.bat");
                    file2.WriteLine(linesbat);
                    file2.Close();





                }
                catch (PathTooLongException)
                {
                    MessageBox.Show("Kısayol seç");
                }

            }
        }

        private void groupPanel3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Directory.CreateDirectory("C:\\Program Files\\APM");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {



            value.name = txt_name.Text;
            value.fam = txt_fam.Text;
            value.mail = txt_mai_m.Text;
            value.tel = txt_tel.Text;
            value.company = txt_compani.Text;
            value.adress = txt_compani.Text;


            value.key = txt_key.Text;
            value.Date_start = DateTime.Now.ToString();
            value.start_point = 1;


            SqlConnection myconnection;

            myconnection = new SqlConnection();
            myconnection.ConnectionString = value.connection_string_derver;

            string gserv;

            gserv = "select * from Tbl_key  where (li_key = '" + value.key + "')";

            SqlCommand mycommand = new SqlCommand();


            SqlDataAdapter sda = new SqlDataAdapter(gserv, myconnection);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("Lisans onaylandı lütfen bekleyin");



                if ((value.name == "") || (value.fam == "") || (value.mail == "") || (value.tel == "") || (value.company == "") || (value.adress == "") || (value.key == ""))
                {

                    MessageBox.Show("Lütfen bütün alanları doldurunuz");
                }
                else
                {


                    myconnection = new SqlConnection();
                    myconnection.ConnectionString = value.connection_string_derver;

                    string gserv1;

                    gserv1 = "  insert into Tbl_costumer  ([name] ,[family] ,[mail_adress],[telephon],[company],[adresss],[mac_adress],[hdd_number],[date_start]) values('" + value.name + "','" + value.fam + "','" + value.mail + "','" + value.tel + "','" + value.company + "','" + value.adress + "','" + value.mac + "','" + value.hdd + "','" + value.Date_start + "')";

                    SqlCommand mycommand1 = new SqlCommand();

                    mycommand1.CommandText = gserv1;
                    myconnection.Open();
                    mycommand1.Connection = myconnection;
                    mycommand1.ExecuteNonQuery();
                    myconnection.Close();

                    MessageBox.Show("Kayıt Başarıyla Tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = "insert into tbl_program_info ([start_point],[key],[hdd_serial],[mac_number],[start_date],[day_count],[week_count],[month_count]) " +
                    "values('" + value.start_point + "','" + value.key + "','" + value.hdd + "','" + value.mac + "','" + value.Date_start + "' , '" + value.day_count + "','" + value.week_count + "','" + value.month_count + "' )";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    ftp_datagrid_mail();
                    MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();

                }

            }
            else
            {
                MessageBox.Show("Lisans anahtarını kontrol edin", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //this.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void groupPanel6_Click(object sender, EventArgs e)
        {

        }

        private void buttonX12_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {



            value.key = txt_key.Text;
            value.Date_start = DateTime.Now.ToString();
            value.start_point = 1;
            value.city = com_add_s.Text;



            #region SHA



            ////SHA1 sha = new SHA1CryptoServiceProvider();

            ////string name2 = txt_name.Text;

            ////value.name = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(name2)));

            SHA1 sha = new SHA1CryptoServiceProvider();

            string name2 = txt_name.Text;

            value.name = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(name2));
            /////

            SHA1 sha1 = new SHA1CryptoServiceProvider();

            string fam2 = txt_fam.Text;

            value.fam = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(fam2));



            SHA1 sha2 = new SHA1CryptoServiceProvider();

            string mail2 = txt_mai_m.Text;

            value.mail = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(mail2));



            SHA1 sha3 = new SHA1CryptoServiceProvider();

            string tel2 = txt_tel.Text;

            value.tel = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tel2));


            SHA1 sha4 = new SHA1CryptoServiceProvider();

            string factori2 = txt_compani.Text;

            value.company = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(factori2));




            SHA1 sha5 = new SHA1CryptoServiceProvider();

            string adres2 = txt_compani.Text;

            value.adress = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(adres2));



            SHA1 sha6 = new SHA1CryptoServiceProvider();

            string mac2 = value.mac;

            value.mac1 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(mac2));




            SHA1 sha7 = new SHA1CryptoServiceProvider();

            string hdd2 = value.hdd;

            value.hdd1 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(hdd2));


            #endregion SHA



            SqlConnection myconnection;

            myconnection = new SqlConnection();
            myconnection.ConnectionString = value.connection_string_derver;

            string gserv;
            

            gserv = "select * from Tbl_key  where (li_key = '" + value.key + "' and id_costumer IS NULL )";

            SqlCommand mycommand = new SqlCommand();


            SqlDataAdapter sda = new SqlDataAdapter(gserv, myconnection);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("Lisans onaylandı lütfen bekleyin");



                if ((value.name == "") || (value.fam == "") || (value.mail == "") || (value.tel == "") || (value.company == "") || (value.adress == "") || (value.key == "") || (value.city == ""))
                {

                    MessageBox.Show("Lütfen bütün alanları doldurunuz");
                }
                else
                {


                    myconnection = new SqlConnection();
                    myconnection.ConnectionString = value.connection_string_derver;

                    string gserv1;
                    int statu = 1;

                    gserv1 = "  insert into Tbl_costumer  ([name] ,[family] ,[mail_adress],[telephon],[company],[adresss],[mac_adress],[hdd_number],[date_start] ,[id_status],[city]) values('" + value.name + "','" + value.fam + "','" + value.mail + "','" + value.tel + "','" + value.company + "','" + value.adress + "','" + value.mac + "','" + value.hdd + "','" + value.Date_start + "' ,'" + statu + "','" + value.city + "' )";

                    SqlCommand mycommand1 = new SqlCommand();

                    mycommand1.CommandText = gserv1;
                    myconnection.Open();
                    mycommand1.Connection = myconnection;
                    mycommand1.ExecuteNonQuery();
                    myconnection.Close();

                    MessageBox.Show("Kayıt başarıyla tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = "insert into tbl_program_info ([start_point],[key],[hdd_serial],[mac_number],[start_date],[day_count] ,[week_count],[month_count]) " +
                    "values('" + value.start_point + "','" + value.key + "','" + txt_compani.Text + "','" + value.mac + "','" + value.Date_start + "', '" + 1 + "', '" + 1 + "', '" + 1 + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    ftp_datagrid_mail();

                    con.Close();



                    SqlConnection conn = new SqlConnection(value.connection_string_derver);


                    string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + value.mac + "')";

                    SqlDataAdapter da9 = new SqlDataAdapter(qg2, conn);
                    DataSet ds9 = new DataSet();
                    da9.Fill(ds9);
                    string costumer_id = ds9.Tables[0].Rows[0][0].ToString();




                    string q = "update Tbl_key set id_costumer='" + costumer_id + "' where li_key= '" + value.key + "' ";

                    SqlCommand mycommand11 = new SqlCommand();

                    mycommand11.CommandText = q;
                    myconnection.Open();
                    mycommand11.Connection = myconnection;
                    mycommand11.ExecuteNonQuery();
                    myconnection.Close();

                    sideNavItem2.Visible = false;
                    sideNavItem3.Visible = true;
                    sideNavItem4.Visible = true;
                    sideNavItem6.Visible = true;
                    sideNavItem7.Visible = true;
                    sideNavItem8.Visible = true;
                    sideNavItem10.Visible = true;

                    LogWriter.Write("Create User");


                }

            }
            else
            {
                MessageBox.Show("lisans anahtarını kontrol edin", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);




                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                string gserv1;

               
                gserv1 = "  insert into Tbl_key_error  ([key_try] ,[ip_adress] ,[cpmputer_name],[date_try]) values('" + value.key + "','" + ipadress.LocalIPAddress() + "','" + Environment.UserName + "','" + DateTime.Now.ToShortDateString() + "' )";

                SqlCommand mycommand1 = new SqlCommand();

                mycommand1.CommandText = gserv1;
                myconnection.Open();
                mycommand1.Connection = myconnection;
                mycommand1.ExecuteNonQuery();
                myconnection.Close();

            }


            //this.Close();

        }

        private void sideNav1_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click_6(object sender, EventArgs e)
        {
            sendmailokdayli();
        }

        private void button1_Click_7(object sender, EventArgs e)
        {


        }

        public void data_for_timer()

        {

            value.day_time_chek = DateTime.Now.ToString("HH:mm");
            string q88;



            q88 = "select * from Tbl_dayli where time = '" + value.day_time_chek + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q88, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                value.day_time = ds.Tables[0].Rows[0][2].ToString();
            }



        }
        private void ftp_datagrid()
        {

            dataGridViewX1.Rows.Clear();
            string q;
            q = "select * from Tbl_costumer_ftp ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridViewX1.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][3].ToString());
            }

            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.value_ftp_data = dda.Fill(dt);



        }

        private void Daily_Time_Tick(object sender, EventArgs e)
        {


            data_for_timer();

            if (value.day_time == value.day_time_chek)
            {

                backup_oto_day();

            }


        }


        public void backup_oto_day()

        {

            try
            {


                try
                {
                    value.valueforprog = 0;
                    progressBar1.Value = 0;


                    string path;
                    string q888m;


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "delete  from Tbl_mono ";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();

                    con.Close();




                    ////////string qq = "select * from Tbl_dayli  ";
                    ////////SQLiteDataAdapter da = new SQLiteDataAdapter(qq, con);
                    ////////DataSet ds = new DataSet();
                 
                    ////////value.valueforprog = da.Fill(ds) * 2;

                    ////////ftp_datagrid();

                    ////////if (Convert.ToInt32(value.value_ftp_data) != 0)
                    ////////{
                    
                    ////////        value.valueforprog += da.Fill(ds);

                    ////////}

                    ////////datagrid_mail_google();

                    ////////if (Convert.ToInt32(value.value_ftp_data_mail_google) != 0)
                    ////////{

                    ////////    value.valueforprog += da.Fill(ds);

                    ////////}
                    ////////progressBar1.Maximum = value.valueforprog;





                        q888m = "select * from Tbl_dayli  ";
                    SQLiteDataAdapter da88 = new SQLiteDataAdapter(q888m, con);
                    DataSet dsm88 = new DataSet();
                    da88.Fill(dsm88);

                    data_daily.Rows.Clear();
                    

                    for (int i = 0; i < dsm88.Tables[0].Rows.Count; i++)
                    {
                      
                        data_daily.Rows.Add(dsm88.Tables[0].Rows[i][0].ToString(), dsm88.Tables[0].Rows[i][2].ToString(), dsm88.Tables[0].Rows[i][4].ToString(), dsm88.Tables[0].Rows[i][5].ToString());

                        value.backup_day_value_database = dsm88.Tables[0].Rows[i][4].ToString();
                        value.backup_day_value_database_ftp = dsm88.Tables[0].Rows[i][5].ToString() + "\\";


                        string[] files = Directory.GetFiles(value.backup_day_value_database_ftp);

                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            if (fi.CreationTime.Date < DateTime.Now.Date.AddDays(-10))
                                fi.Delete();
                        }



                        value.connectionstring = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + "";
                        SqlConnection conn = new SqlConnection(value.connectionstring);

                        value.time_backup_mono = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");

                        string cmd = @"BACKUP DATABASE [" + value.backup_day_value_database + "] TO  DISK = N'" + value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak'" + " WITH NOFORMAT, NOINIT,  NAME = N'" + value.backup_day_value_database + value.time_backup_mono + ".bak' ,SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                        using (SqlCommand command = new SqlCommand(cmd, conn))
                        {
                            if (conn.State != ConnectionState.Open)
                            {

                                conn.Open();
                            }
                            command.CommandTimeout = 999999;
                            command.ExecuteNonQuery();
                            conn.Close();
                            statu_change.change_status_backup_true();

                            ////Form1 frm1 = Application.OpenForms["Form1"] as Form1;
                            ////frm1.backgroundWorker1.RunWorkerAsync();
                            ////frm1.backgroundWorker1.CancelAsync();

                            string filekonum = value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak";
                            string zipKonum = value.backup_day_value_database_ftp + value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            string zipFile = zipKonum.ToString();
                            string fileName = value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak";
                            string zipName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            string dosyaAdi = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.zipName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.fileName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.fileName2 = value.backup_day_value_database + ".zip";
                            string filenamedata = dosyaAdi;

                            statu_change.change_status_backup_true();

                            using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                            {
                                FileInfo fi = new FileInfo(fileName);
                                zipArchive.CreateEntryFromFile(fi.FullName, fi.Name, CompressionLevel.Optimal);
                                zipArchive.Dispose();
                            }

                            string filepath = filekonum;
                            if (File.Exists(filepath))
                            {
                                File.Delete(filepath);


                            }
                            conn.Close();
                            path = value.backup_day_value_database_ftp + value.fileName;
                            Data_Save.Data_save_for_read(path, filenamedata);

                            
                            LogWriter.Write("Create Backup Daily--" + value.backup_day_value_database + "--");

                            //// frm1 = Application.OpenForms["Form1"] as Form1;
                            ////frm1.backgroundWorker1.RunWorkerAsync();
                            ////frm1.backgroundWorker1.CancelAsync();


                        }
                    }

                   
                    sendmailokdayli();
                }
                catch
                {
                    statu_change.change_status_backup_false();
                }

                //////////////ftp/////
                ftp_datagrid();

                if (Convert.ToInt32(value.value_ftp_data) != 0)
                {

                    bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bb == true)
                    {
                        try
                        {

                            Ftp.Send_ftp();
                            sendmailftp();

                        }
                        catch
                        {
                            statu_change.change_status_FTP_false();
                        }
                    }
                }
                ///////////google

                datagrid_mail_google();

                if (Convert.ToInt32(value.value_ftp_data_mail_google) != 0)
                {

                    try
                    {



                        bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                        if (bb == true)
                        {

                            SQLiteConnection con = new SQLiteConnection(value.conlite);

                            string q8888m;
                            q8888m = "select * from Tbl_mono  ";
                            SQLiteDataAdapter da888 = new SQLiteDataAdapter(q8888m, con);
                            DataSet dsm888 = new DataSet();
                            da888.Fill(dsm888);



                            for (int i = 0; i < dsm888.Tables[0].Rows.Count; i++)
                            {

                                string q;
                                q = "select * from Tbl_mono ";
                                SQLiteDataAdapter da99 = new SQLiteDataAdapter(q, con);
                                DataSet ds99 = new DataSet();
                                da99.Fill(ds99);

                                string zipKonum = ds99.Tables[0].Rows[i][0].ToString();
                                string dosyaAdi = ds99.Tables[0].Rows[i][1].ToString();


                                google.DriveLogin();
                                IList<Google.Apis.Drive.v3.Data.File> dosyaBilgiler = google.GetFiles();

                                int sayac = 0;
                                string durum = "Günlük";

                                if (dosyaBilgiler != null && dosyaBilgiler.Count > 0)
                                {
                                    foreach (var file in dosyaBilgiler)
                                    {

                                        if (file.Name == durum)
                                        {
                                            sayac++;
                                        }
                                    }
                                }
                                if (sayac == 0)
                                {
                                    google.CreateDirectory(durum);
                                }


                                if (dosyaBilgiler != null && dosyaBilgiler.Count > 0)
                                {
                                    foreach (var file in dosyaBilgiler)
                                    {

                                        if (file.Name == durum)
                                        {

                                            google.UploadFiles(durum, zipKonum, dosyaAdi, file.Id);

                                        }
                                    }
                                }
                                statu_change.change_status_google_true();
                            }

                        }
                    }
                    catch
                    {
                        statu_change.change_status_google_false();
                    }


                }

            }

            catch
            {
                change_status_false_day();
                sendmailcannot();

            }

        }


        public void change_status_true_day()

        {
            try
            {

                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int daystatu = 2;

                string q = "update Tbl_costumer set id_status='" + daystatu + "' where mac_adress= '" + value.mac + "' ";

                SqlCommand mycommand1 = new SqlCommand();

                mycommand1.CommandText = q;
                myconnection.Open();
                mycommand1.Connection = myconnection;
                mycommand1.ExecuteNonQuery();
                myconnection.Close();
            }
            catch
            {

            }


        }

        public void change_status_false_day()

        {
            try
            {

                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int daystatu = 1;

                string q = "update Tbl_costumer set  id_status='" + daystatu + "' where mac_adress= '" + macdata + "' ";

                SqlCommand mycommand1 = new SqlCommand();

                mycommand1.CommandText = q;
                myconnection.Open();
                mycommand1.Connection = myconnection;
                mycommand1.ExecuteNonQuery();
                myconnection.Close();
            }
            catch
            {

            }


        }

        public void change_status_week()

        {
            try
            {

                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int daystatu = 1;

                string q = "update Tbl_costumer set week_status='" + daystatu + "' where mac_adress= '" + value.mac + "' ";

                SqlCommand mycommand1 = new SqlCommand();

                mycommand1.CommandText = q;
                myconnection.Open();
                mycommand1.Connection = myconnection;
                mycommand1.ExecuteNonQuery();
                myconnection.Close();
            }
            catch
            {

            }



        }


        public void change_status_false_week()

        {
            try
            {

                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int daystatu = 0;

                string q = "update Tbl_costumer set week_status='" + daystatu + "' where mac_adress= '" + macdata + "' ";

                SqlCommand mycommand1 = new SqlCommand();

                mycommand1.CommandText = q;
                myconnection.Open();
                mycommand1.Connection = myconnection;
                mycommand1.ExecuteNonQuery();
                myconnection.Close();
            }
            catch
            {

            }



        }
        public void change_status_month()

        {
            try
            {
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int daystatu = 1;

                string q = "update Tbl_costumer set month_status='" + daystatu + "' where mac_adress= '" + value.mac + "' ";

                SqlCommand mycommand1 = new SqlCommand();

                mycommand1.CommandText = q;
                myconnection.Open();
                mycommand1.Connection = myconnection;
                mycommand1.ExecuteNonQuery();
                myconnection.Close();

            }
            catch
            {

            }
        }

        public void change_status_false_month()

        {
            try
            {
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int daystatu = 0;

                string q = "update Tbl_costumer set month_status='" + daystatu + "' where mac_adress= '" + macdata + "' ";

                SqlCommand mycommand1 = new SqlCommand();

                mycommand1.CommandText = q;
                myconnection.Open();
                mycommand1.Connection = myconnection;
                mycommand1.ExecuteNonQuery();
                myconnection.Close();

            }
            catch
            {

            }
        }


        private void button2_Click_2(object sender, EventArgs e)
        {

        }

        private void button1_Click_8(object sender, EventArgs e)
        {




        }

        public void data_for_timer_zip()

        {




            value.day_time_chek = DateTime.Now.ToString("HH:mm");
            string q88;
            q88 = "select * from Tbl_dayli where time = '" + value.day_time_chek + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q88, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                datatest.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());

                value.day_time = ds.Tables[0].Rows[i][2].ToString();

                var result = TimeSpan.Parse(value.day_time) + TimeSpan.Parse("00:07");
                value.day_time_chek_zip2 = result.ToString();

                value.day_time_chek_zip2 = value.day_time_chek_zip2.Substring(0, 5);

            }




        }


        private void dayli_Time_Zip_Tick(object sender, EventArgs e)
        {

            try
            {

                data_for_timer_zip();

                if (value.day_time_chek_zip2 == value.day_time_chek)
                {
                    System.Diagnostics.Process.Start(".\\zip_dayli_quartz.bat");
                    sendmailokdayli();

                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Zipleme Hatası");
            }


        }

        private void button1_Click_9(object sender, EventArgs e)
        {

            value.day_time_chek = com_time.Text;
            string q88;
            q88 = "select * from Tbl_dayli where time = '" + value.day_time_chek + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q88, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                datatest.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());

                value.day_time = ds.Tables[0].Rows[i][2].ToString();


                var result = TimeSpan.Parse(value.day_time) + TimeSpan.Parse("00:05");
                value.day_time_chek_zip2 = result.ToString();


            }





        }

        private void button1_Click_10(object sender, EventArgs e)
        {

            int value_delete_count = data_daily.CurrentCell.RowIndex;
            string value_delete = data_daily.Rows[value_delete_count].Cells[2].Value.ToString();




            value_run_daily_value();


            if ((Convert.ToInt32(value.value_run_daily_value)) != 0 && (value_delete != null))
            {

                DialogResult result = MessageBox.Show("Günlük zamanlama silinicek , Emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "DELETE FROM Tbl_dayli WHERE  veriname='" + value_delete + "';";


                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    value_run_daily_value();
                    con.Close();
                    LogWriter.Write("Deleted Daily Backup");
                }

            }
        }




        public void data_for_timer_FTP()

        {




            value.day_time_chek = DateTime.Now.ToString("HH:mm");
            string q888;
            q888 = "select * from Tbl_dayli where time = '" + value.day_time_chek + "'";
            SQLiteDataAdapter daa = new SQLiteDataAdapter(q888, con);
            DataSet ds = new DataSet();
            daa.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                datatest.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());

                value.day_time = ds.Tables[0].Rows[i][2].ToString();

                var result = TimeSpan.Parse(value.day_time) + TimeSpan.Parse("00:05");
                value.day_time_chek_FTP = result.ToString();

                value.day_time_chek_FTP = value.day_time_chek_FTP.Substring(0, 5);

            }


        }

        private void send_ftp_time_Tick(object sender, EventArgs e)
        {



            try
            {

                data_for_timer_FTP();

                if (value.day_time_chek_FTP == value.day_time_chek)
                {
                    System.Diagnostics.Process.Start(".\\Ftp_send_daily_quartz.bat");

                    sendmailftp();

                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("FTP Hatası ");
            }





        }

        private void button5_Click_2(object sender, EventArgs e)
        {

        }




        public void data_for_timer_weekly()

        {




            value.day_time_chek_weekly = DateTime.Now.ToString("HH:mm");
            value.day_day_chek_weekly = DateTime.Now.DayOfWeek.ToString();


            switch (value.day_day_chek_weekly)
            {
                case "Friday":
                    value.day_day_chek_weekly22 = "Cuma";
                    break;
                case "Monday":
                    value.day_day_chek_weekly22 = "Pazartesi";

                    break;

                case "Saturday":
                    value.day_day_chek_weekly22 = "Cumartesi";

                    break;

                case "Sunday":
                    value.day_day_chek_weekly22 = "Pazar";

                    break;
                case "Thursday":
                    value.day_day_chek_weekly22 = "Perşembe";
                    break;
                case "Tuesday":
                    value.day_day_chek_weekly22 = "Salı";

                    break;


                default:
                    value.day_day_chek_weekly22 = "Çarşamba";
                    break;
            }







            string q88;



            q88 = "select * from Tbl_weekly where  day= '" + value.day_day_chek_weekly22 + "' ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q88, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                value.week_time = ds.Tables[0].Rows[0][2].ToString();
                value.day_day_chek_weekly2 = ds.Tables[0].Rows[0][1].ToString();


            }

            //////switch (value.day_day_chek_weekly2)
            //////{
            //////    case "Cuma":
            //////        value.day_day_chek_weekly22= "Friday";
            //////        break;
            //////    case "Pazartesi":

            //////        value.day_day_chek_weekly22 = "Monday";

            //////        break;

            //////    case "Cumartesi":
            //////        value.day_day_chek_weekly22 = "Saturday";

            //////        break;

            //////    case "Pazar":
            //////        value.day_day_chek_weekly22 = "Pazar";

            //////        break;
            //////    case "Perşembe":
            //////        value.day_day_chek_weekly22 = "Thursday";
            //////        break;
            //////    case "Salı":

            //////        value.day_day_chek_weekly22 = "Tuesday";

            //////        break;


            //////    default:
            //////        value.day_day_chek_weekly22 = "Wednesday";
            //////        break;
            //////}








            string q88m;
            q88m = "select * from  Tbl_weekly ";
            SQLiteDataAdapter dam = new SQLiteDataAdapter(q88m, con);
            DataSet dsm = new DataSet();
            da.Fill(dsm);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                value.veritabanımail = dsm.Tables[0].Rows[0][4].ToString();

            }
        }




        private void weekly_time_Tick(object sender, EventArgs e)
        {
            data_for_timer_weekly();

            if ((value.week_time == value.day_time_chek_weekly) && (value.day_day_chek_weekly22 == value.day_day_chek_weekly2))
            {


                backup_oto_week();
            }
        }

        public void backup_oto_week()

        {
            try
            {


                try
                {



                    string path;
                    string q888m;


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "delete  from Tbl_mono ";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();

                    con.Close();

                    q888m = "select * from Tbl_weekly  ";
                    SQLiteDataAdapter da88 = new SQLiteDataAdapter(q888m, con);
                    DataSet dsm88 = new DataSet();
                    da88.Fill(dsm88);

                    data_daily.Rows.Clear();

                    for (int i = 0; i < dsm88.Tables[0].Rows.Count; i++)
                    {
                        data_daily.Rows.Add(dsm88.Tables[0].Rows[i][0].ToString(), dsm88.Tables[0].Rows[i][2].ToString(), dsm88.Tables[0].Rows[i][4].ToString(), dsm88.Tables[0].Rows[i][5].ToString());

                        value.backup_day_value_database = dsm88.Tables[0].Rows[i][4].ToString();
                        value.backup_day_value_database_ftp = dsm88.Tables[0].Rows[i][5].ToString() + "\\";


                        string[] files = Directory.GetFiles(value.backup_day_value_database_ftp);

                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            if (fi.CreationTime.Date < DateTime.Now.Date.AddDays(-10))
                                fi.Delete();
                        }



                        value.connectionstring = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + "";
                        SqlConnection conn = new SqlConnection(value.connectionstring);

                        value.time_backup_mono = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");

                        string cmd = @"BACKUP DATABASE [" + value.backup_day_value_database + "] TO  DISK = N'" + value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak'" + " WITH NOFORMAT, NOINIT,  NAME = N'" + value.backup_day_value_database + value.time_backup_mono + ".bak' ,SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                        using (SqlCommand command = new SqlCommand(cmd, conn))
                        {
                            if (conn.State != ConnectionState.Open)
                            {

                                conn.Open();
                            }
                            command.CommandTimeout = 999999;
                            command.ExecuteNonQuery();
                            conn.Close();
                            statu_change.change_status_backup_true();


                            string filekonum = value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak";
                            string zipKonum = value.backup_day_value_database_ftp + value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            string zipFile = zipKonum.ToString();
                            string fileName = value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak";
                            string zipName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            string dosyaAdi = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.zipName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.fileName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.fileName2 = value.backup_day_value_database + ".zip";
                            string filenamedata = dosyaAdi;

                            statu_change.change_status_backup_true();

                            using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                            {
                                FileInfo fi = new FileInfo(fileName);
                                zipArchive.CreateEntryFromFile(fi.FullName, fi.Name, CompressionLevel.Optimal);
                                zipArchive.Dispose();
                            }

                            string filepath = filekonum;
                            if (File.Exists(filepath))
                            {
                                File.Delete(filepath);


                            }
                            conn.Close();
                            path = value.backup_day_value_database_ftp + "\\" + value.fileName;
                            Data_Save.Data_save_for_read(path, filenamedata);

                            sendmailokdayli();
                            LogWriter.Write("Create Backup Daily--" + value.backup_day_value_database + "--");




                        }
                    }
                    sendmailokdayli();
                }
                catch
                {
                    statu_change.change_status_backup_false();
                }

                //////////////ftp/////
                ftp_datagrid();

                if (Convert.ToInt32(value.value_ftp_data) != 0)
                {

                    bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bb == true)
                    {
                        try
                        {

                            Ftp.Send_ftp();
                            sendmailftp();

                        }
                        catch
                        {
                            statu_change.change_status_FTP_false();
                        }
                    }
                }
                ///////////google

                datagrid_mail_google();

                if (Convert.ToInt32(value.value_ftp_data_mail_google) != 0)
                {

                    try
                    {



                        bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                        if (bb == true)
                        {

                            SQLiteConnection con = new SQLiteConnection(value.conlite);

                            string q8888m;
                            q8888m = "select * from Tbl_mono  ";
                            SQLiteDataAdapter da888 = new SQLiteDataAdapter(q8888m, con);
                            DataSet dsm888 = new DataSet();
                            da888.Fill(dsm888);



                            for (int i = 0; i < dsm888.Tables[0].Rows.Count; i++)
                            {

                                string q;
                                q = "select * from Tbl_mono ";
                                SQLiteDataAdapter da99 = new SQLiteDataAdapter(q, con);
                                DataSet ds99 = new DataSet();
                                da99.Fill(ds99);

                                string zipKonum = ds99.Tables[0].Rows[i][0].ToString();
                                string dosyaAdi = ds99.Tables[0].Rows[i][1].ToString();


                                google.DriveLogin();
                                IList<Google.Apis.Drive.v3.Data.File> dosyaBilgiler = google.GetFiles();

                                int sayac = 0;
                                string durum = "Haftalık";

                                if (dosyaBilgiler != null && dosyaBilgiler.Count > 0)
                                {
                                    foreach (var file in dosyaBilgiler)
                                    {

                                        if (file.Name == durum)
                                        {
                                            sayac++;
                                        }
                                    }
                                }
                                if (sayac == 0)
                                {
                                    google.CreateDirectory(durum);
                                }


                                if (dosyaBilgiler != null && dosyaBilgiler.Count > 0)
                                {
                                    foreach (var file in dosyaBilgiler)
                                    {

                                        if (file.Name == durum)
                                        {

                                            google.UploadFiles(durum, zipKonum, dosyaAdi, file.Id);

                                        }
                                    }
                                }
                                statu_change.change_status_google_true();
                            }

                        }
                    }
                    catch
                    {
                        statu_change.change_status_google_false();
                    }


                }

            }

            catch
            {
                change_status_false_day();
                sendmailcannot();

            }
        }






        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_3(object sender, EventArgs e)
        {



            int value_delete_count = Data_wekly.CurrentCell.RowIndex;
            string value_delete = Data_wekly.Rows[value_delete_count].Cells[3].Value.ToString();

            value_run_weekly_value();



            if ((Convert.ToInt32(value.value_run_daily_value) != 0) && (value_delete != null))
            {
                value_run_weekly_value();
                DialogResult result = MessageBox.Show("Haftalık zamanlama silinicek , Emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();

                    cmddd.CommandText = "DELETE FROM Tbl_weekly  WHERE  veriname='" + value_delete + "';";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    value_run_weekly_value();
                    con.Close();
                    LogWriter.Write("Deleted Weekly Backup");
                }

            }
        }
        public void data_for_timer_zip_weekly()

        {



            Data_wekly.Rows.Clear();


            value.day_time_chek_weekly = DateTime.Now.ToString("HH:mm");
            value.day_day_chek_weekly = DateTime.Now.DayOfWeek.ToString();


            switch (value.day_day_chek_weekly)
            {
                case "Friday":
                    value.day_day_chek_weekly2 = "Cuma";
                    break;
                case "Monday":
                    value.day_day_chek_weekly2 = "Pazartesi";

                    break;

                case "Saturday":
                    value.day_day_chek_weekly2 = "Cumartesi";

                    break;

                case "Sunday":
                    value.day_day_chek_weekly2 = "Pazar";

                    break;
                case "Thursday":
                    value.day_day_chek_weekly2 = "Perşembe";
                    break;
                case "Tuesday":
                    value.day_day_chek_weekly2 = "Salı";

                    break;


                default:
                    value.day_day_chek_weekly2 = "Çarşamba";
                    break;
            }



            string q889;



            q889 = "select * from Tbl_weekly where  day= '" + value.day_day_chek_weekly2 + "' ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q889, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Data_wekly.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());

                value.week_time = ds.Tables[0].Rows[i][2].ToString();
                value.day_day_chek_weekly3 = ds.Tables[0].Rows[i][1].ToString();


                switch (value.day_day_chek_weekly2)
                {
                    case "Cuma":
                        value.day_day_chek_weekly2 = "Friday";
                        break;
                    case "Pazartesi":

                        value.day_day_chek_weekly2 = "Monday";

                        break;

                    case "Cumartesi":
                        value.day_day_chek_weekly2 = "Saturday";

                        break;

                    case "Pazar":
                        value.day_day_chek_weekly2 = "Pazar";

                        break;
                    case "Perşembe":
                        value.day_day_chek_weekly2 = "Thursday";
                        break;
                    case "Salı":

                        value.day_day_chek_weekly2 = "Tuesday";

                        break;


                    default:
                        value.day_day_chek_weekly2 = "Wednesday";
                        break;
                }


                var result = TimeSpan.Parse(value.week_time) + TimeSpan.Parse("00:07");
                value.day_day_chek_weekly22 = result.ToString();

                value.day_day_chek_weekly22 = value.day_day_chek_weekly22.Substring(0, 5);

            }


        }
        private void weekly_zip_time_Tick(object sender, EventArgs e)
        {

            Data_wekly.Rows.Clear();
            try
            {

                data_for_timer_zip_weekly();

                if ((value.day_time_chek_weekly == value.day_day_chek_weekly22) && (value.day_day_chek_weekly == value.day_day_chek_weekly2))
                {
                    System.Diagnostics.Process.Start(".\\zip_weekly_quartz.bat");
                    sendmailokdayli();

                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Zipleme Hatası ");
            }
        }


        public void data_for_timer_ftp_weekly()
        {
            Data_wekly.Rows.Clear();


            value.day_time_chek_weekly = DateTime.Now.ToString("HH:mm");
            value.day_day_chek_weekly = DateTime.Now.DayOfWeek.ToString();


            switch (value.day_day_chek_weekly)
            {
                case "Friday":
                    value.day_day_chek_weekly2 = "Cuma";
                    break;
                case "Monday":
                    value.day_day_chek_weekly2 = "Pazartesi";

                    break;

                case "Saturday":
                    value.day_day_chek_weekly2 = "Cumartesi";

                    break;

                case "Sunday":
                    value.day_day_chek_weekly2 = "Pazar";

                    break;
                case "Thursday":
                    value.day_day_chek_weekly2 = "Perşembe";
                    break;
                case "Tuesday":
                    value.day_day_chek_weekly2 = "Salı";

                    break;


                default:
                    value.day_day_chek_weekly2 = "Çarşamba";
                    break;
            }



            string q889;



            q889 = "select * from Tbl_weekly where  day= '" + value.day_day_chek_weekly2 + "' ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q889, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Data_wekly.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());

                value.week_time = ds.Tables[0].Rows[i][2].ToString();
                value.day_day_chek_weekly3 = ds.Tables[0].Rows[i][1].ToString();


                switch (value.day_day_chek_weekly2)
                {
                    case "Cuma":
                        value.day_day_chek_weekly2 = "Friday";
                        break;
                    case "Pazartesi":

                        value.day_day_chek_weekly2 = "Monday";

                        break;

                    case "Cumartesi":
                        value.day_day_chek_weekly2 = "Saturday";

                        break;

                    case "Pazar":
                        value.day_day_chek_weekly2 = "Pazar";

                        break;
                    case "Perşembe":
                        value.day_day_chek_weekly2 = "Thursday";
                        break;
                    case "Salı":

                        value.day_day_chek_weekly2 = "Tuesday";

                        break;


                    default:
                        value.day_day_chek_weekly2 = "Wednesday";
                        break;
                }


                var result = TimeSpan.Parse(value.week_time) + TimeSpan.Parse("00:10");
                value.day_day_chek_weekly22 = result.ToString();

                value.day_day_chek_weekly22 = value.day_day_chek_weekly22.Substring(0, 5);

            }

        }




        private void weekly_ftp_time_Tick(object sender, EventArgs e)
        {
            Data_wekly.Rows.Clear();


            try
            {

                data_for_timer_ftp_weekly();

                if ((value.day_time_chek_weekly == value.day_day_chek_weekly22) && (value.day_day_chek_weekly == value.day_day_chek_weekly2))
                {
                    System.Diagnostics.Process.Start(".\\Ftp_send_week_quartz.bat");
                    sendmailftp();

                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Zipleme Hatası ");
            }


        }

        public void data_for_timer_month()
        {


            value.day_time_chek_month = DateTime.Now.ToString("HH:mm");
            value.day_day_chek_month = DateTime.Now.ToString("dd");

            string q88;


            q88 = "select * from Tbl_month where  day= '" + value.day_day_chek_month + "' ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q88, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                value.day_time_chek_month2 = ds.Tables[0].Rows[0][2].ToString();
                value.day_day_chek_month2 = ds.Tables[0].Rows[0][1].ToString();

            }

            richTextBox1.Clear();

            string q88m;
            q88m = "select * from Tbl_month ";
            SQLiteDataAdapter dam = new SQLiteDataAdapter(q88m, con);
            DataSet dsm = new DataSet();
            dam.Fill(dsm);

            for (int i = 0; i < dsm.Tables[0].Rows.Count; i++)
            {

                value.listOfStrings.Add(dsm.Tables[0].Rows[i][4].ToString());

            }
            foreach (string s in value.listOfStrings)
            {
                richTextBox1.Text += s + Environment.NewLine;
            }
            value.veritabanımail = richTextBox1.Text;


        }





        private void month_time_Tick(object sender, EventArgs e)
        {
            data_for_timer_month();

            if ((value.day_time_chek_month == value.day_time_chek_month2) && (value.day_day_chek_month == value.day_day_chek_month2))
            {

                backup_oto_month();

            }
        }



        public void backup_oto_month()

        {
            try
            {


                try
                {



                    string path;
                    string q888m;


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "delete  from Tbl_mono ";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();

                    con.Close();

                    q888m = "select * from Tbl_month  ";
                    SQLiteDataAdapter da88 = new SQLiteDataAdapter(q888m, con);
                    DataSet dsm88 = new DataSet();
                    da88.Fill(dsm88);

                    data_daily.Rows.Clear();

                    for (int i = 0; i < dsm88.Tables[0].Rows.Count; i++)
                    {
                        data_daily.Rows.Add(dsm88.Tables[0].Rows[i][0].ToString(), dsm88.Tables[0].Rows[i][2].ToString(), dsm88.Tables[0].Rows[i][4].ToString(), dsm88.Tables[0].Rows[i][5].ToString());

                        value.backup_day_value_database = dsm88.Tables[0].Rows[i][4].ToString();
                        value.backup_day_value_database_ftp = dsm88.Tables[0].Rows[i][5].ToString() + "\\";


                        string[] files = Directory.GetFiles(value.backup_day_value_database_ftp);

                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            if (fi.CreationTime.Date < DateTime.Now.Date.AddDays(-10))
                                fi.Delete();
                        }



                        value.connectionstring = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + "";
                        SqlConnection conn = new SqlConnection(value.connectionstring);

                        value.time_backup_mono = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");

                        string cmd = @"BACKUP DATABASE [" + value.backup_day_value_database + "] TO  DISK = N'" + value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak'" + " WITH NOFORMAT, NOINIT,  NAME = N'" + value.backup_day_value_database + value.time_backup_mono + ".bak' ,SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                        using (SqlCommand command = new SqlCommand(cmd, conn))
                        {
                            if (conn.State != ConnectionState.Open)
                            {

                                conn.Open();
                            }
                            command.CommandTimeout = 999999;
                            command.ExecuteNonQuery();
                            conn.Close();
                            statu_change.change_status_backup_true();


                            string filekonum = value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak";
                            string zipKonum = value.backup_day_value_database_ftp + value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            string zipFile = zipKonum.ToString();
                            string fileName = value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak";
                            string zipName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            string dosyaAdi = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.zipName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.fileName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                            value.fileName2 = value.backup_day_value_database + ".zip";
                            string filenamedata = dosyaAdi;

                            statu_change.change_status_backup_true();

                            using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                            {
                                FileInfo fi = new FileInfo(fileName);
                                zipArchive.CreateEntryFromFile(fi.FullName, fi.Name, CompressionLevel.Optimal);
                                zipArchive.Dispose();
                            }

                            string filepath = filekonum;
                            if (File.Exists(filepath))
                            {
                                File.Delete(filepath);


                            }
                            conn.Close();
                            path = value.backup_day_value_database_ftp + "\\" + value.fileName;
                            Data_Save.Data_save_for_read(path, filenamedata);

                            sendmailokdayli();
                            LogWriter.Write("Create Backup Daily--" + value.backup_day_value_database + "--");




                        }
                    }
                    sendmailokdayli();
                }
                catch
                {
                    statu_change.change_status_backup_false();
                }

                //////////////ftp/////
                ftp_datagrid();

                if (Convert.ToInt32(value.value_ftp_data) != 0)
                {

                    bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bb == true)
                    {
                        try
                        {

                            Ftp.Send_ftp();
                            sendmailftp();

                        }
                        catch
                        {
                            statu_change.change_status_FTP_false();
                        }
                    }
                }
                ///////////google

                datagrid_mail_google();

                if (Convert.ToInt32(value.value_ftp_data_mail_google) != 0)
                {

                    try
                    {



                        bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                        if (bb == true)
                        {

                            SQLiteConnection con = new SQLiteConnection(value.conlite);

                            string q8888m;
                            q8888m = "select * from Tbl_mono  ";
                            SQLiteDataAdapter da888 = new SQLiteDataAdapter(q8888m, con);
                            DataSet dsm888 = new DataSet();
                            da888.Fill(dsm888);



                            for (int i = 0; i < dsm888.Tables[0].Rows.Count; i++)
                            {

                                string q;
                                q = "select * from Tbl_mono ";
                                SQLiteDataAdapter da99 = new SQLiteDataAdapter(q, con);
                                DataSet ds99 = new DataSet();
                                da99.Fill(ds99);

                                string zipKonum = ds99.Tables[0].Rows[i][0].ToString();
                                string dosyaAdi = ds99.Tables[0].Rows[i][1].ToString();


                                google.DriveLogin();
                                IList<Google.Apis.Drive.v3.Data.File> dosyaBilgiler = google.GetFiles();

                                int sayac = 0;
                                string durum = "Aykık";

                                if (dosyaBilgiler != null && dosyaBilgiler.Count > 0)
                                {
                                    foreach (var file in dosyaBilgiler)
                                    {

                                        if (file.Name == durum)
                                        {
                                            sayac++;
                                        }
                                    }
                                }
                                if (sayac == 0)
                                {
                                    google.CreateDirectory(durum);
                                }


                                if (dosyaBilgiler != null && dosyaBilgiler.Count > 0)
                                {
                                    foreach (var file in dosyaBilgiler)
                                    {

                                        if (file.Name == durum)
                                        {

                                            google.UploadFiles(durum, zipKonum, dosyaAdi, file.Id);

                                        }
                                    }
                                }
                                statu_change.change_status_google_true();
                            }

                        }
                    }
                    catch
                    {
                        statu_change.change_status_google_false();
                    }


                }

            }

            catch
            {
                change_status_false_day();
                sendmailcannot();

            }

        }





        private void button4_Click_3(object sender, EventArgs e)
        {
            int value_delete_count = data_monthly.CurrentCell.RowIndex;
            string value_delete = data_monthly.Rows[value_delete_count].Cells[3].Value.ToString();



            value_run_month_value();





            if ((Convert.ToInt32(value.value_run_month_value) != 0) && (value_delete != null))
            {

                DialogResult result = MessageBox.Show("Aylık zamanlama silinecek, Emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();

                    cmddd.CommandText = "DELETE FROM Tbl_month WHERE  veriname='" + value_delete + "';";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    value_run_month_value();
                    con.Close();
                    LogWriter.Write("Deleted Monthly Backup");
                }

            }
        }


        public void data_for_timer_month_zip()
        {
            data_monthly.Rows.Clear();

            value.day_time_chek_month = DateTime.Now.ToString("HH:mm");
            value.day_day_chek_month = DateTime.Now.ToString("dd");

            string q868;


            q868 = "select * from Tbl_month where  day= '" + value.day_day_chek_month + "' ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q868, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                data_monthly.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());

                value.day_time_chek_month2 = ds.Tables[0].Rows[i][2].ToString();
                value.day_day_chek_month2 = ds.Tables[0].Rows[i][1].ToString();

                var result = TimeSpan.Parse(value.day_time_chek_month2) + TimeSpan.Parse("00:07");
                value.day_time_chek_month2 = result.ToString();

                value.day_time_chek_month2 = value.day_time_chek_month2.Substring(0, 5);



            }


        }


        private void month_zip_time_Tick(object sender, EventArgs e)
        {



            try
            {
                data_for_timer_month_zip();

                if ((value.day_time_chek_month == value.day_time_chek_month2) && (value.day_day_chek_month == value.day_day_chek_month2))
                {

                    System.Diagnostics.Process.Start(".\\zip_month_quartz.bat");

                    sendmailokdayli();


                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Zipleme Hatası ");
            }



        }

        public void data_for_timer_month_ftp()

        {
            data_monthly.Rows.Clear();

            value.day_time_chek_month1 = DateTime.Now.ToString("HH:mm");
            value.day_day_chek_month1 = DateTime.Now.ToString("dd");

            string q868;


            q868 = "select * from Tbl_month where  day= '" + value.day_day_chek_month + "' ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q868, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                data_monthly.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());

                value.day_time_chek_month2 = ds.Tables[0].Rows[i][2].ToString();
                value.day_day_chek_month2 = ds.Tables[0].Rows[i][1].ToString();

                var result = TimeSpan.Parse(value.day_time_chek_month2) + TimeSpan.Parse("00:10");
                value.day_time_chek_month2 = result.ToString();

                value.day_time_chek_month2 = value.day_time_chek_month2.Substring(0, 5);



            }


        }



        private void month_ftp_time_Tick(object sender, EventArgs e)
        {
            data_monthly.Rows.Clear();

            try
            {

                data_for_timer_month_ftp();

                if ((value.day_time_chek_month1 == value.day_time_chek_month2) && (value.day_day_chek_month1 == value.day_day_chek_month2))
                {

                    System.Diagnostics.Process.Start(".\\Ftp_send_month_quartz.bat");
                    sendmailftp();


                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Zipleme Hatası ");
            }



        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void Form1_MinimumSizeChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void button2_Click_3(object sender, EventArgs e)
        {

        }

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void notifyIcon2_MouseDoubleClick(object sender, MouseEventArgs e)
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
                notifyIcon2.Visible = false;

                timer1.Enabled = false;
                Form3 frm_pass = new Form3();
                frm_pass.Visible = true;

                Form1 frm1 = new Form1();

                frm1.Visible = false;

                Form2 frm2 = new Form2();
                frm2.Visible = false;




            }
            else

            {
                notifyIcon2.Visible = false;

                Form1 form1 = new Form1();
                form1.Visible = true;



                Form2 frm2 = new Form2();
                frm2.Visible = false;

                Form3 frm3 = new Form3();

                frm3.Visible = false;

            }

            timer1.Enabled = false;




        }

        private void button2_Click_4(object sender, EventArgs e)
        {



            Form1 frm2 = new Form1();
            frm2.Close();

        }

        private void buttonX12_Click_1(object sender, EventArgs e)
        {



        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel16_Click(object sender, EventArgs e)
        {

        }

        private void labelX10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_5(object sender, EventArgs e)
        {





        }

        private void button5_Click_3(object sender, EventArgs e)
        {


        }

        private void button2_Click_6(object sender, EventArgs e)
        {

        }

        private void button2_Click_7(object sender, EventArgs e)
        {

        }

        private void button2_Click_8(object sender, EventArgs e)
        {

        }

        private void labelX9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxRestore_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click_9(object sender, EventArgs e)
        {

        }

        private void data_daily_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




        }

        private void tool_data_source_4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_4(object sender, EventArgs e)
        {




        }

        private void button2_Click_10(object sender, EventArgs e)
        {

        }

        private void button5_Click_5(object sender, EventArgs e)
        {

        }

        private void button2_Click_11(object sender, EventArgs e)
        {


        }

        private void button2_Click_12(object sender, EventArgs e)
        {
            if (txt_pass_setting.Text == "@123")
            {
                gr_setting.Visible = true;

                ////////////////////////////////////////////////////////////////////


                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();

                ////////////////////////////////////////////////////
                ///
                string q;
                q = "select * from tbl_program_info ";



                data_setting.Rows.Clear();

                SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {


                    data_setting.Rows.Add(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString(), ds.Tables[0].Rows[i][6].ToString(), ds.Tables[0].Rows[i][7].ToString());

                    txt_day_setting.Text = ds.Tables[0].Rows[0][5].ToString();
                    txt_week_setting.Text = ds.Tables[0].Rows[0][6].ToString();
                    txt_month_setting.Text = ds.Tables[0].Rows[0][7].ToString();

                }

                con.Close();

            }
            else
                   if (txt_pass_setting.Text == "321@")
            {

                gr_setting.Visible = false;

            }
        }

        private void txt_week_seting_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_6(object sender, EventArgs e)
        {




            con.Open();
            SQLiteCommand cmdd = new SQLiteCommand();
            cmdd.CommandText = "update tbl_program_info set  day_count='" + txt_day_setting.Text + "',week_count='" + txt_week_setting.Text + "',month_count='" + txt_month_setting.Text + "'";
            cmdd.Connection = con;
            cmdd.ExecuteNonQuery();
            ftp_datagrid();
            MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }

        private void data_program_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gr_setting_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {



        }

        private void button6_Click_2(object sender, EventArgs e)
        {


        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sideNavPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click_3(object sender, EventArgs e)
        {

        }

        private void groupBoxBackup_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click_4(object sender, EventArgs e)
        {

        }



        private void button7_Click_1(object sender, EventArgs e)
        {





        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void listBoxCloud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sideNavItem10_Click(object sender, EventArgs e)
        {

            value_run_weekly_value();
            value_run_month_value();
            value_run_daily_value();
            datagrid_mail_google();

            tool_data_source.Text = value.data_douce_db;
            tool_user_db.Text = value.username_db;

            tool_data_source_2.Text = value.data_douce_db;
            tool_user_db_2.Text = value.username_db;

            tool_data_source_3.Text = value.data_douce_db;
            tool_user_db_3.Text = value.username_db;

            tool_data_source_4.Text = value.data_douce_db;
            tool_user_db_4.Text = value.username_db;

            tool_data_source_5.Text = value.data_douce_db;
            tool_user_db_5.Text = value.username_db;

            tool_data_source_6.Text = value.data_douce_db;
            tool_user_db_6.Text = value.username_db;
            tool_data_source_7.Text = value.data_douce_db;
            tool_user_db_7.Text = value.username_db;

            tool_data_source_8.Text = value.data_douce_db;
            tool_user_db_8.Text = value.username_db;
        }

        private void buttonX12_Click_2(object sender, EventArgs e)
        {
            bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            if (bb == true)
            {
                google.DriveLogin();

            }

            else
            {

                MessageBox.Show("Lütfen İnternet Bağlantısını Kontrol Edin", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void txt_pass_google_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void datagrid_mail_google()
        {

            data_mail_google.Rows.Clear();
            string q;
            q = "select * from Tbl_google ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                data_mail_google.Rows.Add(ds.Tables[0].Rows[i][0].ToString());

            }

            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.value_ftp_data_mail_google = dda.Fill(dt);

        }



        private void buttonX14_Click(object sender, EventArgs e)
        {
            datagrid_mail_google();

            if (Convert.ToInt32(value.value_ftp_data_mail_google) == 0)
            {
                value.mail_adress = txt_mail.Text;


                if ((txt_mail_google.Text == "") || (txt_pass_google.Text == ""))
                {
                    MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    if (Convert.ToInt32(value.value_ftp_data_mail_google) < 1)
                    {

                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        cmd.CommandText = "insert into Tbl_google ([mail],[password]) " +
                        "values('" + txt_mail_google.Text + "' , '" + txt_pass_google.Text + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        datagrid_mail_google();
                        MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        txt_mail_google.Text = "";
                        txt_pass_google.Text = "";
                        LogWriter.Write("Save GoogleDrive--" + txt_mail_google.Text + "--");
                    }

                    bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bb == true)
                    {
                        google.DriveLogin();

                    }

                    else
                    {

                        MessageBox.Show("Lütfen İnternet Bağlantısını Kontrol Edin", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            else

            {

                MessageBox.Show("Sadece bir tane mail adresi girebilirsiniz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }

        }

        private void data_mail_google_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonX15_Click(object sender, EventArgs e)
        {

            datagrid_mail_google();
            if (Convert.ToInt32(value.value_ftp_data_mail_google) != 0)
            {

                DialogResult result = MessageBox.Show("Mail bilgileri silinecek , emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "delete  from Tbl_google ";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    datagrid_mail_google();
                    con.Close();
                    string credPath = System.Environment.GetFolderPath(
                     System.Environment.SpecialFolder.Personal);

                    credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart");
                    System.IO.Directory.Delete(credPath, true);
                    LogWriter.Write("Deleted GoogleDrive");

                }

            }

            // System.IO.Directory.Delete(@".\\.credentials", true);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {



        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void groupPanel8_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            System.IO.Directory.Delete(@"C:\Documents\.credentials", true);
        }

        private void button6_Click_5(object sender, EventArgs e)
        {

        }

        private void sideNavItem5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_6(object sender, EventArgs e)
        {
            string q888m;
            q888m = "select * from Tbl_dayli  ";
            SQLiteDataAdapter da88 = new SQLiteDataAdapter(q888m, con);
            DataSet dsm88 = new DataSet();
            da88.Fill(dsm88);

            data_daily.Rows.Clear();

            for (int i = 0; i < dsm88.Tables[0].Rows.Count; i++)
            {
                data_daily.Rows.Add(dsm88.Tables[0].Rows[i][0].ToString(), dsm88.Tables[0].Rows[i][2].ToString(), dsm88.Tables[0].Rows[i][4].ToString(), dsm88.Tables[0].Rows[i][5].ToString());

                value.backup_day_value_database = dsm88.Tables[0].Rows[i][4].ToString();
                value.backup_day_value_database_ftp = dsm88.Tables[0].Rows[i][5].ToString() + "\\";


                value.connectionstring = "Data Source = " + value.data_douce_db + "; User Id = " + value.username_db + "; Password = " + value.password_db + "";
                SqlConnection conn = new SqlConnection(value.connectionstring);

                value.time_backup_mono = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");

                string cmd = @"BACKUP DATABASE [" + value.backup_day_value_database + "] TO  DISK = N'" + value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak'" + " WITH NOFORMAT, NOINIT,  NAME = N'" + value.backup_day_value_database + value.time_backup_mono + ".bak' ,SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                using (SqlCommand command = new SqlCommand(cmd, conn))
                {
                    if (conn.State != ConnectionState.Open)
                    {

                        conn.Open();
                    }
                    command.CommandTimeout = 999999;
                    command.ExecuteNonQuery();
                    conn.Close();



                    string filekonum = value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak";
                    string zipKonum = value.backup_day_value_database_ftp + value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                    string zipFile = zipKonum.ToString();
                    string fileName = value.backup_day_value_database_ftp + "\\" + value.backup_day_value_database + "-" + value.time_backup_mono + ".bak";
                    string zipName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                    string dosyaAdi = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                    value.zipName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                    value.fileName = value.backup_day_value_database + "-" + value.time_backup_mono + ".zip";
                    value.fileName2 = value.backup_day_value_database + ".zip";



                    using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                    {
                        FileInfo fi = new FileInfo(fileName);
                        zipArchive.CreateEntryFromFile(fi.FullName, fi.Name, CompressionLevel.Optimal);
                        zipArchive.Dispose();
                    }

                    string filepath = filekonum;
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);

                    }
                    else
                    {
                        MessageBox.Show("delete faild");

                    }
                }
            }

        }

        private void button6_Click_7(object sender, EventArgs e)
        {

        }

        private void button7_Click_3(object sender, EventArgs e)
        {

        }

        private void button6_Click_8(object sender, EventArgs e)
        {



        }

        private void Run_Tick(object sender, EventArgs e)
        {
            statu_change.change_status_run_true();
        }

        private void tool_data_source_3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_9(object sender, EventArgs e)
        {
            con.Open();
            SQLiteCommand cmddd = new SQLiteCommand();
            cmddd.CommandText = "delete  from Tbl_mono ";
            cmddd.Connection = con;
            cmddd.ExecuteNonQuery();

            con.Close();
        }

        private void buttonX6_Click_1(object sender, EventArgs e)
        {

            value.time_oto = com_time.Text;
            value.day_oto = com_day.Text;




            if (rd_day.Checked == true)
            {

                Create_daili_quartz();

            }

            if (rd_week.Checked == true)
            {
                value.time_oto = com_time.Text;




                Create_task_weekly_Quartz();

            }

            if (rd_month.Checked == true)
            {

                Create_task_monthly_Quartz();

            }
        }

        private void buttonX5_Click_1(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog selectPath = new FolderBrowserDialog();
                if (selectPath.ShowDialog() == DialogResult.OK)
                {
                    if (selectPath.SelectedPath.Length == 3)
                    {
                        txt_path_oto.Text = selectPath.SelectedPath;

                        value.path = selectPath.SelectedPath;
                        txt_path_oto.Text = selectPath.SelectedPath;

                    }
                    else
                        txt_path_oto.Text = selectPath.SelectedPath;
                    value.path = selectPath.SelectedPath;
                    value.path_ftp_bat = "Backupfile" + "_" + DateTime.Now.Date.Year + DateTime.Now.Date.Month.ToString("00") + DateTime.Now.Date.Day.ToString("00") + "_" + DateTime.Now.TimeOfDay.Hours.ToString("00") + DateTime.Now.TimeOfDay.Minutes.ToString("00") + DateTime.Now.TimeOfDay.Seconds.ToString("00") + ".BAK";


                }
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Kısayol seç");
            }
        }

        private void buttonX17_Click(object sender, EventArgs e)
        {

            value_run_weekly_value();
            value_run_month_value();
            value_run_daily_value();
            datagrid_mail_google();

            button1.Visible = true;
            data_daily.Visible = true;
            day_grop.Visible = true;


            week_grop.Visible = false;
            Data_wekly.Visible = false;

            month_grop.Visible = false;
            data_monthly.Visible = false;

            data_daily.Height = 200;
            day_grop.Height = 250;

        }

        private void buttonX18_Click(object sender, EventArgs e)
        {
            value_run_weekly_value();
            value_run_month_value();
            value_run_daily_value();
            datagrid_mail_google();


            data_daily.Visible = false;
            day_grop.Visible = false;


            month_grop.Visible = false;
            data_monthly.Visible = false;

            week_grop.Visible = true;
            Data_wekly.Visible = true;

            Data_wekly.Height = 200;
            week_grop.Height = 250;
        }

        private void buttonX19_Click(object sender, EventArgs e)
        {
            value_run_weekly_value();
            value_run_month_value();
            value_run_daily_value();
            datagrid_mail_google();

            day_grop.Visible = false;
            data_daily.Visible = false;

            week_grop.Visible = false;
            Data_wekly.Visible = false;

            month_grop.Visible = true;
            data_monthly.Visible = true;

            data_monthly.Height = 200;
            month_grop.Height = 250;
        }

        private void button1_Click_5(object sender, EventArgs e)
        {
            int value_delete_count = data_daily.CurrentCell.RowIndex;
            string value_delete = data_daily.Rows[value_delete_count].Cells[2].Value.ToString();




            value_run_daily_value();


            if ((Convert.ToInt32(value.value_run_daily_value)) != 0 && (value_delete != null))
            {

                DialogResult result = MessageBox.Show("Günlük zamanlama silinicek , Emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();
                    cmddd.CommandText = "DELETE FROM Tbl_dayli WHERE  veriname='" + value_delete + "';";


                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    value_run_daily_value();
                    con.Close();
                }

            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            int value_delete_count = Data_wekly.CurrentCell.RowIndex;
            string value_delete = Data_wekly.Rows[value_delete_count].Cells[3].Value.ToString();

            value_run_weekly_value();



            if ((Convert.ToInt32(value.value_run_daily_value) != 0) && (value_delete != null))
            {
                value_run_weekly_value();
                DialogResult result = MessageBox.Show("Haftalık zamanlama silinicek , Emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();

                    cmddd.CommandText = "DELETE FROM Tbl_weekly  WHERE  veriname='" + value_delete + "';";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    value_run_weekly_value();
                    con.Close();
                }

            }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            int value_delete_count = data_monthly.CurrentCell.RowIndex;
            string value_delete = data_monthly.Rows[value_delete_count].Cells[3].Value.ToString();



            value_run_month_value();





            if ((Convert.ToInt32(value.value_run_month_value) != 0) && (value_delete != null))
            {

                DialogResult result = MessageBox.Show("Aylık zamanlama silinecek, Emin misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {


                    con.Open();
                    SQLiteCommand cmddd = new SQLiteCommand();

                    cmddd.CommandText = "DELETE FROM Tbl_month WHERE  veriname='" + value_delete + "';";
                    cmddd.Connection = con;
                    cmddd.ExecuteNonQuery();
                    value_run_month_value();
                    con.Close();
                }

                ////FileStream fsOut = File.Create(@"C:\Users\arzu\Desktop\Backup_new\bin\Debug\encrypted.txt");

                ////TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                ////CryptoStream cs = new CryptoStream(fsOut, tdes.CreateEncryptor(), CryptoStreamMode.Write);

                ////StreamWriter sw = new StreamWriter(cs);

                ////sw.WriteLine("str");
                ////sw.Flush();

            }
        }

        private void data_pass_user()
        {


            string q;
            q = "select * from Tbl_connect_costumer ";

            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.status_pass_table = dda.Fill(dt);

        }


        private void data_select_pass()
        {

            data_mail_google.Rows.Clear();
            string q;
            q = "select * from Tbl_connect_costumer where password='" + value.password_customer + "'";

            SQLiteDataAdapter dda = new SQLiteDataAdapter(q, con);
            DataTable dt = new DataTable();
            dda.Fill(dt);
            value.data_select_pass = dda.Fill(dt);

        }
        private void buttonX26_Click(object sender, EventArgs e)
        {
            try
            {
                data_pass_user();

                if (Convert.ToInt32(value.status_pass_table) == 0)
                {

                    bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bb == true)
                    {
                        value.username_customer = txt_user_customer.Text;
                        value.password_customer = txt_pass_customer.Text;
                        int status_pass = 2;


                        if ((value.username_customer == "") || (value.password_customer == ""))
                        {
                            MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {


                            con.Open();
                            SQLiteCommand cmd = new SQLiteCommand();
                            cmd.CommandText = "insert into Tbl_connect_costumer ([username] , [password] , [status]) " +
                            "values('" + value.username_customer + "', '" + value.password_customer + "' , '" + status_pass + "')";
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            ftp_datagrid();
                            MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            con.Close();
                            txt_user_customer.Text = "";
                            txt_pass_customer.Text = "";

                            LogWriter.Write("Save FTP--" + value.ftp_adress + "--" + value.ftp_username + "--" + value.ftp_docpath + "--");
                            grop_pass.Enabled = false;
                            grop_pass_change.Enabled = true;
                        }


                    }

                    else
                    {

                        MessageBox.Show("İnternet bağlantısı kontrol edin", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch
            {

            }
        }

        private void buttonX27_Click(object sender, EventArgs e)
        {
            try
            {
                data_pass_user();

                if (Convert.ToInt32(value.status_pass_table) != 0)
                {

                    bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                    if (bb == true)
                    {
                        value.username_customer = txt_user_change.Text;
                        value.password_customer = txt_pass_old_change.Text;
                        value.change_password = txt_pass_change.Text;

                        int status_pass = 2;


                        if ((value.username_customer == "") || (value.password_customer == "") || (value.change_password == ""))
                        {
                            MessageBox.Show("Lütfen bütün alanları doldurunuz", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            data_select_pass();

                            if (value.data_select_pass != 0)
                            {



                                SQLiteCommand cmdd = new SQLiteCommand();
                                cmdd.CommandText = "UPDATE Tbl_connect_costumer SET password=@pasword ,username=@username where password =@oldpassword ";


                                con.Open();
                                cmdd.Connection = con;
                                cmdd.Parameters.AddWithValue("@pasword", value.change_password);
                                cmdd.Parameters.AddWithValue("@username", value.username_customer);
                                cmdd.Parameters.AddWithValue("@oldpassword", value.password_customer);
                                cmdd.ExecuteNonQuery();


                                con.Close();

                                txt_user_change.Text = "";
                                txt_pass_old_change.Text = "";
                                txt_pass_change.Text = "";
                                MessageBox.Show("Başarıyla Kaydedildi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LogWriter.Write("User change password --");
                            }
                            else
                            {
                                MessageBox.Show("Eski şıfre yanlış", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }


                    }

                    else
                    {

                        MessageBox.Show("İnternet bağlantısı kontrol edin", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch
            {

            }
        }

        private void buttonX30_Click(object sender, EventArgs e)
        {
            int status = 1;
            SQLiteCommand cmdd = new SQLiteCommand();
            cmdd.CommandText = "UPDATE Tbl_connect_costumer SET status=@status ";


            con.Open();
            cmdd.Connection = con;
            cmdd.Parameters.AddWithValue("@status", status);

            cmdd.ExecuteNonQuery();


            con.Close();


            MessageBox.Show("Şifreli Giriş Kapandı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonX29_Click(object sender, EventArgs e)
        {

            int status = 2;
            SQLiteCommand cmdd = new SQLiteCommand();
            cmdd.CommandText = "UPDATE Tbl_connect_costumer SET status=@status ";


            con.Open();
            cmdd.Connection = con;
            cmdd.Parameters.AddWithValue("@status", status);

            cmdd.ExecuteNonQuery();


            con.Close();


            MessageBox.Show("Şifreli Giriş Açldı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sideNavPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sideNavItem11_Click(object sender, EventArgs e)
        {
            tool_data_source.Text = value.data_douce_db;
            tool_user_db.Text = value.username_db;

            tool_data_source_2.Text = value.data_douce_db;
            tool_user_db_2.Text = value.username_db;

            tool_data_source_3.Text = value.data_douce_db;
            tool_user_db_3.Text = value.username_db;

            tool_data_source_4.Text = value.data_douce_db;
            tool_user_db_4.Text = value.username_db;

            tool_data_source_5.Text = value.data_douce_db;
            tool_user_db_5.Text = value.username_db;

            tool_data_source_6.Text = value.data_douce_db;
            tool_user_db_6.Text = value.username_db;
            tool_data_source_7.Text = value.data_douce_db;
            tool_user_db_7.Text = value.username_db;

            tool_data_source_8.Text = value.data_douce_db;
            tool_user_db_8.Text = value.username_db;
        }

        private void txt_key_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_adress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_tel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
                MessageBox.Show("lütfen rakam giriniz", "Alert!");
            }
        }

        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
                MessageBox.Show("lütfen rakam giriniz", "Alert!");
            }
        }

        private void txt_fam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
                MessageBox.Show("Rakam giremezsiniz", "Alert!");
            }
        }

        private void txt_compani_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
                MessageBox.Show("Rakam giremezsiniz", "Alert!");
            }
        }

        private void txt_tel_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length < 10)
            {
                MessageBox.Show("En az 10 tane Rakam girmelisiniz");
            }
        }

        private void button6_Click_10(object sender, EventArgs e)
        {
            
            
        }

        private void button6_Click_11(object sender, EventArgs e)
        {
         
        }

        private void key_chek_Tick(object sender, EventArgs e)
        {
            Key_Chek.Keychek();
        }

        private void button6_Click_12(object sender, EventArgs e)
        {

           
        }

        private void prog_Tick(object sender, EventArgs e)
        {
          



        }

        private void button6_Click_13(object sender, EventArgs e)
        {
           
               
            
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            ////this.sfButton1.Text = "Loading";
            ////busyIndicator.Show(this.sfButton1, new Point((this.sfButton1.Width / 2) + this.busyIndicator.Image.Width, (this.sfButton1.Height / 2) - this.busyIndicator.Image.Height / 2));
            ////for (int i = 0; i <= 10000000; i++)
            ////{
            ////    sampleData.Add(i);
            ////}
            ////busyIndicator.Hide();
            ////this.sfButton1.Text = "Get items";
            ////sampleData.Clear();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
           // progressBar1.Value ++;


        }
    }
}
