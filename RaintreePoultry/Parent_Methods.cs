using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Net;
using SAP.Middleware.Connector;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net.Mail;


namespace RaintreePoultry
{
    public class Parent_Methods : Connection
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt_tomail = new DataTable();
        DataTable dt_data = new DataTable();
        DataTable dt_data_Export = new DataTable();
        DataSet ds = new DataSet();
        Methods meth = new Methods();
        RFC_DATA RFC_DATA = new RFC_DATA();
        System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();
        SmtpClient client = new SmtpClient();
        System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient();

        public class RootMortality
        {
            public List<TpdailytransItem> tpdailytransItem { get; set; }
            public List<TpmortalitydetailsItem> tpmortalitydetailsItem { get; set; }
            public List<TpmortalityreasondetailsItem> tpmortalityreasondetailsItem { get; set; }
        }

        public class TpdailytransItem
        {
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string DATE { get; set; }
            public string FLOCK_NO { get; set; }
            public int ID { get; set; }
            public bool ISDELETED { get; set; }
            public int ids { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public string PLANT_CODE { get; set; }
            public int PLANT_ID { get; set; }
            public string SHED_ID { get; set; }
            public string SQLITE_ID { get; set; }
        }

        public class TpmortalitydetailsItem
        {
            public int BIRD_TYPE_ID { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public int DAILY_TRANS_ID { get; set; }
            public int ID { get; set; }
            public bool ISDELETED { get; set; }
            public int ids { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public double MORTALITY { get; set; }
            public string MORTALITY_PHOTO { get; set; }
            public double QTY { get; set; }
            public string REPORT_PHOTO { get; set; }
            public bool SAP_FLAG { get; set; }
            public double ABW { get; set; }
            public string SQLITE_ID { get; set; }
        }

        public class TpmortalityreasondetailsItem
        {
            public int BIRD_TYPE_ID { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public int DAILY_TRANS_ID { get; set; }
            public int ID { get; set; }
            public bool ISDELETED { get; set; }
            public int ids { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public int MORTALITY_REASON_ID { get; set; }
            public double QTY { get; set; }
            public bool SAP_FLAG { get; set; }
            public string SQLITE_ID { get; set; }
        }

        internal DataTable SAVE_MORTALITY(string TASK, string DATA)
        {
            RootMortality mdata = JsonConvert.DeserializeObject<RootMortality>(DATA);
          

            for (int i = 0; i < mdata.tpdailytransItem.Count; i++)
            {

                try
                {
                    db_connection2();
                    cmd = new SqlCommand("SAVEM_T_P_DAILY_TRANS", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;

                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@DATE", mdata.tpdailytransItem[i].DATE);
                    cmd.Parameters.AddWithValue("@PLANT_ID", mdata.tpdailytransItem[i].PLANT_ID);
                    cmd.Parameters.AddWithValue("@SHED_ID", mdata.tpdailytransItem[i].SHED_ID);
                    cmd.Parameters.AddWithValue("@FLOCK_NO", mdata.tpdailytransItem[i].FLOCK_NO);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.tpdailytransItem[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tpdailytransItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tpdailytransItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tpdailytransItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tpdailytransItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.tpdailytransItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tpdailytransItem[i].SQLITE_ID);

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

            }

            var DAILY_TRANS_ID=dt.Rows[0]["ID"];


            for (int i = 0; i < mdata.tpmortalitydetailsItem.Count; i++)
            {

                string Photo_Path1 = "NA.jpg";
                if (mdata.tpmortalitydetailsItem[i].MORTALITY_PHOTO.ToString().Equals("0"))
                {
                }
                else
                {
                    string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Photo_Path1 = "Mortality" + mdata.tpmortalitydetailsItem[i].CREATED_BY + "_" + mdata.tpmortalitydetailsItem[i].BIRD_TYPE_ID + "_" + currentDateTime1 + ".jpg";
                    String FolderPath = "~/CBF_PHOTO/";
                    meth.UploadPhoto(mdata.tpmortalitydetailsItem[i].MORTALITY_PHOTO, FolderPath, Photo_Path1);
                }

                string Photo_Path2 = "NA.jpg";
                if (mdata.tpmortalitydetailsItem[i].REPORT_PHOTO.ToString().Equals("0"))
                {
                }
                else
                {
                    string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Photo_Path2 = "Report" + mdata.tpmortalitydetailsItem[i].CREATED_BY + "_" + mdata.tpmortalitydetailsItem[i].BIRD_TYPE_ID + "_" + currentDateTime1 + ".jpg";
                    String FolderPath = "~/CBF_PHOTO/";
                    meth.UploadPhoto(mdata.tpmortalitydetailsItem[i].REPORT_PHOTO, FolderPath, Photo_Path2);
                }

                try
                {
                    db_connection2();
                    cmd = new SqlCommand("SAVEM_T_P_MORTALITY_DETAILS", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;

                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@DAILY_TRANS_ID", DAILY_TRANS_ID);
                    cmd.Parameters.AddWithValue("@BIRD_TYPE_ID", mdata.tpmortalitydetailsItem[i].BIRD_TYPE_ID);
                    cmd.Parameters.AddWithValue("@QTY", mdata.tpmortalitydetailsItem[i].QTY);
                    cmd.Parameters.AddWithValue("@MORTALITY", mdata.tpmortalitydetailsItem[i].MORTALITY);
                    cmd.Parameters.AddWithValue("@MORTALITY_PHOTO", Photo_Path1);
                    cmd.Parameters.AddWithValue("@REPORT_PHOTO", Photo_Path2);
                    cmd.Parameters.AddWithValue("@SAP_FLAG", mdata.tpmortalitydetailsItem[i].SAP_FLAG);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tpmortalitydetailsItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tpmortalitydetailsItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tpmortalitydetailsItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tpmortalitydetailsItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.tpmortalitydetailsItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@ABW", mdata.tpmortalitydetailsItem[i].ABW);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tpmortalitydetailsItem[i].SQLITE_ID);
                    dt.Clear();
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
            }


            for (int i = 0; i < mdata.tpmortalityreasondetailsItem.Count; i++)
            {

                try
                {
                    db_connection2();
                    cmd = new SqlCommand("SAVEM_T_P_MORTALITY_REASON_DETAILS", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;

                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@DAILY_TRANS_ID", DAILY_TRANS_ID);
                    cmd.Parameters.AddWithValue("@BIRD_TYPE_ID", mdata.tpmortalityreasondetailsItem[i].BIRD_TYPE_ID);
                    cmd.Parameters.AddWithValue("@MORTALITY_REASON_ID", mdata.tpmortalityreasondetailsItem[i].MORTALITY_REASON_ID);
                    cmd.Parameters.AddWithValue("@QTY", mdata.tpmortalityreasondetailsItem[i].QTY);
                    cmd.Parameters.AddWithValue("@SAP_FLAG", mdata.tpmortalityreasondetailsItem[i].SAP_FLAG);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tpmortalityreasondetailsItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tpmortalityreasondetailsItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tpmortalityreasondetailsItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tpmortalityreasondetailsItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.tpmortalityreasondetailsItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tpmortalityreasondetailsItem[i].SQLITE_ID);

                    dt.Clear();
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
            }



            return dt;
        }



        public class RootProduction
        {
            public List<TpdailytransItem> tpdailytransItem { get; set; }
            public List<TpfeedconsumptiondetailsItem> tpfeedconsumptiondetailsItem { get; set; }
            public List<TpproductiondetailsItem> tpproductiondetailsItem { get; set; }
        }



        public class TpfeedconsumptiondetailsItem
        {
            public double ABW { get; set; }
            public int BIRD_TYPE_ID { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public int DAILY_TRANS_ID { get; set; }
            public int ID { get; set; }
            public bool ISDELETED { get; set; }
            public int ids { get; set; }
            public int MATERIAL_ID { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public double QTY { get; set; }
            public bool SAP_FLAG { get; set; }
            public string SQLITE_ID { get; set; }
            public double UNIFORMITY { get; set; }
            public double BIRD_QTY { get; set; }
            
        }

        public class TpproductiondetailsItem
        {
            public int BIRD_TYPE_ID { get; set; }
            public int COMMERCIAL_EGGS { get; set; }
            public int CRACK_EGGS { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public int CULL_BIRDS { get; set; }
            public int DAILY_TRANS_ID { get; set; }
            public double EGGS_WEIGHT { get; set; }
            public int HATCHING_EGGS { get; set; }
            public int ID { get; set; }
            public bool ISDELETED { get; set; }
            public int ids { get; set; }
            public int JUMBO_EGGS { get; set; }
            public int LEAKER_EGGS { get; set; }
            public int MANURE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public int PULLET_EGGS { get; set; }
            public double QTY { get; set; }
            public bool SAP_FLAG { get; set; }
            public int SICK_BIRDS { get; set; }
            public string SQLITE_ID { get; set; }
        }



        internal DataTable SAVE_PRODUCTION(string TASK, string DATA)
        {
            

            RootProduction mdata = JsonConvert.DeserializeObject<RootProduction>(DATA);


            for (int i = 0; i < mdata.tpdailytransItem.Count; i++)
            {
                //string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                //File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/RAINPOULTRY_FILE/") + mdata.tpdailytransItem[0].CREATED_BY + "_" + currentDateTime1, DATA);
                try
                {
                    
                    db_connection2();
                    cmd = new SqlCommand("SAVEM_T_P_DAILY_TRANS", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;

                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@DATE", mdata.tpdailytransItem[i].DATE);
                    cmd.Parameters.AddWithValue("@PLANT_ID", mdata.tpdailytransItem[i].PLANT_ID);
                    cmd.Parameters.AddWithValue("@SHED_ID", mdata.tpdailytransItem[i].SHED_ID);
                    cmd.Parameters.AddWithValue("@FLOCK_NO", mdata.tpdailytransItem[i].FLOCK_NO);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.tpdailytransItem[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tpdailytransItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tpdailytransItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tpdailytransItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tpdailytransItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.tpdailytransItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tpdailytransItem[i].SQLITE_ID);

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

            }

            var DAILY_TRANS_ID = dt.Rows[0]["ID"];


            for (int i = 0; i < mdata.tpfeedconsumptiondetailsItem.Count; i++)
            {

                try
                {

                

                    db_connection2();
                    cmd = new SqlCommand("SAVEM_T_P_FEED_CONSUMPTION_DETAILS", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;

                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@DAILY_TRANS_ID", DAILY_TRANS_ID);
                    cmd.Parameters.AddWithValue("@BIRD_TYPE_ID", mdata.tpfeedconsumptiondetailsItem[i].BIRD_TYPE_ID);
                    cmd.Parameters.AddWithValue("@MATERIAL_ID", mdata.tpfeedconsumptiondetailsItem[i].MATERIAL_ID);
                    cmd.Parameters.AddWithValue("@QTY", mdata.tpfeedconsumptiondetailsItem[i].QTY);
                    cmd.Parameters.AddWithValue("@ABW", mdata.tpfeedconsumptiondetailsItem[i].ABW);
                    cmd.Parameters.AddWithValue("@SAP_FLAG", mdata.tpfeedconsumptiondetailsItem[i].SAP_FLAG);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tpfeedconsumptiondetailsItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tpfeedconsumptiondetailsItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tpfeedconsumptiondetailsItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tpfeedconsumptiondetailsItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.tpfeedconsumptiondetailsItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tpfeedconsumptiondetailsItem[i].SQLITE_ID);
                    cmd.Parameters.AddWithValue("@UNIFORMITY", mdata.tpfeedconsumptiondetailsItem[i].UNIFORMITY);
                    cmd.Parameters.AddWithValue("@BIRD_QTY", mdata.tpfeedconsumptiondetailsItem[i].BIRD_QTY);
                 
                    dt.Clear();

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
            }


            for (int i = 0; i < mdata.tpproductiondetailsItem.Count; i++)
            {

                try
                {
                    db_connection2();
                    cmd = new SqlCommand("SAVEM_T_P_PRODUCTION_DETAILS", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;

                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@DAILY_TRANS_ID", DAILY_TRANS_ID);
                    cmd.Parameters.AddWithValue("@BIRD_TYPE_ID", mdata.tpproductiondetailsItem[i].BIRD_TYPE_ID);
                    cmd.Parameters.AddWithValue("@QTY", mdata.tpproductiondetailsItem[i].QTY);
                    cmd.Parameters.AddWithValue("@CULL_BIRDS", mdata.tpproductiondetailsItem[i].CULL_BIRDS);
                    cmd.Parameters.AddWithValue("@SICK_BIRDS", mdata.tpproductiondetailsItem[i].SICK_BIRDS);
                    cmd.Parameters.AddWithValue("@MANURE", mdata.tpproductiondetailsItem[i].MANURE);
                    cmd.Parameters.AddWithValue("@HATCHING_EGGS", mdata.tpproductiondetailsItem[i].HATCHING_EGGS);
                    cmd.Parameters.AddWithValue("@COMMERCIAL_EGGS", mdata.tpproductiondetailsItem[i].COMMERCIAL_EGGS);
                    cmd.Parameters.AddWithValue("@JUMBO_EGGS", mdata.tpproductiondetailsItem[i].JUMBO_EGGS);
                    cmd.Parameters.AddWithValue("@PULLET_EGGS", mdata.tpproductiondetailsItem[i].PULLET_EGGS);
                    cmd.Parameters.AddWithValue("@CRACK_EGGS", mdata.tpproductiondetailsItem[i].CRACK_EGGS);
                    cmd.Parameters.AddWithValue("@LEAKER_EGGS", mdata.tpproductiondetailsItem[i].LEAKER_EGGS);
                    cmd.Parameters.AddWithValue("@EGGS_WEIGHT", mdata.tpproductiondetailsItem[i].EGGS_WEIGHT);
                    cmd.Parameters.AddWithValue("@SAP_FLAG", mdata.tpproductiondetailsItem[i].SAP_FLAG);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tpproductiondetailsItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tpproductiondetailsItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tpproductiondetailsItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tpproductiondetailsItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.tpproductiondetailsItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tpproductiondetailsItem[i].SQLITE_ID);

                    dt.Clear();
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
            }



            return dt;
        }


        public class TpwatertestItem
        {
            public double CLO2 { get; set; }
            public string CLO2_PHOTO { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public int ID { get; set; }
            public bool ISDELETED { get; set; }
            public int ids { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public double PH { get; set; }
            public string PH_PHOTO { get; set; }
            public string PLANT_ID { get; set; }
            public string SHED_ID { get; set; }
            public string SQLITE_ID { get; set; }
        }

        public class RootWaterTest
        {
            public List<TpwatertestItem> tpwatertestItem { get; set; }
        }

        internal DataTable SAVEM_WATER_TEST(string TASK,string DATA)
        {
            RootWaterTest mdata = JsonConvert.DeserializeObject<RootWaterTest>(DATA);

            //HEADER 
            for (int i = 0; i < mdata.tpwatertestItem.Count(); i++)
            {
                try
                {

                    db_connection2();

                    cmd = new SqlCommand("SAVEM_T_P_WATER_TEST", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@PLANT_ID", mdata.tpwatertestItem[i].PLANT_ID);
                    cmd.Parameters.AddWithValue("@SHED_ID", mdata.tpwatertestItem[i].SHED_ID);
                    cmd.Parameters.AddWithValue("@PH", mdata.tpwatertestItem[i].PH);
                    cmd.Parameters.AddWithValue("@CLO2", mdata.tpwatertestItem[i].CLO2);
                    cmd.Parameters.AddWithValue("@PH_PHOTO", mdata.tpwatertestItem[i].PH_PHOTO);
                    cmd.Parameters.AddWithValue("@CLO2_PHOTO", mdata.tpwatertestItem[i].CLO2_PHOTO);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.tpwatertestItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.tpwatertestItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.tpwatertestItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.tpwatertestItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.tpwatertestItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.tpwatertestItem[i].SQLITE_ID);
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {

                }

                finally
                {
                    db_closed2();
                }

            }
            //DETAILS

            return dt;
        }


        internal string SAVE_PHOTO(string DATA)
        {
            try
            {
                string Photo_Path1 = "NA";
                string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                Photo_Path1 = "Mortality" + 1010 + "_" + currentDateTime1 + ".jpg";
                String FolderPath = "~/CBF_PHOTO/";
                meth.UploadPhoto(DATA, FolderPath, Photo_Path1);
                return Photo_Path1;
            }

            catch (Exception e)
            {
                return "ERROR";
            }


        }


        // Aditya Yadav : Save Egg Collection 

        public class RootEggsCollection
        {
            public List<TEggsCollectionItem> tEggsCollectionItem { get; set; }
        }

        public class TEggsCollectionItem
        {

            public string CREATED_DATE { get; set; }
            public string SQLITE_ID { get; set; }
            public string CREATED_BY { get; set; }
            public string DATE { get; set; }
            public string FLOCK_NO { get; set; }
            public bool ISDELETED { get; set; }
            public int id { get; set; }
            public string PLANT_CODE { get; set; }
            public string PLANT_ID { get; set; }
            public string SHED_ID { get; set; }
            public string TOTAL_EGGS { get; set; }
            public string WEEK_CODE { get; set; }
            public string EGGS_DAILY_TRANS_ID { get; set; }
            public string EGGS_COLLECTION_ID { get; set; }
            public string COLLECTION { get; set; }
            public string IS_EDITABLE { get; set; }
            public string GRADING_TOTAL_EGGS { get; set; }
            public string EGGS_TYPE_ID { get; set; }
            public string DESCRIPTION { get; set; }

        }

        internal DataTable SAVE_PARENT_EGGS_DAILY_TRANS(string TASK, string DATA)
        {

            List<TEggsCollectionItem> mdata = JsonConvert.DeserializeObject<List<TEggsCollectionItem>>(DATA);
            //HEADER 
            for (int i = 0; i < mdata.Count(); i++)
            {

                try
                {

                    db_connection2();

                    cmd = new SqlCommand("SAVE_P_EGGS_DAILY_TRANS", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@EGGS_DAILY_TRANS_ID", mdata[i].EGGS_DAILY_TRANS_ID);
                    cmd.Parameters.AddWithValue("@EGGS_COLLECTION_ID", mdata[i].EGGS_COLLECTION_ID);
                    cmd.Parameters.AddWithValue("@COLLECTION", mdata[i].COLLECTION);
                    cmd.Parameters.AddWithValue("@DATE", mdata[i].DATE);
                    cmd.Parameters.AddWithValue("@PLANT_ID", mdata[i].PLANT_ID);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@SHED_ID", mdata[i].SHED_ID);
                    cmd.Parameters.AddWithValue("@FLOCK_NO", mdata[i].FLOCK_NO);
                    cmd.Parameters.AddWithValue("@WEEK_CODE", mdata[i].WEEK_CODE);
                    cmd.Parameters.AddWithValue("@TOTAL_EGGS", mdata[i].TOTAL_EGGS);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata[i].SQLITE_ID);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@EGGS_TYPE_ID", mdata[i].EGGS_TYPE_ID);

                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {

                }

                finally
                {
                    db_closed2();
                }

            }
            //DETAILS

            return dt;
        }


        // Feed Qualaity Control

        public class FeedQualityControlItem
        {
            public double AFFECTED_QTY { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string DELIVERY_CHALLAN { get; set; }
            public string FEED_COMPLAINT_TYPE_ID { get; set; }
            public string FEED_STORAGE_TYPE_ID { get; set; }
            public string FLOCK_NO { get; set; }
            public bool ISDELETED { get; set; }
            public int id { get; set; }
            public string MATERIAL_ID { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public string PHOTO1 { get; set; }
            public string PHOTO2 { get; set; }
            public string PLANT_CODE { get; set; }
            public string PLANT_ID { get; set; }
            public string REMARK { get; set; }
            public string SHED_CODE { get; set; }
            public string SHED_ID { get; set; }
            public string SQLITE_ID { get; set; }
            public string STATUS { get; set; }
        }


        internal DataTable SAVE_FEED_QUALITY_COMPLAINT(string TASK, string DATA)
        {

            List<FeedQualityControlItem> mdata = JsonConvert.DeserializeObject<List<FeedQualityControlItem>>(DATA);

            for (int i = 0; i < mdata.Count(); i++)
            {

                try
                {



                    string Photo1 = "NA.jpg";
                    string Photo2 = "NA.jpg";
                    String FolderPath = "~/PARENT_FEED_QUALITY_COMPLAINT_PHOTO/";

                    if (!mdata[i].PHOTO1.ToString().Equals("0"))
                    {

                        Photo1 = "P_FEED_Q_COMPLAINT" + "_PHOTO1_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].PHOTO1, FolderPath, Photo1);
                    }
                    if (!mdata[i].PHOTO2.ToString().Equals("0"))
                    {

                        Photo2 = "P_FEED_Q_COMPLAINT" + "_PHOTO2_" + mdata[i].CREATED_BY + "_" + mdata[i].SQLITE_ID + ".jpg";
                        UploadPhoto(mdata[i].PHOTO2, FolderPath, Photo2);
                    }

                    db_connection2();
                    cmd = new SqlCommand("SAVE_P_FEED_QUALITY_COMPLAINT", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;

                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@PLANT_ID", mdata[i].PLANT_ID);
                    cmd.Parameters.AddWithValue("@SHED_ID", mdata[i].SHED_ID);
                    cmd.Parameters.AddWithValue("@FLOCK_NO", mdata[i].FLOCK_NO);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@SHED_CODE", mdata[i].SHED_CODE);
                    cmd.Parameters.AddWithValue("@MATERIAL_ID", mdata[i].MATERIAL_ID);
                    cmd.Parameters.AddWithValue("@FEED_STORAGE_TYPE_ID", mdata[i].FEED_STORAGE_TYPE_ID);
                    cmd.Parameters.AddWithValue("@AFFECTED_QTY", mdata[i].AFFECTED_QTY);
                    cmd.Parameters.AddWithValue("@FEED_COMPLAINT_TYPE_ID", mdata[i].FEED_COMPLAINT_TYPE_ID);
                    cmd.Parameters.AddWithValue("@DELIVERY_CHALLAN", mdata[i].DELIVERY_CHALLAN);
                    cmd.Parameters.AddWithValue("@PHOTO1", Photo1);
                    cmd.Parameters.AddWithValue("@PHOTO2", Photo2);
                    cmd.Parameters.AddWithValue("@REMARK", mdata[i].REMARK);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata[i].SQLITE_ID);

                    dt.Clear();
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
            }

            DYANAMIC_AUTO_MAIL("Breeder Feed Complaint");

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


        public void DYANAMIC_AUTO_MAIL(String Mail)
        {
            //GET AUTOMAIL FROM DETAILS
            dt1 = GET_AUTO_MAIL_DETAILS("GET_AUTO_MAIL_FROM_DETAILS", "");

            //GET AUTOMAIL TO DETAILS
            dt_tomail = GET_AUTO_MAIL_TO_DETAILS("GET_AUTO_MAIL_TO_DETAILS", Mail);
            string ID = "";
            for (int k = 0; k < dt_tomail.Rows.Count; k++)
            {
                //GET AUTOMAIL DATA DETAILS
                dt_data = GET_AUTO_MAIL_DATA_DETAILS(dt_tomail.Rows[k]["TITLE"].ToString(), "");
                dt_data_Export = dt_data;
                dt_data_Export.TableName = "Settlement";
         
                try
                {
                    if (dt_data.Rows.Count > 0)
                    {                     
                    MailMessage mailMsg = null;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("*** This is a System Generated Message. Please do not respond to this message! ***");
                    sb.Append("<br/>");
                    mailMsg = new MailMessage(dt1.Rows[0]["MAIL"].ToString(), dt_tomail.Rows[k]["TO_MAIL"].ToString());
                    mailMsg.CC.Add(dt_tomail.Rows[k]["CC_MAIL"].ToString());
                    mailMsg.Bcc.Add(dt_tomail.Rows[k]["BCC_MAIL"].ToString());
                    mailMsg.IsBodyHtml = true;

                    mailMsg.Subject = dt_tomail.Rows[k]["TITLE"].ToString().Trim();

                    sb.Append("<b>" + dt_tomail.Rows[k]["SUBJECT"].ToString().Trim() + "</b>");
                    sb.Append("<br/>");
                    sb.Append("<br/>");
                    sb.Append("\n<br><center><table width=100% border=none>");
                    sb.Append("<tr BGCOLOR=#99CCFF style=\"text-align:Center;\">");
                     for (int i = 0; i < dt_data.Columns.Count; i++)
                        {
                         if (dt_data.Columns[i].ColumnName == "REF_NO")
                                {
                                   
                                }
                                else
                                {
                                    sb.Append("<td style=\"text-align:Center;\"><b><font color=Black>" + dt_data.Columns[i].ColumnName + "</font></b></td>");
                                }
                        }

                        for (int j = 0; j < dt_data.Rows.Count; j++)
                        {
                            sb.Append("<tr style=\"text-align:Center;\">");
                            for (int i = 0; i < dt_data.Columns.Count; i++)
                            {
                                if (dt_data.Columns[i].ColumnName == "REF_NO")
                                {
                                    ID += dt_data.Rows[j][dt_data.Columns[i].ColumnName] + ",";
                                }
                                else
                                {
                                    if (dt_data.Rows[j][dt_data.Columns[i].ColumnName].ToString().Contains(".jpg"))
                                    {
                                        sb.Append("<td>");
                                        sb.Append(" <a style=\"text-align:Center;\"\"display:block;\" href=" + dt_data.Rows[j][dt_data.Columns[i].ColumnName] + " target=\"_blank\">" + dt_data.Columns[i].ColumnName + "</a>");
                                        sb.Append("</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style=\"text-align:Center;\"> " + dt_data.Rows[j][dt_data.Columns[i].ColumnName] + "</td>");
                                    }
                                }

                            }
                            sb.Append("\n ");
                        }

                        sb.Append("</table></center>");
                        sb.Append("<br/>");
                        sb.Append("Greetings,");
                        sb.Append("<br/>");
                        sb.Append("This message has been automatically generated in response to the creation of " + dt_tomail.Rows[k]["DEPARTMENT"].ToString().Trim() + ".");
                        sb.Append("<br/>");
                        sb.Append("<br/>");
                        sb.Append("<br/>");
                        sb.Append("<br/>");
                        sb.Append("Regards,");
                        sb.Append("<br/>");
                        sb.Append(dt_tomail.Rows[k]["DEPARTMENT"].ToString().Trim());

                        mailMsg.Body = sb.ToString();



                        //System.IO.DirectoryInfo di = new DirectoryInfo("D://IIS//POS//Settlement//");
                        //foreach (FileInfo file in di.GetFiles())
                        //{
                        //    file.Delete();
                        //}
                        //foreach (DirectoryInfo dir in di.GetDirectories())
                        //{
                        //    dir.Delete(true);
                        //}
                        //string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //String path = currentDateTime1 + ".xls";
                        //HttpContext.Current.Server.MapPath("~/Settlement/" + path);
                        //dt_data_Export.WriteXml("D://IIS//POS//Settlement//" + path);
                        //mailMsg.Attachments.Add(new Attachment("D://IIS//POS//Settlement//" + path));



                        SmtpUser.UserName = dt1.Rows[0]["USER_NAME"].ToString();
                        SmtpUser.Password = dt1.Rows[0]["PASSWORD"].ToString();
                        client.Host = dt1.Rows[0]["HOST"].ToString();
                        client.Port = Convert.ToInt32(dt1.Rows[0]["PORT"]);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(SmtpUser.UserName, SmtpUser.Password);
                        client.EnableSsl = true;
                        client.Send(mailMsg);

                        GET_AUTO_MAIL_DATA_DETAILS("UPDATE " + dt_tomail.Rows[k]["TITLE"].ToString(), ID);
                    }
                }
                catch (Exception e)
                {
                   // this.Context.Response.Write(e.ToString());
                }
            }

        }


        //GET_SAP_DETAILS
        internal DataTable GET_AUTO_MAIL_DETAILS(string TASK, string SEARCH)
        {

            db_connection2();
            try
            {
                cmd = new SqlCommand("GET_AUTO_MAIL_DETAILS", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;

                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);

                da = new SqlDataAdapter(cmd);
                da.Fill(dt3);
                con2.Close();

            }
            catch (Exception e)
            {
                db_closed2();

            }
            finally
            {
                db_closed2();

            }

            return dt3;
        }



        //GET_AUTO_MAIL_TO_DETAILS
        internal DataTable GET_AUTO_MAIL_TO_DETAILS(string TASK, string SEARCH)
        {

            db_connection2();
            try
            {
                cmd = new SqlCommand("GET_AUTO_MAIL_DETAILS", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;

                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);

                da = new SqlDataAdapter(cmd);
                da.Fill(dt4);
                con2.Close();

            }
            catch (Exception e)
            {
                db_closed2();

            }
            finally
            {
                db_closed2();

            }

            return dt4;
        }


        //GET_AUTO_MAIL_TO_DETAILS
        internal DataTable GET_AUTO_MAIL_DATA_DETAILS(string TASK, string SEARCH)
        {

            db_connection2();
            try
            {
                cmd = new SqlCommand("GET_AUTO_MAIL_DATA", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;

                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);

                da = new SqlDataAdapter(cmd);
                da.Fill(dt5);
                con2.Close();

            }
            catch (Exception e)
            {
                db_closed2();

            }
            finally
            {
                db_closed2();

            }

            return dt5;
        }

    }
}