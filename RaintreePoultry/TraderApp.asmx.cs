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
    /// Summary description for TraderApp
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TraderApp : System.Web.Services.WebService
    {

        Trader_Mthds methods = new Trader_Mthds();
        GetData getdata_methods = new GetData();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        [WebMethod]
        public void SAVM_TRADER_ORDER_REQUEST(string Data)
        {
            dt = methods.SAVM_TRADER_ORDER_REQUEST(Data);
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
        public void SAVM_TRADER_FUND_TRANSFER(string Data)
        {
            dt = methods.SAVM_TRADER_FUND_TRANSFER(Data);
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
        public void GET_CUSTOMER_BALANCE(string TASK,string CUSTOMER_CODE)
        {
            ds = methods.GET_CUSTOMER_BALANCE(TASK,CUSTOMER_CODE);


            ds.Tables[0].TableName = "CUSTOMER_BALANCE";
              

            getdata_methods.writeOutput(ds);



        }



        // Aditya Yadav 2024-01-02
        [WebMethod]
        public void GET_CLAIM_MORT_DETAILS(string TASK, string CUST_CODE, string SEARCH, string SEARCH1)
        {

            dt = methods.GET_CLAIM_MORT_DETAILS(TASK, CUST_CODE, SEARCH, SEARCH1);
            getdata_methods.writeOutput(dt);

        }

        // Aditya Yadav 2024-01-02
        [WebMethod]
        public void SAVE_CLAIM_MORT(string TASK, string DATA)
        {
            dt = methods.SAVE_CLAIM_MORT(TASK, DATA);
            getdata_methods.writeOutput(dt);
        }



    }
}
