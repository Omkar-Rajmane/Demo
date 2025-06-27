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
    /// <summary>
    /// Summary description for ParentApp
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ParentApp : System.Web.Services.WebService
    {
        Parent_Methods methods = new Parent_Methods();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        Methods method = new Methods();
        RFC_DATA_Mehods rfc_method = new RFC_DATA_Mehods();
      



        [WebMethod]
        public void SAVE_MORTALITY(string TASK,string DATA)
        {
            dt = methods.SAVE_MORTALITY(TASK,DATA);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            HttpContext.Current.Response.Write(jsSerializer.Serialize(parentRow));

        }


        [WebMethod]
        public void SAVE_PRODUCTION(string TASK, string DATA)
        {
            dt = methods.SAVE_PRODUCTION(TASK, DATA);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            HttpContext.Current.Response.Write(jsSerializer.Serialize(parentRow));

        }

           [WebMethod]
        public void SAVEM_WATER_TEST(string TASK, string DATA)
        {
            dt = methods.SAVEM_WATER_TEST(TASK, DATA);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            HttpContext.Current.Response.Write(jsSerializer.Serialize(parentRow));

        }


           [WebMethod]
           public void SAVE_PHOTO(string DATA)
           {
               String A = methods.SAVE_PHOTO(DATA);

               HttpContext.Current.Response.Write(A);

           }


           [WebMethod]
           public void GET_DAILY_TRANS_DATA(string TASK, string SEARCH)
           {

               

            DataSet ds = null;

            ds = new DataSet();

            try
            {
                this.Context.Response.Write("{\"ParentDailyTransaction\":[");
              //  DataSet ds1 = new DataSet();
                ds = rfc_method.GET_P_DAILY_TRANS_DATA(TASK,SEARCH);
                ds.Tables[0].TableName = "MORTALITY_DETAILS";
                ds.Tables[1].TableName = "MORTALITY_REASON_DETAILS";
                ds.Tables[2].TableName = "FEED_CONSUMPTION_DETAILS";
                ds.Tables[3].TableName = "PRODUCTION_DETAILS";
        
                writeOutput(ds);
                this.Context.Response.Write("]}");
            }
            catch (Exception ee) { this.Context.Response.Write(ee.ToString()); }

               //dt = rfc_method.GET_P_DAILY_TRANS_RFC("GET_P_MORTALITY_DETAILS", "");
               //dt = rfc_method.GET_P_DAILY_TRANS_RFC("GET_P_MORTALITY_REASON_DETAILS", "");
               //dt = rfc_method.GET_P_DAILY_TRANS_RFC("GET_P_FEED_CONSUMPTION_DETAILS", "");
               //dt = rfc_method.GET_P_DAILY_TRANS_RFC("GET_P_PRODUCTION_DETAILS", "");


           }


           [WebMethod]
           public void DAILY_TRANS_SEND_TO_SAP(string TASK, string SEARCH)
           {
               dt = rfc_method.DAILY_TRANS_SEND_TO_SAP(TASK, SEARCH);
               writeOutput(dt);

           }


        // 2023-12-07 Aditya Yadav : Save Egg Collection 
        [WebMethod]
        public void SAVE_PARENT_EGGS_DAILY_TRANS(string TASK, string DATA)
        {
            dt = methods.SAVE_PARENT_EGGS_DAILY_TRANS(TASK, DATA);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            HttpContext.Current.Response.Write(jsSerializer.Serialize(parentRow));

        }



        // 2023-02-09 Aditya Yadav : Save Egg Collection 
        [WebMethod]
        public void SAVE_FEED_QUALITY_COMPLAINT(string TASK, string DATA)
        {
            dt = methods.SAVE_FEED_QUALITY_COMPLAINT(TASK, DATA);
            if (dt.Rows.Count > 0)
            {
                writeOutput(dt);
               
            }
            else
            {
                this.Context.Response.Write("[{\"STATUS\":\"false\",\"SQLITE_ID\":\"0\",\"MSG\":\"Failed to save feed quality complaint...!\"}]");
            }
        }


        [WebMethod]
        public void DYANAMIC_AUTO_MAIL(string Mail)
        {
            methods.DYANAMIC_AUTO_MAIL(Mail);
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
        
    }


}
