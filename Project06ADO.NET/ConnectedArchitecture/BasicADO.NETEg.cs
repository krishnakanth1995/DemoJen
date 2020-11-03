using System;
using System.Data.SqlClient;

namespace ConnectedArchitecture
{
    class Shipper
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public void GetShipper()
        {
            Console.WriteLine(" Enter ShipperName");
            CompanyName = Console.ReadLine();
            Console.WriteLine(" Enter Phone Number");
            Phone = Console.ReadLine();
        }
    }
    class BasicADO
    {
        static void Main()
        {
            /// create sqlConnection object
            /// Connection String 
            SqlConnection connection = null;
            // create object for command class
            SqlCommand command;
            try
            {
                // sql server authentication
                connection = new SqlConnection(
                    "Data Source= SRIKANTH\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Password=newuser123#");
                Console.WriteLine("Connection Established");

                // open connection
                connection.Open();
                // insert operation in shippers table 
                //command = new SqlCommand(" insert into Shippers values('krishna','9000885561')", connection);
                Shipper shipper = new Shipper();
                shipper.GetShipper();
                /*command = new SqlCommand(" insert into Shippers values(@cname,@phone)", connection);
                command.Parameters.AddWithValue("@cname",shipper.CompanyName);
                command.Parameters.AddWithValue("@phone", shipper.Phone);*/
                // delete operation
                Console.WriteLine(" Enter Company Name to be Deleted");
                string delcommand = " delete from Shipper where CompanyName = @cname";
                command = new SqlCommand(delcommand, connection);
                command.Parameters.AddWithValue("@cname", shipper.CompanyName);
                int i = command.ExecuteNonQuery(); // returns integer
                Console.WriteLine("No of rows affected:{0}", i);
                command.Parameters.Clear();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
            }

            Console.Read();
        } 

    }
}
