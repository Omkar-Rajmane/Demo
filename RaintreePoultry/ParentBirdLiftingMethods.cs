using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RaintreePoultry
{
    public class ParentBirdLiftingMethods:Connection
    {

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        internal DataSet GETM_LOGIN(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
        {
            db_connection();
            try
            {

                cmd = new SqlCommand("GETM_RPT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);
                cmd.Parameters.AddWithValue("@TO_DATE", TO_DATE);
                cmd.Parameters.AddWithValue("@FROM_DATE", FROM_DATE);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);


                if (TASK.ToString() == "GET_PARENT_BIRD_LIFTING_LOGIN")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        string pwd = ds.Tables[0].Rows[0]["PASSWORD"].ToString();

                        pwd = Decrypt(pwd);


                        ds.Tables[0].Rows[0]["PASSWORD"] = pwd;

                    }

                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                db_closed();
            }
            return ds;
        }


        // 2023-06-20 Aditya Yadav
        internal DataSet GET_MASTERS(string TASK, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string CREATED_BY)
        {

            db_connection();

            try
            {
                cmd = new SqlCommand("GETM_MASTERS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@TO_DATE", TO_DATE);
                cmd.Parameters.AddWithValue("@FROM_DATE", FROM_DATE);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);
                cmd.Parameters.AddWithValue("@SEARCH3", SEARCH3);
                cmd.Parameters.AddWithValue("@USER_ID", CREATED_BY);

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                con.Close();
            }
            catch (Exception e)
            {

            }
            finally
            {
                db_closed();
            }

            return ds;
        }




        // 2023-06-28 Aditya Yadav for bird Lifting app
        internal DataTable GET_PARENT_ORDER_TOKEN_DETAILS(string TASK, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3)
        {

            db_connection2();

            try
            {
                cmd = new SqlCommand("GET_P_ORDER_TOKEN", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;

                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);
                cmd.Parameters.AddWithValue("@SEARCH3", SEARCH3);

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                con2.Close();
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



        // 2023-06-21 Aditya Yadav
        internal DataTable SAVE_PARENT_BASE_RATE(string TASK, string CREATED_BY, string ZONES, string PLANTS, string MATERIALS)
        {

            db_connection2();

            try
            {
                cmd = new SqlCommand("SAVE_P_BASE_RATE", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;

                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);
                cmd.Parameters.AddWithValue("@ZONES", ZONES);
                cmd.Parameters.AddWithValue("@PLANTS", PLANTS);
                cmd.Parameters.AddWithValue("@MATERIALS", MATERIALS);


                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                con2.Close();
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



        public class RootPARENTTOKENWEIGHT
        {
            public double EMPTY_WEIGHT { get; set; }
            public double FULL_WEIGHT { get; set; }
            public int ids { get; set; }
            public double NT_WT { get; set; }
            public string PLANT { get; set; }
            public string RECEIPT_PHOTO { get; set; }
            public string SQLITE_ID { get; set; }
            public int STATUS { get; set; }
            public int TOKEN_NO { get; set; }
            public string VEHICLE_PHOTO { get; set; }
        }

        internal DataTable SAVE_PARENT_TOKEN_WEIGHT(string TASK, string CREATED_BY, string DATA)
        {
            RootPARENTTOKENWEIGHT[] mdata = JsonConvert.DeserializeObject<RootPARENTTOKENWEIGHT[]>(DATA);
            db_connection2();

            try
            {
                string VEHICLE_PHOTO = "";
                string RECEIPT_PHOTO = "";
                string TRIP = "";
                if (TASK.Equals("SAVE_P_TOKEN_EMPTY_WEIGHT"))
                {
                    TRIP = "Trip1";
                }
                else
                {
                    TRIP = "Trip2";
                }

                if (mdata[0].VEHICLE_PHOTO.ToString().Equals("0"))
                {
                }
                else
                {
                    // string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    VEHICLE_PHOTO = mdata[0].TOKEN_NO + "_Vehicle_" + mdata[0].PLANT + "_" + TRIP +"_"+ mdata[0].SQLITE_ID + ".jpg";
                    String FolderPath = "~/PARENT_ORDER_PHOTO/";
                    UploadPhoto(mdata[0].VEHICLE_PHOTO, FolderPath, VEHICLE_PHOTO);

                }

                if (mdata[0].RECEIPT_PHOTO.ToString().Equals("0"))
                {
                }
                else
                {
                    //string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    RECEIPT_PHOTO = mdata[0].TOKEN_NO + "_" + mdata[0].PLANT + "_Receipt_" + TRIP +"_"+ mdata[0].SQLITE_ID + ".jpg";
                    String FolderPath = "~/PARENT_ORDER_PHOTO/";
                    UploadPhoto(mdata[0].RECEIPT_PHOTO, FolderPath, RECEIPT_PHOTO);

                }


                cmd = new SqlCommand("SAVE_P_TOKEN_WEIGHT", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;

                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);
                cmd.Parameters.AddWithValue("@TOKEN", mdata[0].TOKEN_NO);
                cmd.Parameters.AddWithValue("@EMPTY_WEIGHT", mdata[0].EMPTY_WEIGHT);
                cmd.Parameters.AddWithValue("@FULL_WEIGHT", mdata[0].FULL_WEIGHT);
                cmd.Parameters.AddWithValue("@NET_WEIGHT", mdata[0].NT_WT);
                cmd.Parameters.AddWithValue("@VEHICLE_PHOTO", VEHICLE_PHOTO);
                cmd.Parameters.AddWithValue("@RECEIPT_PHOTO", RECEIPT_PHOTO);
                cmd.Parameters.AddWithValue("@SQLITE_ID", mdata[0].SQLITE_ID);

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                con2.Close();
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


        //To decrypt
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length)
                            ;
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        
        
 // 2024-04-23 Aditya Yadav for bird Lifting app
 internal DataSet GET_PARENT_ORDER_TOKEN_DETAILS_NEW(string TASK, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3)
 {

     db_connection2();

     try
     {
         cmd = new SqlCommand("GET_P_ORDER_TOKEN", con2);
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Connection = con2;

         cmd.Parameters.AddWithValue("@TASK", TASK);
         cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
         cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
         cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);
         cmd.Parameters.AddWithValue("@SEARCH3", SEARCH3);

         da = new SqlDataAdapter();
         da.SelectCommand = cmd;
         da.Fill(ds);
         con2.Close();

         ds.Tables[0].TableName = "T_TOKEN";
         ds.Tables[1].TableName = "T_MATERAIL";

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


    }
}