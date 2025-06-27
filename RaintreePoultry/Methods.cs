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
    public class Methods:Connection
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds = new DataSet();

        internal DataSet AllDataMasters(string TASK, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string USER_ID)
        {
            db_connection();
            try
            {

                //string EncryptionKey = "MAKV2SPBNI99212";



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
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);

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

        internal DataTable SAVM_SHED_CLEANING(string TASK, string Data)
        {

            RootShedCleaning mdata = JsonConvert.DeserializeObject<RootShedCleaning>(Data);

            for (int i = 0; i < mdata.tshedcleaningItem.Count; i++)
            {

                try
                {

                    string Photo_Path1 = "0";
                    if (mdata.tshedcleaningItem[i].BURNING_PHOTO.ToString().Equals("0"))
                    {

                    }
                    else
                    {
                        string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Photo_Path1 = "Burning" + mdata.tshedcleaningItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";



                        String FolderPath = "~/CBF_PHOTO/";

                        UploadPhoto(mdata.tshedcleaningItem[i].BURNING_PHOTO, FolderPath, Photo_Path1);

                    }


                    string Photo_Path2 = "0";
                    if (mdata.tshedcleaningItem[i].FUMIGATION_PHOTO.ToString().Equals("0"))
                    {

                    }
                    else
                    {
                        string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Photo_Path2 = "Fumigation" + mdata.tshedcleaningItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";



                        String FolderPath = "~/CBF_PHOTO/";

                        UploadPhoto(mdata.tshedcleaningItem[i].FUMIGATION_PHOTO, FolderPath, Photo_Path2);

                    }


                    db_connection2();
                    cmd = new SqlCommand("SAVM_SHED_CLEANING", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@BURNING_DATE", mdata.tshedcleaningItem[i].BURNING_DATE);
                    cmd.Parameters.AddWithValue("@BURNING_PHOTO", Photo_Path1);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tshedcleaningItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@DT_BURNING_DAYS", mdata.tshedcleaningItem[i].DT_BURNING_DAYS);
                    cmd.Parameters.AddWithValue("@DT_FUMIGATION_DAYS", mdata.tshedcleaningItem[i].DT_FUMIGATION_DAYS);
                    cmd.Parameters.AddWithValue("@FUMIGATION_DATE", mdata.tshedcleaningItem[i].FUMIGATION_DATE);
                    cmd.Parameters.AddWithValue("@FUMIGATION_PHOTO", Photo_Path2);
                    cmd.Parameters.AddWithValue("@LAST_LIFTING_DATE", mdata.tshedcleaningItem[i].LAST_LIFTING_DATE);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.tshedcleaningItem[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@PRO_PLACEMENT_DATE", mdata.tshedcleaningItem[i].PRO_PLACEMENT_DATE);
                    cmd.Parameters.AddWithValue("@SHED_GAP_DATE", mdata.tshedcleaningItem[i].SHED_GAP_DATE);
                    cmd.Parameters.AddWithValue("@ST_LOC", mdata.tshedcleaningItem[i].ST_LOC);
                    cmd.Parameters.AddWithValue("@SQLITE_DATE", mdata.tshedcleaningItem[i].SQLITE_DATE);
                    cmd.Parameters.AddWithValue("@LAT", mdata.tshedcleaningItem[i].LAT);
                    cmd.Parameters.AddWithValue("@LON", mdata.tshedcleaningItem[i].LON);  
                    cmd.Parameters.AddWithValue("@TOTAL_SQFT", mdata.tshedcleaningItem[i].TOTAL_SQFT);
                    cmd.Parameters.AddWithValue("@FUMIGATION_FROM_DATE", mdata.tshedcleaningItem[i].FUMIGATION_FROM_DATE);
                    cmd.Parameters.AddWithValue("@FUMIGATION_TO_DATE", mdata.tshedcleaningItem[i].FUMIGATION_TO_DATE);
                    cmd.Parameters.AddWithValue("@POTASSIUM_PERMANGANAT_QTY", mdata.tshedcleaningItem[i].POTASSIUM_PERMANGANAT_QTY);
                    cmd.Parameters.AddWithValue("@POTASSIUM_PERMANGANAT_PERTBARDS", mdata.tshedcleaningItem[i].POTASSIUM_PERMANGANAT_PERTBARDS);
                    cmd.Parameters.AddWithValue("@BLEACHING_POWEDER_QTY", mdata.tshedcleaningItem[i].BLEACHING_POWEDER_QTY);
                    cmd.Parameters.AddWithValue("@BLEACHING_POWEDER_PERTBARDS", mdata.tshedcleaningItem[i].BLEACHING_POWEDER_PERTBARDS);
                    cmd.Parameters.AddWithValue("@FORMALDEHYDE_QTY", mdata.tshedcleaningItem[i].FORMALDEHYDE_QTY);
                    cmd.Parameters.AddWithValue("@FORMALDEHYDE_PERTBARDS", mdata.tshedcleaningItem[i].FORMALDEHYDE_PERTBARDS);
                    cmd.Parameters.AddWithValue("@BURNING_DATE1", mdata.tshedcleaningItem[i].BURNING_DATE1);
                    cmd.Parameters.AddWithValue("@BURNING_START_TIME", mdata.tshedcleaningItem[i].BURNING_START_TIME);
                    cmd.Parameters.AddWithValue("@BURNING_END_TIME", mdata.tshedcleaningItem[i].BURNING_END_TIME);
                    cmd.Parameters.AddWithValue("@BURNING_TOTAL_TIME", mdata.tshedcleaningItem[i].BURNING_TOTAL_TIME);
                    cmd.Parameters.AddWithValue("@BURNING_PERMTBIRDS", mdata.tshedcleaningItem[i].BURNING_PERMTBIRDS); 

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

      

        public class TshedcleaningItem
        {
            public string BURNING_DATE { get; set; }
            public string BURNING_PHOTO { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public int DOC_ID { get; set; }
            public int DT_BURNING_DAYS { get; set; }
            public int DT_FUMIGATION_DAYS { get; set; }
            public string FUMIGATION_DATE { get; set; }
            public string FUMIGATION_PHOTO { get; set; }
            public bool ISDELETED { get; set; }
            public int id { get; set; }
            public string LAST_LIFTING_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public string PLANT_CODE { get; set; }
            public string PRO_PLACEMENT_DATE { get; set; }
            public string SHED_GAP_DATE { get; set; }
            public string ST_LOC { get; set; }
            public string SQLITE_DATE { get; set; }
            public string LAT { get; set; }
            public string LON { get; set; }
            // Added by Aditya Yadav 2023-02-13
            public double TOTAL_SQFT { get; set; }
            public string FUMIGATION_FROM_DATE { get; set; }
            public string FUMIGATION_TO_DATE { get; set; }
            public int POTASSIUM_PERMANGANAT_QTY { get; set; }
            public double POTASSIUM_PERMANGANAT_PERTBARDS { get; set; }
            public int BLEACHING_POWEDER_QTY { get; set; }
            public double BLEACHING_POWEDER_PERTBARDS { get; set; }
            public int FORMALDEHYDE_QTY { get; set; }
            public double FORMALDEHYDE_PERTBARDS { get; set; }
            public string BURNING_DATE1 { get; set; }
            public string BURNING_START_TIME { get; set; }
            public string BURNING_END_TIME { get; set; }
            public string BURNING_TOTAL_TIME { get; set; }
            public double BURNING_PERMTBIRDS { get; set; }
            
        }

        public class RootShedCleaning
        {
            public List<TshedcleaningItem> tshedcleaningItem { get; set; }
        }


        public class TgradingItem
        {
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public int DOC_ID { get; set; }
            public int GRADING_AGE_FIRST { get; set; }
            public string GRADING_DATE_SECOND { get; set; }
            public string GRADING_DATE_THIRD { get; set; }
            public int GRADING_SMALL_BIRDS { get; set; }
            public bool ISDELETED { get; set; }
            public int id { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public string PHOTO { get; set; }
            public string PLACEMENT_DATE { get; set; }
            public string PLANT_CODE { get; set; }
            public string SQLITE_DATE { get; set; }
            public string ST_LOC { get; set; }
            public string LAT { get; set; }
            public string LON { get; set; }
        }

        public class RootGrading
        {
            public List<TgradingItem> tgradingItem { get; set; }
        }


        internal DataTable SAVM_GRADIN(string TASK, string Data)
        {

            RootGrading mdata = JsonConvert.DeserializeObject<RootGrading>(Data);

            for (int i = 0; i < mdata.tgradingItem.Count; i++)
            {
                try
                {
                    string Photo_Path1 = "0";
                    if (mdata.tgradingItem[i].PHOTO.ToString().Equals("0"))
                    {

                    }
                    else
                    {
                        string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Photo_Path1 = "Grading" + mdata.tgradingItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";
                        String FolderPath = "~/CBF_PHOTO/";
                        UploadPhoto(mdata.tgradingItem[i].PHOTO, FolderPath, Photo_Path1);
                    }
                    db_connection2();
                    cmd = new SqlCommand("SAVM_GRADING", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tgradingItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tgradingItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@DOC_ID", mdata.tgradingItem[i].DOC_ID);
                    cmd.Parameters.AddWithValue("@GRADING_AGE_FIRST", mdata.tgradingItem[i].GRADING_AGE_FIRST);
                    cmd.Parameters.AddWithValue("@GRADING_DATE_SECOND", mdata.tgradingItem[i].GRADING_DATE_SECOND);
                    cmd.Parameters.AddWithValue("@GRADING_DATE_THIRD", mdata.tgradingItem[i].GRADING_DATE_THIRD);
                    cmd.Parameters.AddWithValue("@GRADING_SMALL_BIRDS", mdata.tgradingItem[i].GRADING_SMALL_BIRDS);
                    cmd.Parameters.AddWithValue("@PHOTO", Photo_Path1);
                    cmd.Parameters.AddWithValue("@PLACEMENT_DATE", mdata.tgradingItem[i].PLACEMENT_DATE);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.tgradingItem[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@SQLITE_DATE", mdata.tgradingItem[i].SQLITE_DATE);
                    cmd.Parameters.AddWithValue("@ST_LOC", mdata.tgradingItem[i].ST_LOC);
                    cmd.Parameters.AddWithValue("@LAT", mdata.tgradingItem[i].LAT);
                    cmd.Parameters.AddWithValue("@LON", mdata.tgradingItem[i].LON);  
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
            }

            return dt;
        }

        internal DataSet GETM_RPT(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
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


                if (TASK.ToString() == "GET_LOGIN")
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



        internal DataTable GETM_REPORT_LOCAL(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
        {
            db_connection2();
            try
            {

                cmd = new SqlCommand("GETM_RPT", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;
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





        internal DataTable GETM_REPORT(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
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
                da.Fill(dt);

            }
            catch (Exception e)
            {

            }
            finally
            {
                db_closed();
            }
            return dt;
        }

        internal DataSet GETM_LOGIN(string TASK, string USER_ID, string MOBILE_NO, string SEARCH, string SEARCH1, string SEARCH2)
        {
            db_connection();
            try
            {

                cmd = new SqlCommand("GETM_LOGIN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);
                cmd.Parameters.AddWithValue("@MOBILE_NO", MOBILE_NO);
             
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);


                if (TASK.ToString() == "GET_LOGIN")
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
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        internal DataTable SAPConnection(String task)
        {

            db_connection();

            cmd.CommandText = "GETM_SAP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@TASK", task);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            return dt;
        }//rate data


        public class TrequestItem
        {
            public string ABW { get; set; }
            public string CHICKS_PLACEMENT_DATE { get; set; }
            public string CHICKS_PLACE_QTY { get; set; }
            public int CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public int id { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public string PLANT_CODE { get; set; }
            public string RATE { get; set; }
            public int REG_ID { get; set; }
            public string REMARKS { get; set; }
            public string SELLABLE_BIRDS_QTY { get; set; }
            public string SQLITE_DATE { get; set; }
            public string VENDOR_CODE { get; set; }
            public string AGE { get; set; }
            public string MORT_1 { get; set; }
            public string MORT_2 { get; set; }
            public string MORT_3 { get; set; }

        }
        public class RootRequest
        {
            public IList<TrequestItem> trequestItem { get; set; }

        }
         internal DataTable SAVM_REQUEST(string TASK, string Data)
        {

            RootRequest mdata = JsonConvert.DeserializeObject<RootRequest>(Data);

            for (int i = 0; i < mdata.trequestItem.Count; i++)
            {
                try
                {
                  
                    db_connection2();
                    cmd = new SqlCommand("SAVM_REQUEST", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@ABW", mdata.trequestItem[i].ABW);
                    cmd.Parameters.AddWithValue("@CHICKS_PLACEMENT_DATE", mdata.trequestItem[i].CHICKS_PLACEMENT_DATE);
                    cmd.Parameters.AddWithValue("@CHICKS_PLACE_QTY", mdata.trequestItem[i].CHICKS_PLACE_QTY);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.trequestItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.trequestItem[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@RATE", mdata.trequestItem[i].RATE);
                    cmd.Parameters.AddWithValue("@REMARKS", mdata.trequestItem[i].REMARKS);
                    cmd.Parameters.AddWithValue("@SELLABLE_BIRDS_QTY", mdata.trequestItem[i].SELLABLE_BIRDS_QTY);
                    cmd.Parameters.AddWithValue("@SQLLITE_DATE", mdata.trequestItem[i].SQLITE_DATE);
                    cmd.Parameters.AddWithValue("@VENDOR_CODE", mdata.trequestItem[i].VENDOR_CODE);
                    cmd.Parameters.AddWithValue("@AGE", mdata.trequestItem[i].AGE);
                    cmd.Parameters.AddWithValue("@MORT_1", mdata.trequestItem[i].MORT_1);
                    cmd.Parameters.AddWithValue("@MORT_2", mdata.trequestItem[i].MORT_2);
                    cmd.Parameters.AddWithValue("@MORT_3", mdata.trequestItem[i].MORT_3);
                    cmd.Parameters.AddWithValue("@REG_ID", mdata.trequestItem[i].REG_ID);
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
            }

            return dt;
        }


         public class TrateItem
         {
             public string CREATED_BY { get; set; }
             public string CREATED_DATE { get; set; }
             public bool ISDELETED { get; set; }
             public int id { get; set; }
             public string MODIFIED_BY { get; set; }
             public double RATE { get; set; }
             public string RATE_DATE { get; set; }
             public int RATE_ID { get; set; }
             public string SQLITE_DATE { get; set; }
             public int REG_ID { get; set; }
         }

         public class TstatusItem
         {
             public string CREATED_BY { get; set; }
             public string CREATED_DATE { get; set; }
             public bool ISDELETED { get; set; }
             public int id { get; set; }
             public string MODIFIED_BY { get; set; }
             public string MODIFIED_DATE { get; set; }
             public string REAMRKS { get; set; }
             public int REQ_ID { get; set; }
             public string STATUS { get; set; }
             public int STATUS_ID { get; set; }
             public string SQLITE_DATE { get; set; }
             public string EMP_CODE { get; set; }
             public string LEVEL_CODE { get; set; }
         }

         public class RootStatus
         {
             public List<TrequestItem> trequestItem { get; set; }
             public List<TstatusItem> tstatusItem { get; set; }
             public List<TrateItem> trateItem { get; set; }
         }

         public class RootStatusOnly
         {
            
             public List<TstatusItem> tstatusItem { get; set; }
             
         }

         internal DataTable SAVM_STATUS_FIRST(string TASK, string Data)
         {

             RootStatus mdata = JsonConvert.DeserializeObject<RootStatus>(Data);



             for (int i = 0; i < mdata.tstatusItem.Count; i++)
             {
                 try
                 {

                     db_connection2();
                     cmd = new SqlCommand("SAVM_BIDDING_STATUS", con2);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection = con2;
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tstatusItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@REAMRKS", mdata.tstatusItem[i].REAMRKS);
                     cmd.Parameters.AddWithValue("@REQ_ID", mdata.tstatusItem[i].REQ_ID);
                     cmd.Parameters.AddWithValue("@STATUS", mdata.tstatusItem[i].STATUS);
                     cmd.Parameters.AddWithValue("@SQLITE_DATE", mdata.tstatusItem[i].SQLITE_DATE);
                     cmd.Parameters.AddWithValue("@EMP_CODE", mdata.tstatusItem[i].EMP_CODE);
                     cmd.Parameters.AddWithValue("@LEVEL_ID", mdata.tstatusItem[i].LEVEL_CODE);
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
             }

             for (int i = 0; i < mdata.trequestItem.Count; i++)
             {
                 try
                 {

                     db_connection2();
                     cmd = new SqlCommand("SAVM_REQUEST", con2);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection = con2;
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@ABW", mdata.trequestItem[i].ABW);
                     cmd.Parameters.AddWithValue("@CHICKS_PLACEMENT_DATE", mdata.trequestItem[i].CHICKS_PLACEMENT_DATE);
                     cmd.Parameters.AddWithValue("@CHICKS_PLACE_QTY", mdata.trequestItem[i].CHICKS_PLACE_QTY);
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.trequestItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.trequestItem[i].PLANT_CODE);
                     cmd.Parameters.AddWithValue("@RATE", mdata.trequestItem[i].RATE);
                     cmd.Parameters.AddWithValue("@REMARKS", mdata.trequestItem[i].REMARKS);
                     cmd.Parameters.AddWithValue("@SELLABLE_BIRDS_QTY", mdata.trequestItem[i].SELLABLE_BIRDS_QTY);
                     cmd.Parameters.AddWithValue("@SQLLITE_DATE", mdata.trequestItem[i].SQLITE_DATE);
                     cmd.Parameters.AddWithValue("@VENDOR_CODE", mdata.trequestItem[i].VENDOR_CODE);
                     cmd.Parameters.AddWithValue("@AGE", mdata.trequestItem[i].AGE);
                     cmd.Parameters.AddWithValue("@MORT_1", mdata.trequestItem[i].MORT_1);
                     cmd.Parameters.AddWithValue("@MORT_2", mdata.trequestItem[i].MORT_2);
                     cmd.Parameters.AddWithValue("@MORT_3", mdata.trequestItem[i].MORT_3);
                     cmd.Parameters.AddWithValue("@REG_ID", mdata.trequestItem[i].REG_ID);
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
             }
             for (int i = 0; i < mdata.trateItem.Count; i++)
             {
                 try
                 {

                     db_connection2();
                     cmd = new SqlCommand("SAVM_RATE", con2);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection = con2;
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.trateItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@RATE", mdata.trateItem[i].RATE);
                     cmd.Parameters.AddWithValue("@RATE_DATE", mdata.trateItem[i].RATE_DATE);
                     cmd.Parameters.AddWithValue("@SQLITE_DATE", mdata.trateItem[i].SQLITE_DATE);
                     cmd.Parameters.AddWithValue("@REG_ID", mdata.trateItem[i].REG_ID);
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
             }


             return dt;
         }


         internal DataTable SAVM_STATUS(string TASK, string Data)
         {

             RootStatusOnly mdata = JsonConvert.DeserializeObject<RootStatusOnly>(Data);



             for (int i = 0; i < mdata.tstatusItem.Count; i++)
             {
                 try
                 {

                     db_connection2();
                     cmd = new SqlCommand("SAVM_BIDDING_STATUS", con2);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection = con2;
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tstatusItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@REAMRKS", mdata.tstatusItem[i].REAMRKS);
                     cmd.Parameters.AddWithValue("@REQ_ID", mdata.tstatusItem[i].REQ_ID);
                     cmd.Parameters.AddWithValue("@STATUS", mdata.tstatusItem[i].STATUS);
                     cmd.Parameters.AddWithValue("@SQLITE_DATE", mdata.tstatusItem[i].SQLITE_DATE);
                     cmd.Parameters.AddWithValue("@EMP_CODE", mdata.tstatusItem[i].EMP_CODE);
                     cmd.Parameters.AddWithValue("@LEVEL_ID", mdata.tstatusItem[i].LEVEL_CODE);
                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                 }
                 catch (Exception e)
                 {

                     db_closed2();

                 }
                 finally
                 {
                     db_closed2();
                 }
             }

            
       


             return dt;
         }


         public class TtraderbiddingItem
         {
             public string SQLITE_DATE { get; set; }
             public double BIDDING_RATE { get; set; }
             public string CREATED_BY { get; set; }
             public string CREATED_DATE { get; set; }
             public int CUSTOMER_CODE { get; set; }
             public bool ISDELETED { get; set; }
             public int id { get; set; }
             public string MODIFIED_BY { get; set; }
             public string MODIFIED_DATE { get; set; }
             public double QTY { get; set; }
             public int REQ_ID { get; set; }
             public int TRADER_BIDDING_ID { get; set; }
         }

         public class RootTtraderbiddingItem
         {
             public List<TtraderbiddingItem> ttraderbiddingItem { get; set; }
         }


         internal DataTable SAVM_TTRADERBIDDING(string TASK, string Data)
         {

             RootTtraderbiddingItem mdata = JsonConvert.DeserializeObject<RootTtraderbiddingItem>(Data);

             for (int i = 0; i < mdata.ttraderbiddingItem.Count; i++)
             {
                 try
                 {

                     db_connection2();
                     cmd = new SqlCommand("SAVM_TTRADERBIDDING", con2);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection = con2;
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@SQLITE_DATE", mdata.ttraderbiddingItem[i].SQLITE_DATE);
                     cmd.Parameters.AddWithValue("@BIDDING_RATE", mdata.ttraderbiddingItem[i].BIDDING_RATE);
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.ttraderbiddingItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@CUSTOMER_CODE", mdata.ttraderbiddingItem[i].CUSTOMER_CODE);
                     cmd.Parameters.AddWithValue("@QTY", mdata.ttraderbiddingItem[i].QTY);
                     cmd.Parameters.AddWithValue("@REQ_ID", mdata.ttraderbiddingItem[i].REQ_ID);
                     cmd.Parameters.AddWithValue("@TRADER_BIDDING_ID", mdata.ttraderbiddingItem[i].TRADER_BIDDING_ID);
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
             }

             return dt;
         }


         internal DataSet GETM_SAMPLE(string TASK, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string CREATED_BY)
         {
             db_connection1();
             try
             {

                 cmd = new SqlCommand("GETM_MASTERS", con1);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con1;
                 cmd.Parameters.Clear();
                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@TO_DATE", TO_DATE);
                 cmd.Parameters.AddWithValue("@FROM_DATE", FROM_DATE);
                 cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
            
                 cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                 cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);
                 cmd.Parameters.AddWithValue("@SEARCH3", SEARCH3);
                 cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);
                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(ds);
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed1();
             }
             return ds;
         }


         public class TresultsdetailsItem
         {
             public int CREATED_BY { get; set; }
             public string CREATED_DATE { get; set; }
             public int DOC_ID { get; set; }
             public bool IS_DELETED { get; set; }
             public int id { get; set; }
             public string MODIFIED_BY { get; set; }
             public string MODIFIED_DATE { get; set; }
             public double POINTS { get; set; }
             public int REF_DOC_ID { get; set; }
             public int SAMPLE_ASSIGN_ID { get; set; }
             public string SQLITE_ID { get; set; }
         }

         public class TresultsheaderItem
         {
             public int CREATED_BY { get; set; }
             public string CREATED_DATE { get; set; }
             public string DESCRIPTION { get; set; }
             public int DOC_ID { get; set; }
             public bool IS_DELETED { get; set; }
             public int id { get; set; }
             public string LATITUDE { get; set; }
             public string LONGITUDE { get; set; }
             public int MODIFIED_BY { get; set; }
             public string MODIFIED_DATE { get; set; }
             public string PLANT_CODE { get; set; }
             public int RECEIVED_BY { get; set; }
             public string RECEIVED_DATE { get; set; }
             public int SAMPLE_TYPE_ID { get; set; }
             public string SQLITE_ID { get; set; }
             public string VENDOR_CODE { get; set; }
             public string OBS_DATE { get; set; }
             public string MOBILE_DATE { get; set; }
         }

         public class RootResultRecording
         {
             public List<TresultsdetailsItem> tresultsdetailsItem { get; set; }
             public List<TresultsheaderItem> tresultsheaderItem { get; set; }
         }



         internal DataTable SAVEM_RESULT_RECORDING(string DATA)
         {
             RootResultRecording mdata = JsonConvert.DeserializeObject<RootResultRecording>(DATA);

             //HEADER 
             for (int i = 0; i < mdata.tresultsheaderItem.Count(); i++)
             {
                 try
                 {

                     db_connection1();


                     cmd = new SqlCommand("SAVEM_RESULT_RECORDING_HEADER", con1);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@TASK", "SAVEM_RESULT_RECORDING_HEADER");
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tresultsheaderItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@DESCRIPTION", mdata.tresultsheaderItem[i].DESCRIPTION);
                     cmd.Parameters.AddWithValue("@LATITUDE", mdata.tresultsheaderItem[i].LATITUDE);
                     cmd.Parameters.AddWithValue("@LONGITUDE", mdata.tresultsheaderItem[i].LONGITUDE);
                     cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.tresultsheaderItem[i].PLANT_CODE);
                     cmd.Parameters.AddWithValue("@SAMPLE_TYPE_ID", mdata.tresultsheaderItem[i].SAMPLE_TYPE_ID);
                     cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tresultsheaderItem[i].SQLITE_ID);
                     cmd.Parameters.AddWithValue("@VENDOR_CODE", mdata.tresultsheaderItem[i].VENDOR_CODE);
                     cmd.Parameters.AddWithValue("@OBS_DATE", mdata.tresultsheaderItem[i].OBS_DATE);
                     cmd.Parameters.AddWithValue("@MOBILE_DATE", mdata.tresultsheaderItem[i].MOBILE_DATE);
                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                 }
                 catch (Exception ex) { 
                 
                 }

                 finally
                 {
                     db_closed1();
                 }

             }
             //DETAILS
             for (int i = 0; i < mdata.tresultsdetailsItem.Count(); i++)
             {
                 try
                 {

                     db_connection1();


                     cmd = new SqlCommand("SAVEM_RESULT_RECORDING_DETAILS", con1);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@TASK", "SAVEM_RESULT_RECORDING_DETAILS");
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tresultsdetailsItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@REF_DOC_ID", mdata.tresultsdetailsItem[i].REF_DOC_ID);
                     cmd.Parameters.AddWithValue("@SAMPLE_ASSIGN_ID", mdata.tresultsdetailsItem[i].SAMPLE_ASSIGN_ID);
                     cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tresultsdetailsItem[i].SQLITE_ID);
                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                 }
                 catch (Exception ex) { }
                 finally
                 {
                     db_closed1();
                 }
             }
             return dt;
         }//rate data

         internal DataSet GETM_ED_DATA(string TASK, string CREATED_BY, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string SEARCH4)
         {
             db_connection1();
             try
             {

                 cmd = new SqlCommand("GETM_ED_DATA", con1);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con1;
                 cmd.Parameters.Clear();
                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);
                 cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                 cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                 cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);
                 cmd.Parameters.AddWithValue("@SEARCH3", SEARCH3);
                 cmd.Parameters.AddWithValue("@SEARCH4", SEARCH4);
                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(ds);
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed1();
             }
             return ds;
         }

         public class GetmEdCbf2item
         {
             public int DOC_ID { get; set; }
             public int id { get; set; }
             public bool IS_DELETED { get; set; }
             public string Observation { get; set; }
             public string POINTS { get; set; }
             public int REF_DOC_ID { get; set; }
             public string Report_Name { get; set; }
             public string STD_Weight { get; set; }
             public int SAMPLE_ASSIGN_ID { get; set; }
             public string SQLITE_ID { get; set; }
             public string MODIFIED_BY { get; set; }
         }

         public class RootEDData
         {
             public List<GetmEdCbf2item> getmEdCbf2item { get; set; }
         }





         internal DataTable SAVEM_RESULT_RECORDING_ED(string DATA)
         {
             RootEDData mdata = JsonConvert.DeserializeObject<RootEDData>(DATA);

             //HEADER 
             for (int i = 0; i < mdata.getmEdCbf2item.Count(); i++)
             {
                 try
                 {

                     db_connection1();


                   cmd = new SqlCommand("SAVEM_RESULT_RECORDING_ED", con1);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.Clear();
                   cmd.Parameters.AddWithValue("@TASK", "");
                   cmd.Parameters.AddWithValue("@DOC_ID", mdata.getmEdCbf2item[i].DOC_ID);
                   cmd.Parameters.AddWithValue("@POINTS", mdata.getmEdCbf2item[i].POINTS);
                   cmd.Parameters.AddWithValue("@REF_DOC_ID", mdata.getmEdCbf2item[i].REF_DOC_ID);
                   cmd.Parameters.AddWithValue("@SAMPLE_ASSIGN_ID", mdata.getmEdCbf2item[i].SAMPLE_ASSIGN_ID);
                   cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.getmEdCbf2item[i].SQLITE_ID);
                   cmd.Parameters.AddWithValue("@Report_Name", mdata.getmEdCbf2item[i].Report_Name);
                   cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.getmEdCbf2item[i].MODIFIED_BY);
                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                 }
                 catch (Exception ex)
                 {

                 }

                 finally
                 {
                     db_closed1();
                 }

             }
             //DETAILS
    
             return dt;
         }//rate data


        //CBF PHYSSICAL RESULT RECORDING



      

         public class TcbfphyiscaldetailsItem
         {
             public int SAMPLE_ASSIGN_ID { get; set; }
             public string SQLLiteID { get; set; }
             public int cREATEDBY { get; set; }
             public string cREATEDDATE { get; set; }
             public int dOCID { get; set; }
             public bool iSDELETED { get; set; }
             public int id { get; set; }
             public string mODIFIEDBY { get; set; }
             public string mODIFIEDDATE { get; set; }
             public string pOINTS { get; set; }
             public int rEFDOCID { get; set; }
             public int tESTSTATUS { get; set; }
         }

         public class TcbfphysicalheaderItem
         {
             public string SQLLiteID { get; set; }
             public bool aDAVANCECHECK { get; set; }
             public string aDREESS { get; set; }
             public string cLEANINGGAP { get; set; }
             public int cOMPID { get; set; }
             public int cREATEDBY { get; set; }
             public string cREATEDDATE { get; set; }
             public bool dESIBIRDS { get; set; }
             public int dOCID { get; set; }
             public bool iSDELETED { get; set; }
             public int id { get; set; }
             public string lAT { get; set; }
             public string lON { get; set; }
             public string mODIFIEDBY { get; set; }
             public string mODIFIEDDATE { get; set; }
             public string oBSDATE { get; set; }
             public string oTHER1 { get; set; }
             public string oTHER2 { get; set; }
             public string pHOTO { get; set; }
             public string pLANTCODE { get; set; }
             public int sAMPLETYPEID { get; set; }
             public string sTLCODE { get; set; }
             public int vENDORCODE { get; set; }
             public int REF_DOC_ID { get; set; }
             public string OBS_DATE { get; set; }
             public string MOBILE_DATE { get; set; }
         }

         public class RootCBFPhysical
         {
             public List<TcbfphyiscaldetailsItem> tcbfphyiscaldetailsItem { get; set; }
             public List<TcbfphysicalheaderItem> tcbfphysicalheaderItem { get; set; }
         }


         internal DataTable SAVEM_CBF_PHYSICAL(string DATA, string HEADER_TASK, string DETAILS_TASK)
         {
             RootCBFPhysical mdata = JsonConvert.DeserializeObject<RootCBFPhysical>(DATA);

             //HEADER 
             for (int i = 0; i < mdata.tcbfphysicalheaderItem.Count(); i++)
             {
                 try
                 {

                     db_connection1();


                     string Photo_Path1 = "0";
                     if (mdata.tcbfphysicalheaderItem[i].pHOTO.ToString().Equals("0"))
                     {

                     }
                     else
                     {
                         string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                         Photo_Path1 = "CQC" + mdata.tcbfphysicalheaderItem[i].cREATEDBY + "_" + currentDateTime1 + ".jpg";
                         String FolderPath = "~/CBF_PHOTO/";
                         UploadPhoto(mdata.tcbfphysicalheaderItem[i].pHOTO, FolderPath, Photo_Path1);
                     }


                     cmd = new SqlCommand("SAVEM_C_PHYSICAL", con1);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.Clear();
                     cmd.Parameters.AddWithValue("@TASK", HEADER_TASK);
                     cmd.Parameters.AddWithValue("@SQLLiteID", mdata.tcbfphysicalheaderItem[i].SQLLiteID);
                     cmd.Parameters.AddWithValue("@aDAVANCECHECK", mdata.tcbfphysicalheaderItem[i].aDAVANCECHECK);
                     cmd.Parameters.AddWithValue("@aDREESS", mdata.tcbfphysicalheaderItem[i].aDREESS);
                     cmd.Parameters.AddWithValue("@cLEANINGGAP", mdata.tcbfphysicalheaderItem[i].cLEANINGGAP);
                     cmd.Parameters.AddWithValue("@cOMPID", mdata.tcbfphysicalheaderItem[i].cOMPID);
                     cmd.Parameters.AddWithValue("@cREATEDBY", mdata.tcbfphysicalheaderItem[i].cREATEDBY);
                     cmd.Parameters.AddWithValue("@cREATEDDATE", mdata.tcbfphysicalheaderItem[i].cREATEDDATE);
                     cmd.Parameters.AddWithValue("@dESIBIRDS", mdata.tcbfphysicalheaderItem[i].dESIBIRDS);
                     cmd.Parameters.AddWithValue("@dOCID", mdata.tcbfphysicalheaderItem[i].dOCID);
                 
                     cmd.Parameters.AddWithValue("@lAT", mdata.tcbfphysicalheaderItem[i].lAT);
                     cmd.Parameters.AddWithValue("@lON", mdata.tcbfphysicalheaderItem[i].lON);
                     cmd.Parameters.AddWithValue("@oBSDATE", mdata.tcbfphysicalheaderItem[i].oBSDATE);
                     cmd.Parameters.AddWithValue("@oTHER1", mdata.tcbfphysicalheaderItem[i].oTHER1);
                     cmd.Parameters.AddWithValue("@oTHER2", mdata.tcbfphysicalheaderItem[i].oTHER2);
                     cmd.Parameters.AddWithValue("@pHOTO", Photo_Path1);
                     cmd.Parameters.AddWithValue("@pLANTCODE", mdata.tcbfphysicalheaderItem[i].pLANTCODE);
                     cmd.Parameters.AddWithValue("@sAMPLETYPEID", mdata.tcbfphysicalheaderItem[i].sAMPLETYPEID);
                     cmd.Parameters.AddWithValue("@sTLCODE", mdata.tcbfphysicalheaderItem[i].sTLCODE);
                     cmd.Parameters.AddWithValue("@vENDORCODE", mdata.tcbfphysicalheaderItem[i].vENDORCODE);
                     cmd.Parameters.AddWithValue("@REF_DOC_ID", mdata.tcbfphysicalheaderItem[i].REF_DOC_ID);
                     cmd.Parameters.AddWithValue("@OBS_DATE", mdata.tcbfphysicalheaderItem[i].OBS_DATE);
                     cmd.Parameters.AddWithValue("@MOBILE_DATE", mdata.tcbfphysicalheaderItem[i].MOBILE_DATE);
                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                 }
                 catch (Exception ex)
                 {

                 }

                 finally
                 {
                     db_closed1();
                 }

             }
             //DETAILS
             for (int i = 0; i < mdata.tcbfphyiscaldetailsItem.Count(); i++)
             {
                 try
                 {

                     db_connection1();


                     cmd = new SqlCommand("SAVEM_C_PHYSICAL_DETAILS", con1);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@TASK", DETAILS_TASK);
                     cmd.Parameters.AddWithValue("@SAMPLE_ASSIGN_ID", mdata.tcbfphyiscaldetailsItem[i].SAMPLE_ASSIGN_ID);
                     cmd.Parameters.AddWithValue("@SQLLiteID", mdata.tcbfphyiscaldetailsItem[i].SQLLiteID);
                     cmd.Parameters.AddWithValue("@cREATEDBY", mdata.tcbfphyiscaldetailsItem[i].cREATEDBY);
                     cmd.Parameters.AddWithValue("@cREATEDDATE", mdata.tcbfphyiscaldetailsItem[i].cREATEDDATE);
                     cmd.Parameters.AddWithValue("@dOCID", mdata.tcbfphyiscaldetailsItem[i].dOCID);
                     cmd.Parameters.AddWithValue("@iSDELETED", mdata.tcbfphyiscaldetailsItem[i].iSDELETED);
                     cmd.Parameters.AddWithValue("@pOINTS", mdata.tcbfphyiscaldetailsItem[i].pOINTS);
                     cmd.Parameters.AddWithValue("@rEFDOCID", mdata.tcbfphyiscaldetailsItem[i].rEFDOCID);
                     cmd.Parameters.AddWithValue("@tESTSTATUS", mdata.tcbfphyiscaldetailsItem[i].tESTSTATUS);
                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                 }
                 catch (Exception ex) { }
                 finally
                 {
                     db_closed1();
                 }
             }
             return dt;
         }//rate data



         //GETM_RPT_QC
         internal DataSet GETM_RPT_QC(string TASK, string USER_ID,  string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string SEARCH4)
         {
             db_connection1();
             try
             {

                 cmd = new SqlCommand("GETM_RPT", con1);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con1;
                 cmd.Parameters.Clear();
                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@CREATED_BY", USER_ID);            
                 cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                 cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                 cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);
                 cmd.Parameters.AddWithValue("@SEARCH3", SEARCH3);
                 cmd.Parameters.AddWithValue("@SEARCH4", SEARCH4);
                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(ds);

             }
             catch (Exception e)
             {
                 db_closed2();
             }
             finally
             {
                 db_closed1();
             }
             return ds;
         }




         public class TattendanceItem
         {
             public int CREATED_BY { get; set; }
             public string CREATED_DATE { get; set; }
             public string END_DATE { get; set; }
             public int END_KM { get; set; }
             public string END_LAT { get; set; }
             public string END_LON { get; set; }
             public string END_PHOTO { get; set; }
             public bool ISDELETED { get; set; }
             public int id { get; set; }
             public string MODIFIED_BY { get; set; }
             public string MODIFIED_DATE { get; set; }
             public string SQLITE_ID { get; set; }
             public string START_DATE { get; set; }
             public int START_KM { get; set; }
             public string START_LAT { get; set; }
             public string START_LON { get; set; }
             public int TA_ID { get; set; }
             public string START_PHOTO { get; set; }
             public string START_SELFI { get; set; }
             public string END_SELFI { get; set; }

         }

         public class RootTAEntry
         {
             public List<TattendanceItem> tattendanceItem { get; set; }
         }


         internal DataTable SAVE_TAENTRY(string task, string Data)
         {
             db_connection1();
             try
             {

                 RootTAEntry mdata = JsonConvert.DeserializeObject<RootTAEntry>(Data);

                 for (int i = 0; i < mdata.tattendanceItem.Count; i++)
                 {
                     try
                     {
                         string start_photo = "", end_photo = "", start_selfi = "", end_selfi = "";
                         if (mdata.tattendanceItem[i].START_PHOTO != "0")
                         {
                             String FolderPath = "~/PHOTO/";
                             string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                             start_photo = "StartTA" + currentDateTime1 + ".jpg";

                             UploadPhoto(mdata.tattendanceItem[i].START_PHOTO, FolderPath, start_photo);
                         }

                         if (mdata.tattendanceItem[i].START_SELFI != "0")
                         {
                             String FolderPath = "~/PHOTO/";
                             string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                             start_selfi = "StartSelfi" + currentDateTime1 + ".jpg";

                             UploadPhoto(mdata.tattendanceItem[i].START_SELFI, FolderPath, start_selfi);
                         }

                         if (mdata.tattendanceItem[i].END_PHOTO != "0")
                         {
                             String FolderPath = "~/PHOTO/";
                             string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                             end_photo = "endTA" + currentDateTime1 + ".jpg";

                             UploadPhoto(mdata.tattendanceItem[i].END_PHOTO, FolderPath, end_photo);
                         }

                         if (mdata.tattendanceItem[i].END_SELFI != "0")
                         {
                             String FolderPath = "~/PHOTO/";
                             string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                             end_selfi = "EndSelfi" + currentDateTime1 + ".jpg";

                             UploadPhoto(mdata.tattendanceItem[i].END_SELFI, FolderPath, end_selfi);
                         }


                         cmd = new SqlCommand("SAVM_TA_ENTRY", con1);
                         cmd.CommandType = CommandType.StoredProcedure;
                         cmd.Connection = con1;
                         cmd.Parameters.Clear();

                         cmd.Parameters.AddWithValue("@TASK", task);
                         cmd.Parameters.AddWithValue("@START_KM", mdata.tattendanceItem[i].START_KM);
                         cmd.Parameters.AddWithValue("@START_PHOTO", start_photo);
                         cmd.Parameters.AddWithValue("@START_DATE", mdata.tattendanceItem[i].START_DATE);
                         cmd.Parameters.AddWithValue("@START_LAT", mdata.tattendanceItem[i].START_LAT);
                         cmd.Parameters.AddWithValue("@START_LON", mdata.tattendanceItem[i].START_LON);
                         cmd.Parameters.AddWithValue("@START_SELFI", start_selfi);
                         cmd.Parameters.AddWithValue("@END_KM", mdata.tattendanceItem[i].END_KM);
                         cmd.Parameters.AddWithValue("@END_PHOTO", end_photo);
                         cmd.Parameters.AddWithValue("@END_DATE", mdata.tattendanceItem[i].END_DATE);
                         cmd.Parameters.AddWithValue("@END_LAT", mdata.tattendanceItem[i].END_LAT);
                         cmd.Parameters.AddWithValue("@END_LON", mdata.tattendanceItem[i].END_LON);
                         cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tattendanceItem[i].CREATED_BY);
                         cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tattendanceItem[i].CREATED_DATE);
                         cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tattendanceItem[i].MODIFIED_BY);
                         cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tattendanceItem[i].MODIFIED_DATE);
                         cmd.Parameters.AddWithValue("@ISDELETED", mdata.tattendanceItem[i].ISDELETED);
                         cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tattendanceItem[i].SQLITE_ID);
                         cmd.Parameters.AddWithValue("@END_SELFI", end_selfi);

                         da = new SqlDataAdapter();
                         da.SelectCommand = cmd;
                         da.Fill(dt);
                     }
                     catch (Exception e)
                     {

                     }
                     finally
                     {
                         db_closed1();
                     }


                 }







             }
             catch (Exception e)
             {
                 Console.Write("Hello World," + e);
             }
             return dt;
         }

         internal DataSet GETM_SHED_REASON(string TASK, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string CREATED_BY)
         {
             db_connection1();
             try
             {

                 cmd = new SqlCommand("GETM_MASTERS", con1);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con1;
                 cmd.Parameters.Clear();
                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@TO_DATE", TO_DATE);
                 cmd.Parameters.AddWithValue("@FROM_DATE", FROM_DATE);
                 cmd.Parameters.AddWithValue("@SEARCH", SEARCH);

                 cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                 cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);
                 cmd.Parameters.AddWithValue("@SEARCH3", SEARCH3);
                 cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);
                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(ds);
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed1();
             }
             return ds;
         }


         internal DataTable SAVEM_SHED_CANCEL(string TASK, string DATA)
         {
             RootShedCancel mdata = JsonConvert.DeserializeObject<RootShedCancel>(DATA);
             for (int i = 0; i < mdata.tshedcancelItem.Count(); i++)
             {
                 try
                 {

                     db_connection1();


                     cmd = new SqlCommand("SAVEM_SHED_CANCEL", con1);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@ID", mdata.tshedcancelItem[i].id);
                     cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.tshedcancelItem[i].PLANT_CODE);
                     cmd.Parameters.AddWithValue("@VENDOR_CODE", mdata.tshedcancelItem[i].VENDOR_CODE);
                     cmd.Parameters.AddWithValue("@R_ID", mdata.tshedcancelItem[i].R_ID);
                     cmd.Parameters.AddWithValue("@REMARK", mdata.tshedcancelItem[i].REMARK);
                     cmd.Parameters.AddWithValue("@LATITUDE", mdata.tshedcancelItem[i].LATITUDE);
                     cmd.Parameters.AddWithValue("@LONGITUDE", mdata.tshedcancelItem[i].LONGITUDE);
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tshedcancelItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tshedcancelItem[i].CREATED_DATE);
                     cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tshedcancelItem[i].SQLITE_ID);
                     cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tshedcancelItem[i].MODIFIED_BY);
                     cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tshedcancelItem[i].MODIFIED_DATE);
                     cmd.Parameters.AddWithValue("@IS_DELETED", mdata.tshedcancelItem[i].IS_DELETED);

                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                 }
                 catch (Exception ex)
                 {

                 }
                 finally
                 {
                     db_closed1();
                 }

             }
             return dt;
         }
         public class RootShedCancel
         {
             public List<TshedcancelItem> tshedcancelItem { get; set; }
         }

         public class TshedcancelItem
         {
             public int CREATED_BY { get; set; }
             public string CREATED_DATE { get; set; }
             public bool IS_DELETED { get; set; }
             public int id { get; set; }
             public string LATITUDE { get; set; }
             public string LONGITUDE { get; set; }
             public int MODIFIED_BY { get; set; }
             public string MODIFIED_DATE { get; set; }
             public string PLANT_CODE { get; set; }
             public string REMARK { get; set; }
             public string R_ID { get; set; }
             public string SQLITE_ID { get; set; }
             public string STATUS { get; set; }
             public string VENDOR_CODE { get; set; }
         }

         internal DataTable SAVEM_WATER_AVAILABILITY(string TASK, string DATA)
         {
             RootWaterAvailability mdata = JsonConvert.DeserializeObject<RootWaterAvailability>(DATA);
             for (int i = 0; i < mdata.twateravailabilityItem.Count(); i++)
             {
                 try
                 {

                     db_connection1();

                     string Photo_Path1 = "0";
                     if (mdata.twateravailabilityItem[i].PHOTO.ToString().Equals("0"))
                     {

                     }
                     else
                     {
                         string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                         Photo_Path1 = "CQC" + mdata.twateravailabilityItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";
                         String FolderPath = "~/PHOTO/";
                         UploadPhoto(mdata.twateravailabilityItem[i].PHOTO, FolderPath, Photo_Path1);
                     }




                     cmd = new SqlCommand("SAVEM_WATER_AVAILABILITY", con1);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@ID", mdata.twateravailabilityItem[i].id);
                     cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.twateravailabilityItem[i].PLANT_CODE);
                     cmd.Parameters.AddWithValue("@VENDOR_CODE", mdata.twateravailabilityItem[i].VENDOR_CODE);
                     cmd.Parameters.AddWithValue("@PHOTO", Photo_Path1);
                     cmd.Parameters.AddWithValue("@REMARK", mdata.twateravailabilityItem[i].REMARK);
                     cmd.Parameters.AddWithValue("@LATITUDE", mdata.twateravailabilityItem[i].LATITUDE);
                     cmd.Parameters.AddWithValue("@LONGITUDE", mdata.twateravailabilityItem[i].LONGITUDE);
                     cmd.Parameters.AddWithValue("@CREATED_BY", mdata.twateravailabilityItem[i].CREATED_BY);
                     cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.twateravailabilityItem[i].CREATED_DATE);
                     cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.twateravailabilityItem[i].SQLITE_ID);
                     cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.twateravailabilityItem[i].MODIFIED_BY);
                     cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.twateravailabilityItem[i].MODIFIED_DATE);
                     cmd.Parameters.AddWithValue("@IS_DELETED", mdata.twateravailabilityItem[i].IS_DELETED);

                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                 }
                 catch (Exception ex)
                 {

                 }
                 finally
                 {
                     db_closed1();
                 }

             }
             return dt;
         }
         public class RootWaterAvailability
         {
             public List<TwateravailabilityItem> twateravailabilityItem { get; set; }
         }


         public class TwateravailabilityItem
         {
             public int CREATED_BY { get; set; }
             public string CREATED_DATE { get; set; }
             public bool IS_DELETED { get; set; }
             public int id { get; set; }
             public string LATITUDE { get; set; }
             public string LONGITUDE { get; set; }
             public int MODIFIED_BY { get; set; }
             public string MODIFIED_DATE { get; set; }
             public string PHOTO { get; set; }
             public string PLANT_CODE { get; set; }
             public string REMARK { get; set; }
             public string SQLITE_ID { get; set; }
             public string STATUS { get; set; }
             public string VENDOR_CODE { get; set; }
        }



        //------------------------------------- TA Entry ----------------------------------
        // Aditya Yadav 2023-12-29

         public class TTAENTERYItem
        {
            public int ids { get; set; }
            public int TA_ENTRY_ID { get; set; }
            public double START_POINT { get; set; }
            public string START_POINT_PHOTO { get; set; }
            public string START_TIME { get; set; }
            public double SP_LATITUDE { get; set; }
            public double SP_LONGITUDE { get; set; }
            public double TOTAL_KM { get; set; }
            public string ROUTE { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public String SQLITE_ID { get; set; }
        }

        public class RootTTAENTERYItem
        {
            public List<TTAENTERYItem> tTAENTERYItem { get; set; }
        }


        internal DataTable SAVE_TA_ENTRY(string TASK, string Data)
        {

            RootTTAENTERYItem mdata = JsonConvert.DeserializeObject<RootTTAENTERYItem>(Data);

            for (int i = 0; i < mdata.tTAENTERYItem.Count; i++)
            {
                try
                {

                    string Photo_Path1 = "0";
                    if (mdata.tTAENTERYItem[i].START_POINT_PHOTO.ToString().Equals("0"))
                    {

                    }
                    else
                    {
                        string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        if (TASK.ToString().Equals("SAVE_START_TA_ENTRY"))
                        {
                            Photo_Path1 = "TA_Entry_Start_" + mdata.tTAENTERYItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";
                        }
                        else if (TASK.ToString().Equals("SAVE_END_TA_ENTRY"))
                        {
                            Photo_Path1 = "TA_Entry_End_" + mdata.tTAENTERYItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";
                        }

                        String FolderPath = "~/TA_PHOTO/";
                        UploadPhoto(mdata.tTAENTERYItem[i].START_POINT_PHOTO, FolderPath, Photo_Path1);
                    }


                    db_connection2();
                    cmd = new SqlCommand("SAVE_T_TA_ENTRY", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@TA_ENTRY_ID", mdata.tTAENTERYItem[i].TA_ENTRY_ID);
                    cmd.Parameters.AddWithValue("@START_POINT", mdata.tTAENTERYItem[i].START_POINT);
                    cmd.Parameters.AddWithValue("@START_POINT_PHOTO", Photo_Path1);
                    cmd.Parameters.AddWithValue("@START_TIME", mdata.tTAENTERYItem[i].START_TIME);
                    cmd.Parameters.AddWithValue("@SP_LATITUDE", mdata.tTAENTERYItem[i].SP_LATITUDE);
                    cmd.Parameters.AddWithValue("@SP_LONGITUDE", mdata.tTAENTERYItem[i].SP_LONGITUDE);
                    cmd.Parameters.AddWithValue("@TOTAL_KM", mdata.tTAENTERYItem[i].TOTAL_KM);
                    cmd.Parameters.AddWithValue("@ROUTE", mdata.tTAENTERYItem[i].ROUTE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tTAENTERYItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tTAENTERYItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tTAENTERYItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tTAENTERYItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.tTAENTERYItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tTAENTERYItem[i].SQLITE_ID);
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
            }

            return dt;
        }


        // 2024-02-09 // Added by Aditya Yadav NSP, GET DETAILS , TRP Expense

        internal DataSet GET_NPS_QUESTION(string TASK, string SEARCH, string CREATED_BY)
        {
            db_connection2();
            try
            {

                cmd = new SqlCommand("GET_SAV_NPS_QUESTION", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);
                cmd.Parameters.AddWithValue("@REGION_ID", "");
                cmd.Parameters.AddWithValue("@PLANT_ID", "");
                cmd.Parameters.AddWithValue("@QUE_ID", "");
                cmd.Parameters.AddWithValue("@RATING", "");
                cmd.Parameters.AddWithValue("@CREATED_DATE", "");
                cmd.Parameters.AddWithValue("@IS_DELETED", "");
                cmd.Parameters.AddWithValue("@SQLITE_ID", "");

                //  cmd.Parameters.AddWithValue("@", "");

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


        internal DataTable SAVE_TNPS(string TASK, string Data)
        {

            List<TNPSItem> mdata = JsonConvert.DeserializeObject<List<TNPSItem>>(Data);

            for (int i = 0; i < mdata.Count(); i++)
            {
                try
                {

                    db_connection2();
                    cmd = new SqlCommand("GET_SAV_NPS_QUESTION", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@SEARCH", "");
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@REGION_ID", mdata[i].REGION_ID);
                    cmd.Parameters.AddWithValue("@PLANT_ID", mdata[i].PLANT_ID);
                    cmd.Parameters.AddWithValue("@QUE_ID", mdata[i].QUE_ID);
                    cmd.Parameters.AddWithValue("@RATING", mdata[i].RATING);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@IS_DELETED", mdata[i].IS_DELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata[i].SQLITE_DATE);

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

            }
            return dt;
        }

        public class RootTNPSItem
        {
            public List<TNPSItem> TNPSItem { get; set; }
        }

        public class TNPSItem
        {
            public int id { get; set; }
            public string REGION_ID { get; set; }
            public int CREATED_BY { get; set; }
            public Boolean IS_DELETED { get; set; }

            public int RATING { get; set; }

            public string CREATED_DATE { get; set; }
            public string PLANT_ID { get; set; }
            public string SQLITE_DATE { get; set; }
            public int QUE_ID { get; set; }

        }

        internal DataTable GET_NPS_REPORT(string TASK, string USER_ID, string SEARCH, string SEARCH1)
        {

            try
            {

                db_connection2();
                cmd = new SqlCommand("GET_SAV_NPS_QUESTION", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                cmd.Parameters.AddWithValue("@CREATED_BY", USER_ID);
                cmd.Parameters.AddWithValue("@REGION_ID", "");
                cmd.Parameters.AddWithValue("@PLANT_ID", "");
                cmd.Parameters.AddWithValue("@QUE_ID", "");
                cmd.Parameters.AddWithValue("@RATING", "");
                cmd.Parameters.AddWithValue("@CREATED_DATE", SEARCH1);
                cmd.Parameters.AddWithValue("@IS_DELETED", "");
                cmd.Parameters.AddWithValue("@SQLITE_ID", "");

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



        internal DataSet GETM_DETAILS(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string SEARCH4, string SEARCH5)
        {
            db_connection2();
            try
            {

                cmd = new SqlCommand("GETM_DETAILS", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@USER_ID", USER_ID);
                cmd.Parameters.AddWithValue("@TO_DATE", TO_DATE);
                cmd.Parameters.AddWithValue("@FROM_DATE", FROM_DATE);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
                cmd.Parameters.AddWithValue("@SEARCH1", SEARCH1);
                cmd.Parameters.AddWithValue("@SEARCH2", SEARCH2);
                cmd.Parameters.AddWithValue("@SEARCH3", SEARCH3);
                cmd.Parameters.AddWithValue("@SEARCH4", SEARCH4);
                cmd.Parameters.AddWithValue("@SEARCH5", SEARCH5);

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


        // TA Expense 2024-01-25


        public class TExpenseItem
        {
            public string AUTO_BILL { get; set; }
            public string AUTO_BILL_PHOTO { get; set; }
            public string BUS_BILL { get; set; }
            public string BUS_BILL_PHOTO { get; set; }
            public int CITY_TYPE_ID { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string DATE { get; set; }
            public int EXPENSE_ID { get; set; }
            public string FLIGHT_BILL { get; set; }
            public string FLIGHT_BILL_PHOTO { get; set; }
            public string FOOD_BILL { get; set; }
            public string FOOD_BILL_PHOTO { get; set; }
            public bool ISDELETED { get; set; }
            public int ids { get; set; }
            public string LODGE_BILL { get; set; }
            public string LODGE_BILL_PHOTO { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public string OTHER_BILL { get; set; }
            public string OTHER_BILL_PHOTO { get; set; }
            public string OTHER_CITY_TYPE { get; set; }
            public string SQLITE_ID { get; set; }
            public string STATUS { get; set; }
            public int TA_ENTRY_ID { get; set; }
            public string TAXI_BILL { get; set; }
            public string TAXI_BILL_PHOTO { get; set; }
            public double TOTAL_KM { get; set; }
            public string TRAIN_BILL { get; set; }
            public string TRAIN_BILL_PHOTO { get; set; }
            public int USER_CODE { get; set; }
        }


        internal DataTable SAVE_EXPENSE(string TASK, string DATA)
        {
            List<TExpenseItem> mdata = JsonConvert.DeserializeObject<List<TExpenseItem>>(DATA);

            for (int i = 0; i < mdata.Count; i++)
            {
                try
                {

                    string Photo_BUS_BILL = "NA.jpg";
                    string Photo_TRAIN_BILL = "NA.jpg";
                    string Photo_FLIGHT_BILL = "NA.jpg";
                    string Photo_AUTO_BILL = "NA.jpg";
                    string Photo_TAXI_BILL = "NA.jpg";
                    string Photo_FOOD_BILL = "NA.jpg";
                    string Photo_LODGE_BILL = "NA.jpg";
                    string Photo_OTHER_BILL = "NA.jpg";
                    String FolderPath = "~/EXPENSE_PHOTO/";

                    if (!mdata[i].BUS_BILL_PHOTO.ToString().Equals("0"))
                    {

                        Photo_BUS_BILL = "Expense_BusBill_" + "_" + mdata[i].TA_ENTRY_ID + "_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].BUS_BILL_PHOTO, FolderPath, Photo_BUS_BILL);
                    }
                    if (!mdata[i].TRAIN_BILL_PHOTO.ToString().Equals("0"))
                    {

                        Photo_TRAIN_BILL = "Expense_TrainBill_" + "_" + mdata[i].TA_ENTRY_ID + "_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].TRAIN_BILL_PHOTO, FolderPath, Photo_TRAIN_BILL);
                    }
                    if (!mdata[i].FLIGHT_BILL_PHOTO.ToString().Equals("0"))
                    {

                        Photo_FLIGHT_BILL = "Expense_FlightBill_" + "_" + mdata[i].TA_ENTRY_ID + "_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].FLIGHT_BILL_PHOTO, FolderPath, Photo_FLIGHT_BILL);
                    }
                    if (!mdata[i].AUTO_BILL_PHOTO.ToString().Equals("0"))
                    {

                        Photo_AUTO_BILL = "Expense_AutoBill_" + "_" + mdata[i].TA_ENTRY_ID + "_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].AUTO_BILL_PHOTO, FolderPath, Photo_AUTO_BILL);
                    }
                    if (!mdata[i].TAXI_BILL_PHOTO.ToString().Equals("0"))
                    {

                        Photo_TAXI_BILL = "Expense_TaxiBill_" + "_" + mdata[i].TA_ENTRY_ID + "_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].TAXI_BILL_PHOTO, FolderPath, Photo_TAXI_BILL);
                    }
                    if (!mdata[i].FOOD_BILL_PHOTO.ToString().Equals("0"))
                    {

                        Photo_FOOD_BILL = "Expense_FoodBill_" + "_" + mdata[i].TA_ENTRY_ID + "_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].FOOD_BILL_PHOTO, FolderPath, Photo_FOOD_BILL);
                    }
                    if (!mdata[i].LODGE_BILL_PHOTO.ToString().Equals("0"))
                    {

                        Photo_LODGE_BILL = "Expense_LodgeBill_" + "_" + mdata[i].TA_ENTRY_ID + "_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].LODGE_BILL_PHOTO, FolderPath, Photo_LODGE_BILL);
                    }
                    if (!mdata[i].OTHER_BILL_PHOTO.ToString().Equals("0"))
                    {

                        Photo_OTHER_BILL = "Expense_OtherBill_" + "_" + mdata[i].TA_ENTRY_ID + "_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].OTHER_BILL_PHOTO, FolderPath, Photo_OTHER_BILL);
                    }


                    db_connection2();
                    cmd = new SqlCommand("SAVE_EXPENSE", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@EXPENSE_ID", mdata[i].EXPENSE_ID);
                    cmd.Parameters.AddWithValue("@USER_CODE", mdata[i].USER_CODE);
                    cmd.Parameters.AddWithValue("@DATE", mdata[i].DATE);
                    cmd.Parameters.AddWithValue("@TA_ENTRY_ID", mdata[i].TA_ENTRY_ID);
                    cmd.Parameters.AddWithValue("@CITY_TYPE_ID", mdata[i].CITY_TYPE_ID);
                    cmd.Parameters.AddWithValue("@OTHER_CITY_TYPE", mdata[i].OTHER_CITY_TYPE);
                    cmd.Parameters.AddWithValue("@TOTAL_KM", mdata[i].TOTAL_KM);
                    cmd.Parameters.AddWithValue("@BUS_BILL", mdata[i].BUS_BILL);
                    cmd.Parameters.AddWithValue("@BUS_BILL_PHOTO", Photo_BUS_BILL);
                    cmd.Parameters.AddWithValue("@TRAIN_BILL", mdata[i].TRAIN_BILL);
                    cmd.Parameters.AddWithValue("@TRAIN_BILL_PHOTO", Photo_TRAIN_BILL);
                    cmd.Parameters.AddWithValue("@FLIGHT_BILL", mdata[i].FLIGHT_BILL);
                    cmd.Parameters.AddWithValue("@FLIGHT_BILL_PHOTO", Photo_FLIGHT_BILL);
                    cmd.Parameters.AddWithValue("@AUTO_BILL", mdata[i].AUTO_BILL);
                    cmd.Parameters.AddWithValue("@AUTO_BILL_PHOTO", Photo_AUTO_BILL);
                    cmd.Parameters.AddWithValue("@TAXI_BILL", mdata[i].TAXI_BILL);
                    cmd.Parameters.AddWithValue("@TAXI_BILL_PHOTO", Photo_TAXI_BILL);
                    cmd.Parameters.AddWithValue("@FOOD_BILL", mdata[i].FOOD_BILL);
                    cmd.Parameters.AddWithValue("@FOOD_BILL_PHOTO", Photo_FOOD_BILL);
                    cmd.Parameters.AddWithValue("@LODGE_BILL", mdata[i].LODGE_BILL);
                    cmd.Parameters.AddWithValue("@LODGE_BILL_PHOTO", Photo_LODGE_BILL);
                    cmd.Parameters.AddWithValue("@OTHER_BILL", mdata[i].OTHER_BILL);
                    cmd.Parameters.AddWithValue("@OTHER_BILL_PHOTO", Photo_OTHER_BILL);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata[i].SQLITE_ID);
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
            }

            return dt;
        }

    }
}