using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace RaintreePoultry
{
    public class CBFShedSurveyMethods : Connection
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;

        class RootShedSurvey
        {
            public List<TCSHEDSURVEYItem> TCSHEDSURVEYItem { get; set; }
        }

        class TCSHEDSURVEYItem
        {
            public int ids { get; set; }
            public int PLANT_ID { get; set; }
            public string PLANT_CODE { get; set; }
            public string FARMER_NAME { get; set; }
            public string ADDRESS { get; set; }
            public string PINCODE { get; set; }
            public string MOBILE_NO { get; set; }
            public string LINE_NAME { get; set; }
            public string AREA { get; set; }
            public double LENGTH { get; set; }
            public double WIDTH { get; set; }
            public double SIDE_HEIGHT { get; set; }
            public double SQ_FT { get; set; }
            public double REGULAR { get; set; }
            public double SUMMER { get; set; }
            public double TURBO_FEEDER_ACT { get; set; }
            public double TURBO_FEEDER_VARIANCE { get; set; }
            public double CHICK_DRINKER_ACT { get; set; }
            public double CHICK_DRINKER_VARIANCE { get; set; }
            public double J_FEEDER_ACT { get; set; }
            public double J_FEEDER_VARIANCE { get; set; }
            public double J_DRINKER_ACT { get; set; }
            public double J_DRINKER_VARIANCE { get; set; }
            public bool DIAMOND_SHAPE { get; set; }
            public bool CHAIN_LINK_PARTITION { get; set; }
            public bool STORAGE_WATER { get; set; }
            public bool FEED_STAND { get; set; }
            public bool LIFTING_CRATE { get; set; }
            public bool FOOT_DIP { get; set; }
            public double STORAGE_WATER_TANK_CAP { get; set; }
            public string SOURCE { get; set; }
            public double PH { get; set; }
            public double HARDNESS { get; set; }
            public int NO_OF_TANKS { get; set; }
            public double TANK_CAPACITY { get; set; }
            public int GAS_BROODER_QTY { get; set; }
            public double NO_OF_CHICK_GUARD { get; set; }
            public double CHICK_GUARD_LENGTH { get; set; }
            public double STD_CHICK_GUARD_QTY { get; set; }
            public double ACTUAL_CHICK_GUARD_QTY { get; set; }
            public string CHARCOAL_TIN { get; set; }
            public string OTHER_BR_ARRANGE { get; set; }
            public double D_FROM_BRANCH_OFFICE { get; set; }
            public double D_FROM_HATCHERY { get; set; }
            public double D_FROM_GULUNCHE_HATCHERY { get; set; }
            public double D_FROM_PIMPALI_HATCHERY { get; set; }
            public int SHED_TYPE { get; set; }
            public string COMPANY_NAME { get; set; }
            public bool CURRENT_BAL_STATUS { get; set; }
            public double STD_PC { get; set; }
            public double ACT_PC { get; set; }
            public double MORT { get; set; }
            public double GC_KG { get; set; }
            public int LAST_DAY_LIFTING_DT { get; set; }
            public double STD_PC1 { get; set; }
            public double ACT_PC1 { get; set; }
            public double MORT1 { get; set; }
            public double GC_KG1 { get; set; }
            public int LAST_DAY_LIFTING_DT1 { get; set; }
            public double LATITUDE { get; set; }
            public double LONGITUDE { get; set; }
            public bool ISAPPROVE { get; set; }
            public string APPROVE_BY { get; set; }
            public string APPROVE_DATE { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_DATE { get; set; }
            public string MODIFIED_BY { get; set; }
            public string MODIFIED_DATE { get; set; }
            public bool ISDELETED { get; set; }
            public string SQLITE_ID { get; set; }
        }
        // {"TCSHEDSURVEYItem":[{"ACT_PC":0.0,"ACT_PC1":0.0,"ACTUAL_CHICK_GUARD_QTY":-5.98,"ADDRESS":"PQR","APPROVE_BY":"","APPROVE_DATE":"","AREA":"asd","CHAIN_LINK_PARTITION":false,"CHARCOAL_TIN":"asd","CHICK_DRINKER_ACT":6.0,"CHICK_DRINKER_VARIANCE":3.0,"CHICK_GUARD_LENGTH":6.0,"COMPANY_NAME":"","CREATED_BY":"10001","CREATED_DATE":"1676442867750","CURRENT_BAL_STATUS":false,"D_FROM_BRANCH_OFFICE":2.0,"D_FROM_GULUNCHE_HATCHERY":5.0,"D_FROM_HATCHERY":6.0,"D_FROM_PIMPALI_HATCHERY":8.0,"DIAMOND_SHAPE":true,"FARMER_NAME":"XYZ","FEED_STAND":false,"FOOT_DIP":false,"GAS_BROODER_QTY":5,"GC_KG":0.0,"GC_KG1":0.0,"HARDNESS":60.0,"ID":0,"ISAPPROVE":false,"ISDELETED":false,"ids":1,"J_DRINKER_ACT":5.0,"J_DRINKER_VARIANCE":9.0,"J_FEEDER_ACT":5.0,"J_FEEDER_VARIANCE":6.0,"LAST_DAY_LIFTING_DT":0,"LAST_DAY_LIFTING_DT1":0,"LATITUDE":18.1638395,"LENGTH":10.0,"LIFTING_CRATE":false,"LINE_NAME":"qwe","LONGITUDE":74.589398,"MOBILE_NO":"1245789553","MODIFIED_BY":"","MODIFIED_DATE":"","MORT":0.0,"MORT1":0.0,"NO_OF_CHICK_GUARD":5.0,"NO_OF_TANKS":5,"OTHER_BR_ARRANGE":"lmn","PH":25.0,"PINCODE":"143102","PLANT_CODE":"PP01","PLANT_ID":0,"REGULAR":750.0,"SHED_TYPE":2,"SIDE_HEIGHT":25.0,"SOURCE":"poi","SQ_FT":900.0,"SQLITE_ID":"1676442867751","STD_CHICK_GUARD_QTY":0.02,"STD_PC":0.0,"STD_PC1":0.0,"STORAGE_WATER":true,"STORAGE_WATER_TANK_CAP":500.0,"SUMMER":692.31,"TANK_CAPACITY":6.0,"TURBO_FEEDER_ACT":1.0,"TURBO_FEEDER_VARIANCE":2.0,"WIDTH":90.0}]}
        internal DataTable SAVE_SHED_SURVEY(string TASK, string DATA)
        {
            RootShedSurvey mdata = JsonConvert.DeserializeObject<RootShedSurvey>(DATA);  //Convert json to poco

            //HEADER 
            for (int i = 0; i < mdata.TCSHEDSURVEYItem.Count(); i++)
            {
                try
                {
                    db_connection2();

                    cmd = new SqlCommand("SAVE_T_SHED_SURVEY", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TASK", TASK);
                    cmd.Parameters.AddWithValue("@PLANT_ID", mdata.TCSHEDSURVEYItem[i].PLANT_ID);
                    cmd.Parameters.AddWithValue("@PLANT_CODE", mdata.TCSHEDSURVEYItem[i].PLANT_CODE);
                    cmd.Parameters.AddWithValue("@FARMER_NAME", mdata.TCSHEDSURVEYItem[i].FARMER_NAME);
                    cmd.Parameters.AddWithValue("@ADDRESS", mdata.TCSHEDSURVEYItem[i].ADDRESS);
                    cmd.Parameters.AddWithValue("@PINCODE", mdata.TCSHEDSURVEYItem[i].PINCODE);
                    cmd.Parameters.AddWithValue("@MOBILE_NO", mdata.TCSHEDSURVEYItem[i].MOBILE_NO);
                    cmd.Parameters.AddWithValue("@LINE_NAME", mdata.TCSHEDSURVEYItem[i].LINE_NAME);
                    cmd.Parameters.AddWithValue("@AREA", mdata.TCSHEDSURVEYItem[i].AREA);
                    cmd.Parameters.AddWithValue("@LENGTH", mdata.TCSHEDSURVEYItem[i].LENGTH);
                    cmd.Parameters.AddWithValue("@WIDTH", mdata.TCSHEDSURVEYItem[i].WIDTH);
                    cmd.Parameters.AddWithValue("@SIDE_HEIGHT", mdata.TCSHEDSURVEYItem[i].SIDE_HEIGHT);
                    cmd.Parameters.AddWithValue("@SQ_FT", mdata.TCSHEDSURVEYItem[i].SQ_FT);
                    cmd.Parameters.AddWithValue("@REGULAR", mdata.TCSHEDSURVEYItem[i].REGULAR);
                    cmd.Parameters.AddWithValue("@SUMMER", mdata.TCSHEDSURVEYItem[i].SUMMER);
                    cmd.Parameters.AddWithValue("@TURBO_FEEDER_ACT", mdata.TCSHEDSURVEYItem[i].TURBO_FEEDER_ACT);
                    cmd.Parameters.AddWithValue("@TURBO_FEEDER_VARIANCE", mdata.TCSHEDSURVEYItem[i].TURBO_FEEDER_VARIANCE);
                    cmd.Parameters.AddWithValue("@CHICK_DRINKER_ACT", mdata.TCSHEDSURVEYItem[i].CHICK_DRINKER_ACT);
                    cmd.Parameters.AddWithValue("@CHICK_DRINKER_VARIANCE", mdata.TCSHEDSURVEYItem[i].CHICK_DRINKER_VARIANCE);
                    cmd.Parameters.AddWithValue("@J_FEEDER_ACT", mdata.TCSHEDSURVEYItem[i].J_FEEDER_ACT);
                    cmd.Parameters.AddWithValue("@J_FEEDER_VARIANCE", mdata.TCSHEDSURVEYItem[i].J_FEEDER_VARIANCE);
                    cmd.Parameters.AddWithValue("@J_DRINKER_ACT", mdata.TCSHEDSURVEYItem[i].J_DRINKER_ACT);
                    cmd.Parameters.AddWithValue("@J_DRINKER_VARIANCE", mdata.TCSHEDSURVEYItem[i].J_DRINKER_VARIANCE);
                    cmd.Parameters.AddWithValue("@DIAMOND_SHAPE", mdata.TCSHEDSURVEYItem[i].DIAMOND_SHAPE);
                    cmd.Parameters.AddWithValue("@CHAIN_LINK_PARTITION", mdata.TCSHEDSURVEYItem[i].CHAIN_LINK_PARTITION);
                    cmd.Parameters.AddWithValue("@STORAGE_WATER", mdata.TCSHEDSURVEYItem[i].STORAGE_WATER);
                    cmd.Parameters.AddWithValue("@FEED_STAND", mdata.TCSHEDSURVEYItem[i].FEED_STAND);
                    cmd.Parameters.AddWithValue("@LIFTING_CRATE", mdata.TCSHEDSURVEYItem[i].LIFTING_CRATE);
                    cmd.Parameters.AddWithValue("@FOOT_DIP", mdata.TCSHEDSURVEYItem[i].FOOT_DIP);
                    cmd.Parameters.AddWithValue("@STORAGE_WATER_TANK_CAP", mdata.TCSHEDSURVEYItem[i].STORAGE_WATER_TANK_CAP);
                    cmd.Parameters.AddWithValue("@SOURCE", mdata.TCSHEDSURVEYItem[i].SOURCE);
                    cmd.Parameters.AddWithValue("@PH", mdata.TCSHEDSURVEYItem[i].PH);
                    cmd.Parameters.AddWithValue("@HARDNESS", mdata.TCSHEDSURVEYItem[i].HARDNESS);
                    cmd.Parameters.AddWithValue("@NO_OF_TANKS", mdata.TCSHEDSURVEYItem[i].NO_OF_TANKS);
                    cmd.Parameters.AddWithValue("@TANK_CAPACITY", mdata.TCSHEDSURVEYItem[i].TANK_CAPACITY);
                    cmd.Parameters.AddWithValue("@GAS_BROODER_QTY", mdata.TCSHEDSURVEYItem[i].GAS_BROODER_QTY);
                    cmd.Parameters.AddWithValue("@NO_OF_CHICK_GUARD", mdata.TCSHEDSURVEYItem[i].NO_OF_CHICK_GUARD);
                    cmd.Parameters.AddWithValue("@CHICK_GUARD_LENGTH", mdata.TCSHEDSURVEYItem[i].CHICK_GUARD_LENGTH);
                    cmd.Parameters.AddWithValue("@STD_CHICK_GUARD_QTY", mdata.TCSHEDSURVEYItem[i].STD_CHICK_GUARD_QTY);
                    cmd.Parameters.AddWithValue("@ACTUAL_CHICK_GUARD_QTY", mdata.TCSHEDSURVEYItem[i].ACTUAL_CHICK_GUARD_QTY);
                    cmd.Parameters.AddWithValue("@CHARCOAL_TIN", mdata.TCSHEDSURVEYItem[i].CHARCOAL_TIN);
                    cmd.Parameters.AddWithValue("@OTHER_BR_ARRANGE", mdata.TCSHEDSURVEYItem[i].OTHER_BR_ARRANGE);
                    cmd.Parameters.AddWithValue("@D_FROM_BRANCH_OFFICE", mdata.TCSHEDSURVEYItem[i].D_FROM_BRANCH_OFFICE);
                    cmd.Parameters.AddWithValue("@D_FROM_HATCHERY", mdata.TCSHEDSURVEYItem[i].D_FROM_HATCHERY);
                    cmd.Parameters.AddWithValue("@D_FROM_GULUNCHE_HATCHERY", mdata.TCSHEDSURVEYItem[i].D_FROM_GULUNCHE_HATCHERY);
                    cmd.Parameters.AddWithValue("@D_FROM_PIMPALI_HATCHERY", mdata.TCSHEDSURVEYItem[i].D_FROM_PIMPALI_HATCHERY);
                    cmd.Parameters.AddWithValue("@SHED_TYPE", mdata.TCSHEDSURVEYItem[i].SHED_TYPE);
                    cmd.Parameters.AddWithValue("@COMPANY_NAME", mdata.TCSHEDSURVEYItem[i].COMPANY_NAME);
                    cmd.Parameters.AddWithValue("@CURRENT_BAL_STATUS", mdata.TCSHEDSURVEYItem[i].CURRENT_BAL_STATUS);
                    cmd.Parameters.AddWithValue("@STD_PC", mdata.TCSHEDSURVEYItem[i].STD_PC);
                    cmd.Parameters.AddWithValue("@ACT_PC", mdata.TCSHEDSURVEYItem[i].ACT_PC);
                    cmd.Parameters.AddWithValue("@MORT", mdata.TCSHEDSURVEYItem[i].MORT);
                    cmd.Parameters.AddWithValue("@GC_KG", mdata.TCSHEDSURVEYItem[i].GC_KG);
                    cmd.Parameters.AddWithValue("@LAST_DAY_LIFTING_DT", mdata.TCSHEDSURVEYItem[i].LAST_DAY_LIFTING_DT);
                    cmd.Parameters.AddWithValue("@STD_PC1", mdata.TCSHEDSURVEYItem[i].STD_PC1);
                    cmd.Parameters.AddWithValue("@ACT_PC1", mdata.TCSHEDSURVEYItem[i].ACT_PC1);
                    cmd.Parameters.AddWithValue("@MORT1", mdata.TCSHEDSURVEYItem[i].MORT1);
                    cmd.Parameters.AddWithValue("@GC_KG1", mdata.TCSHEDSURVEYItem[i].GC_KG1);
                    cmd.Parameters.AddWithValue("@LAST_DAY_LIFTING_DT1", mdata.TCSHEDSURVEYItem[i].LAST_DAY_LIFTING_DT1);
                    cmd.Parameters.AddWithValue("@LATITUDE", mdata.TCSHEDSURVEYItem[i].LATITUDE);
                    cmd.Parameters.AddWithValue("@LONGITUDE", mdata.TCSHEDSURVEYItem[i].LONGITUDE);
                    cmd.Parameters.AddWithValue("@ISAPPROVE", mdata.TCSHEDSURVEYItem[i].ISAPPROVE);
                    cmd.Parameters.AddWithValue("@APPROVE_BY", mdata.TCSHEDSURVEYItem[i].APPROVE_BY);
                    cmd.Parameters.AddWithValue("@APPROVE_DATE", mdata.TCSHEDSURVEYItem[i].APPROVE_DATE);
                    cmd.Parameters.AddWithValue("@CREATED_BY", mdata.TCSHEDSURVEYItem[i].CREATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_DATE", mdata.TCSHEDSURVEYItem[i].CREATED_DATE);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", mdata.TCSHEDSURVEYItem[i].MODIFIED_BY);
                    cmd.Parameters.AddWithValue("@MODIFIED_DATE", mdata.TCSHEDSURVEYItem[i].MODIFIED_DATE);
                    cmd.Parameters.AddWithValue("@ISDELETED", mdata.TCSHEDSURVEYItem[i].ISDELETED);
                    cmd.Parameters.AddWithValue("@SQLITE_ID", mdata.TCSHEDSURVEYItem[i].SQLITE_ID);

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


        internal DataTable APPROVE_SHED_SURVEY(string TASK, string ID, string APPROVE_BY, string APPROVE_DATE)
        {
            try
            {
                db_connection2();

                cmd = new SqlCommand("UPD_SHED_SURVEY", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@APPROVE_BY", APPROVE_BY);
                cmd.Parameters.AddWithValue("@APPROVE_DATE", APPROVE_DATE);

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
            return dt;
        }

    }
}