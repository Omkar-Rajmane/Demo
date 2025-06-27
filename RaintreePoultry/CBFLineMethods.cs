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
namespace RaintreePoultry
{
    public class CBFLineMethods : Connection
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds = new DataSet();
        Methods meth = new Methods();

        public class RootDailyTrans
        {
            public List<TCDAILYTRANSItem> TCDAILYTRANSItem { get; set; }
        }

        public class TCDAILYTRANSItem
        {
            public string DATE { get; set; }
            public double AVG_WT { get; set; }
            public double BG_GROWER { get; set; }
            public int BIRD_AGE { get; set; }
            public string BIRD_QTY { get; set; }
            public double BNF_FINISHER { get; set; }
            public double BS_STARTER { get; set; }
            public int CHLORINE_STATUS_ID { get; set; }
            public int CHLORINE_STATUS_POINT_ID { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public double FIN_QTY { get; set; }
            public string FLOCK_NO { get; set; }
            public int ID { get; set; }
            public bool ISDELETED { get; set; }
            public int ids { get; set; }
            public double LATITUDE { get; set; }
            public double LONGITUDE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public int MORALITY_QTY { get; set; }
            public string MORTALITY_PHOTO { get; set; }
            public string MORTALITY_REASON_ID { get; set; }
            public string NO_OF_DRINKER { get; set; }
            public string NO_OF_FEEDER { get; set; }
            public double OTHER { get; set; }
            public double PBS_PRESTARTER { get; set; }
            public string PLANT_CODE { get; set; }
            public int PLANT_ID { get; set; }
            public double PRESTR_QTY { get; set; }
            public string REPORT_PHOTO { get; set; }
            public bool SAP_FLAG { get; set; }
            public string SQLITE_ID { get; set; }
            public double STR_QTY { get; set; }
            public string VENDOR_CODE { get; set; }
            public string VISITED_PERSON { get; set; }
            public int VISIT_REASON_ID { get; set; }
            public bool VISIT_TO_FARMER { get; set; }
            public double WATER_CONSUMPTION { get; set; }
            public string WATER_PHOTO { get; set; }
            public double GROW_QTY { get; set; }
            public bool ISMEDICINE { get; set; }
            public int MEDICINE_ID { get; set; }
            public int MEDICINE_QTY { get; set; }
            public bool ISANTIBIOTIC_MEDICINE { get; set; }
            public int AMEDICINE_ID { get; set; }
            public int AMEDICINE_QTY { get; set; }
        }


        internal DataTable SAVE_DAILY_TRANS(string TASK, string DATA)
        {
            RootDailyTrans mdata = JsonConvert.DeserializeObject<RootDailyTrans>(DATA);

            //HEADER 
            for (int i = 0; i < mdata.TCDAILYTRANSItem.Count(); i++)
            {
                try
                {
                    string Photo_Path1 = "NA";
                    if (mdata.TCDAILYTRANSItem[i].MORTALITY_PHOTO.ToString().Equals("0"))
                    {
                    }
                    else
                    {
                        string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Photo_Path1 = "Mortality" + mdata.TCDAILYTRANSItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";
                        String FolderPath = "~/CBF_PHOTO/";
                        meth.UploadPhoto(mdata.TCDAILYTRANSItem[i].MORTALITY_PHOTO, FolderPath, Photo_Path1);
                    }

                    string Photo_Path2 = "NA";
                    if (mdata.TCDAILYTRANSItem[i].REPORT_PHOTO.ToString().Equals("0"))
                    {
                    }
                    else
                    {
                        string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Photo_Path2 = "Report" + mdata.TCDAILYTRANSItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";
                        String FolderPath = "~/CBF_PHOTO/";
                        meth.UploadPhoto(mdata.TCDAILYTRANSItem[i].REPORT_PHOTO, FolderPath, Photo_Path2);
                    }

                    string Photo_Path3 = "NA";
                    if (mdata.TCDAILYTRANSItem[i].WATER_PHOTO.ToString().Equals("0"))
                    {
                    }
                    else
                    {
                        string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Photo_Path3 = "Report" + mdata.TCDAILYTRANSItem[i].CREATED_BY + "_" + currentDateTime1 + ".jpg";
                        String FolderPath = "~/CBF_PHOTO/";
                        meth.UploadPhoto(mdata.TCDAILYTRANSItem[i].REPORT_PHOTO, FolderPath, Photo_Path3);
                    }

                    db_connection2();

                    cmd = new SqlCommand("SAVEM_T_C_DAILY_TRANS", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@DATE", mdata.TCDAILYTRANSItem[i].DATE);
                    cmd.Parameters.AddWithValue("@VENDOR_CODE", mdata.TCDAILYTRANSItem[i].VENDOR_CODE);
                    cmd.Parameters.AddWithValue("@PLANT_ID", mdata.TCDAILYTRANSItem[i].PLANT_ID);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.TCDAILYTRANSItem[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@FLOCK_NO", mdata.TCDAILYTRANSItem[i].FLOCK_NO);
                    cmd.Parameters.AddWithValue("@MORALITY_QTY", mdata.TCDAILYTRANSItem[i].MORALITY_QTY);
                    cmd.Parameters.AddWithValue("@PRESTR_QTY", mdata.TCDAILYTRANSItem[i].PRESTR_QTY);
                    cmd.Parameters.AddWithValue("@STR_QTY", mdata.TCDAILYTRANSItem[i].STR_QTY);
                    cmd.Parameters.AddWithValue("@GROW_QTY", mdata.TCDAILYTRANSItem[i].GROW_QTY);
                    cmd.Parameters.AddWithValue("@FIN_QTY", mdata.TCDAILYTRANSItem[i].FIN_QTY);
                    cmd.Parameters.AddWithValue("@AVG_WT", mdata.TCDAILYTRANSItem[i].AVG_WT);
                    cmd.Parameters.AddWithValue("@BIRD_QTY", mdata.TCDAILYTRANSItem[i].BIRD_QTY);
                    cmd.Parameters.AddWithValue("@BIRD_AGE", mdata.TCDAILYTRANSItem[i].BIRD_AGE);
                    cmd.Parameters.AddWithValue("@NO_OF_FEEDER", mdata.TCDAILYTRANSItem[i].NO_OF_FEEDER);
                    cmd.Parameters.AddWithValue("@NO_OF_DRINKER", mdata.TCDAILYTRANSItem[i].NO_OF_DRINKER);
                    cmd.Parameters.AddWithValue("@OTHER", mdata.TCDAILYTRANSItem[i].OTHER);
                    cmd.Parameters.AddWithValue("@VISIT_TO_FARMER", mdata.TCDAILYTRANSItem[i].VISIT_TO_FARMER);
                    cmd.Parameters.AddWithValue("@VISITED_PERSON", mdata.TCDAILYTRANSItem[i].VISITED_PERSON);
                    cmd.Parameters.AddWithValue("@VISIT_REASON_ID", mdata.TCDAILYTRANSItem[i].VISIT_REASON_ID);
                    cmd.Parameters.AddWithValue("@WATER_CONSUMPTION", mdata.TCDAILYTRANSItem[i].WATER_CONSUMPTION);
                    cmd.Parameters.AddWithValue("@MORTALITY_REASON_ID", mdata.TCDAILYTRANSItem[i].MORTALITY_REASON_ID);
                    cmd.Parameters.AddWithValue("@PBS_PRESTARTER", mdata.TCDAILYTRANSItem[i].PBS_PRESTARTER);
                    cmd.Parameters.AddWithValue("@BS_STARTER", mdata.TCDAILYTRANSItem[i].BS_STARTER);
                    cmd.Parameters.AddWithValue("@BG_GROWER", mdata.TCDAILYTRANSItem[i].BG_GROWER);
                    cmd.Parameters.AddWithValue("@BNF_FINISHER", mdata.TCDAILYTRANSItem[i].BNF_FINISHER);
                    cmd.Parameters.AddWithValue("@CHLORINE_STATUS_ID", mdata.TCDAILYTRANSItem[i].CHLORINE_STATUS_ID);
                    cmd.Parameters.AddWithValue("@CHLORINE_STATUS_POINT_ID", mdata.TCDAILYTRANSItem[i].CHLORINE_STATUS_POINT_ID);
                    cmd.Parameters.AddWithValue("@REPORT_PHOTO", Photo_Path2);
                    cmd.Parameters.AddWithValue("@MORTALITY_PHOTO", Photo_Path1);
                    cmd.Parameters.AddWithValue("@WATER_PHOTO", Photo_Path3);
                    cmd.Parameters.AddWithValue("@LATITUDE", mdata.TCDAILYTRANSItem[i].LATITUDE);
                    cmd.Parameters.AddWithValue("@LONGITUDE", mdata.TCDAILYTRANSItem[i].LONGITUDE);
                    cmd.Parameters.AddWithValue("@SAP_FLAG", mdata.TCDAILYTRANSItem[i].SAP_FLAG);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TCDAILYTRANSItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TCDAILYTRANSItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TCDAILYTRANSItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TCDAILYTRANSItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TCDAILYTRANSItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TCDAILYTRANSItem[i].SQLITE_ID);
                    cmd.Parameters.AddWithValue("@ISMEDICINE", mdata.TCDAILYTRANSItem[i].ISMEDICINE);
                    cmd.Parameters.AddWithValue("@MEDICINE_ID", mdata.TCDAILYTRANSItem[i].MEDICINE_ID);
                    cmd.Parameters.AddWithValue("@MEDICINE_QTY", mdata.TCDAILYTRANSItem[i].MEDICINE_QTY);
                    cmd.Parameters.AddWithValue("@ISANTIBIOTIC_MEDICINE", mdata.TCDAILYTRANSItem[i].ISANTIBIOTIC_MEDICINE);
                    cmd.Parameters.AddWithValue("@AMEDICINE_ID", mdata.TCDAILYTRANSItem[i].AMEDICINE_ID);
                    cmd.Parameters.AddWithValue("@AMEDICINE_QTY", mdata.TCDAILYTRANSItem[i].AMEDICINE_QTY);

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

    }
}