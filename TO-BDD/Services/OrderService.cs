using TO_BDD.Models;

namespace TO_BDD.Services
{
    public interface IOrderService 
    {
        void CreateOrder(Cart cart);
        List<Order> GetAllOrders();

        //do testow
        void ReomoveAllOrders();
    }

    public class OrderService
    {
    }
}
