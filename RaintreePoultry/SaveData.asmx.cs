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
    /// Summary description for SaveData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SaveData : System.Web.Services.WebService
    {
        Methods methods = new Methods();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        [WebMethod]
        public void SAVM_SHED_CLEANING(string TASK, string DATA)
        {
            dt = methods.SAVM_SHED_CLEANING(TASK, DATA);
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
        public void SAVM_GRADIN(string TASK, string DATA)
        {
            dt = methods.SAVM_GRADIN(TASK, DATA);
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
        public void SAVM_REQUEST(string TASK, string DATA)
        {
            dt = methods.SAVM_REQUEST(TASK, DATA);
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
        public void SAVM_STATUS_FIRST(string TASK, string DATA)
        {
            dt = methods.SAVM_STATUS_FIRST(TASK, DATA);
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
        public void SAVM_STATUS(string TASK, string DATA)
        {
            dt = methods.SAVM_STATUS(TASK, DATA);
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
        public void SAVM_TTRADERBIDDING(string TASK, string DATA)
        {
            dt = methods.SAVM_TTRADERBIDDING(TASK, DATA);
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
        public void SAVEM_CBF_PHYSICAL(string DATA,string HEADER_TASK,string DETAILS_TASK)
        {


            dt = methods.SAVEM_CBF_PHYSICAL(DATA, HEADER_TASK, DETAILS_TASK);
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
        public void SAVE_TA_ENTRY(string TASK, string DATA)
        {
            dt = methods.SAVE_TA_ENTRY(TASK, DATA);
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
        public void SAVE_TNPS(string TASK, string DATA)
        {
            dt = methods.SAVE_TNPS(TASK, DATA);
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
        public void SAVE_EXPENSE(string TASK, string DATA)
        {
            dt = methods.SAVE_EXPENSE(TASK, DATA);
            if (dt.Rows.Count > 0)
            {
                writeOutput(dt);
            }
            else
            {
                this.Context.Response.Write("[{\"STATUS\":\"false\",\"SQLITE_ID\":\"0\",\"MSG\":\"Failed to save expense...!\"}]");
            }
        }


        public void writeOutput(DataTable dt)
        {
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented));
        }




    }
}
