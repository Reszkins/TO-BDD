using TO_BDD.Models;
using TO_BDD.Providers;
using TO_BDD.Repositories;

namespace TO_BDD.Services
{
    public interface IOrderService 
    {
        Task CreateOrder(List<Book> books, string username);
        Task<List<Order>> GetAllOrders(string username);
    }

    public class OrderService : IOrderService
    {
        private DbRepository _db;
        public OrderService()
        {
            _db = new DbRepository();
        }

        public async Task<List<Order>> GetAllOrders(string username)
        {
            var userService = new UserService();
            string sql = $"SELECT * FROM [dbo].[Orders] WHERE UserId = {await userService.GetUserId(username)}";

            return await _db.LoadData<Order>(sql);
        }

        public async Task CreateOrder(List<Book> books, string username)
        {
            var userService = new UserService();
            var bookString = "";
            foreach(var book in books)
            {
                bookString += book.Title + ",";
            }
            string sql = $"INSERT INTO [TO-BDD].[dbo].[Order] (TimeStamp, UserId, Books) VALUES ('{DateTime.Now.ToString("yyyy-MM-dd")}', {await userService.GetUserId(username)}, '{bookString}')";



            await _db.SaveData(sql);
        }
    }
}
