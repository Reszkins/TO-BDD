namespace TO_BDD.Models
{
    public class Cart
    {
        public List<Book> Books { get; set; } = new();
        public int UserId { get; set; } = new();
    }
}
