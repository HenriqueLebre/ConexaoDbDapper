using System;
using Dapper;
using ConexaoBDDapper.Models;
using Microsoft.Data.SqlClient;

namespace ConexaoBDDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

            using(var conn  = new SqlConnection(connectionString))
            {
                GetCategory(conn);
            }
        }

        static void ListCategorys(SqlConnection conn)
        {
            var categories = conn.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id} - {category.Title}");
            }
        }
    }
}