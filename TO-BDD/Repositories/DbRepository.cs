using Dapper;
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
    }
}
