using TO_BDD.Models;

namespace TO_BDD.Services
{
    public interface ICartService
    {
        void AddToCart(Book book);
        void RemoveFromCart(Book book);
        List<Book> GetBooksFromCart();
        void ClearCart();
    }
    public class CartService : ICartService
    {
        Cart cart;
        public CartService()
        {
            cart = new Cart();
        }

        public void ClearCart()
        {
            cart.Books.Clear();
        }
        public void AddToCart(Book book)
        {
            cart.Books.Add(book);
        }

        public void RemoveFromCart(Book book)
        {
            cart.Books.Remove(book);
        }
        public List<Book> GetBooksFromCart()
        {
            return cart.Books;
        }

        public void RemoveAllFromCart()
        {
            cart.Books.Clear();
        }
    }
}
