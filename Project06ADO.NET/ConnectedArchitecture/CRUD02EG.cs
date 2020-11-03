using System;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;


namespace ConnectedArchitecture
{
    class Movietheater
    { 
        public int movieid { get; set; }
        public int theaterid { get; set; }
        public int ticketprice { get; set; }

        internal void GetMovietheater()
        {
            
            Console.WriteLine(" Enter TheaterID");
            theaterid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" Enter TicketPrice");
            ticketprice = Convert.ToInt32(Console.ReadLine());

        }
    }

    class MovieAccess
    {
        SqlConnection scon = null;
        SqlCommand sc;

        public SqlConnection GetConnection()
        { 
            
                scon = new SqlConnection(
               "Data Source= SRIKANTH\\SQLEXPRESS;Initial Catalog=dbmovie;User ID=sa;Password=newuser123#");
                Console.WriteLine(" Connection Established");
                scon.Open();
                return scon;
           
           
        }

        public void DisplayMovieTheater()
        {
            try
            {
                scon = GetConnection();
                SqlDataReader sdr;

                sc = new SqlCommand(" select * from tblmovietheater", scon);
                sdr = sc.ExecuteReader();

                while (sdr.Read())
                {
                    Console.WriteLine(sdr["MovieId"] + " " + sdr["TheaterId"] + " " + sdr["TicketPrice"]);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
            finally
            {
                scon.Close();
            }                
        }
    
        public void GetByScalar()
        { try
            {
                scon = GetConnection();
                sc = new SqlCommand(" select avg(ticketprice)  from tblmovietheater", scon);
                var avg = sc.ExecuteScalar();

                Console.WriteLine(" AVG Tktprice is {0}", avg);
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }
        /*public void InsertValue()
        { try
            {
                Movietheater movietheater = new Movietheater();
                scon = GetConnection();
                sc = new SqlCommand("insert tblmovietheater (movieid,theaterid,ticketprice) values(@mid,@tid,@tp)", scon);
                sc.Parameters.AddWithValue("@mid", movietheater.movieid);
                sc.Parameters.AddWithValue("@tid", movietheater.theaterid);
                sc.Parameters.AddWithValue("@tp", movietheater.ticketprice);
                int i = sc.ExecuteNonQuery();

                Console.WriteLine(" No of Row affected: {0}", i);
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }*/
        
        public void UpdateVales()
        {try
            {
                Movietheater movietheater = new Movietheater();
                Console.WriteLine(" Enter ticket price to be Updated");
                movietheater.GetMovietheater();
                sc = new SqlCommand(" update tblmovietheater set ticketprice = @tp where theaterid in (@tid1,@tid2)", scon);
                sc.Parameters.AddWithValue("@tp", movietheater.ticketprice);
                sc.Parameters.AddWithValue("@tid1", movietheater.theaterid);
               /// sc.Parameters.AddWithValue("@tid2", movietheater.theaterid);
                int i = sc.ExecuteNonQuery();

                Console.WriteLine(" No of Row affected: {0}", i);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    
    }


    class CRUD02
    {
        static void Main()
        {
            
            MovieAccess movieAccess = new MovieAccess();
            movieAccess.DisplayMovieTheater();
            movieAccess.GetByScalar();
            ///movieAccess.InsertValue();
            movieAccess.UpdateVales();

            Console.Read();
        }
    }


}       




