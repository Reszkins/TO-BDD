using TO_BDD.Models;
namespace TO_BDD.Services
{
    public interface ICartService
    {
        void AddToCart(string book);
        void RemoveFromCart(string book);

        //do testów
        bool RemoveAllBooks();
        Cart GetCart();
    }
    public class CartService
    {
    }
}
