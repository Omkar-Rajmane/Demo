using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
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
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;




namespace RaintreePoultry
{
    public class RFC_DATA_Mehods:Connection
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds = new DataSet();

        //GET_SAP_DETAILS
        internal String GET_SAP_DETAILS(string TASK, string SEARCH)
        {

            db_connection();
            try
            {
                cmd = new SqlCommand("GET_SAP_DETAILS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@TASK", TASK);
                cmd.Parameters.AddWithValue("@SEARCH", SEARCH);   

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["IP_ADDRESS"].ToString();
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception e)
            {
                db_closed();
                return "0";
            }
            finally
            {
                db_closed();
                
            }
        }

        public class PARENT_BATCH_DETAILS
        {
            public string LOC_CODE { get; set; }
            public string FLOCK { get; set; }
            public string HOUSE_SH_CODE { get; set; }
            public string FEMALE_CODE { get; set; }
            public string PLACMNT_DATE { get; set; }
            public string MALE_CODE { get; set; }
            public string F_QUANTITY { get; set; }
            public string M_QUANTITY { get; set; }
        }


        //GET PARENT BATCH DETAILS
         internal DataTable GET_PARENT_BATCH_DETAILS_RFC(string DATA)
        {
            List<PARENT_BATCH_DETAILS> mdata = JsonConvert.DeserializeObject<List<PARENT_BATCH_DETAILS>>(DATA);
         
               for (int i = 0; i < mdata.Count; i++)
               {

                  
                   try
                   {
                       db_connection();
                       cmd = new SqlCommand("SAV_BATCH", con);
                       cmd.CommandType = CommandType.StoredProcedure;
                       cmd.Connection = con;

                       cmd.Parameters.AddWithValue("@TASK", "SAVE_P_BATCH");
                       cmd.Parameters.AddWithValue("@PLANT_CODE", mdata[i].LOC_CODE);
                       cmd.Parameters.AddWithValue("@FLOCK_NO", mdata[i].FLOCK);
                       cmd.Parameters.AddWithValue("@SHED_CODE", mdata[i].HOUSE_SH_CODE);
                       cmd.Parameters.AddWithValue("@HOUSING_DATE", mdata[i].PLACMNT_DATE);
                       cmd.Parameters.AddWithValue("@FEMALE_ITEM_CODE", mdata[i].FEMALE_CODE);
                       cmd.Parameters.AddWithValue("@MALE_ITEM_CODE", mdata[i].MALE_CODE);
                       cmd.Parameters.AddWithValue("@FEMALE_QTY", mdata[i].F_QUANTITY);
                       cmd.Parameters.AddWithValue("@MALE_QTY", mdata[i].M_QUANTITY);

                       da = new SqlDataAdapter();
                       da.SelectCommand = cmd;
                       da.Fill(dt);
                       con.Close();
                   }
                   catch (Exception e)
                   {

                   }
                   finally
                   {
                       db_closed();
                   }
               }

            return dt;
        }


         //GET PARENT DAILY MORTALITY FOR RFC
         internal DataTable GET_P_DAILY_TRANS_RFC(string TASK,string SEARCH)
         {
             
                 try
                 {
                     db_connection2();
                     cmd = new SqlCommand("GET_P_RFC_DATA", con2);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection = con2;

                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
         
                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                     con2.Close();
                 }
                 catch (Exception e)
                 {

                 }
                 finally
                 {
                     db_closed2();
                 }

             return dt;
         }


         //GET PARENT DAILY MORTALITY FOR RFC
         internal DataTable UPD_P_DAILY_TRANS(string TASK, string SEARCH)
         {

             try
             {
                 db_connection2();
                 cmd = new SqlCommand("UPD_P_RFC_DATA", con2);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con2;

                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@SEARCH", SEARCH);

                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(dt);
                 con2.Close();
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed2();
             }

             return dt;
         }

         public class CBF_BATCH_DETAILS
         {
             public string DOC_NO { get; set; }
             public string LOCATION_CODE { get; set; }
             public string LOCATION_NAME { get; set; }
             public string FARMER_CODE { get; set; }
             public string FARMER_NAME { get; set; }
             public string FARM_CODE { get; set; }
             public string FARM_NAME { get; set; }
             public string VEHICLE { get; set; }
             public string WAREHOUSE_CODE { get; set; }
             public string WAREHOUSE_NAME { get; set; }
             public string SUPERVISOR_CODE { get; set; }
             public string SUPERVISOR { get; set; }
             public string ZDATE { get; set; }
             public string BATCH_NO { get; set; }
             public string STORE_CODE { get; set; }
             public string STORE_LOC { get; set; }
             public string ITEM_CODE { get; set; }
             public string PLACED_QTY { get; set; }
         }
         //GET PARENT BATCH DETAILS
         internal DataTable GET_CBF_BATCH_DETAILS_RFC(string DATA)
         {
             List<CBF_BATCH_DETAILS> mdata = JsonConvert.DeserializeObject<List<CBF_BATCH_DETAILS>>(DATA);

             for (int i = 0; i < mdata.Count; i++)
             {


                 try
                 {
                     db_connection();
                     cmd = new SqlCommand("SAV_BATCH", con);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection = con;

                     cmd.Parameters.AddWithValue("@TASK", "SAVE_C_BATCH");
                     cmd.Parameters.AddWithValue("@PLANT_CODE", mdata[i].LOCATION_CODE);
                     cmd.Parameters.AddWithValue("@FLOCK_NO", mdata[i].BATCH_NO);
                     cmd.Parameters.AddWithValue("@SHED_CODE", mdata[i].FARM_CODE);
                     cmd.Parameters.AddWithValue("@HOUSING_DATE", mdata[i].ZDATE);
                     cmd.Parameters.AddWithValue("@FEMALE_ITEM_CODE", mdata[i].ITEM_CODE);
                     cmd.Parameters.AddWithValue("@MALE_ITEM_CODE", "NA");
                     cmd.Parameters.AddWithValue("@FEMALE_QTY", 0);
                     cmd.Parameters.AddWithValue("@MALE_QTY", mdata[i].PLACED_QTY);

                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                     con.Close();
                 }
                 catch (Exception e)
                 {

                 }
                 finally
                 {
                     db_closed();
                 }
             }

             return dt;
         }


         public class Distance
         {
             public string dist { get; set; }
             public string duration { get; set; }
         }

         internal Double GET_DISTANCE(string S_Latitude, string S_Longitude, string D_Latitude, string D_Longitude)
         {
             Double dist = 0.0;
             var unit = "";
         try
            {
                string new_url = "http://123.63.131.6:8053/api?origin=" + S_Latitude + ",%20" + S_Longitude + "&destination=" + D_Latitude + ",%20" + D_Longitude + "&key=bfc64ff9-0073-444b-9e47-6535b2eeed11";
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(new_url.ToString());
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                Distance mdata = JsonConvert.DeserializeObject<Distance>(responseString);
                char[] spearator = { ' ' };
                String[] strlist = mdata.dist.ToString().Split(spearator);
                unit = strlist[1].ToString();
                for (int i = 0; i < strlist.Length; i++)
                {
                    if (i == 0)
                    {
                        if (strlist[1].ToString() == "m")
                        {
                            dist =  Convert.ToDouble(strlist[i])/1000;
                        }
                        else
                        {
                            dist = Convert.ToDouble(strlist[i]);
                        }
                    }
                    
                 
               }
              return dist;

            }
            catch(Exception e)
            {
                return dist;
            }

    }


         //GET PARENT DAILY MORTALITY FOR RFC
         internal DataSet GET_P_DAILY_TRANS_DATA(string TASK, string SEARCH)
         {

             try
             {
                 db_connection2();
                 cmd = new SqlCommand("GET_P_DAILY_TRANS_DATA", con2);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con2;

                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@SEARCH", SEARCH);

                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(ds);
                 con2.Close();
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed2();
             }

             return ds;
         }



         internal DataTable DAILY_TRANS_SEND_TO_SAP(string TASK, string SEARCH)
         {
          
                 try
                 {
                     db_connection2();
                     cmd = new SqlCommand("PUSH_DAILY_TRANS_TO_SAP_RFC", con2);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection = con2;

                     cmd.Parameters.AddWithValue("@TASK", TASK);
                     cmd.Parameters.AddWithValue("@SEARCH", SEARCH);
            
                     da = new SqlDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dt);
                     con2.Close();
                 }
                 catch (Exception e)
                 {

                 }
                 finally
                 {
                     db_closed2();
                 }
             

             return dt;
         }

         //SAVE FEED DC FOR RFC
         internal DataTable SAV_SMART_DELIVERY_DC_RFC(string TASK, string SEARCH)
         {

             try
             {
                 db_connection3();
                 cmd = new SqlCommand("SAV_SAP_DOC_RFC", con3);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con3;

                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@JSON", SEARCH);

                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(dt);
                 con3.Close();
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed3();
             }

             return dt;
         }

         //SAVE SAV_ELICIOUS_INVOICE_RFC
         internal DataTable SAV_ELICIOUS_INVOICE_RFC(string TASK, string SEARCH)
         {

             try
             {
                 db_connection2();
                 cmd = new SqlCommand("SAVE_INVOICE_DATA", con2);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con2;

                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@JSON", SEARCH);

                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(dt);
                 con2.Close();
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed2();
             }

             return dt;
         }




         //SAVE FEED DC FOR RFC
         internal DataTable SAV_HATCHERY_EGGS_RFC(string TASK, string SEARCH)
         {
             try
             {
                 db_connection2();
                 cmd = new SqlCommand("SAV_HATCHERY_EGGS_RFC", con2);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con2;
                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@JSON", SEARCH);
                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(dt);
                 con2.Close();
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed2();
             }

             return dt;
         }


         //SAVE QC BOM RFC
         internal DataSet SAV_QC_BOM_RFC(string TASK, string SEARCH)
         {

             try
             {
                 db_connection2();
                 cmd = new SqlCommand("SAV_QC_BOM_RFC", con2);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con2;

                 cmd.Parameters.AddWithValue("@TASK", TASK);
                 cmd.Parameters.AddWithValue("@JSON", SEARCH);

                 da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(ds);
                 con2.Close();
             }
             catch (Exception e)
             {

             }
             finally
             {
                 db_closed2();
             }

             return ds;
         }
    }
}