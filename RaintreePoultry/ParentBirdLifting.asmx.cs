using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SAP.Middleware.Connector;

namespace RaintreePoultry
{
    /// <summary>
    /// Summary description for ParentBirdLifting
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ParentBirdLifting : System.Web.Services.WebService
    {


        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        ParentBirdLiftingMethods methods = new ParentBirdLiftingMethods();
        DataSet ds = new DataSet();





        [WebMethod]
        public void GETM_LOGIN(string TASK, string USER_ID, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2)
        {
            ds = methods.GETM_LOGIN(TASK, USER_ID, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2);

            if (ds.Tables.Count>0)
            {
                if (TASK == "GET_PARENT_BIRD_LIFTING_LOGIN")
                {
                    ds.Tables[0].TableName = "RESPONSE_LOGIN";

                }
                writeOutput(ds);
            }
            else
            {
                this.Context.Response.Write("{\"RESPONSE_LOGIN\": [{\"Status\":\"Somthing Problem..! Try Agian.\"}]}");
            }

        }




        // 2023-06-20 Aditya Yadav

        [WebMethod]
        public void GET_MASTERS(string TASK, string TO_DATE, string FROM_DATE, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3, string CREATED_BY)
        {
            ds = methods.GET_MASTERS(TASK, TO_DATE, FROM_DATE, SEARCH, SEARCH1, SEARCH2, SEARCH3, CREATED_BY);

            if (TASK == "GET_MASTERS_BIRD_LIFITING")
            {
                this.Context.Response.Write("{\"BirdLiftingMasters\":[");
                ds.Tables[0].TableName = "M_SHED";

                writeOutput(ds);
                this.Context.Response.Write("]}");
            }

        }


        [WebMethod]
        public void GET_PARENT_ORDER_TOKEN_DETAILS_NEW(string TASK, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3)
        {
            ds = methods.GET_PARENT_ORDER_TOKEN_DETAILS_NEW(TASK, SEARCH, SEARCH1, SEARCH2, SEARCH3);
            writeOutput(ds);
        }



        [WebMethod]
        public void GET_PARENT_ORDER_TOKEN_DETAILS(string TASK, string SEARCH, string SEARCH1, string SEARCH2, string SEARCH3)
        {
            dt = methods.GET_PARENT_ORDER_TOKEN_DETAILS(TASK, SEARCH, SEARCH1, SEARCH2, SEARCH3);
            writeOutput(dt);
        }

        [WebMethod]
        public void SAVE_PARENT_BASE_RATE(string TASK, string CREATED_BY, string ZONES, string PLANTS, string MATERIALS)
        {
            dt = methods.SAVE_PARENT_BASE_RATE(TASK, CREATED_BY, ZONES, PLANTS, MATERIALS);
            writeOutput(dt);
        }

        [WebMethod]
        public void SAVE_PARENT_TOKEN_WEIGHT(string TASK, string CREATED_BY, string DATA)
        {
            dt = methods.SAVE_PARENT_TOKEN_WEIGHT(TASK, CREATED_BY, DATA);
            writeOutput(dt);
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

    }
}
