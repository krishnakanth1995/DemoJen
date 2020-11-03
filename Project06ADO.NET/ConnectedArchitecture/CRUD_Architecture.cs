using System;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;

namespace ConnectedArchitecture
{
    class Region
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }

        internal void GetRegion()
        {
            Console.WriteLine("Enter RegionId");
            RegionId =Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Region");
            RegionDescription = Console.ReadLine();
        }
    }
    class DataAccess
    {
        SqlConnection con = null;
        SqlCommand cmd;
       
       

        public SqlConnection GetConnection()
        {
            con = new SqlConnection(
                "Data Source= SRIKANTH\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Password=newuser123#");
            Console.WriteLine(" Connection Established");
            con.Open();
            return con;
        }
    

        public void DisplayRegion()
        {
            try
            {
                con = GetConnection();
                SqlDataReader dr;

                cmd = new SqlCommand("select * from Region", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine(dr["RegionId"] + " " + dr["RegionDescription"]);
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                con.Close();
            }
           
       
        
        }

        internal void GetbyExecuteScalar()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select count(RegionId) from Region", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine(" No of Region :{0}", count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                con.Close();
            }


        }

        // update command 
        internal void EditRegion()
        {
            try
            {
                Region region = new Region();
                Console.WriteLine(" Please Enter the region description to be update ");
                region.GetRegion();
                con = GetConnection();
                cmd = new SqlCommand(" update Region set RegionDescription = @RDesc where RegionId = @Rid", con);
                cmd.Parameters.AddWithValue("@Rid", region.RegionId);
                cmd.Parameters.AddWithValue("@RDesc", region.RegionDescription);
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine(" Rows Updated are :{0}", i);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                con.Close();
            }
        }

        internal void CallProcedure()
        {
            con = GetConnection();
            Console.WriteLine(" Enter the Customer ID");
            string CustId = Console.ReadLine();
                                /// procedure name
            cmd = new SqlCommand("CustOrdersOrders", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", CustId);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr["RegionId"] + " " + rdr["RegionDescription"]);
            }





        }

        
    }

    class ClientEnd

    {
        static void Main()
        {
            DataAccess dataAccess = new DataAccess();

            try
            {
                dataAccess.DisplayRegion();
                dataAccess.GetbyExecuteScalar();
                dataAccess.EditRegion();
                dataAccess.CallProcedure();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.Read();
        }
        

        
        
       
        

    }

    class CRUD_Architecture
    {
    }
}
