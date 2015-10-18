using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace StudentDb
{
    class Program
    {
        static string connectionString = "Data Source=172.16.2.93;Initial Catalog=00000000_Student;Persist Security Info=True;User ID=00-00000-0;Password=123456";
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("==========================");
                Console.WriteLine("Query:\t1,");
                Console.WriteLine("Insert:\t2,");
                Console.WriteLine("Update:\t3,");
                Console.WriteLine("Delete:\t4,");
                Console.WriteLine("Exit:\t0");
                Console.WriteLine("==========================");
                Console.Write("Enter Command Here: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Query();
                        break;
                    case "2":
                        Insert();
                        break;
                    case "3":
                        Update();
                        break;
                    case "4":
                        Delete();
                        break;
                    case "0":
                        return;

                }
                Console.WriteLine("==========================");
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            } while (true);

        }

        private static void Delete()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());
          
            string insertCommand = "DELETE Student " +
                                   "WHERE ID = @ID";
            SqlCommand command = new SqlCommand(insertCommand, connection);
            SqlParameter idParameter = new SqlParameter("@ID", SqlDbType.Int);
            idParameter.Value = id;
  
            command.Parameters.Add(idParameter);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            Query();
        }
        private static void Update()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            string insertCommand = "UPDATE Student SET Name = @Name " +
                                   "WHERE ID = @ID";
            SqlCommand command = new SqlCommand(insertCommand, connection);
            SqlParameter idParameter = new SqlParameter("@ID", SqlDbType.Int);
            idParameter.Value = id;
            SqlParameter nameParameter = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            nameParameter.Value = name;
            command.Parameters.Add(idParameter);
            command.Parameters.Add(nameParameter);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            Query();
        }

        private static void Insert()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            string insertCommand = "INSERT INTO Student (ID, Name) " +
                                   "VALUES (@ID, @Name)";
            SqlCommand command = new SqlCommand(insertCommand, connection);
            SqlParameter idParameter = new SqlParameter("@ID", SqlDbType.Int);
            idParameter.Value = id;
            SqlParameter nameParameter = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            nameParameter.Value = name;
            command.Parameters.Add(idParameter);
            command.Parameters.Add(nameParameter);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            Query();
        }

        static void Query()
        {
            
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Student";
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            connection.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();

            ShowTable(dt);
        }

        static void ShowTable(DataTable dt)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Console.Write(dt.Columns[j].ColumnName);
                Console.Write("\t");
            }
            Console.WriteLine();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Console.Write(dt.Rows[i][j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }

    }
}
