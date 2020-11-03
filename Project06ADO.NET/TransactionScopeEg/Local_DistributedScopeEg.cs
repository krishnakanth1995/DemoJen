using System;
using System.Data.SqlClient;
using System.Transactions;


namespace TransactionScopeEg
{
    class Local_DistributedScopeEg
    {
        static void LocalTransactionScope()
        {
            int i, j;
            /// by using tranasaction class 
            SqlTransaction myTran = null;
            string myconnectionString = "Data Source= SRIKANTH\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Password=newuser123#";

            using (TransactionScope myscope = new TransactionScope())
            {
                // connection 
                using(SqlConnection conn = new SqlConnection(myconnectionString))
                {
                    conn.Open();
                    try
                    {
                        // begin transaction
                        myTran = conn.BeginTransaction();
                        SqlCommand mycommand2 = new SqlCommand("Insert into Shippers values('Myshop','9000885561')", conn);
                        j = mycommand2.ExecuteNonQuery();
                        Console.WriteLine("Inserted record in Shipper table: {0}", j);
                        var mycommand = new SqlCommand("Insert into Region values (9,'southwest')", conn);                    
                        i = mycommand.ExecuteNonQuery();
                        Console.WriteLine("Inserted record in Region table: {0}", i);
                        // commiting transaction
                        myTran.Commit();
                       /// myscope.Complete();                  
                    
                    } 
                    catch(Exception ex)
                    {
                        if (myTran != null)
                        {
                            myTran.Rollback();
                            Console.WriteLine(ex);
                            Console.WriteLine(" Transaction not complete ");
                        }
                        
                        
                    }
                }
            }
        
        }     
        
        static void DistributedTransactionScope()
        {
            string myconnectionString = "Data Source= SRIKANTH\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Password=newuser123#";
            string myconnectionstring1 = "Data Source= SRIKANTH\\SQLEXPRESS;Initial Catalog=dbemployee873;User ID=sa;Password=newuser123#";
            using (TransactionScope myscope = new TransactionScope()) 
            {
                using (var myconn = new SqlConnection(myconnectionString))
                {
                    myconn.Open();

                    try
                    {
                        var mycommand2 = new SqlCommand(" Insert into Shippers values ('Veggieshope','9949907437')", myconn);
                        mycommand2.ExecuteNonQuery();
                        using (var myconn1 = new SqlConnection(myconnectionstring1))
                        {
                            myconn1.Open();
                            var cmd = new SqlCommand(" Insert into Book values ('B005','KRISHNA')", myconn1);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    catch(Exception Ex)
                    {
                        Console.WriteLine(Ex);
                        Console.WriteLine("Transaction not complete");
                    }

                    
                }
                myscope.Complete();
            }
        
        }
            

    
    static void Main()
        {
            try
            {
                LocalTransactionScope();
               /// DistributedTransactionScope();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.Read();
        }
    
    
    
    
    
    
    }

    
    

    



}
