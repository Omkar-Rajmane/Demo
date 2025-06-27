using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;

namespace RaintreePoultry 
{
    public class LiveSaleAndBranddingMethods : Connection
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;
        Methods meth = new Methods();

        // Tradeer Survey----------------------------

        class RootTraderSurvey
        {
            public List<TTRADERSURVEYItem> TTRADERSURVEYItem { get; set; }
        }

        class TTRADERSURVEYItem
        {
            public int ids { get; set; }
            public int TRADER_REQ_ID { get; set; }
            public int CITY_ID { get; set; }
            public string PLACE { get; set; }
            public string STREET_NAME { get; set; }
            public string TREDING_NAME { get; set; }
            public string TREDAR_NAME { get; set; }
            public string MOBILE_NO { get; set; }
            public double SALE_DAY { get; set; }
            public double SALE_WEEKLY { get; set; }
            public double SALE_MONTHLY { get; set; }
            public int COMP_ID { get; set; }
            public int IFOTHER_WAY { get; set; }
            public int LIFTING_AREA { get; set; }
            public double SIZE_REQURED { get; set; }
            public string SHOP_PHOTO { get; set; }
            public int BAL_CHIKEN_QUILITY { get; set; }
            public int SALE_CONDITION { get; set; }
            public string LATITUDE { get; set; }
            public string LONGITUDE { get; set; }
            public bool IS_APPROVE { get; set; }
            public int APPROVE_BY { get; set; }
            public string APPROVE_DATE { get; set; }
            public int CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
        }

        internal DataTable SAVE_TRADER_SURVEY(string TASK, string DATA)
        {
            RootTraderSurvey mdata = JsonConvert.DeserializeObject<RootTraderSurvey>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TTRADERSURVEYItem.Count(); i++)
            {
                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_M_BR_TRADER_REQUEST", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@CITY_ID", mdata.TTRADERSURVEYItem[i].CITY_ID);
                    cmd.Parameters.AddWithValue("@PLACE", mdata.TTRADERSURVEYItem[i].PLACE);
                    cmd.Parameters.AddWithValue("@STREET_NAME", mdata.TTRADERSURVEYItem[i].STREET_NAME);
                    cmd.Parameters.AddWithValue("@TREDING_NAME", mdata.TTRADERSURVEYItem[i].TREDING_NAME);
                    cmd.Parameters.AddWithValue("@TREDAR_NAME", mdata.TTRADERSURVEYItem[i].TREDAR_NAME);
                    cmd.Parameters.AddWithValue("@MOBILE_NO", mdata.TTRADERSURVEYItem[i].MOBILE_NO);
                    cmd.Parameters.AddWithValue("@SALE_DAY", mdata.TTRADERSURVEYItem[i].SALE_DAY);
                    cmd.Parameters.AddWithValue("@SALE_WEEKLY", mdata.TTRADERSURVEYItem[i].SALE_WEEKLY);
                    cmd.Parameters.AddWithValue("@SALE_MONTHLY", mdata.TTRADERSURVEYItem[i].SALE_MONTHLY);
                    cmd.Parameters.AddWithValue("@COMP_ID", mdata.TTRADERSURVEYItem[i].COMP_ID);
                    cmd.Parameters.AddWithValue("@IFOTHER_WAY", mdata.TTRADERSURVEYItem[i].IFOTHER_WAY);
                    cmd.Parameters.AddWithValue("@LIFTING_AREA", mdata.TTRADERSURVEYItem[i].LIFTING_AREA);
                    cmd.Parameters.AddWithValue("@SIZE_REQURED", mdata.TTRADERSURVEYItem[i].SIZE_REQURED);
                    cmd.Parameters.AddWithValue("@SHOP_PHOTO", mdata.TTRADERSURVEYItem[i].SHOP_PHOTO);
                    cmd.Parameters.AddWithValue("@BAL_CHIKEN_QUILITY", mdata.TTRADERSURVEYItem[i].BAL_CHIKEN_QUILITY);
                    cmd.Parameters.AddWithValue("@SALE_CONDITION", mdata.TTRADERSURVEYItem[i].SALE_CONDITION);
                    cmd.Parameters.AddWithValue("@LATITUDE", mdata.TTRADERSURVEYItem[i].LATITUDE);
                    cmd.Parameters.AddWithValue("@LONGITUDE", mdata.TTRADERSURVEYItem[i].LONGITUDE);
                    cmd.Parameters.AddWithValue("@IS_APPROVE", mdata.TTRADERSURVEYItem[i].IS_APPROVE);
                    cmd.Parameters.AddWithValue("@APPROVE_BY", mdata.TTRADERSURVEYItem[i].APPROVE_BY);
                    cmd.Parameters.AddWithValue("@APPROVE_DATE", mdata.TTRADERSURVEYItem[i].APPROVE_DATE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TTRADERSURVEYItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TTRADERSURVEYItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TTRADERSURVEYItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TTRADERSURVEYItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TTRADERSURVEYItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TTRADERSURVEYItem[i].SQLITE_ID);

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
            return dt;
        }


        // Retailer Survey----------------------------

        class RootRetilerSurvey
        {
            public List<TRETAILERSURVEYItem> TRETAILERSURVEYItem { get; set; }
        }

        class TRETAILERSURVEYItem
        {
            public int ids { get; set; }
            public int SHOP_NO { get; set; }
            public int SHOP_CATOGERY_ID { get; set; }
            public int CITY_ID { get; set; }
            public string OWNER_NAME { get; set; }
            public string MOBILE_NO { get; set; }
            public string PLACE { get; set; }
            public string STREET_NAME { get; set; }
            public string SHOP_NAME { get; set; }
            public string SALE_DAY { get; set; }
            public string SALE_WEEKLY { get; set; }
            public string SALE_MONTHLY { get; set; }
            public int TRADER_ID { get; set; }
            public string TRADER_NAME_OTHER { get; set; }
            public int COMP_ID { get; set; }
            public string COMPANY_NAME_OTHER { get; set; }
            public bool IS_BAL_BOARD { get; set; }
            public bool IS_BOARD_REQUIRED { get; set; }
            public string COMP_BRANDDING_ACT { get; set; }
            public int MARKET_PREDICTION { get; set; }
            public int IFOTHER_WHY { get; set; }
            public int BAL_CHIKEN_QUILITY { get; set; }
            public int SALE_CONDITION { get; set; }
            public string SHOP_PHOTO { get; set; }
            public string LATITUDE { get; set; }
            public string LONGITUDE { get; set; }
            public bool IS_APPROVE { get; set; }
            public int APPROVE_BY { get; set; }
            public string APPROVE_DATE { get; set; }
            public int CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public int MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
            public bool IS_PROMOTIONAL_ACTIVITY { get; set; }
            public string PROMOTIONAL_ACTIVITY { get; set; }
        }

        internal DataTable SAVE_RETAILER_SURVEY(string TASK, string DATA)
        {
            RootRetilerSurvey mdata = JsonConvert.DeserializeObject<RootRetilerSurvey>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TRETAILERSURVEYItem.Count(); i++)
            {
                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_M_BR_RETAILER_REQUEST", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@SHOP_CATOGERY_ID", mdata.TRETAILERSURVEYItem[i].SHOP_CATOGERY_ID);
                    cmd.Parameters.AddWithValue("@CITY_ID", mdata.TRETAILERSURVEYItem[i].CITY_ID);
                    cmd.Parameters.AddWithValue("@OWNER_NAME", mdata.TRETAILERSURVEYItem[i].OWNER_NAME);
                    cmd.Parameters.AddWithValue("@MOBILE_NO", mdata.TRETAILERSURVEYItem[i].MOBILE_NO);
                    cmd.Parameters.AddWithValue("@PLACE", mdata.TRETAILERSURVEYItem[i].PLACE);
                    cmd.Parameters.AddWithValue("@STREET_NAME", mdata.TRETAILERSURVEYItem[i].STREET_NAME);
                    cmd.Parameters.AddWithValue("@SHOP_NAME", mdata.TRETAILERSURVEYItem[i].SHOP_NAME);
                    cmd.Parameters.AddWithValue("@SALE_DAY", mdata.TRETAILERSURVEYItem[i].SALE_DAY);
                    cmd.Parameters.AddWithValue("@SALE_WEEKLY", mdata.TRETAILERSURVEYItem[i].SALE_WEEKLY);
                    cmd.Parameters.AddWithValue("@SALE_MONTHLY", mdata.TRETAILERSURVEYItem[i].SALE_MONTHLY);
                    cmd.Parameters.AddWithValue("@TRADER_ID", mdata.TRETAILERSURVEYItem[i].TRADER_ID);
                    cmd.Parameters.AddWithValue("@TRADER_NAME_OTHER", mdata.TRETAILERSURVEYItem[i].TRADER_NAME_OTHER);
                    cmd.Parameters.AddWithValue("@COMP_ID", mdata.TRETAILERSURVEYItem[i].COMP_ID);
                    cmd.Parameters.AddWithValue("@COMPANY_NAME_OTHER", mdata.TRETAILERSURVEYItem[i].COMPANY_NAME_OTHER);
                    cmd.Parameters.AddWithValue("@IS_BAL_BOARD", mdata.TRETAILERSURVEYItem[i].IS_BAL_BOARD);
                    cmd.Parameters.AddWithValue("@IS_BOARD_REQUIRED", mdata.TRETAILERSURVEYItem[i].IS_BOARD_REQUIRED);
                    cmd.Parameters.AddWithValue("@COMP_BRANDDING_ACT", mdata.TRETAILERSURVEYItem[i].COMP_BRANDDING_ACT);
                    cmd.Parameters.AddWithValue("@MARKET_PREDICTION", mdata.TRETAILERSURVEYItem[i].MARKET_PREDICTION);
                    cmd.Parameters.AddWithValue("@IFOTHER_WHY", mdata.TRETAILERSURVEYItem[i].IFOTHER_WHY);
                    cmd.Parameters.AddWithValue("@BAL_CHIKEN_QUILITY", mdata.TRETAILERSURVEYItem[i].BAL_CHIKEN_QUILITY);
                    cmd.Parameters.AddWithValue("@SALE_CONDITION", mdata.TRETAILERSURVEYItem[i].SALE_CONDITION);
                    cmd.Parameters.AddWithValue("@SHOP_PHOTO", mdata.TRETAILERSURVEYItem[i].SHOP_PHOTO);
                    cmd.Parameters.AddWithValue("@LATITUDE", mdata.TRETAILERSURVEYItem[i].LATITUDE);
                    cmd.Parameters.AddWithValue("@LONGITUDE", mdata.TRETAILERSURVEYItem[i].LONGITUDE);
                    cmd.Parameters.AddWithValue("@IS_APPROVE", mdata.TRETAILERSURVEYItem[i].IS_APPROVE);
                    cmd.Parameters.AddWithValue("@APPROVE_BY", mdata.TRETAILERSURVEYItem[i].APPROVE_BY);
                    cmd.Parameters.AddWithValue("@APPROVE_DATE", mdata.TRETAILERSURVEYItem[i].APPROVE_DATE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TRETAILERSURVEYItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TRETAILERSURVEYItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TRETAILERSURVEYItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TRETAILERSURVEYItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TRETAILERSURVEYItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TRETAILERSURVEYItem[i].SQLITE_ID);
                    cmd.Parameters.AddWithValue("@IS_PROMOTIONAL_ACTIVITY", mdata.TRETAILERSURVEYItem[i].IS_PROMOTIONAL_ACTIVITY);
                    cmd.Parameters.AddWithValue("@PROMOTIONAL_ACTIVITY", mdata.TRETAILERSURVEYItem[i].PROMOTIONAL_ACTIVITY);

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
            return dt;
        }



        // Board Locator----------------------------

        class RootBoardLocator
        {
            public List<TBOARDLOCATORItem> TBOARDLOCATORItem { get; set; }
        }

        class TBOARDLOCATORItem
        {
            public int ids { get; set; }
            public int BOARD_ID { get; set; }
            public int RETAILER_ID { get; set; }
            public string BOARD { get; set; }
            public double BOARD_HIGHT { get; set; }
            public double BOARD_WIDTH { get; set; }
            public int BOARD_TYPE { get; set; }
            public int SHOP_TYPE { get; set; }
            public string BOARD_SR_NO { get; set; }
            public string BOARD_PHOTO { get; set; }
            public string LATITUDE { get; set; }
            public string LONGITUDE { get; set; }
            public bool IS_APPROVE { get; set; }
            public string APPROVE_BY { get; set; }
            public string APPROVE_DATE { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
        }

        internal DataTable SAVE_BOARD_LOCATOR(string TASK, string DATA)
        {
            RootBoardLocator mdata = JsonConvert.DeserializeObject<RootBoardLocator>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TBOARDLOCATORItem.Count(); i++)
            {
                string Photo_Path = "NA";
                if (mdata.TBOARDLOCATORItem[i].BOARD_PHOTO.ToString().Equals("0"))
                {
                }
                else
                {
                    string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Photo_Path = "BocatorLocator_" + mdata.TBOARDLOCATORItem[i].CREATED_BY + "_" + mdata.TBOARDLOCATORItem[i].SQLITE_ID + ".jpg";
                    String FolderPath = "~/BRADING_PHOTO/";
                    meth.UploadPhoto(mdata.TBOARDLOCATORItem[i].BOARD_PHOTO, FolderPath, Photo_Path);
                }
                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_M_BR_BOARD_LOCATOR", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@RETAILER_ID", mdata.TBOARDLOCATORItem[i].RETAILER_ID);
                    cmd.Parameters.AddWithValue("@BOARD", mdata.TBOARDLOCATORItem[i].BOARD);
                    cmd.Parameters.AddWithValue("@BOARD_HIGHT", mdata.TBOARDLOCATORItem[i].BOARD_HIGHT);
                    cmd.Parameters.AddWithValue("@BOARD_WIDTH", mdata.TBOARDLOCATORItem[i].BOARD_WIDTH);
                    cmd.Parameters.AddWithValue("@BOARD_TYPE", mdata.TBOARDLOCATORItem[i].BOARD_TYPE);
                    cmd.Parameters.AddWithValue("@SHOP_TYPE", mdata.TBOARDLOCATORItem[i].SHOP_TYPE);
                    cmd.Parameters.AddWithValue("@BOARD_SR_NO", mdata.TBOARDLOCATORItem[i].BOARD_SR_NO);
                    cmd.Parameters.AddWithValue("@BOARD_PHOTO", Photo_Path);
                    cmd.Parameters.AddWithValue("@LATITUDE", mdata.TBOARDLOCATORItem[i].LATITUDE);
                    cmd.Parameters.AddWithValue("@LONGITUDE", mdata.TBOARDLOCATORItem[i].LONGITUDE);
                    cmd.Parameters.AddWithValue("@IS_APPROVE", mdata.TBOARDLOCATORItem[i].IS_APPROVE);
                    cmd.Parameters.AddWithValue("@APPROVE_BY", mdata.TBOARDLOCATORItem[i].APPROVE_BY);
                    cmd.Parameters.AddWithValue("@APPROVE_DATE", mdata.TBOARDLOCATORItem[i].APPROVE_DATE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TBOARDLOCATORItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TBOARDLOCATORItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TBOARDLOCATORItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TBOARDLOCATORItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TBOARDLOCATORItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TBOARDLOCATORItem[i].SQLITE_ID);

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
            return dt;
        }


        // Trader Visit Save to db----------------------------

        class RootTraderVisit
        {
            public List<TTRADERVISITItem> TTRADERVISITItem { get; set; }
        }

        class TTRADERVISITItem
        {
            public int ids { get; set; }
            public int TRADER_VISIT_ID { get; set; }
            public int TRADER_ID { get; set; }
            public int VISIT_REASON { get; set; }
            public double SALE_DAY { get; set; }
            public double SALE_WEEKLY { get; set; }
            public double SALE_MONTHLY { get; set; }
            public double SIZE_REQUIRED { get; set; }
            public int IFOTHER_WAY { get; set; }
            public int LIFTING_AREA { get; set; }
            public int COMP_ID { get; set; }
            public int BAL_CHIKEN_QUILITY { get; set; }
            public int SALE_CONDITION { get; set; }
            public string LATITUDE { get; set; }
            public string LONGITUDE { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
        }

        internal DataTable SAVE_TRADER_VISIT(string TASK, string DATA)
        {
            RootTraderVisit mdata = JsonConvert.DeserializeObject<RootTraderVisit>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TTRADERVISITItem.Count(); i++)
            {

                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_T_BR_TRADER_VISIT", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@TRADER_ID", mdata.TTRADERVISITItem[i].TRADER_ID);
                    cmd.Parameters.AddWithValue("@VISIT_REASON", mdata.TTRADERVISITItem[i].VISIT_REASON);
                    cmd.Parameters.AddWithValue("@SALE_DAY", mdata.TTRADERVISITItem[i].SALE_DAY);
                    cmd.Parameters.AddWithValue("@SALE_WEEKLY", mdata.TTRADERVISITItem[i].SALE_WEEKLY);
                    cmd.Parameters.AddWithValue("@SALE_MONTHLY", mdata.TTRADERVISITItem[i].SALE_MONTHLY);
                    cmd.Parameters.AddWithValue("@SIZE_REQUIRED", mdata.TTRADERVISITItem[i].SIZE_REQUIRED);
                    cmd.Parameters.AddWithValue("@IFOTHER_WHY", mdata.TTRADERVISITItem[i].IFOTHER_WAY);
                    cmd.Parameters.AddWithValue("@LIFTING_AREA", mdata.TTRADERVISITItem[i].LIFTING_AREA);
                    cmd.Parameters.AddWithValue("@COMP_ID", mdata.TTRADERVISITItem[i].COMP_ID);
                    cmd.Parameters.AddWithValue("@BAL_CHIKEN_QUILITY", mdata.TTRADERVISITItem[i].BAL_CHIKEN_QUILITY);
                    cmd.Parameters.AddWithValue("@SALE_CONDITION", mdata.TTRADERVISITItem[i].SALE_CONDITION);
                    cmd.Parameters.AddWithValue("@LATITUDE", mdata.TTRADERVISITItem[i].LATITUDE);
                    cmd.Parameters.AddWithValue("@LONGITUDE", mdata.TTRADERVISITItem[i].LONGITUDE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TTRADERVISITItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TTRADERVISITItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TTRADERVISITItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TTRADERVISITItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TTRADERVISITItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TTRADERVISITItem[i].SQLITE_ID);

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
            return dt;
        }


        // Retailer Visit save to DB ----------------------------

        class RootRetailerVisit
        {
            public List<TRETAILERVISITItem> TRETAILERVISITItem { get; set; }
        }

        class TRETAILERVISITItem
        {
            public int ids { get; set; }
            public int RETAILER_ID { get; set; }
            public String SHOP_NAME { get; set; }    
            public String OWNER_NAME { get; set; }      
            public String MOBILE_NUMBER { get; set; }        
            public String STREET_NAME { get; set; }
            public String PLACE { get; set; }
            public String City { get; set; }
            public int VISIT_REASON { get; set; }
            public bool IS_BAL_BOARD { get; set; }
            public String EX_BRD_CONDITION { get; set; }
            public bool IS_PREV_BRD_REMOVED { get; set; }
            public String BRD_REMOVE_REASON { get; set; }
            public String SIZE { get; set; }
            public String DELIVERY_RATE { get; set; }
            public String LIVE_RATE { get; set; }
            public String DRESSED_RATE { get; set; }
            public String SALE_DAYLY { get; set; }
            public String SALE_WEEKLY { get; set; }
            public String SALE_MONTHLY { get; set; }
            public int COMP_ID { get; set; }
            public String OTHER_COMPANY_NAME { get; set; }
            public int IFOTHER_WAY { get; set; }
            public int TRADER_ID { get; set; }
            public String OTHER_TRADER_NAME { get; set; }
            public String COMPETITOR_BR_ACTIVITY { get; set; }
            public int MARKET_PREDICTION { get; set; }
            public int CUSTOMER_FEEDBACK { get; set; }
            public bool IS_PROMOTIONAL_ACTIVITY { get; set; }
            public String PROMOTIONAL_ACTIVITY { get; set; }
            public int BAL_CHIKEN_QUILITY { get; set; }
            public int SALE_CONDITION { get; set; }
            public String REMARK { get; set; }
            public String VISIT_PHOTO { get; set; }
            public string LATITUDE { get; set; }
            public string LONGITUDE { get; set; }
            public bool IS_APPROVE { get; set; }
            public string APPROVE_BY { get; set; }
            public string APPROVE_DATE { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public String SQLITE_ID { get; set; }

        }

        internal DataTable SAVE_RETAILER_VISIT(string TASK, string DATA)
        {
            RootRetailerVisit mdata = JsonConvert.DeserializeObject<RootRetailerVisit>(DATA);  //Convert json to poco

         

            //HEADER 
            for (int i = 0; i < mdata.TRETAILERVISITItem.Count(); i++)
            {
                string Photo_Path = "NA";
                if (mdata.TRETAILERVISITItem[i].VISIT_PHOTO.ToString().Equals("0"))
                {
                }
                else
                {
                    string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Photo_Path = "RetilerVisit_" + mdata.TRETAILERVISITItem[i].CREATED_BY + "_" + mdata.TRETAILERVISITItem[i].SQLITE_ID + ".jpg";
                    String FolderPath = "~/BRADING_PHOTO/";
                    meth.UploadPhoto(mdata.TRETAILERVISITItem[i].VISIT_PHOTO, FolderPath, Photo_Path);
                }
                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_BR_T_RETAILER_VISIT", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@RETAILER_ID", mdata.TRETAILERVISITItem[i].RETAILER_ID);
                    cmd.Parameters.AddWithValue("@VISIT_REASON", mdata.TRETAILERVISITItem[i].VISIT_REASON);
                    cmd.Parameters.AddWithValue("@IS_BAL_BOARD", mdata.TRETAILERVISITItem[i].IS_BAL_BOARD);
                    cmd.Parameters.AddWithValue("@EX_BRD_CONDITION", mdata.TRETAILERVISITItem[i].EX_BRD_CONDITION);
                    cmd.Parameters.AddWithValue("@IS_PREV_BRD_REMOVED", mdata.TRETAILERVISITItem[i].IS_PREV_BRD_REMOVED);
                    cmd.Parameters.AddWithValue("@BRD_REMOVE_REASON", mdata.TRETAILERVISITItem[i].BRD_REMOVE_REASON);
                    cmd.Parameters.AddWithValue("@SIZE", mdata.TRETAILERVISITItem[i].SIZE);
                    cmd.Parameters.AddWithValue("@DELIVERY_RATE", mdata.TRETAILERVISITItem[i].DELIVERY_RATE);
                    cmd.Parameters.AddWithValue("@LIVE_RATE", mdata.TRETAILERVISITItem[i].LIVE_RATE);
                    cmd.Parameters.AddWithValue("@DRESSED_RATE", mdata.TRETAILERVISITItem[i].DRESSED_RATE);
                    cmd.Parameters.AddWithValue("@SALE_DAYLY", mdata.TRETAILERVISITItem[i].SALE_DAYLY);
                    cmd.Parameters.AddWithValue("@SALE_WEEKLY", mdata.TRETAILERVISITItem[i].SALE_WEEKLY);
                    cmd.Parameters.AddWithValue("@SALE_MONTHLY", mdata.TRETAILERVISITItem[i].SALE_MONTHLY);
                    cmd.Parameters.AddWithValue("@COMP_ID", mdata.TRETAILERVISITItem[i].COMP_ID);
                    cmd.Parameters.AddWithValue("@OTHER_COMPANY_NAME", mdata.TRETAILERVISITItem[i].OTHER_COMPANY_NAME);
                    cmd.Parameters.AddWithValue("@IFOTHER_WAY", mdata.TRETAILERVISITItem[i].IFOTHER_WAY);
                    cmd.Parameters.AddWithValue("@TRADER_ID", mdata.TRETAILERVISITItem[i].TRADER_ID);
                    cmd.Parameters.AddWithValue("@OTHER_TRADER_NAME", mdata.TRETAILERVISITItem[i].OTHER_TRADER_NAME);
                    cmd.Parameters.AddWithValue("@COMPETITOR_BR_ACTIVITY", mdata.TRETAILERVISITItem[i].COMPETITOR_BR_ACTIVITY);
                    cmd.Parameters.AddWithValue("@MARKET_PREDICTION", mdata.TRETAILERVISITItem[i].MARKET_PREDICTION);
                    cmd.Parameters.AddWithValue("@CUSTOMER_FEEDBACK", mdata.TRETAILERVISITItem[i].CUSTOMER_FEEDBACK);
                    cmd.Parameters.AddWithValue("@IS_PROMOTIONAL_ACTIVITY", mdata.TRETAILERVISITItem[i].IS_PROMOTIONAL_ACTIVITY);
                    cmd.Parameters.AddWithValue("@PROMOTIONAL_ACTIVITY", mdata.TRETAILERVISITItem[i].PROMOTIONAL_ACTIVITY);
                    cmd.Parameters.AddWithValue("@BAL_CHIKEN_QUILITY", mdata.TRETAILERVISITItem[i].BAL_CHIKEN_QUILITY);
                    cmd.Parameters.AddWithValue("@SALE_CONDITION", mdata.TRETAILERVISITItem[i].SALE_CONDITION);
                    cmd.Parameters.AddWithValue("@REMARK", mdata.TRETAILERVISITItem[i].REMARK);
                    cmd.Parameters.AddWithValue("@VISIT_PHOTO", Photo_Path);
                    cmd.Parameters.AddWithValue("@LATITUDE", mdata.TRETAILERVISITItem[i].LATITUDE);
                    cmd.Parameters.AddWithValue("@LONGITUDE", mdata.TRETAILERVISITItem[i].LONGITUDE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TRETAILERVISITItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TRETAILERVISITItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TRETAILERVISITItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TRETAILERVISITItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TRETAILERVISITItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TRETAILERVISITItem[i].SQLITE_ID);

                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    string currentDateTime2 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/RAINPOULTRY_FILE/") + mdata.TRETAILERVISITItem[0].CREATED_BY + "_" + currentDateTime2, DATA);
             
                    db_closed2();
                }
            }
            return dt;
        }



        // Other Activity----------------------------

        class RootOtherActivity
        {
            public List<TOTHERACTIVITYItem> TOTHERACTIVITYItem { get; set; }
        }

        class TOTHERACTIVITYItem
        {
            public int ids { get; set; }
            public int A_ID { get; set; }
            public string OTHER_ACTIVITY { get; set; }
            public string ACTIVITY_PHOTO { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
        }

        internal DataTable SAVE_OTHER_ACTIVITY(string TASK, string DATA)
        {
            RootOtherActivity mdata = JsonConvert.DeserializeObject<RootOtherActivity>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TOTHERACTIVITYItem.Count(); i++)
            {
                string Photo_Path = "NA";
                if (mdata.TOTHERACTIVITYItem[i].ACTIVITY_PHOTO.ToString().Equals("0"))
                {
                }
                else
                {
                    string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Photo_Path = "OtherAcivity_" + mdata.TOTHERACTIVITYItem[i].CREATED_BY + "_" + mdata.TOTHERACTIVITYItem[i].SQLITE_ID + ".jpg";
                    String FolderPath = "~/BRADING_PHOTO/";
                    meth.UploadPhoto(mdata.TOTHERACTIVITYItem[i].ACTIVITY_PHOTO, FolderPath, Photo_Path);
                }
                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_BR_T_OTHER_ACTIVITY", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@A_ID", mdata.TOTHERACTIVITYItem[i].A_ID);
                    cmd.Parameters.AddWithValue("@OTHER_ACTIVITY", mdata.TOTHERACTIVITYItem[i].OTHER_ACTIVITY);
                    cmd.Parameters.AddWithValue("@ACTIVITY_PHOTO", Photo_Path);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TOTHERACTIVITYItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TOTHERACTIVITYItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TOTHERACTIVITYItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TOTHERACTIVITYItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TOTHERACTIVITYItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TOTHERACTIVITYItem[i].SQLITE_ID);

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
            return dt;
        }


        // Brandding Customer Complaint----------------------------

        class RootCustomerComplaint
        {
            public List<TCUSTOMERCOMPLAINTItem> TCUSTOMERCOMPLAINTItem { get; set; }
        }

        class TCUSTOMERCOMPLAINTItem
        {
            public int ids { get; set; }
            public int RETAILER_ID { get; set; }
            public string COMPLAINT { get; set; }
            public string COMPLAINT_PHOTO { get; set; }
            public string ACTION { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
        }

        internal DataTable SAVE_CUSTOMER_COMPLAINT(string TASK, string DATA)
        {
            RootCustomerComplaint mdata = JsonConvert.DeserializeObject<RootCustomerComplaint>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TCUSTOMERCOMPLAINTItem.Count(); i++)
            {
                string Photo_Path = "NA";
                if (mdata.TCUSTOMERCOMPLAINTItem[i].COMPLAINT_PHOTO.ToString().Equals("0"))
                {
                }
                else
                {
                    string currentDateTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Photo_Path = "CustomerComplaint_" + mdata.TCUSTOMERCOMPLAINTItem[i].CREATED_BY + "_" + mdata.TCUSTOMERCOMPLAINTItem[i].SQLITE_ID + ".jpg";
                    String FolderPath = "~/BRADING_PHOTO/";
                    meth.UploadPhoto(mdata.TCUSTOMERCOMPLAINTItem[i].COMPLAINT_PHOTO, FolderPath, Photo_Path);
                }
                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_BR_T_CUSTOMER_COMPLAINT", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@RETAILER_ID", mdata.TCUSTOMERCOMPLAINTItem[i].RETAILER_ID);
                    cmd.Parameters.AddWithValue("@COMPLAINT", mdata.TCUSTOMERCOMPLAINTItem[i].COMPLAINT);
                    cmd.Parameters.AddWithValue("@COMPLAINT_PHOTO", Photo_Path);
                    cmd.Parameters.AddWithValue("@ACTION", mdata.TCUSTOMERCOMPLAINTItem[i].ACTION);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TCUSTOMERCOMPLAINTItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TCUSTOMERCOMPLAINTItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TCUSTOMERCOMPLAINTItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TCUSTOMERCOMPLAINTItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TCUSTOMERCOMPLAINTItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TCUSTOMERCOMPLAINTItem[i].SQLITE_ID);

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
            return dt;
        }

        // Brandding Appron Distributor----------------------------

        class RootAppronDistributor
        {
            public List<TAPPRONDISTRIBUTORItem> TAPPRONDISTRIBUTORItem { get; set; }
        }

        class TAPPRONDISTRIBUTORItem
        {
            public int ids { get; set; }
            public string DATE { get; set; }
            public string TRADER_NAME { get; set; }
            public int QUANTITY { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
        }

        internal DataTable SAVE_APPRON_DISTRIBUTOR(string TASK, string DATA)
        {
            RootAppronDistributor mdata = JsonConvert.DeserializeObject<RootAppronDistributor>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TAPPRONDISTRIBUTORItem.Count(); i++)
            {

                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_BR_T_APPRON_DISTRIBUTOR", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@DATE", mdata.TAPPRONDISTRIBUTORItem[i].DATE);
                    cmd.Parameters.AddWithValue("@TRADER_NAME", mdata.TAPPRONDISTRIBUTORItem[i].TRADER_NAME);
                    cmd.Parameters.AddWithValue("@QUANTITY", mdata.TAPPRONDISTRIBUTORItem[i].QUANTITY);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TAPPRONDISTRIBUTORItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TAPPRONDISTRIBUTORItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TAPPRONDISTRIBUTORItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TAPPRONDISTRIBUTORItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TAPPRONDISTRIBUTORItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TAPPRONDISTRIBUTORItem[i].SQLITE_ID);

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
            return dt;
        }

        // Brandding Owner----------------------------

        class RootBranddingOwner
        {
            public List<TOWNERItem> TOWNERItem { get; set; }
        }

        class TOWNERItem
        {
            public int ids { get; set; }
            public int OWNER_ID { get; set; }
            public int CITY_ID { get; set; }
            public string OWNER_NAME { get; set; }
            public string MOBILE_NO { get; set; }
            public string SHOP_NAME { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
        }

        internal DataTable SAVE_BR_OWNER(string TASK, string DATA)
        {
            RootBranddingOwner mdata = JsonConvert.DeserializeObject<RootBranddingOwner>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TOWNERItem.Count(); i++)
            {

                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_BR_T_OWNER", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@CITY_ID", mdata.TOWNERItem[i].CITY_ID);
                    cmd.Parameters.AddWithValue("@OWNER_NAME", mdata.TOWNERItem[i].OWNER_NAME);
                    cmd.Parameters.AddWithValue("@MOBILE_NO", mdata.TOWNERItem[i].MOBILE_NO);
                    cmd.Parameters.AddWithValue("@SHOP_NAME", mdata.TOWNERItem[i].SHOP_NAME);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TOWNERItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TOWNERItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TOWNERItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TOWNERItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TOWNERItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TOWNERItem[i].SQLITE_ID);

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
            return dt;
        }


        // GET ALL Brandding Reports

        internal DataTable GET_BRANDDING_REPORT(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
        {
            db_connection2();
            try
            {

                cmd = new SqlCommand("GET_RPT_BRANDDING", con2);
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

        internal DataTable SAVE_BOARD_REMOVE(string TASK, string BOARD_ID, string STATUS, string CREATED_BY)
        {
            db_connection2();
            try
            {

                cmd = new SqlCommand("SAVE_BR_BOARD_REMOVE", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con2;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@BOARD_ID", BOARD_ID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);

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


    }

}