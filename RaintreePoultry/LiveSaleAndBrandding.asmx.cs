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
    /// Summary description for Brandding
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Brandding : System.Web.Services.WebService
    {

        LiveSaleAndBranddingMethods LS_BRMethods = new LiveSaleAndBranddingMethods();
        DataTable dt = new DataTable();

        [WebMethod]
        public void SAVE_TRADER_SURVEY(string TASK, string DATA)
        {
            dt = LS_BRMethods.SAVE_TRADER_SURVEY(TASK, DATA);
            writeOutput(dt);
        }

        [WebMethod]
        public void SAVE_RETAILER_SURVEY(string TASK, string DATA)
        {
            dt = LS_BRMethods.SAVE_RETAILER_SURVEY(TASK, DATA);
            writeOutput(dt);
        }

        [WebMethod]
        public void SAVE_BOARD_LOCATOR(string TASK, string DATA)
        {
            dt = LS_BRMethods.SAVE_BOARD_LOCATOR(TASK, DATA);
            writeOutput(dt);
        }

        [WebMethod]
        public void SAVE_TRADER_VISIT(string TASK, string DATA)
        {
            dt = LS_BRMethods.SAVE_TRADER_VISIT(TASK, DATA);
            writeOutput(dt);
        }

        [WebMethod]
        public void SAVE_RETAILER_VISIT(string TASK, string DATA)
        {
           dt = LS_BRMethods.SAVE_RETAILER_VISIT(TASK, DATA);
           writeOutput(dt);
        }

    // Save form other activity 

        [WebMethod]
        public void SAVE_OTHER_ACTIVITY(string TASK, string DATA)
        {
            dt = LS_BRMethods.SAVE_OTHER_ACTIVITY(TASK, DATA);
            writeOutput(dt);
        }

    // Save Customer Complaint

        [WebMethod]
        public void SAVE_CUSTOMER_COMPLAINT(string TASK, string DATA)
        {
            dt = LS_BRMethods.SAVE_CUSTOMER_COMPLAINT(TASK, DATA);
            writeOutput(dt);
        }

    // Save Appron Distributor 

        [WebMethod]
        public void SAVE_APPRON_DISTRIBUTOR(string TASK, string DATA)
        {
            dt = LS_BRMethods.SAVE_APPRON_DISTRIBUTOR(TASK, DATA);
            writeOutput(dt);
        }

   
    // Save Owner 

        [WebMethod]
        public void SAVE_BR_OWNER(string TASK, string DATA)
         {
            dt = LS_BRMethods.SAVE_BR_OWNER(TASK, DATA);
            writeOutput(dt);
        }

    // GET Brandding Reports

        [WebMethod]
        public void GET_BRANDDING_REPORT(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
        {
            dt = LS_BRMethods.GET_BRANDDING_REPORT(TASK, USER_ID, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2);
            writeOutput(dt);
        }

        [WebMethod]
        public void SAVE_BOARD_REMOVE(string TASK, string BOARD_ID, string STATUS, string CREATED_BY)
        {
            dt = LS_BRMethods.SAVE_BOARD_REMOVE(TASK, BOARD_ID, STATUS, CREATED_BY);
            writeOutput(dt);
        }


        public void writeOutput(DataTable dt)
        {
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(dt, Formatting.Indented));
        }
    }
}
