using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace RaintreePoultry
{
    public class Connection
    {

        public SqlConnection con,con1,con2,con3;
        public void db_connection()
        {


            con = new SqlConnection(@"server=192.168.21.19;User ID=psa;Password=Sa<>Cbf0;Initial Catalog=PARENT_MASTER_DATA");
           // con = new SqlConnection(@"server=192.168.18.34\MSSQL12_SAIL;User ID=sa;Password=sa@123;Initial Catalog=PARENT_MASTER_DATA");
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Open();
            }


        }

        public void db_connection1()
        {


            //con1 = new SqlConnection(@"server=192.168.21.19;User ID=psa;Password=Sa<>Cbf0;Initial Catalog=QualityControl");
            con1 = new SqlConnection(@"server=192.168.21.19;User ID=psa;Password=Sa<>Cbf0;Initial Catalog=QC_ANALYISIS");
            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
                con1.Open();
            }


        }


        public void db_connection2()
        {


            //con1 = new SqlConnection(@"server=192.168.21.19;User ID=psa;Password=Sa<>Cbf0;Initial Catalog=QualityControl");
            con2 = new SqlConnection(@"server=192.168.21.19;User ID=psa;Password=Sa<>Cbf0;Initial Catalog=RaintreePoultry");
            //con2 = new SqlConnection(@"server=192.168.18.34\MSSQL12_SAIL;User ID=sa;Password=sa@123;Initial Catalog=RaintreePoultry");
            if (con2.State == ConnectionState.Open)
            {
                con2.Close();
                con2.Open();
            }


        }
        public void db_connection3()
        {
            //con1 = new SqlConnection(@"server=192.168.21.19;User ID=psa;Password=Sa<>Cbf0;Initial Catalog=QualityControl");
            con3 = new SqlConnection(@"server=192.168.21.19;User ID=psa;Password=Sa<>Cbf0;Initial Catalog=DB_DELIVERY_APP");
            //con2 = new SqlConnection(@"server=192.168.18.34\MSSQL12_SAIL;User ID=sa;Password=sa@123;Initial Catalog=RaintreePoultry");
            if (con3.State == ConnectionState.Open)
            {
                con3.Close();
                con3.Open();
            }


        }



        public void db_closed()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
        }

        public void db_closed1()
        {
            if (con1.State == ConnectionState.Open)
            {
                con1.Close();

            }
        }


        public void db_closed2()
        {
            if (con2.State == ConnectionState.Open)
            {
                con2.Close();

            }
        }

        public void db_closed3()
        {
            if (con3.State == ConnectionState.Open)
            {
                con3.Close();

            }
        }

    }
}