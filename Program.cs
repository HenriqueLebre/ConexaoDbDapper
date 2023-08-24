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

        static void GetCategory(SqlConnection conn)
        {
            var category = conn
                .QueryFirstOrDefault<Category>(
                    "SELECT TOP 1 [Id], [Title] FROM [Category] WHERE [Id]=@id",
                    new
                    {
                        id = ""
                    }
                );
            Console.WriteLine($"{category.Id} - {category.Title}");
        }

        static void CreateCategory(SqlConnection conn)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "AMAZON AWS";

            var insertSql = "INSERT INTO [Category] VALUES(@Id, @Title)";

            var rows = conn.Execute(insertSql, new
            {
                category.Id,
                category.Title
            });

            Console.WriteLine($"{rows} quantidade de registros inseridos.");
        }

        static void UpdateCategory(SqlConnection conn)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@Title WHERE [Id]=@id";
            var rows = conn.Execute(updateQuery, new
            {
                id = new Guid(""),
                title = "DevSecOps"
            });

            Console.WriteLine($"{rows} quantidade de registro atualizados.");

        }

        static void DeleteCategory(SqlConnection conn)
        {
            var deleteQuery = "DELETE [Category] WHERE [id]=@id";
            var rows = conn.Execute(deleteQuery, new
            {
                id = new Guid("")
            });

            Console.WriteLine($"{rows} quantidade de registros deletados.");

        }

    }
}