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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace RaintreePoultry
{
    /// <summary> A
    /// Summary description for RFC_DATA
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RFC_DATA : System.Web.Services.WebService
    {
        RFC_DATA_Mehods methods = new RFC_DATA_Mehods();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataSet ds = new DataSet();
        Methods method = new Methods();

        [WebMethod]
        public void GET_PARENT_BATCH_DETAILS_RFC(DateTime DATE)
        {
            
            string SAP_IP_ADDRESS = "0";
            //SAP_IP_ADDRESS = methods.GET_SAP_DETAILS("GET_SAP_DETAILS", "");
            //if (SAP_IP_ADDRESS.ToString().Trim().Equals("0"))
            //{
            //    this.Context.Response.Write("GET SAP DETAILS FAILD");
            //    return;
            //}

            //RFC CALL
           // RfcConfigParameters RFC = new RfcConfigParameters();
  //Development Server
            //RFC.Add(RfcConfigParameters.Name, "DEV_Baramati_agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "EV");
            //RFC.Add(RfcConfigParameters.Client, "400");
            //RFC.Add(RfcConfigParameters.User, "BATCH1");
            //RFC.Add(RfcConfigParameters.Password, "123456");
  //Development Server SAP
            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-GSAGAR");
            //RFC.Add(RfcConfigParameters.Password, "Agro#4321");
//Quality Server
            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-DGUNESH");
            //RFC.Add(RfcConfigParameters.Password, "QASagro#987");


            dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, dt.Rows[0]["SAP_IP"].ToString());
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP= RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZBRD_BTCH_DTL");
            FUNCTION.SetValue("ZDATE", DATE);
            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }      
            var msg = FUNCTION.GetTable("LT_DATA");
            dt = DataTable(msg);
            var DATA = GetJson(dt);
           // var DATA = "[{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0002\",\"HOUSE_SH_CODE\":\"GL03\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"760.000\",\"M_QUANTITY\":\"380.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0001\",\"HOUSE_SH_CODE\":\"GL02\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"800.000\",\"M_QUANTITY\":\"700.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0001\",\"HOUSE_SH_CODE\":\"GL01\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"800.000\",\"M_QUANTITY\":\"700.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0002\",\"HOUSE_SH_CODE\":\"GL02\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"760.000\",\"M_QUANTITY\":\"380.000\"}]";
           // HttpContext.Current.Response.Write(ss);
            dt = methods.GET_PARENT_BATCH_DETAILS_RFC(DATA);
            writeOutput(dt);

        }


        [WebMethod]
        public void GET_PARENT_BATCH_DETAILS_TEMP_RFC(DateTime DATE)
        {

            string SAP_IP_ADDRESS = "0";
            dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, dt.Rows[0]["SAP_IP"].ToString());
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "400");
            //RFC.Add(RfcConfigParameters.User, "FI-GSAGAR");
            //RFC.Add(RfcConfigParameters.Password, "DEVagro#9876");
        //    RFC.Add(RfcConfigParameters.Password, "Agro#987");

            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-gsagar");
            //RFC.Add(RfcConfigParameters.Password, "Quality#987");


            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZBRD_BATCH_CREATE");
            FUNCTION.SetValue("ZDATE", DATE);
            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("LT_DATA");
            dt = DataTable(msg);
            var DATA = GetJson(dt);
            // var DATA = "[{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0002\",\"HOUSE_SH_CODE\":\"GL03\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"760.000\",\"M_QUANTITY\":\"380.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0001\",\"HOUSE_SH_CODE\":\"GL02\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"800.000\",\"M_QUANTITY\":\"700.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0001\",\"HOUSE_SH_CODE\":\"GL01\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"800.000\",\"M_QUANTITY\":\"700.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0002\",\"HOUSE_SH_CODE\":\"GL02\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"760.000\",\"M_QUANTITY\":\"380.000\"}]";
            // HttpContext.Current.Response.Write(ss);
            dt = methods.GET_PARENT_BATCH_DETAILS_RFC(DATA);
            writeOutput(dt);

        }





        [WebMethod]
        public void GET_PARENT_BATCH_DETAILS_SHED_RFC(DateTime DATE)
        {

            string SAP_IP_ADDRESS = "0";
            dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            //RFC.Add(RfcConfigParameters.Name, "PRD");
            //RFC.Add(RfcConfigParameters.AppServerHost, dt.Rows[0]["SAP_IP"].ToString());
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "PRD");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "400");
            //RFC.Add(RfcConfigParameters.User, "FI-GSAGAR");
            //RFC.Add(RfcConfigParameters.Password, "DEVagro#9876");
            //    RFC.Add(RfcConfigParameters.Password, "Agro#987");

            RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "QAS");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");


            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZRFC_STS_TRANS");
            FUNCTION.SetValue("ZDATE", DATE);
            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("ZBRD_O_STS_TRANS ");
            var msg1 = FUNCTION.GetTable("ZBRD_I_STS_TRANS ");
            dt = DataTable(msg);
            dt1 = DataTable(msg1);
            var DATA = GetJson(dt);
            // var DATA = "[{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0002\",\"HOUSE_SH_CODE\":\"GL03\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"760.000\",\"M_QUANTITY\":\"380.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0001\",\"HOUSE_SH_CODE\":\"GL02\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"800.000\",\"M_QUANTITY\":\"700.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0001\",\"HOUSE_SH_CODE\":\"GL01\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"800.000\",\"M_QUANTITY\":\"700.000\"},{\"LOC_CODE\":\"PC01\",\"FLOCK\":\"SD0002\",\"HOUSE_SH_CODE\":\"GL02\",\"FEMALE_CODE\":\"BAL101\",\"PLACMNT_DATE\":\"2022-06-03\",\"MALE_CODE\":\"BAL815\",\"F_QUANTITY\":\"760.000\",\"M_QUANTITY\":\"380.000\"}]";
            // HttpContext.Current.Response.Write(ss);
          //  dt = methods.GET_PARENT_BATCH_DETAILS_RFC(DATA);
            writeOutput(dt);

        }


        [WebMethod]
        public void GET_DAILY_BIRD_STOCK_RFC(string FLOCK_NO,string PLANT_CODE,string SHED_CODE)
        {

            string SAP_IP_ADDRESS = "0";
            dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, dt.Rows[0]["SAP_IP"].ToString());
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "LOGICALDNA-1");
            //RFC.Add(RfcConfigParameters.Password, "Welcome#123");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZRFC_BREEDER_DAILY_STOCK");    
            FUNCTION.SetValue("IM_FLOCK", FLOCK_NO);
            FUNCTION.SetValue("IM_LOC_CODE", PLANT_CODE);
            FUNCTION.SetValue("IM_SHED", SHED_CODE);
            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("IT_BATCh_DTL");
            var msg1 = FUNCTION.GetTable("IT_FEED_STOCK");
            dt = DataTable(msg);
            dt1 = DataTable(msg1);
            var DATA = GetJson(dt);

            this.Context.Response.Write("{\"DAILY_BIRD_STOCK_RFC\":[");
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            ds.Tables[0].TableName = "IT_BATCH_DTL";
            ds.Tables[1].TableName = "IT_FEED_STOCK";
            writeOutput(ds);
            this.Context.Response.Write("]}");
            //  this.Context.Response.Write("{");
            //writeOutput(dt);
           // this.Context.Response.Write("}");

        }



        //GET BIRD STOCK SHED WISE AGINST FLOCK NO Date-14-07-2023
        [WebMethod]
        public void GET_DAILY_BIRD_STOCK_FLOCK_WISE_RFC(string FLOCK_NO)
        {

            string SAP_IP_ADDRESS = "0";
           // dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            //RFC.Add(RfcConfigParameters.Name, "NEW DEV");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-gsagar");
            //RFC.Add(RfcConfigParameters.Password, "Agro#987");


            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "LOGICALDNA-1");
            //RFC.Add(RfcConfigParameters.Password, "Welcome#123");

            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZRFC_BRD_SALE");
            FUNCTION.SetValue("IM_FLOCK", FLOCK_NO);
            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("IT_BATCh_DTL");
           // var msg_total = FUNCTION.GetTable("IT_TOTAL");
            dt = DataTable(msg);
           // dt = DataTable(msg_total);
            var DATA = GetJson(dt);
            //  this.Context.Response.Write("{");
            writeOutput(dt);
            // this.Context.Response.Write("}");

        }



        /// <summary>
        /// DAILY PARENT MORTALITY PUSH TIO SAP
        /// </summary>

        [WebMethod]
        public void PUSH_DAILY_P_MORTALITY_RFC()
        {
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_P_MORTALITY_DETAILS","");
            string SAP_IP_ADDRESS = "0";
           // dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZRFC_MORT_DETAILS");
            IRfcTable TABLE=null;
            TABLE = FUNCTION.GetTable("T_ZCONF_AND_MORT");
            TABLE.Insert();

               foreach(DataRow row in dt.Rows)
            {

                TABLE.SetValue("MANDT", "900");
                TABLE.SetValue("REF_NO", row["ID"].ToString());
                TABLE.SetValue("AUFNR", "");
                TABLE.SetValue("XMNGA", row["MORTALITY"].ToString());
                TABLE.SetValue("HMATNR", row["ITEM_CODE"].ToString());
                TABLE.SetValue("LMNGA", "0");
                TABLE.SetValue("BUDAT", row["DATE"]);
                TABLE.SetValue("LGORT",  row["SHED_CODE"].ToString());
                TABLE.SetValue("AVG_WT", row["ABW"].ToString());
                TABLE.SetValue("WERKS", row["PLANT_CODE"].ToString());
                TABLE.SetValue("CHARG", row["FLOCK_NO"].ToString());
                TABLE.SetValue("FLAG", "X");
                TABLE.SetValue("MESSAGE", "0");
                 TABLE.SetValue("STATUS", "N");
                 TABLE.SetValue("ERNAM", row["SAP_CODE"].ToString());
                 TABLE.SetValue("BDC_STAT", "Y");
                 TABLE.SetValue("F_M_UNIFORM", row["UNIFORMITY"].ToString());
                try
                {
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
                dt = methods.UPD_P_DAILY_TRANS("UDP_P_MORTALITY_DETAILS", row["ID"].ToString());
            }
              
            writeOutput(dt);

        }



        [WebMethod]
        public void PUSH_DAILY_P_MORTALITY_REASON_RFC()
        {
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_P_MORTALITY_REASON_DETAILS", "");
            string SAP_IP_ADDRESS = "0";


            //dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZRFC_MORT");
            IRfcTable TABLE = null;
            TABLE = FUNCTION.GetTable("T_MORT");
            TABLE.Insert();

            foreach (DataRow row in dt.Rows)
            {

                TABLE.SetValue("REF_NO", row["ID"].ToString());
                TABLE.SetValue("PLANT", row["PLANT_CODE"].ToString());
                TABLE.SetValue("SHED", row["SHED_CODE"].ToString());
                TABLE.SetValue("FLOCK", row["FLOCK_NO"].ToString());
                TABLE.SetValue("ZDATE", row["DATE"]);
                TABLE.SetValue("ITEM_CODE", row["ITEM_CODE"].ToString());
                TABLE.SetValue("BIRD_TYPE", row["BIRD_TYPE"].ToString());
                TABLE.SetValue("MORT_ID", row["MORTALITY_REASON_ID"].ToString());
                TABLE.SetValue("MORT_QTY", Convert.ToInt32(row["QTY"]));
             //   TABLE.SetValue("ERNAM", row["SAP_CODE"].ToString());

                try
                {
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
                dt = methods.UPD_P_DAILY_TRANS("UDP_P_MORTALITY_REASON_DETAILS", row["ID"].ToString());
            }

            writeOutput(dt);

        }

        //FEED CONSUMPTION
        [WebMethod]
        public void PUSH_DAILY_P_FEED_CONSUMPTION_RFC()
        {
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_P_FEED_CONSUMPTION_DETAILS", "");
            string SAP_IP_ADDRESS = "0";
          //  dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");
            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
          //  FUNCTION = RFCREP.CreateFunction("YRFC_PARENT_FARM");
            FUNCTION = RFCREP.CreateFunction("YRFC_PARENT_FARM_ZPOULTRY");
            IRfcTable TABLE = null;
            TABLE = FUNCTION.GetTable("T_PARENT_FARM");
            TABLE.Insert();

            foreach (DataRow row in dt.Rows)
            {

                TABLE.SetValue("DOCNO", row["ID"].ToString());
                TABLE.SetValue("WERKS", row["PLANT_CODE"].ToString());
                TABLE.SetValue("ENTRY_DATE", row["DATE"]);
                TABLE.SetValue("LGORT", row["SHED_CODE"].ToString());
              //  TABLE.SetValue("TYPE", row["BIRD_TYPE"].ToString());//Brooding & laying
                TABLE.SetValue("TYPE", row["PLANT_TYPE_ID"].ToString());
                TABLE.SetValue("CHARG", row["FLOCK_NO"].ToString());
                TABLE.SetValue("CMATNR", row["MATERIAL_CODE"].ToString());
                TABLE.SetValue("CQTY", row["QTY"].ToString());
                TABLE.SetValue("CUOM", row["UOM"].ToString());
                TABLE.SetValue("HMATNR", row["ITEM_CODE"].ToString());
                TABLE.SetValue("CLGORT","0");
                TABLE.SetValue("ERSDA", row["DATE"]);
                TABLE.SetValue("ERNAM", row["SAP_CODE"].ToString());
                TABLE.SetValue("BDC_STAT", "Y");
             
               // TABLE.SetValue("AVG_WT", Convert.ToInt32(row["ABW"]));

                try
                {
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
                dt = methods.UPD_P_DAILY_TRANS("UDP_P_FEED_CONSUMPTION_DETAILS", row["ID"].ToString());
            }

            writeOutput(dt);

        }


        //PRODUCTION
        [WebMethod]
        public void PUSH_DAILY_P_PRODUCTION_RFC()
        {
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_P_PRODUCTION_DETAILS", "");
            string SAP_IP_ADDRESS = "0";
           // dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZRFC_GOODS_RECEIPT");
            IRfcTable TABLE = null;
            TABLE = FUNCTION.GetTable("T_ZGOODS_RECEIPT");
            TABLE.Insert();

            foreach (DataRow row in dt.Rows)
            {

                TABLE.SetValue("MANDT", "900");
                TABLE.SetValue("REF_NO", row["ID"].ToString());
                TABLE.SetValue("AUFNR", "0");
                //TABLE.SetValue("HMATNR", row["BIRD_TYPE"].ToString());
                TABLE.SetValue("HMATNR", row["ITEM_CODE"].ToString());
                TABLE.SetValue("BUDAT", row["DATE"]);
                TABLE.SetValue("BLDAT", row["DATE"]);
                TABLE.SetValue("AUART", "0");
                TABLE.SetValue("WERKS", row["PLANT_CODE"].ToString());
                TABLE.SetValue("LGORT", row["SHED_CODE"].ToString());
                TABLE.SetValue("CHARG", row["FLOCK_NO"].ToString());
                TABLE.SetValue("ERFME", "0");
                TABLE.SetValue("ZACT_QTY", row["ACTUAL_QTY"].ToString());
                TABLE.SetValue("ZCULL_BRD", row["CULL_BIRDS"].ToString());
                TABLE.SetValue("ZSICK_BRD", row["SICK_BIRDS"].ToString());
                TABLE.SetValue("ZHAT_EGG", row["HATCHING_EGGS"].ToString());
                TABLE.SetValue("ZCUM_EGG", row["COMMERCIAL_EGGS"].ToString());
                TABLE.SetValue("ZJUMBO_EGG", row["JUMBO_EGGS"].ToString());
                TABLE.SetValue("ZPULL_EGG", row["PULLET_EGGS"].ToString());
                TABLE.SetValue("ZCRK_EGG", row["CRACK_EGGS"].ToString());
                TABLE.SetValue("MESSAGE", "0");
                TABLE.SetValue("STATUS", "N");
                TABLE.SetValue("SUPERVISOR", row["SAP_CODE"]);
                TABLE.SetValue("AVG_EGGS", row["EGGS_WEIGHT"].ToString());
                TABLE.SetValue("BDC_STATUS", "N");
                try
                {
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
                dt = methods.UPD_P_DAILY_TRANS("UDP_P_PRODUCTION_DETAILS", row["ID"].ToString());
            }

            writeOutput(dt);

        }

        [WebMethod]
        public void CUSTOMER_BALANCE_RFC(string CUSTOMER_CODE)
        {
            try
            {

                string SAP_IPAddress = "0";

                dt = method.SAPConnection("GET_SAP_IP");

                RfcConfigParameters rfc = new RfcConfigParameters();
                rfc.Add(RfcConfigParameters.Name, "PRD");
                rfc.Add(RfcConfigParameters.AppServerHost, dt.Rows[0]["SAP_IP"].ToString());
                rfc.Add(RfcConfigParameters.SystemNumber, "00");
                rfc.Add(RfcConfigParameters.SystemID, "PRD");
                rfc.Add(RfcConfigParameters.Client, "900");
                rfc.Add(RfcConfigParameters.User, "batch1");
                rfc.Add(RfcConfigParameters.Password, "123456");


    

                RfcDestination rfcDest = RfcDestinationManager.GetDestination(rfc);
                var rfcRep = rfcDest.Repository;
                string fmt = "0000000000";
                string k1 = CUSTOMER_CODE.PadLeft(10, '0');
                IRfcFunction function1 = null;
                function1 = rfcRep.CreateFunction("YCBF_CUST_BAL");
                function1.SetValue("I_KUNNR", k1);
                function1.SetValue("I_BUKRS", "BAPL");
                function1.Invoke(rfcDest);
                function1.GetValue("BALANCE");
                object balance = function1.GetString("BALANCE");
                Double a = Convert.ToDouble(balance);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write("{\"Balance\" : \""+a);
//                this.Context.Response.Write(a);
                this.Context.Response.Write("\"}");
                //this.Context.Response.Write("{ \"Balance\" : [{\"Amount\" : \"");
                //this.Context.Response.Write(a);
                //this.Context.Response.Write("\"}]}");
               // return a;
            }
            catch (Exception ex)
            {
            }
            //return "0.0";

        }


        public static DataTable DataTable(IRfcTable rfcTable)
        {
            var dataTable = new DataTable();

            for (int element = 0; element < rfcTable.ElementCount; element++)
            {
                RfcElementMetadata metadata = rfcTable.GetElementMetadata(element);
                dataTable.Columns.Add(metadata.Name);
            }

            foreach (IRfcStructure row in rfcTable)
            {
                DataRow newRow = dataTable.NewRow();
                for (int element = 0; element < rfcTable.ElementCount; element++)
                {
                    RfcElementMetadata metadata = rfcTable.GetElementMetadata(element);
                    newRow[metadata.Name] = row.GetString(metadata.Name);
                }
                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }


        //CBF Bird Material Batch Details

        [WebMethod]
        public void GET_CBF_BATCH_DETAILS_RFC(DateTime DATE)
        {

            string SAP_IP_ADDRESS = "0";
            //SAP_IP_ADDRESS = methods.GET_SAP_DETAILS("GET_SAP_DETAILS", "");
            //if (SAP_IP_ADDRESS.ToString().Trim().Equals("0"))
            //{
            //    this.Context.Response.Write("GET SAP DETAILS FAILD");
            //    return;
            //}a

            //RFC CALL
            RfcConfigParameters RFC = new RfcConfigParameters();

            //RFC.Add(RfcConfigParameters.Name, "NEW DEV");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-gsagar");
            //RFC.Add(RfcConfigParameters.Password, "Agro#987");

            RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "QAS");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "FI-GSAGAR");
            RFC.Add(RfcConfigParameters.Password, "PRDagro#9888");


            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZCBF_BTCH_DTL");
            FUNCTION.SetValue("ZDATE", DATE);
            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("LT_DATA");
            dt = DataTable(msg);
            var DATA = GetJson(dt);
            dt = methods.GET_CBF_BATCH_DETAILS_RFC(DATA);
            writeOutput(dt);

        }

        //GET_CBF_BIRD_STOCK
        [WebMethod]
        public void GET_DAILY_CBF_BIRD_STOCK_RFC( string FLOCK_NO, string PLANT_CODE, string SHED_CODE)
        {

            string SAP_IP_ADDRESS = "0";
            //SAP_IP_ADDRESS = methods.GET_SAP_DETAILS("GET_SAP_DETAILS", "");
            //if (SAP_IP_ADDRESS.ToString().Trim().Equals("0"))
            //{
            //    this.Context.Response.Write("GET SAP DETAILS FAILD");
            //    return;
            //}

            //RFC CALL
            RfcConfigParameters RFC = new RfcConfigParameters();
            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-gsagar");
            //RFC.Add(RfcConfigParameters.Password, "Agro#987");

            RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "QAS");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "FI-GSAGAR");
            RFC.Add(RfcConfigParameters.Password, "PRDagro#9888");


            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZCBF_DAILY_TRAN");
            FUNCTION.SetValue("LV_BATCH", FLOCK_NO);
            FUNCTION.SetValue("LV_FARM_CODE", SHED_CODE);
            FUNCTION.SetValue("LV_LOCATION", PLANT_CODE);
            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("IT_DATA");
            dt = DataTable(msg);
            var DATA = GetJson(dt);
            //  this.Context.Response.Write("{");
            writeOutput(dt);
            // this.Context.Response.Write("}");

       
        }


        //GET_CBF_TRANS
        [WebMethod]
        public void PUSH_DAILY_CBF_TRANS_RFC()
        {
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_C_DAILY_TRANS", "");
            string SAP_IP_ADDRESS = "0";
            //SAP_IP_ADDRESS = methods.GET_SAP_DETAILS("GET_SAP_DETAILS", "");
            //if (SAP_IP_ADDRESS.ToString().Trim().Equals("0"))
            //{
            //    this.Context.Response.Write("GET SAP DETAILS FAILD");
            //    return;
            //}

            //RFC CALL
            RfcConfigParameters RFC = new RfcConfigParameters();
            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-gsagar");
            //RFC.Add(RfcConfigParameters.Password, "Agro#987");

            RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "QAS");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "FI-GSAGAR");
            RFC.Add(RfcConfigParameters.Password, "PRDagro#9888");

        //    RFC.Add(RfcConfigParameters.User, "LOGICALDNA-1");
          //  RFC.Add(RfcConfigParameters.Password, "Welcome#123");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            //FUNCTION = RFCREP.CreateFunction("ZCBF_MOB");
            FUNCTION = RFCREP.CreateFunction("Z_RFC_DAILY_TRANS");
            IRfcTable TABLE = null;
            // TABLE = FUNCTION.GetTable("ZCBF_DATA");  //2023-08-11 change SAP table
            TABLE = FUNCTION.GetTable("T1_ZCBF_DAILY_DATA");
            TABLE.Insert();
            Double a = 0.0;
            foreach (DataRow row in dt.Rows)
            {
                a = Convert.ToDouble(row["AVG_WT"]);
                TABLE.SetValue("REF_NO", row["ID"].ToString());
                TABLE.SetValue("WERKS", row["PLANT_CODE"].ToString());
                TABLE.SetValue("LGORT", row["STL_CODE"]);
                TABLE.SetValue("MORT_QTY", row["MORALITY_QTY"].ToString());
                TABLE.SetValue("PRESTR_QTY", row["PRESTR_QTY"].ToString());
                TABLE.SetValue("STR_QTY", row["STR_QTY"].ToString());
                TABLE.SetValue("FIN_QTY", row["FIN_QTY"].ToString());
                TABLE.SetValue("AVG_WT", Convert.ToDouble(row["AVG_WT"]));// row["AVG_WT"].ToString());
                TABLE.SetValue("CREATED_DATE", row["DATE"]);
                TABLE.SetValue("CREATED_BY", row["SAP_CODE"]);
                TABLE.SetValue("FLAG", "0");
                TABLE.SetValue("NAME", row["FNAME"].ToString());
                TABLE.SetValue("GRW_QTY", row["GROW_QTY"].ToString());
                TABLE.SetValue("BATCH", row["FLOCK_NO"].ToString());
                TABLE.SetValue("BDC_STATUS", "N");
                try
                {
                    FUNCTION.Invoke(RFCDEST);
                    dt = methods.UPD_P_DAILY_TRANS("UDP_C_DAILY_DETAILS", row["ID"].ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("" + e);
                }
               
            }

            this.Context.Response.Write("{\"ABW\" : \"" + a);
            this.Context.Response.Write("\"}");
            //  writeOutput(dt);



        }

        // Get Json Form Data Table
        public string GetJson(DataTable dt)
        {
            return JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        }

        public void writeOutput(DataSet ds)
        {
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(ds, Formatting.Indented));
        }


        public void writeOutput(DataTable dt)
        {
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented));
        }


        public void writeOutput(string tableName, DataSet ds)
        {
            ds.Tables[0].TableName = tableName;
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(ds, Formatting.Indented));
        }


        public class Distance
        {
            public string dist { get; set; }
            public string duration { get; set; }
        }

        [WebMethod]
        public void distance(string S_Latitude, string S_Longitude, string D_Latitude, string D_Longitude)
        {

            string dist = methods.GET_DISTANCE(S_Latitude, S_Longitude, D_Latitude, D_Longitude).ToString().Replace(".",".");
            http://192.168.21.19:8069/RaintreePoultry/RFC_DATA.asmx/distance?S_Latitude=18.29188195236133&S_LONGITUDE=74.7544906582893&D_LATITUDE=18.149234151659726&D_LONGITUDE=74.57269651022213
            this.Context.Response.ContentType = "application/json; charset=utf-8";

            HttpContext.Current.Response.Write(dist);
            
        }

        [WebMethod]
        public string TranslateText(string input)
        {
            string translation = "";
            //if (IsConnectedToInternet())
            //{

            try
            {
                // Set the language from/to in the url (or pass it into this function)
                string url = String.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                 "en", "MR", Uri.EscapeUriString(input));
                HttpClient httpClient = new HttpClient();
                string result = httpClient.GetStringAsync(url).Result;
                string[] JsonString = result.Split('[');
                var SortString = "[" + JsonString[3] + "]";
                // Get all json data
                var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(SortString);
                var translationItems = jsonData[0];
                //txtMarathiString.Text = translationItems.ToString();
                translation = translationItems;
                // Return translation
                return translation;
            }
            catch
            {
                return translation;
            }
            //}
            //else
            //{
            //    translation = "";
            //}
            return translation;
        }


        //Logical DNA STOCK SALES RFC
        [WebMethod]
        public void PUSH_DAILY_SALE_STOCK_P_RFC()
        {
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_P_DAILY_SALES_TRANS", "");
            string SAP_IP_ADDRESS = "0";


            //dt = method.SAPConnection("GET_SAP_IP");


            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");


            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

          

            //RFC.Add(RfcConfigParameters.Name  //RFC.Add(RfcConfigParameters.Name, "NEW DEV");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-gsagar");
            //RFC.Add(RfcConfigParameters.Password, "Agro#987");, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-gsagar");
            //RFC.Add(RfcConfigParameters.Password, "Quality#987");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZSALE_BIRD_RFC");
            IRfcTable TABLE = null;
            TABLE = FUNCTION.GetTable("IT_SALE");
            TABLE.Insert();

            foreach (DataRow row in dt.Rows)
            {

                TABLE.SetValue("REF_NUM", row["REF_NO"].ToString());
                TABLE.SetValue("POSNR", row["ITEM_NO"].ToString());
                TABLE.SetValue("STATUS", row["STATUS"].ToString());
                TABLE.SetValue("AUART", row["SALE_DOC_TYPE"].ToString());
                TABLE.SetValue("VKORG", row["SALE_ORG"].ToString());
                TABLE.SetValue("VTWEG", row["DIST_CHL"].ToString());
                TABLE.SetValue("SPART", row["DIV"].ToString());
                TABLE.SetValue("CUST", row["CUSTOMER"].ToString());
                TABLE.SetValue("BSTKD", row["PO_NO"].ToString());
                TABLE.SetValue("BSTDK", row["PO_DATDE"].ToString());
                TABLE.SetValue("BUDAT", row["POSTING_DATE"].ToString());
                TABLE.SetValue("MATNR", row["MATERIAL"].ToString());
                TABLE.SetValue("QNTITY", row["QUQNTITY"].ToString());
                TABLE.SetValue("KWMENG", row["DELI_QTY"].ToString());
                TABLE.SetValue("AWGHT", row["AVERAGE_WEIGHT"].ToString());
                TABLE.SetValue("WERKS", row["PLANT"].ToString());
                TABLE.SetValue("LOC", row["STORAGE_LOC"].ToString());
                TABLE.SetValue("SHED_NAME", row["SHED_NAME"].ToString());
                TABLE.SetValue("CHARG", row["BATCH"].ToString());
                TABLE.SetValue("SALES_DOC_NO", row["OREDR_NO"].ToString());
                TABLE.SetValue("OUT_DEL_NO", row["DELI_NO"].ToString());
                TABLE.SetValue("BILL_DOC_NO", row["INVOICE_NO"].ToString());
                TABLE.SetValue("RATE", row["KG_RATE"].ToString());
                TABLE.SetValue("RATE_PER_BIRD", row["BIRD_RATE"].ToString());
                TABLE.SetValue("AREA", row["Area"].ToString());
                TABLE.SetValue("BIRD_SIZE", row["Bird_size"].ToString());
                TABLE.SetValue("OFFICER_NAME", row["OFFICER_NAME"].ToString());
                TABLE.SetValue("SIZE_POLICY", row["SIZE_POLICY"].ToString());
                TABLE.SetValue("ZONE1", row["ZONE1"].ToString());
                TABLE.SetValue("ACTUAL_DATE", Convert.ToDateTime(row["ACTUAL_DATE"])); //row["ACTUAL_DATE"].ToString());
                TABLE.SetValue("RATE_REMARK", row["RATE_REMARK"].ToString());
                TABLE.SetValue("TRNO", row["TRNO"].ToString());

                // TABLE.SetValue("ERNAM", row["SAP_CODE"].ToString());

                try
                {
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
              ///  dt = methods.UPD_P_DAILY_TRANS("UDP_DAILY_SALE_STOCK_P_RFC", row["ID"].ToString());
            }

            writeOutput(dt);

        }
      


        // Primus Sales RFC
        [WebMethod]
        public void PUSH_DAILY_SALE_P_RFC()
        {
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_P_DAILY_SALES_TRANS", "");
            string SAP_IP_ADDRESS = "0";


            //dt = method.SAPConnection("GET_SAP_IP");


            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456"); 


            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZCL_BIRD_RFC");
            IRfcTable TABLE = null;
            TABLE = FUNCTION.GetTable("IT_VA01");
            TABLE.Insert();

            foreach (DataRow row in dt.Rows)
            {

                TABLE.SetValue("REF_NUM", row["REF_NO"].ToString());
                TABLE.SetValue("POSNR", row["ITEM_NO"].ToString());
                TABLE.SetValue("STATUS", row["STATUS"].ToString());
                TABLE.SetValue("AUART", row["SALE_DOC_TYPE"].ToString());
                TABLE.SetValue("VKORG", row["SALE_ORG"].ToString());
                TABLE.SetValue("VTWEG", row["DIST_CHL"].ToString());
                TABLE.SetValue("SPART", row["DIV"].ToString());
                TABLE.SetValue("CUST", row["CUSTOMER"].ToString());
                TABLE.SetValue("BSTKD", row["PO_NO"].ToString());
                TABLE.SetValue("BSTDK", row["PO_DATDE"].ToString());
                TABLE.SetValue("BUDAT", row["POSTING_DATE"].ToString());
                TABLE.SetValue("MATNR", row["MATERIAL"].ToString());
                TABLE.SetValue("QNTITY", row["QUQNTITY"].ToString());
                TABLE.SetValue("KWMENG", row["DELI_QTY"].ToString());
                TABLE.SetValue("AWGHT", row["AVERAGE_WEIGHT"].ToString());
                TABLE.SetValue("WERKS", row["PLANT"].ToString());
                TABLE.SetValue("LOC", row["STORAGE_LOC"].ToString());
                TABLE.SetValue("SHED_NAME", row["SHED_NAME"].ToString());
                TABLE.SetValue("CHARG", row["BATCH"].ToString());
                TABLE.SetValue("SALES_DOC_NO", row["OREDR_NO"].ToString());
                TABLE.SetValue("OUT_DEL_NO", row["DELI_NO"].ToString());
                TABLE.SetValue("BILL_DOC_NO", row["INVOICE_NO"].ToString());
                TABLE.SetValue("RATE", row["KG_RATE"].ToString());
                TABLE.SetValue("RATE_PER_BIRD", row["BIRD_RATE"].ToString());
                TABLE.SetValue("AREA", row["Area"].ToString());
                TABLE.SetValue("BIRD_SIZE", row["Bird_size"].ToString());
                TABLE.SetValue("OFFICER_NAME", row["OFFICER_NAME"].ToString());
                TABLE.SetValue("SIZE_POLICY", row["SIZE_POLICY"].ToString());
                TABLE.SetValue("ZONE1", row["ZONE1"].ToString());
                TABLE.SetValue("ACTUAL_DATE", Convert.ToDateTime(row["ACTUAL_DATE"])); //row["ACTUAL_DATE"].ToString());
                TABLE.SetValue("RATE_REMARK", row["RATE_REMARK"].ToString());
                TABLE.SetValue("TRNO", row["TRNO"].ToString());

                // TABLE.SetValue("ERNAM", row["SAP_CODE"].ToString());

                try
                {
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
                //dt = methods.UPD_P_DAILY_TRANS("UDP_P_DAILY_SALES_TRANS", row["ID"].ToString());
            }

            writeOutput(dt);

        }

        //ACCOUNT BOOK RFC FOR TESTING
        [WebMethod]
        public void GET_ACCOUNT_BALANCE_RFC(string COMAPNY_CODE, string PLANT_CODE, DateTime FROM_DATE, DateTime TO_DATE)
        {

            string SAP_IP_ADDRESS = "0";
          //  dt = method.SAPConnection("GET_SAP_IP");

            RfcConfigParameters RFC = new RfcConfigParameters();
            //RFC.Add(RfcConfigParameters.Name, "PRD");
            //RFC.Add(RfcConfigParameters.AppServerHost, dt.Rows[0]["SAP_IP"].ToString());
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "PRD");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "DEV");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZFBCJ_RFC");
            FUNCTION.SetValue("I_COMPANY_CODE", COMAPNY_CODE);
            FUNCTION.SetValue("I_CASH_JOURNAL_NO", PLANT_CODE);
            FUNCTION.SetValue("I_FROM_DATE", FROM_DATE);
            FUNCTION.SetValue("I_TO_DATE", TO_DATE);
            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("LT_FINAL");
            dt = DataTable(msg);
            var DATA = GetJson(dt);
            //dt = methods.GET_PARENT_BATCH_DETAILS_RFC(DATA);
            writeOutput(dt);

        }

        //QC BOM RFC FOR CHECKING EXCECUTION TIME
        [WebMethod]
        public void RFCBOM(string DATE)
        {

            RfcConfigParameters rfc = new RfcConfigParameters();
            rfc.Add(RfcConfigParameters.Name, "PRD");
            rfc.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            rfc.Add(RfcConfigParameters.SystemNumber, "00");
            rfc.Add(RfcConfigParameters.SystemID, "PRD");
            rfc.Add(RfcConfigParameters.Client, "900");
            rfc.Add(RfcConfigParameters.User, "batch1");
            rfc.Add(RfcConfigParameters.Password, "123456");

            RfcDestination rfcDest = RfcDestinationManager.GetDestination(rfc);
            RfcRepository rfcRep = rfcDest.Repository;
            IRfcFunction function1 = null;

            function1 = rfcRep.CreateFunction("ZRFC_BATCH_CHECK");
            DateTime s = new DateTime();

            String date = DateTime.Now.ToString("yyyy-MM-dd");
            s = Convert.ToDateTime(date);

            function1.SetValue("LV_DATE", DATE);

            try
            {
                function1.Invoke(rfcDest);
            }                                                                                                                                                                                       
            catch (Exception)
            {

            }
            var msg = function1.GetTable("LT_FINAL");
            dt1 = DataTable(msg);
            var DATA = JsonConvert.SerializeObject(dt1, Formatting.Indented);
            ds = methods.SAV_QC_BOM_RFC("SAVE_FEED_DOC", DATA);
            writeOutput(ds);

        }

        //EGGS RATE PLANT WISE
        [WebMethod]
        public void PUSH_DAILY_EGGS_RATE_P_RFC()
        {
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_P_EGGS_RATE", "");
            string SAP_IP_ADDRESS = "0";

            //dt = method.SAPConnection("GET_SAP_IP");
            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZRATE_MAINTAIN_EGGS");
            IRfcTable TABLE = null;
            ///TABLE = FUNCTION.GetTable("IT_SALE");
           // TABLE.Insert();

            foreach (DataRow row in dt.Rows)
            {

                FUNCTION.SetValue("WERKS", row["PLANT_CODE"].ToString());
                FUNCTION.SetValue("MATERIAL_CODE", row["MATERIAL_CODE"].ToString());
              //FUNCTION.SetValue("MAKTX", row["MATERIAL_NAME"].ToString());
               // FUNCTION.SetValue("FROM_DATE", row["DATE"].ToString());
               // FUNCTION.SetValue("TIME", "");
                FUNCTION.SetValue("I_RATE", row["RATE"].ToString());
                FUNCTION.SetValue("FLAG", "N");
             
                try
                {
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
                dt = methods.UPD_P_DAILY_TRANS("UDP_P_EGGS_RATE", row["EGGS_RATE_ID"].ToString());
            }

            writeOutput(dt);

        }



        //SMART DELIVERY DOC SYNC
        [WebMethod]
        public void GET_FEED_DATA_RFC(string DATE)
        {

            string SAP_IP_ADDRESS = "0";
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_PLANT", "");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZFEED311_RFC");
           

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    var a = row["PLANT_CODE"].ToString();
                    FUNCTION.SetValue("BLDAT", DATE);
                    FUNCTION.SetValue("WERKS", row["PLANT_CODE"].ToString());
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
                var msg = FUNCTION.GetTable("LT_FINAL");
                dt1 = DataTable(msg);
                if(dt1.Rows.Count>0)
                {

                }

               
               
            }
            var DATA = JsonConvert.SerializeObject(dt1, Formatting.Indented);
            dt = methods.SAV_SMART_DELIVERY_DC_RFC("SAVE_FEED_DOC", DATA);
            writeOutput(dt);
            
           

        }

      



        //SMART DELIVERY DOC SYNC
        [WebMethod]
        public void GET_FEED_DATA_PLANT_WISE_RFC(string DATE, string PLANT)
        {

            string SAP_IP_ADDRESS = "0";
            dt = methods.GET_P_DAILY_TRANS_RFC("GET_PLANT_DATE", "");

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZFEED311_RFC");


            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    var a = row["PLANT_CODE"].ToString();
                    FUNCTION.SetValue("BLDAT", row["DATE"]);
                    FUNCTION.SetValue("WERKS", row["PLANT_CODE"].ToString());
                    FUNCTION.Invoke(RFCDEST);
                }
                catch (Exception e)
                {

                }
                var msg = FUNCTION.GetTable("LT_FINAL");
                dt1 = DataTable(msg);
                if (dt1.Rows.Count > 0)
                {

                }

            }
            var DATA = JsonConvert.SerializeObject(dt1, Formatting.Indented);
            dt = methods.SAV_SMART_DELIVERY_DC_RFC("SAVE_FEED_DOC", DATA);
            writeOutput(dt);

        }


        //HATCHERY EGGS DATA STO
        [WebMethod]
        public void GET_HATCHERY_EGGS_RFC(string DATE)
        {

            string SAP_IP_ADDRESS = "0";

            RfcConfigParameters RFC = new RfcConfigParameters();
            //RFC.Add(RfcConfigParameters.Name, "PRD");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "PRD");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "QAS");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZGRN_HATCHING_RFC");

            try
            {

                FUNCTION.SetValue("LV_DATE", DATE);
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("LT_FINAL");
            dt = DataTable(msg);

             var DATA = JsonConvert.SerializeObject(dt, Formatting.Indented);
            dt = methods.SAV_HATCHERY_EGGS_RFC("SAVE_EGGS_STOCK", DATA);
            writeOutput(dt);



        }



        // GET ELICIOUS INVOICE
        [WebMethod]
        public void GET_ELICIOUS_INVOICE_RFC(string DATE)
        {

            string SAP_IP_ADDRESS = "0";

            RfcConfigParameters RFC = new RfcConfigParameters();
            RFC.Add(RfcConfigParameters.Name, "PRD");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.3");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "PRD");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "batch1");
            RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "QAS");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "batch1");
            //RFC.Add(RfcConfigParameters.Password, "123456");

            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZRFC_INVOICE");

            try
            {

                FUNCTION.SetValue("LV_DATE", DATE);
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("IT_INV");
            dt = DataTable(msg);

             var DATA = JsonConvert.SerializeObject(dt, Formatting.Indented);
            dt = methods.SAV_ELICIOUS_INVOICE_RFC("SAVE_FEED_DOC", DATA);
            writeOutput(dt);

        }



        //GET_CBF_BIRD_STOCK
        [WebMethod]
        public void GET_DAILY_CBF_BIRD_STOCK_WITHOUTBATCH_RFC(string PLANT_CODE, string SHED_CODE)
        {

            string SAP_IP_ADDRESS = "0";
            //SAP_IP_ADDRESS = methods.GET_SAP_DETAILS("GET_SAP_DETAILS", "");
            //if (SAP_IP_ADDRESS.ToString().Trim().Equals("0"))
            //{
            //    this.Context.Response.Write("GET SAP DETAILS FAILD");
            //    return;
            //}

            //RFC CALL
            RfcConfigParameters RFC = new RfcConfigParameters();
            //RFC.Add(RfcConfigParameters.Name, "BAL WTS");
            //RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.6");
            //RFC.Add(RfcConfigParameters.SystemNumber, "00");
            //RFC.Add(RfcConfigParameters.SystemID, "DEV");
            //RFC.Add(RfcConfigParameters.Client, "900");
            //RFC.Add(RfcConfigParameters.User, "FI-gsagar");
            //RFC.Add(RfcConfigParameters.Password, "Agro#987");

            RFC.Add(RfcConfigParameters.Name, "QAS_Baramati_Agro");
            RFC.Add(RfcConfigParameters.AppServerHost, "192.168.21.42");
            RFC.Add(RfcConfigParameters.SystemNumber, "00");
            RFC.Add(RfcConfigParameters.SystemID, "QAS");
            RFC.Add(RfcConfigParameters.Client, "900");
            RFC.Add(RfcConfigParameters.User, "FI-GSAGAR");
            RFC.Add(RfcConfigParameters.Password, "PRDagro#9888");


            RfcDestination RFCDEST = RfcDestinationManager.GetDestination(RFC);
            RfcRepository RFCREP = null;
            RFCREP = RFCDEST.Repository;
            IRfcFunction FUNCTION = null;
            FUNCTION = RFCREP.CreateFunction("ZCBF_DAILY_RFC");
            FUNCTION.SetValue("FARMCODE", SHED_CODE);
            FUNCTION.SetValue("PLANT", PLANT_CODE);


            //function1 = rfcRep.CreateFunction("ZCBF_DAILY_RFC")
            //    function1.SetValue("PLANT", cmbPlant.Text)
            //    function1.SetValue("FARMCODE", CCode)
            //    function1.Invoke(rfcDest)
            //    function1.GetValue("QTY")

            try
            {
                FUNCTION.Invoke(RFCDEST);
            }
            catch (Exception e)
            {

            }
            var msg = FUNCTION.GetTable("IT_DATA");
            dt = DataTable(msg);
            var DATA = GetJson(dt);
            //  this.Context.Response.Write("{");
            writeOutput(dt);
            // this.Context.Response.Write("}");


        }
    }
}
