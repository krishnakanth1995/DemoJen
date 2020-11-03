using System;
using System.Data;
using System.Data.SqlClient;



namespace DisconnectedArchitecture
{
    class CRUD_DisconnectArchitecture
    {
        static void Main()

        {
            //create sqlconnection object
            SqlConnection con = null;
            /// data adapter as command for discoonected architecture
            SqlDataAdapter da;


            con = new SqlConnection(
                   "Data Source= SRIKANTH\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Password=newuser123#");
            Console.WriteLine("Connection Established");
            da = new SqlDataAdapter(" select * from Region", con);

            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "NorthwindRegion"); /// here name of table is ourwhish we can write an alias for identification purpose
            DataTable dt = ds.Tables["NorthwindRegion"];

            /// here data is stored as tables in dataset, to fetch we need to iterate over rows and col

            foreach (DataRow dataRow in dt.Rows)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    Console.Write(dataRow[dataColumn]);
                    Console.Write(" ");
                }

                Console.WriteLine(" ");

            }

            // we can fectch multiple tables at a time with same data adapter object

            da = new SqlDataAdapter(" select * from Shippers", con);

            da.Fill(ds, "NorthwindShippers");
            DataTable dataTable = ds.Tables["NorthwindShippers"];

            foreach (DataRow dataRow1 in dataTable.Rows)
            {
                foreach (DataColumn datacolumn1 in dataTable.Columns)
                {
                    Console.Write(dataRow1[datacolumn1]);
                    Console.Write(" ");
                }
                Console.WriteLine(" ");
            }

            // updating a record using command bulider
            SqlCommandBuilder scb = new SqlCommandBuilder(da);
            da.Fill(ds);
            // adding row 
            DataRow row2 = ds.Tables["NorthwindRegion"].NewRow();
            row2["RegionID"] = 10;
            row2["RegionDescription"] = "NW";
            // Adding row to datatable in dataset
            ds.Tables["NorthwindRegion"].Rows.Add(row2);
            da.UpdateCommand = scb.GetUpdateCommand();
            da.Update(ds);
            Console.WriteLine("----------");
            dt = ds.Tables["NorthwindRegion"];

            foreach (DataRow r in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns) 
                {
                    
                }
            }



            Console.Read();


        }

    }
}
