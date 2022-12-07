using System.ComponentModel.DataAnnotations;

namespace TO_BDD.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Book> Books { get; set; } = new();
        public int UserId { get; set; } = new();
    }
}
