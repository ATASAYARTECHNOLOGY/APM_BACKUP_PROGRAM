using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SideNavSample
{
    public class value
    {
        public static string ftp_adress;
        public static string ftp_username;
        public static string ftp_password;
        public static string ftp_docpath;
        public static string source;
        public static string zipName;
        public static string fileName;
        public static string fileName2;



        public static int count_info_program;



        public static int day_count = 1;
        public static int week_count = 1;
        public static int month_count = 1;


        public static string day_count1;
        public static string week_count1;
        public static string month_count1;




        public static int connect;


        public static string time_backup_mono;


        public static string datasource;
        public static string datausername;
        public static string datapassword;



        public static string dateme;

        public static int value_ftp_data;
        public static int value_mail_data;

        public static int value_ftp_data_mail;

        public static string mail_adress;


        public static SqlConnection con = new SqlConnection("");

        public static string connectionstring = "";



        // public static string connection_string_derver="Data Source=DESKTOP-MU88J8Q  ; Database =APM; Uid=sa; password=123162  ; MultipleActiveResultSets=True";

        public static string connection_string_derver = "Data Source=95.9.183.48,2401  ; Database =APM; Uid=sa; password=136213Ata!! ; MultipleActiveResultSets=True";
        public static string conlite = "Data Source=APM.sqlite;Version=3;password=@ta";

        //password=@ta

        public static string username_db;
        public static string password_db;
        public static string data_douce_db;


       // public static string mailbcc="teknik@atasayarteknoloji.com";
        public static string mailbcc="arzu@atasayarteknoloji.com";
        public static string mailpass= "123123!!";
        public static string mailfrom= "teknoloji.atasayar@yandex.com";
        public static string mailsubject= "APM Backup Manager";


        public static string send_ftp_adress;
        public static string send_ftp_username;
        public static string send_ftp_password;
        public static string send_ftp_doc_path;
        public static string send_file_path;


        public static string path;
        public static string pathnew;

        public static string path_zip;
        public static string path_ftp_bat;

        public static string time_oto;
        public static string day_of_week;
        public static int i = 0;
        public static string day_oto;


        public static string send_mail_message_oto1;
        public static string send_mail_message_oto2;


        public static string path_for_file;
        public static string path_for_file2;
        public static string path_for_file3;
        public static string path_for_file4;
        public static string path_for_file5;
        public static string path_for_file6;
        public static string path_for_file7;
        public static string path_for_file8;



        public static string name;
        public static string fam;
        public static string mail;
        public static string tel;
        public static string company;
        public static string adress;
        public static string mac;
        public static string mac1;
        public static string hdd;
        public static string hdd1;
        public static string key;
        public static string Date_start;
        public static string city;

        public static int start_point;


        public static string queri_name;
        public static string queri_date;
        public static string queri_time;
        public static string queri_ftp;
        public static string queri_mail;
        public static string queri_issend;
        public static string day_time = "";
        public static string day_time_chek = "";
        public static string day_time_chek_zip = "";
        public static string day_time_chek_zip2 = "";


        public static Int64 value_google;


        public static string queri_weekly_day = "";


        public static int value_ftp_data_mail_google;

        public static string day_time_chek_FTP = "";


        public static string day_time_chek_weekly = "";
        public static string day_time_chek_weekly2 = "";
        public static string day_day_chek_weekly = "";
        public static string day_day_chek_weekly2 = "";
        public static string week_chek_weekly_zip = "";
        public static string day_day_chek_weekly3 = "";
        public static string day_day_chek_weekly22 = "";

        public static int value_run_month_value;


        public static string day_time_chek_month = "";

        public static string day_time_chek_month2 = "";


        public static string day_day_chek_month = "";


        public static string day_day_chek_month2 = "";

        public static string day_time_chek_month1;
        public static string day_day_chek_month1;

        public static string backup_day_value_database;
        public static string backup_day_value_database_ftp;



        public static string week_time = "";


        public static int value_run_daily_value;


        public static string Dayofweek;


        public static string keyforpage;

        public static List<string> listOfStrings = new List<string>();


        public static string veritabanımail;

        public static string ftpmail;


        public static string paswordforbat;

        public static string companyname;
        public static int value_company_data;


        public static string username_customer;
        public static string password_customer;
        public static string change_password;
        public static int status_pass_table;
        public static int data_select_pass;


        public static int cmd;


        public static string keydateforchekstart;
        public static string keydateforchekend;
        public static string  keychek;
        public static string _timeLength = string.Empty;
    }
}
