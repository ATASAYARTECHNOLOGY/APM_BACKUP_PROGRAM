using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideNavSample
{
    public class Ftp
    {

        public static void Send_ftp()
        {



            try
            {
               
                string Server;
                string Username;
                string Password;

                string path;
                string dirpath;

                string q1_ftp;
                SQLiteConnection con = new SQLiteConnection(value.conlite);

                string q888m;
                q888m = "select * from Tbl_mono  ";
                SQLiteDataAdapter da88 = new SQLiteDataAdapter(q888m, con);
                DataSet dsm88 = new DataSet();
                da88.Fill(dsm88);

               

                for (int i = 0; i < dsm88.Tables[0].Rows.Count; i++)
                {

                    string q;
                    q = "select * from Tbl_mono ";
                    SQLiteDataAdapter da99 = new SQLiteDataAdapter(q, con);
                    DataSet ds99 = new DataSet();
                    da99.Fill(ds99);

                    path = ds99.Tables[0].Rows[i][0].ToString();
                    value.fileName = ds99.Tables[0].Rows[i][1].ToString();

                    q1_ftp = "select * from Tbl_costumer_ftp ";
                    SQLiteDataAdapter da9 = new SQLiteDataAdapter(q1_ftp, con);
                    DataSet ds9 = new DataSet();
                    da9.Fill(ds9);



                    Server = "ftp://" + ds9.Tables[0].Rows[0][0].ToString();
                    Username = ds9.Tables[0].Rows[0][1].ToString();
                    Password = ds9.Tables[0].Rows[0][2].ToString();


                    dirpath = "/" + ds9.Tables[0].Rows[0][3].ToString();
                    //path = value.backup_day_value_database_ftp + value.fileName;
                    string Server2 = Server + dirpath;

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", Server2, value.fileName)));
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.UseBinary = true;
                    request.UsePassive = true;
                    request.Timeout = 2000000;
                    request.KeepAlive = true;
                    request.Credentials = new NetworkCredential(Username, Password);

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



                    LogWriter.Write("Send Ftp Daily--" + value.backup_day_value_database + "--" + Server + "--");

                    statu_change.change_status_FTP_true();

                    ////Form1 frm1 = Application.OpenForms["Form1"] as Form1;
                    ////frm1.backgroundWorker1.RunWorkerAsync();
                    ////frm1.backgroundWorker1.CancelAsync();

                }

            }
            catch
            {
                statu_change.change_status_FTP_false();
            }




        }
        
    }
}
