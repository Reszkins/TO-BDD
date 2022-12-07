using TO_BDD.Models;
using TO_BDD.Repositories;

namespace TO_BDD.Services
{
    public interface IOrderService 
    {
        void CreateOrder(Cart cart);
        Task<List<Order>> GetAllOrders();
    }

    public class OrderService : IOrderService
    {
        private DbRepository _db;
        public OrderService()
        {
            _db = new DbRepository();
        }

        public Task<List<Order>> GetAllOrders()
        {
            string sql = "SELECT * FROM [dbo].[Orders]";

            return _db.LoadData<Order>(sql);
        }

        public void CreateOrder(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
