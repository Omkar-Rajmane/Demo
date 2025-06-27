using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Summary description for CBFShedSurvey
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CBFShedSurvey : System.Web.Services.WebService
    {

        CBFShedSurveyMethods cbfshedsurveymethods = new CBFShedSurveyMethods();
        DataTable dt = new DataTable();

        [WebMethod]
        public void SAVE_SHED_SURVEY(string TASK, string DATA)
        {
            dt = cbfshedsurveymethods.SAVE_SHED_SURVEY(TASK, DATA);
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
        public void APPROVE_SHED_SURVEY(string TASK, string ID, string APPROVE_BY, string APPROVE_DATE)
        {
            dt=cbfshedsurveymethods.APPROVE_SHED_SURVEY(TASK, ID, APPROVE_BY, APPROVE_DATE);
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
