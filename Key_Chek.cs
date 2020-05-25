using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


            string qg2 = "  select * from Tbl_key where(li_key = '" + value.keyforpage + "')";

            SqlDataAdapter da9 = new SqlDataAdapter(qg2, conn);
            DataSet ds9 = new DataSet();
            da9.Fill(ds9);

            // string  vv= ds9.Tables[0].Rows[0][3].ToString();
             value.keydateforchekstart = ds9.Tables[0].Rows[0][2].ToString();
             value.keydateforchekend = ds9.Tables[0].Rows[0][3].ToString();
           
            string datestart = value.keydateforchekstart.Substring(0, 10);
          string start= datestart.Replace(".", "/");


            string dateend = value.keydateforchekend.Substring(0, 10);
            string end = dateend.Replace(".", "/");


            DateTime chekdatestart= DateTime.ParseExact(start, @"dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture);

            DateTime chekdateend= DateTime.ParseExact(end, @"dd/MM/yyyy",
             System.Globalization.CultureInfo.InvariantCulture);

            //TimeSpan zamanFarki = Convert.ToDateTime(chekdateend) - Convert.ToDateTime(chekdatestart);
            TimeSpan timedifference = Convert.ToDateTime(chekdateend) - DateTime.Now;
            int day = Convert.ToInt32(timedifference.TotalDays);

            if (day == 3) 

            {
                MessageBox.Show("Lisans süreniz bitmesine 3 gün kalmış. Lütfen Lisans alınız!");
               
            }
            else

            if (day <= 1)

            {
                MessageBox.Show("Lisans süreniz dolmuştur. Lütfen Lisans alınız!");
                Application.Exit();
            }


        }
    }
}
