using System;
using System.Collections;
using System.Data.SqlClient;

namespace SQLQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            // Grabs server you want to auth to. If you press enter without supplying any server, it will try to auth to the current host
            Console.WriteLine("Enter the server you want to auth to: ");
            string sqlServer = Console.ReadLine();
            // Connection info
            string database = "master";
            string conString = "Server = " + sqlServer + "; Database = " + database + "; Integrated Security = True;";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                Console.WriteLine("Auth Success!");
            }
            catch
            {
                con.Close();
                Console.WriteLine("Auth failed");
                Environment.Exit(0);
            }
            while (1 == 1)
            {
                Console.WriteLine("SQL >");
                // Enter the command you will like to run
                string execCmd = Console.ReadLine();
                SqlCommand command = new SqlCommand(execCmd, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read() == true)
                {
                    Console.WriteLine("Output:" + reader[0]);
                }

                reader.Close();
            }

        }
    }
}
