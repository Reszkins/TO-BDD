using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TO_BDD.Models;

namespace TO_BDD.Repositories
{
    public class DbRepository
    {
        public async Task<List<T>> LoadData<T>(string sql)
        {
            string connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=TO-BDD;Trusted_Connection=True;";

            using(IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql);
                return data.ToList();
            }
        }

        public async Task SaveData(string sql)
        {
            string connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=TO-BDD;Trusted_Connection=True;";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task SaveUserData(string username, byte[] passwordHash, byte[] passwordSalt )
        {

            string connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=TO-BDD;Trusted_Connection=True;";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync("INSERT INTO dbo.Users (UserName, PasswordHash, PasswordSalt) " +
                    "VALUES (@name, @hash, @salt)", new {name = username, hash = passwordHash, salt = passwordSalt});
            }
        }

        public async Task TruncateTable(string table)
        {
            string connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=TO-BDD;Trusted_Connection=True;";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync($"TRUNCATE TABLE {table}");
            }
        }

        public async Task Remove(string sql)
        {
            string connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=TO-BDD;Trusted_Connection=True;";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
