using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;
namespace RaintreePoultry
{
    public class Trader_Mthds : Connection
    {

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds = new DataSet();

        internal DataTable SAVM_TRADER_ORDER_REQUEST(string Data)
        {

            var  mdata = JsonConvert.DeserializeObject<RootTraderOrderRequest>(Data);

           
                try
                {

                    db_connection2();
                    cmd = new SqlCommand("SAVM_TRADER_ORDER_BOOKING", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", "SAVM_T_ORDER_BOOKING");
                    cmd.Parameters.AddWithValue("@SIZE_ID", mdata.SIZE_ID);
                    cmd.Parameters.AddWithValue("@ORDER_QTY", mdata.ORDER_QTY);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.CREATED_BY);
                    cmd.Parameters.AddWithValue("@CUST_CODE", mdata.CUST_CODE);
                    cmd.Parameters.AddWithValue("@ZONE_ID", mdata.ZONE_ID);
                    cmd.Parameters.AddWithValue("@SIZE", mdata.SIZE);
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    InsertConfirmation_NewBooking_Mail(dt, mdata.SIZE);
                }
                catch (Exception e)
                {

                }
                finally
                {
                    db_closed2();
                }
            

            return dt;
        }


        public class RootTraderOrderRequest
        {
            public int CREATED_BY { get; set; }
            public string CUST_CODE { get; set; }
            public string ORDER_QTY { get; set; }
            public int SIZE_ID { get; set; }
            public string ZONE_ID { get; set; }
            public string SIZE { get; set; }

            
        }


        public void InsertConfirmation_NewBooking_Mail(DataTable dt,string size)
        {
            //get data from db

            String Customer_Name = dt.Rows[0]["Customer_Name"].ToString();
            String Mobile_User_name = dt.Rows[0]["Mobile_User_Name"].ToString();
            String Bird_Size_Android = dt.Rows[0]["Bird_Size_Android"].ToString();
            String Quantity_Android = dt.Rows[0]["Quantity_Android"].ToString();
            String mDate1 = dt.Rows[0]["mDate1"].ToString();
            String Android_Zone = dt.Rows[0]["Android_Zone"].ToString();
           

            //making the html
            String msgBody = "<html xmlns=\"http://www.w3.org/1999/xhtml\">";
            msgBody = msgBody + "<head> <style> .tableClass { border-collapse: collapse; border: 1px solid #c6c6c6; width: auto; }  .tableClasstrtd { width: 150px; font-weight:bold; color: Black; background:#ffe4ac;} .tableClasstrtd2 { width: 150px; color: Black; background:#fffbf4;} .tableClass tr td { text-align: left; padding: 8px; border: 1px solid #c6c6c6;} .tableClass tr:nth-child(even) {background-color: #f2f2f2;} </style> </head>";
            msgBody = msgBody + "<body><p>Booking Details</p><br><table class=\"tableClass\" style=\"width:100%;\" border=\"1\">";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Trader Name</td> <td class=\"tableClasstrtd2\">" + Customer_Name + "</td></tr>";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Mobile User Name</td> <td class=\"tableClasstrtd2\">" + Mobile_User_name + "</td></tr>";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Bird Size</td> <td class=\"tableClasstrtd2\">" + size + "</td></tr>";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Todays Quantity</td> <td class=\"tableClasstrtd2\">" + Quantity_Android + " KG</td></tr>";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Zone</td> <td class=\"tableClasstrtd2\">" + Android_Zone + "</td></tr>";
            msgBody = msgBody + "</table><br><p>*** This is auto generated mail, Please do not reply to this mail ***</p> </body> </html>";
            //making the html

            //get data from db

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();

            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();
            SmtpUser.UserName = "automail.poultry@baramatiagro.com";
            SmtpUser.Password = "Bal@12345";
            //mail.To.Add("tushar.nalawade@raintreecomputing.com, tushar.nalawade@raintreecomputing.com");


            string xTo = "";
            if (Android_Zone.ToString().Trim().Contains("MORGAON"))
            {
                xTo = "suraj.gulumkar@baramatiagro.com,mahadev.jagtap@baramatiagro.com,gaurav.jagtap@baramatiagro.com,jagdish.pangarkar@baramatiagro.com,umesh.bhujbal@baramatiagro.com,madhav.chavan@baramatiagro.com,lav.jadhav@baramatiagro.com,rahul.shinde@baramatiagro.com";

            }
         
            else if (Android_Zone.ToString().Trim().Contains("TEMBHURNI"))
            {
                xTo = "suraj.gulumkar@baramatiagro.com,mahadev.jagtap@baramatiagro.com,madhav.chavan@baramatiagro.com,lav.jadhav@baramatiagro.com,amol.mane@baramatiagro.com";

            }
            else if (Android_Zone.ToString().Trim().Contains("KOLHAPUR") || Android_Zone.ToString().Trim().Contains("SATARA") || Android_Zone.ToString().Trim().Contains("NIPANI"))
            {
                xTo = "lav.jadhav@baramatiagro.com,pramod.gaikwad@baramatiagro.com,vijay.pawar@baramatiagro.com,umesh.bhujbal@baramatiagro.com,sushant.kamble@baramatiagro.com";

            }
            else if (Android_Zone.ToString().Trim().Contains("KHOPOLI") || Android_Zone.ToString().Trim().Contains("WADA") || Android_Zone.ToString().Trim().Contains("SHAHAPUR"))
            {
                xTo = "umesh.bhujbal@baramatiagro.com,juber.patel@baramatiagro.com,sandip.pagare@baramatiagro.com";

            }
            else if (Android_Zone.ToString().Trim().Contains("BAIHATA CHARIALI"))
            {
                xTo = "lalramnghka.chhakchhuak@baramatiagro.com,murugan.n@baramatiagro.com,dwipen.deka@baramatiagro.com,shahrukh.jaman@baramatiagro.com,ranjit.bora@baramatiagro.com,mannan.hussian@baramatiagro.com,mantu.deka@baramatiagro.com,deepak.phukan@baramatiagro.com,bidyut.kalita@baramatiagro.com,samar.kalita@baramatiagro.com";

            }
            else if (Android_Zone.ToString().Trim().Contains("CHH SAMBHAJINAGAR") || Android_Zone.ToString().Trim().Contains("CHALISGAON") )
            {
                xTo = "juber.patel@baramatiagro.com";

            }
            else if (Android_Zone.ToString().Trim().Contains("CHIKHALI"))
            {
                xTo = "gajanan.raut@baramatiagro.com";

            }
            else if (Android_Zone.ToString().Trim().Contains("RISOD") || Android_Zone.ToString().Trim().Contains("MURTIZAPUR"))
            {
                xTo = "suraj.vasudev@baramatiagro.com";

            }
            else if (Android_Zone.ToString().Trim().Contains("AMRAVATI"))
            {
                xTo = "jivandas.naik@baramatiagro.com";

            }
            else
            {
                  xTo = "mahadev.jagtap@baramatiagro.com,akash.poman@baramatiagro.com,fairoj.shaikh@baramatiagro.com,rahul.shinde@baramatiagro.com";
            }

           // xTo = "sonal.pise@raintreecomputing.com";

            mail.To.Add(xTo);

            mail.From = new MailAddress("automail.poultry@baramatiagro.com");
            //mail.CC.Add("dinesh.joshi@baramatiagro.com");
            // mail.Bcc.Add("tushar.nalawade@raintreecomputing.com");
            mail.Subject = "BAL Live Sales | New Booking by " + Mobile_User_name;
            mail.IsBodyHtml = true;
            mail.Body = msgBody;
            //SmtpServer.Host = "balmail.baramatiagro.com";
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUser.UserName, SmtpUser.Password);
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                
            }
        }


        internal DataTable SAVM_TRADER_FUND_TRANSFER(string Data)
        {

            var mdata = JsonConvert.DeserializeObject<RootTRADER_FUND_TRANSFER>(Data);

            string Photo_Path1 = "0";
            if (mdata.PHOTO_PATH.ToString().Equals("0"))
            {

            }
            else
            {
                string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                Photo_Path1 = "TrasferMode" + mdata.CREATED_BY + "_" + currentDateTime1 + ".jpg";
                String FolderPath = "~/FUNDTRANS_PHOTO/";
                UploadPhoto(mdata.PHOTO_PATH, FolderPath, Photo_Path1);
            }
            try
            {

                db_connection2();
                cmd = new SqlCommand("SAVM_TRADER_FUND_TRANSFER", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", "SAVM_T_FUND_TRANSFER");
                cmd.Parameters.AddWithValue("@BANK_ID", mdata.BANK_ID);
                cmd.Parameters.AddWithValue("@TRANSFER_ID", mdata.FUND_TRANS_MODE);
                cmd.Parameters.AddWithValue("@CREATED_BY", mdata.CREATED_BY);
                cmd.Parameters.AddWithValue("@CUST_CODE", mdata.CUST_CODE);
                cmd.Parameters.AddWithValue("@AMOUNT", mdata.AMOUNT);
                cmd.Parameters.AddWithValue("@PHOTO", Photo_Path1);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                mailTo_AccountSaleDept_FundTransfer(dt);
            }
            catch (Exception e)
            {

            }
            finally
            {
                db_closed2();
            }


            return dt;
        }


        public void UploadPhoto(String PhotoBlob, String FolderPath, String PhotoPath)
        {
            if (PhotoBlob != null)
            {
                try
                {
                    byte[] b = Convert.FromBase64String(PhotoBlob);
                    // PhotoPath = "a.png";
                    MemoryStream ms = new MemoryStream(b);
                    FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath(FolderPath) + PhotoPath, FileMode.Create);
                    ms.WriteTo(fs); ms.Close(); fs.Close(); fs.Dispose();
                }
                catch (Exception ex)
                {
                }
            }
        }
        public class RootTRADER_FUND_TRANSFER
        {
            public int CREATED_BY { get; set; }
            public string CUST_CODE { get; set; }
            public string AMOUNT { get; set; }
            public int BANK_ID { get; set; }
            public string FUND_TRANS_MODE { get; set; }
            public string PHOTO_PATH { get; set; }
        }


        private void mailTo_AccountSaleDept_FundTransfer(DataTable dt)
        {


            String BANK_NAME = dt.Rows[0]["BANK_NAME"].ToString();
            String BANK_TYPE = dt.Rows[0]["BANK_TYPE"].ToString();
            String AMOUNT = dt.Rows[0]["AMOUNT"].ToString();
            String UserName = dt.Rows[0]["USER_NAME"].ToString();



            string s = "http://123.63.131.10:7074/FUNDTRANS_PHOTO/" + dt.Rows[0]["Photo_Path"].ToString();
            //making the html
            String msgBody = "<html xmlns=\"http://www.w3.org/1999/xhtml\">";
            msgBody = msgBody + "<head> <style> .tableClass { border-collapse: collapse; border: 1px solid #c6c6c6; width: auto; }  .tableClasstrtd { width: 150px; font-weight:bold; color: Black; background:#ffe4ac;} .tableClasstrtd2 { width: 150px; color: Black; background:#fffbf4;} .tableClass tr td { text-align: left; padding: 8px; border: 1px solid #c6c6c6;} .tableClass tr:nth-child(even) {background-color: #f2f2f2;} </style> </head>";
            msgBody = msgBody + "<body><p>New Fund Transfer Posted</p><br><table class=\"tableClass\" style=\"width:100%;\" border=\"1\">";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Posted By</td> <td class=\"tableClasstrtd2\">" + UserName + "</td><td id=\"mImage\" rowspan=\"6\"><a href ='" + s + "'><img src='" + s + "' width=200px;height=300px;'></a></td></tr>";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Bank Name</td> <td class=\"tableClasstrtd2\">" + BANK_NAME + "</td></tr>";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Fund Transfer Mode</td> <td class=\"tableClasstrtd2\">" + BANK_TYPE + "</td></tr>";
            msgBody = msgBody + "<tr><td class=\"tableClasstrtd\">Amount</td> <td class=\"tableClasstrtd2\">" + AMOUNT + "</td></tr>";

            msgBody = msgBody + "</table>";
            msgBody = msgBody + "<br><p>*** Please post your remark in BAL Tradar Mobile App ***</p>";
            msgBody = msgBody + "<br><p>*** This is auto generated mail, Please do not reply to this mail ***</p>";
            msgBody = msgBody + " </body> </html>";
            //making the html

            //get data from db
            //string xTo = Unit_Head_Email_Addr + ", " +
            //    SO_Email_Addr + ", " +
            //    MR_Email_Addr;

           string xTo = "mahadev.nikam@baramatiagro.com,nilesh.bobade@baramatiagro.com,amol.rupnawar@baramatiagro.com";
           // string xTo = "sonal.pise@raintreecomputing.com";
            sendMyMail(xTo, msgBody, "BAL CBF Tradar App | New Fund Transfer Posted by " + UserName);
        }


        private void sendMyMail(string xTo, string msgBody, string subject)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            //mail.To.Add("tushar.nalawade@raintreecomputing.com, tushar.nalawade@raintreecomputing.com");
            mail.To.Add(xTo);
            mail.From = new MailAddress("automail.poultry@baramatiagro.com");
           // mail.CC.Add("dinesh.joshi@baramatiagro.com,suraj.gulumkar@baramatiagro.com, mahadev.jagtap@baramatiagro.com, gaurav.jagtap@baramatiagro.com");
            //mail.Bcc.Add("contractfarm@baramatiagro.com,tushar.nalawade@raintreecomputing.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = msgBody;
            SmtpServer.Host = "balmail.baramatiagro.com";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
              
            }
        }


        internal DataSet GET_CUSTOMER_BALANCE(string TASK,string CUSTOMER_CODE)
        {
            db_connection2();
            try
            {

                cmd = new SqlCommand("GET_CUSTOMER_BALANCE", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@CUSTOMER_CODE", CUSTOMER_CODE);
         
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {

            }
            finally
            {
                db_closed2();
            }
            return ds;
        }






        // Claim Mort Aditya Yadav 2023-07-11
        internal DataTable GET_CLAIM_MORT_DETAILS(string TASK, string CUST_CODE, string SEARCH, string SEARCH1)
        {
            db_connection2();
            try
            {

                cmd = new SqlCommand("GET_TRADER_CLAIM_MORT", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@CUST_CODE", CUST_CODE);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {

            }
            finally
            {
                db_closed2();
            }
            return dt;
        }

        // Claim Mort Aditya Yadav 2023-07-11

        public class RootCLAIM_MORT
        {
            public int id { get; set; }
            public int TOKAN { get; set; }
            public string PLANT_CODE { get; set; }
            public string VEHICLE_NO { get; set; }
            public string WEIGHT { get; set; }
            public string ORDER_REASON { get; set; }
            public double SUM { get; set; }
            public double AVG { get; set; }
            public double RATE { get; set; }
            public string TOKAN_DATE { get; set; }
            public int CLAIM_MORT_BIRDS { get; set; }
            public double CLAIM_TOTAL_KG { get; set; }
            public double TOTAL_AVG_WEIGHT { get; set; }
            public string PHOTO { get; set; }
            public int CREATED_BY { get; set; }
            public string CUSTOMER_CODE { get; set; }
            public string CREATED_DATE { get; set; }
            public int MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public string SQLITE_ID { get; set; }
            public bool ISDELETED { get; set; }

        }

        internal DataTable SAVE_CLAIM_MORT(string TASK, string DATA)
        {

            RootCLAIM_MORT[] mdata = JsonConvert.DeserializeObject<RootCLAIM_MORT[]>(DATA);

            try
            {
                db_connection2();
                da = new SqlDataAdapter();

                for (int i = 0; i < mdata.Count(); i++)
                {

                    string Photo_Path1 = "0";
                    if (mdata[i].PHOTO.ToString().Equals("0"))
                    {
                        Photo_Path1 = "NA";
                    }
                    else
                    {
                        string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Photo_Path1 = "Claim_Mort" + mdata[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";
                        String FolderPath = "~/CLAIM_MORT_PHOTO/";
                        UploadPhoto(mdata[i].PHOTO, FolderPath, Photo_Path1);
                    }



                    cmd = new SqlCommand("SAVE_CLAIM_MORT", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@TOKAN", mdata[i].TOKAN);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@TOKAN_DATE", mdata[i].TOKAN_DATE);
                    cmd.Parameters.AddWithValue("@CUSTOMER_CODE", mdata[i].CUSTOMER_CODE);
                    cmd.Parameters.AddWithValue("@VEHICLE_NO", mdata[i].VEHICLE_NO);
                    cmd.Parameters.AddWithValue("@WEIGHT", mdata[i].WEIGHT);
                    cmd.Parameters.AddWithValue("@ORDER_REASON", mdata[i].ORDER_REASON);
                    cmd.Parameters.AddWithValue("@TOTAL_SUM", mdata[i].SUM);
                    cmd.Parameters.AddWithValue("@TOTAL_AVG", mdata[i].AVG);
                    cmd.Parameters.AddWithValue("@RATE", mdata[i].RATE);
                    cmd.Parameters.AddWithValue("@CLAIM_MORT_BIRDS", mdata[i].CLAIM_MORT_BIRDS);
                    cmd.Parameters.AddWithValue("@CLAIM_TOTAL_KG", mdata[i].CLAIM_TOTAL_KG);
                    cmd.Parameters.AddWithValue("@TOTAL_AVG_WEIGHT", mdata[i].TOTAL_AVG_WEIGHT);
                    cmd.Parameters.AddWithValue("@PHOTO", Photo_Path1);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata[i].SQLITE_ID);

                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                db_closed2();
            }


            return dt;
        }
    }
}