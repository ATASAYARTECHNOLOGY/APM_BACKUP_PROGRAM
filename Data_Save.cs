using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideNavSample
{
    public class Data_Save
    {

        public static void Data_save_for_read(string path,string filenamedata)

        {
            try
            {

                SQLiteConnection con = new SQLiteConnection(value.conlite);
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.CommandText = "insert into Tbl_mono ([name] ,[date]) " +
                "values('" + path + "','" + filenamedata + "')";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
             
              
                con.Close();
              
                LogWriter.Write("Save Email--" + value.mail_adress + "--");


            }
            catch
            {

            }


        }

    }
}
