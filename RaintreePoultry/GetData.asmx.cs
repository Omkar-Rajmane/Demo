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

namespace RaintreePoultry
{
    /// <summary>
    /// Summary description for GetData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GetData : System.Web.Services.WebService
    {
        Methods methods = new Methods();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        [WebMethod]
        public void AllDataMasters(string TASK, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string CREATED_BY)
        {


            DataSet ds = null;

            ds = new DataSet();

            try
            {
                if (TASK == "GET_ALL_MASTERS" || TASK == "GET_PARENT_MASTERS" || TASK == "GET_TRADER_MASTERS" || TASK == "GET_CBF_MASTERS")
                {
                    this.Context.Response.Write("{\"PoultryMasters\":[");
                    DataSet ds1 = new DataSet();
                    ds = methods.AllDataMasters(TASK, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2, SEARCH3, CREATED_BY);
                    ds.Tables[0].TableName = "M_PLANT";
                    ds.Tables[1].TableName = "M_PAGE";
                    ds.Tables[2].TableName = "M_VENDOR";
                    ds.Tables[3].TableName = "M_BATCH";
                    ds.Tables[4].TableName = "M_BIRD_TYPE";
                    ds.Tables[5].TableName = "M_SHED";
                    ds.Tables[6].TableName = "M_MATERIAL";
                    ds.Tables[7].TableName = "M_MORTALITY_REASON";
                    ds.Tables[8].TableName = "M_PLANT_TYPE";
                    ds.Tables[9].TableName = "M_VISIT_REASON";
                    ds.Tables[10].TableName = "M_CHLORINE_STATUS";
                    ds.Tables[11].TableName = "M_CHLORINE_STATUS_POINTS";
                    ds.Tables[12].TableName = "M_MEDICINE";
                    ds.Tables[13].TableName = "M_FEED_STORAGE_TYPE";
                    ds.Tables[14].TableName = "M_FEED_COMPLAINT_TYPE";

                    writeOutput(ds);
                    this.Context.Response.Write("]}");
                }
                else if (TASK == "GET_BATCH_MASTER")
                {
                    this.Context.Response.Write("{\"PoultryMasters\":[");
                    ds = methods.AllDataMasters(TASK, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2, SEARCH3, CREATED_BY);
                    ds.Tables[0].TableName = "M_BATCH";

                    writeOutput(ds);
                    this.Context.Response.Write("]}");
                }

                else if (TASK == "GET_BRANDDING_MASTERS")
                {
                    this.Context.Response.Write("{\"PoultryMasters\":[");
                    ds = methods.AllDataMasters(TASK, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2, SEARCH3, CREATED_BY);
                    ds.Tables[0].TableName = "M_COMPANY";
                    ds.Tables[1].TableName = "M_CITY";
                    ds.Tables[2].TableName = "M_DISTRICT";
                    ds.Tables[3].TableName = "M_IF_OTHER_WHY";
                    ds.Tables[4].TableName = "M_SALE_MARKET_PREDICTION";
                    ds.Tables[5].TableName = "M_BOARDTYPE";
                    ds.Tables[6].TableName = "M_SHOPTYPE";
                    ds.Tables[7].TableName = "M_SHOP_CATEGORY";
                    ds.Tables[8].TableName = "M_BOARD_STATUS";
                    ds.Tables[9].TableName = "M_BOARD_REMOVE_REASON";
                    ds.Tables[10].TableName = "M_ACTIVITY";
                    ds.Tables[11].TableName = "M_BARNDDING_VISIT_REASON";

                    writeOutput(ds);
                    this.Context.Response.Write("]}");
                }
            }
            catch (Exception ee) { this.Context.Response.Write(ee.ToString()); }
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

        [WebMethod]
        public void GETM_REPORT_LOCAL(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
        {
            dt = methods.GETM_REPORT_LOCAL(TASK, USER_ID, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2);
            writeOutput(dt);

        }


        [WebMethod]
        public void GETM_RPT(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
        {
            ds = methods.GETM_RPT(TASK, USER_ID, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2);


            if (TASK == "GET_LAST_LIFTING_DATE")
            {
                ds.Tables[0].TableName = "RptLastLifting";

            }
            else if (TASK == "GET_SHED_CLEANING")
            {
                ds.Tables[0].TableName = "RptShedCleaning";

            }
            else if (TASK == "GET_SHED_GRADING")
            {
                ds.Tables[0].TableName = "RptShedGrading";

            }
            else if (TASK == "GET_LOGIN")
            {
                ds.Tables[0].TableName = "ResponseLogin";

            }
            else if (TASK == "GET_OTP")
            {
                ds.Tables[0].TableName = "ResponseLogin";

            }

            else if (TASK == "UPDATE_IMEI")
            {
                ds.Tables[0].TableName = "ResponseLogin";

            }
            else if (TASK == "GET_REQUEST_FIRST" || TASK == "GET_REQUEST_SECOND" || TASK == "GET_REQUEST_THRID" || TASK == "GET_REQUEST_FOURTH" )
            {
                ds.Tables[0].TableName = "ResponseRequestData";
                ds.Tables[1].TableName = "ResponseRequestApproveMEC";
                ds.Tables[2].TableName = "M_STATUS";

            }

            else if ( TASK == "GET_REQUEST_FIVE" )
            {
                ds.Tables[0].TableName = "ResponseRequestData";
               

            }
            else if (TASK == "GET_REQUEST_SEVEN")
            {
                ds.Tables[0].TableName = "ResponseTraderOrder";

            }
            else if (TASK == "GET_REQUEST_SIX")
            {
                ds.Tables[0].TableName = "ResponseRequestTRADER";

            }
                //REQUEST APPROVAL DATA
            else if (TASK == "GET_REQUEST_APPROVE_MEC")
            {
                ds.Tables[0].TableName = "ResponseRequestApproveMEC";

            }
                 
                 //trader master
            else if (TASK == "GET_TRADER_MST")
            {
                ds.Tables[0].TableName = "M_ZONE";
                ds.Tables[1].TableName = "M_BIRDS_SIZE";
                ds.Tables[2].TableName = "TRD_BANK_MASTER";
                ds.Tables[3].TableName = "TRD_FUND_TRASFER_MODE";
            }

                 //REQUEST APPROVAL DATA
            else if (TASK == "GET_REQUEST_APPROVE_MEC")
            {
                ds.Tables[0].TableName = "ResponseRequestApproveMEC";

            }

            else if (TASK == "GET_SHED_LOCATION")
            {
                ds.Tables[0].TableName = "SHED_LOCATION";

            }
            else if (TASK == "GET_TRADER_ORDER")
            {
                ds.Tables[0].TableName = "Rpt_T_Order";

            }
            else if (TASK == "GET_PARENT_MORTALITY_PRODUCTION")
            {
                ds.Tables[0].TableName = "PARENT_MORTALITY_PRODUCTION";

            }
            else if (TASK == "GET_SHED_SUERVEY_NOTAPPROVE")
            {
                ds.Tables[0].TableName = "SHED_SUERVEY_NOTAPPROVE";
            }
            writeOutput(ds);

        }


        [WebMethod]
        public void GETM_REPORT(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
        {
            dt = methods.GETM_REPORT(TASK, USER_ID, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2);
            writeOutput(dt);

        }
        [WebMethod]
        public void GETM_LOGIN(string TASK, string USER_ID, string MOBILE_NO, string SEARCH, string SEARCH1, string SEARCH2)
        {
            ds = methods.GETM_LOGIN(TASK, USER_ID, MOBILE_NO, SEARCH, SEARCH1, SEARCH2);



            if (TASK == "GET_LOGIN" || TASK == "GET_DEVICE_REG" || TASK == "DEVICE_REG" || TASK == "GET_OTP")
            {
                ds.Tables[0].TableName = "ResponseLogin";

            }
           
            writeOutput(ds);

        }
        [WebMethod]
        public void RFC(string plant_code, string vendorcode,string SEARCH,string TASK)
        //  public void RFC()
        {
            dt = methods.SAPConnection("GET_SAP_IP");

            RfcConfigParameters rfc = new RfcConfigParameters();
            rfc.Add(RfcConfigParameters.Name, "PRD");
            rfc.Add(RfcConfigParameters.AppServerHost, dt.Rows[0]["SAP_IP"].ToString());
            rfc.Add(RfcConfigParameters.SystemNumber, "00");
            rfc.Add(RfcConfigParameters.SystemID, "PRD");
            rfc.Add(RfcConfigParameters.Client, "900");
            rfc.Add(RfcConfigParameters.User, "batch1");
            rfc.Add(RfcConfigParameters.Password, "123456");
            RfcDestination rfcDest = RfcDestinationManager.GetDestination(rfc);
            RfcRepository rfcRep = rfcDest.Repository;
            IRfcFunction function1 = null;
                string pcode = plant_code;
                string vcode = vendorcode;
                function1 = rfcRep.CreateFunction("YRFC_CBF_BIRD_VALUATION");
                function1.SetValue("WERKS", pcode.ToString());
                function1.SetValue("LGORT", vcode.ToString());
                try
                {
                    function1.Invoke(rfcDest);
                }
                catch (Exception)
                {

                }
                dt = new DataTable();
                dt = create_dt();
           
                DataRow dr = null;
                IRfcTable table1 = default(IRfcTable);
                string birdage = null, housingdate = null, mortalaity = null, sale_qty = null;
                double house_qty = 0;
                birdage = function1.GetString("BIRD_AGE");
                //HttpContext.Current.Response.Write(birdage + "$");
                string birdstock = null;

                birdstock = function1.GetString("REM_QTY");
                housingdate = function1.GetString("HOUSED_DATE");
                house_qty = Double.Parse(function1.GetString("HOUSED_QTY"));
                sale_qty = function1.GetString("sale_qty");
                mortalaity = function1.GetString("mortality");
                dr = dt.NewRow();
                dr["birdage"] = birdage;
                dr["birdstock"] = birdstock;
                dr["housingdate"] = housingdate;
                dr["house_qty"] = house_qty;
                dr["mortalaity"] = mortalaity;
                dr["sale_qty"] = sale_qty;
                dt.Rows.Add(dr);

                ds = methods.GETM_RPT(TASK, SEARCH, "", house_qty.ToString(), housingdate, plant_code, vendorcode);

                    ds.Tables.Add(dt);
               
                  
                    ds.Tables[0].TableName = "ReponseQty";
                    ds.Tables[1].TableName = "ReponseSAPQTY";

                    writeOutput(ds);
        }

        private DataTable create_dt()
        {
            //dt_simulation
            DataTable dtt = new DataTable();

            dtt.Columns.Add(new DataColumn("birdage", typeof(string)));
            dtt.Columns.Add(new DataColumn("birdstock", typeof(string)));
            dtt.Columns.Add(new DataColumn("housingdate", typeof(string)));
            dtt.Columns.Add(new DataColumn("house_qty", typeof(string)));
     
            dtt.Columns.Add(new DataColumn("mortalaity", typeof(string)));
            dtt.Columns.Add(new DataColumn("sale_qty", typeof(string)));
            return dtt;

        }


        [WebMethod]
        public void GETM_SAMPLE(string TASK, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string CREATED_BY)
        {
            ds = methods.GETM_SAMPLE(TASK, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2,SEARCH3, CREATED_BY);

            if (TASK == "GETM_SAMPLE")
            {
                ds.Tables[0].TableName = "SAMPLE_PARAMETER";

            }
            if (TASK == "GETM_SAMPLE_TYPE" || TASK == "GETM_SAMPLE_TYPE_TEST")
            {
                ds.Tables[0].TableName = "M_SAMPLE_TYPE";

            }
            if (TASK == "GET_RPT_SAMPLE_COLLECTION")
            {
                ds.Tables[0].TableName = "Rpt_T_RESULTS_HEADER";
                ds.Tables[1].TableName = "Rpt_T_RESULTS_DETAILS";
            }

            if (TASK == "GET_CBF")
            {
                ds.Tables[0].TableName = "T_CBF_PHYSICAL_HEADER";
                ds.Tables[1].TableName = "T_CBF_PHYISCAL_DETAILS";
            }

            if (TASK == "GETM_SHED_REASON")
            {
                ds.Tables[0].TableName = "MR";

            }
  
            writeOutput(ds);

        }



        
        [WebMethod]
        public void SendPostsToGCM(string enter, string key)
        {

            try
            {

                //    var applicationID = "AIzaSyCkjpozFTTcqH6-MxIruQ3mlhCa826JHEM";
                //  var senderId = "592449004681";
                var applicationID = "AIzaSyCkjpozFTTcqH6-MxIruQ3mlhCa826JHEM";
                var senderId = "592449004681";


                //                string deviceId = "fLKXMRBHe_k:APA91bETTfjtcp6xgqTZsnl4IS8DbEbgRviVIiUGHt7Usw179YLS7s-bY1NQEqPwLYIhcjFZsJGx-f2MdCM55SFo4Han-H0w5MofoIC6PAoVsySfpXn8OtrqtE8FalzVVhLUkCNAahHs";
                string deviceId = key;
                //                string deviceId = "/topics/weather";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = enter,
                        title = "MyHR-CMS"
                    },
                    priority = "high"
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                this.Context.Response.Write(sResponseFromServer);
                                //   Response.Write(sResponseFromServer);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                this.Context.Response.Write(ex.Message);
                //   Response.Write(ex.Message);
            }
        }


        [WebMethod]
        public void GET_NPS_QUESTION(string TASK, string SEARCH, string CREATED_BY)
        {
            ds = methods.GET_NPS_QUESTION(TASK, SEARCH, CREATED_BY);
            ds.Tables[0].TableName = "M_REGION";
            ds.Tables[1].TableName = "M_NPS_QUESTIONS";

            writeOutput(ds);

        }


        [WebMethod]
        public void GET_NPS_REPORT(string TASK, string USER_ID, string SEARCH, string SEARCH1)
        {
            dt = methods.GET_NPS_REPORT(TASK, USER_ID, SEARCH, SEARCH1);
            writeOutput(dt);

        }



        [WebMethod]
        public void GETM_DETAILS(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string SEARCH4, string SEARCH5)
        {
            try
            {
                ds = methods.GETM_DETAILS(TASK, USER_ID, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2, SEARCH3, SEARCH4, SEARCH5);
                if (TASK == "GET_TA_START_ENTRY")
                {
                    ds.Tables[0].TableName = "M_VEHICLE_TYPE";
                    ds.Tables[1].TableName = "TA_ENTRY_DETAIL";
                    ds.Tables[2].TableName = "M_STATE";
                }
                else if (TASK == "GET_TRIP_EXPENSE")
                {
                    ds.Tables[0].TableName = "M_CITY_TYPE";
                    ds.Tables[1].TableName = "TRIP_EXPENSE_DETAIL";

                }
                else if (TASK == "GET_TRIP_EXPENSE")
                {
                    ds.Tables[0].TableName = "M_BATCH";

                }
                writeOutput(ds);

            }
            catch (Exception e)
            {

            }
        }


        [WebMethod]
        public void PASSWORD_ENCRYPTION(string plainText)
        {
     
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            dt.Columns.Add(new DataColumn("Password", typeof(string)));
            DataRow dr = null;
            dr = dt.NewRow();
            dr["Password"] = System.Convert.ToBase64String(plainTextBytes);
            dt.Rows.Add(dr);        
            writeOutput(dt);
        }


        [WebMethod]
        public void PASSWORD_DECRYPTION(string EncryptText)
        {

            var base64EncodedBytes = System.Convert.FromBase64String(EncryptText);
          
            dt.Columns.Add(new DataColumn("Password", typeof(string)));
            DataRow dr = null;
            dr = dt.NewRow();
            dr["Password"] = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            dt.Rows.Add(dr);
            writeOutput(dt);
        }

    }




}
