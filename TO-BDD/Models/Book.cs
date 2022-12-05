using TO_BDD.Enums;

namespace TO_BDD.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public BookType Type { get; set; }
    }
}
