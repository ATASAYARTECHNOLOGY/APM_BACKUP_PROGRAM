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
    public class Key_Chek
    {
   
         public static void Keychek()
        {
            string qw;
            qw = "select * from tbl_program_info ";

            SQLiteDataAdapter da = new SQLiteDataAdapter(qw, value.conlite);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
              
                value.keyforpage = ds.Tables[0].Rows[i][1].ToString();

            }


            SqlConnection conn = new SqlConnection(value.connection_string_derver);


            string qg2 = "  select * from Tbl_costumer where(mac_adress = '" + value.keyforpage + "')";

            SqlDataAdapter da9 = new SqlDataAdapter(qg2, conn);
            DataSet ds9 = new DataSet();
            da9.Fill(ds9);
           value.keydateforchek = ds9.Tables[0].Rows[0][3].ToString();

            ////if (value.keydateforchek >= DateTime.Now.ToString())
            ////{

            ////}


        }
    }
}
