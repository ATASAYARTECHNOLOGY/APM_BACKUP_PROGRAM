using org.omg.CORBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideNavSample
{
    public class statu_change
    {
        /// <summary>
        /// ///////////////////////////////////////ftp///////////////////////////
        /// </summary>
        public static void change_status_FTP_true()

        {
            try
            {

              


                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();



                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();



                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 2;

                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);

                string q = "update Tbl_costumer set id_ftp='" + Startstatu + "', FTP_Date='" + datestatu + "' where id_costumer= '" + costumer_id + "' ";

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

        public static void change_status_FTP_false()

        {
            try
            {


                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();


                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();


                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 1;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);

                string q = "update Tbl_costumer set id_ftp='" + Startstatu + "',FTP_Date='" + datestatu + "' where id_costumer= '" + costumer_id + "' ";

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

        /// <summary>
        /// ////////////////////////////////////////////google//////////////////
        /// </summary>

        public static void change_status_google_true()

        {
            try
            {

                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();



                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 2;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);

                string q = "update Tbl_costumer set id_google='" + Startstatu + "',google_Date='" + datestatu + "' where id_costumer= '" + costumer_id + "' ";

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


        public static void change_status_google_false()

        {
            try
            {

                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();


                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 1;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);
                string q = "update Tbl_costumer set id_google='" + Startstatu + "',google_Date='" + datestatu + "' where id_costumer= '" + costumer_id + "' ";

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

        ///////////////////////////////backup///////////////////////////////
        public static void change_status_backup_true()

        {
            try
            {




                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();

                SqlConnection myconnection1;

               
                SqlConnection conn = new SqlConnection(value.connection_string_derver);
               

                string qg2 = "  select * from Tbl_costumer where mac_adress = '" + macdata + "'";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();
              


                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 2;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);

                string q = "update Tbl_costumer set id_backup='" + Startstatu + "' , backup_Date='" + datestatu + "'  where id_costumer= '" + costumer_id + "' ";

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



        public static void change_status_backup_false()

        {
            try
            {


                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();


                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;
                int Startstatu = 1;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);

                string q = "update Tbl_costumer set id_backup='" + Startstatu + "',Backup_Date='" + datestatu + "'  where id_costumer= '" + costumer_id + "' ";

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
        //////////////////////////////////////mail////////////////////////////
        ///

        public static void change_status_mail_true()

        {
            try
            {

                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();



                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();
                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 2;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);

                string q = "update Tbl_costumer set id_mail='" + Startstatu + "',mail_Date='" + datestatu + "'  where id_costumer= '" + costumer_id + "' ";

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


        public static void change_status_mail_false()

        {
            try
            {

                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();



                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 1;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);

                string q = "update Tbl_costumer set id_mail='" + Startstatu + "',mail_Date='" + datestatu + "' where id_costumer= '" + costumer_id + "' ";

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
        //////////////////////////////////////////run status////////////////////////
        ///


        public static void change_status_run_true()

        {
            try
            {

                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();



                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 2;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);

                string q = "update Tbl_costumer set id_status='" + Startstatu + "',status_Date='" + datestatu + "' where id_costumer= '" + costumer_id + "' ";

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


        public static void change_status_run_false()

        {
            try
            {


                SQLiteConnection con = new SQLiteConnection(value.conlite);
                string macdata;
                string q1_ftp = "select * from tbl_program_info ";
                SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9);

                macdata = ds9.Tables[0].Rows[0][3].ToString();


                SqlConnection conn = new SqlConnection(value.connection_string_derver);


                string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + macdata + "')";

                SqlDataAdapter da99 = new SqlDataAdapter(qg2, conn);
                DataSet ds99 = new DataSet();
                da99.Fill(ds99);
                string costumer_id = ds99.Tables[0].Rows[0][0].ToString();

                SqlConnection myconnection;

                myconnection = new SqlConnection();
                myconnection.ConnectionString = value.connection_string_derver;

                int Startstatu = 1;
                string format = "yyyy-MM-dd HH:mm:ss";
                string datestatu = DateTime.Now.ToString(format);
                string q = "update Tbl_costumer set id_status='" + Startstatu + "',status_Date='" + datestatu + "' where id_costumer= '" + costumer_id + "' ";

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
    }
}
