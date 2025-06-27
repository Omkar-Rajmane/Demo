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

using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace RaintreePoultry
{
    /// <summary>
    /// Summary description for QC_ANALYISIS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class QC_ANALYISIS : System.Web.Services.WebService
    {
        Methods methods = new Methods();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        [WebMethod]
        public void SAVEM_RESULT_RECORDING(string DATA)
        {


            dt = methods.SAVEM_RESULT_RECORDING(DATA);
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

        }//INSERT CHICKS DATA


        [WebMethod]
        public void GETM_ED_DATA(string TASK, string CREATED_BY, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string SEARCH4)
        {
            ds = methods.GETM_ED_DATA(TASK, CREATED_BY, SEARCH, SEARCH1, SEARCH2, SEARCH3, SEARCH4);

            if (TASK == "GETM_ED_CBF_2")
            {
                ds.Tables[0].TableName = "GETM_ED_CBF_2";

            }
            if (TASK == "GETM_ED_CBF_2_R")
            {
                ds.Tables[0].TableName = "GETM_ED_CBF_2";

            }
            writeOutput(ds);

        }
        public void writeOutput(DataSet ds)
        {
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(ds, Formatting.Indented));
        }

        [WebMethod]
        public void SAVEM_RESULT_RECORDINGED(string DATA)
        {


            dt = methods.SAVEM_RESULT_RECORDING_ED(DATA);
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

        }//INSERT CHICKS DATA




        [WebMethod]
        public void GETM_RPT_QC(string TASK, string CREATED_BY, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string SEARCH4)
        {
            ds = methods.GETM_RPT_QC(TASK, CREATED_BY, SEARCH, SEARCH1, SEARCH2, SEARCH3, SEARCH4);

            if (TASK == "GET_ENCODING")
            {
                ds.Tables[0].TableName = "RPT_ENCODING";

            }
            if (TASK == "GET_SAMPLE_COLLECTIONS")
            {
                ds.Tables[0].TableName = "RPT_SAMPLE_COLLECTIONS";

            }


            writeOutput(ds);

        }


        [WebMethod]
        public void SAVE_TAENTRY(string TASK, string Data)
        {
            dt = methods.SAVE_TAENTRY(TASK, Data);
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
        public void SAVEM_SHED_CANCEL(string TASK, string Data)
        {
            dt = methods.SAVEM_SHED_CANCEL(TASK, Data);
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
        public void SAVEM_WATER_AVAILABILITY(string TASK, string Data)
        {
            dt = methods.SAVEM_WATER_AVAILABILITY(TASK, Data);
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

    }
}
