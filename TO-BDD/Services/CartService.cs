namespace TO_BDD.Services
{
    public interface ICartService
    {
        void AddToCart(string book);
        void RemoveFromCart(string book);
    }
    public class CartService
    {
    }
}
