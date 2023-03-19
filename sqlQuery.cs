using System;
using System.Data.SqlClient;

namespace SQLQuery
{
    internal class Program
    {
        static SqlConnection authenticate(string sqlServer)
        {
            string conString = String.Format("Server = {0}; Database = master; Integrated Security = True;", sqlServer);
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                Console.WriteLine("[+] Auth Success!");
            }
            catch
            {
                con.Close();
                Console.WriteLine("[-] Auth Failed.");
                Environment.Exit(0);
            }
            return con;
        }

        static void executeQuery(SqlConnection con, string queryStr)
        {
            SqlCommand command = new SqlCommand(queryStr, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read() == true)
                {
                    Console.WriteLine("[+] " + reader[0]);
                }
                reader.Close();
                con.Close();
            }
            catch
            {
                con.Close();
                Console.WriteLine("[-] Command Execution Failed.");
                Environment.Exit(0);
            }

        }

        static void Main(string[] args)
        {
            SqlConnection con = authenticate(args[0]);
            executeQuery(con, args[1]);

        }
    }
}
