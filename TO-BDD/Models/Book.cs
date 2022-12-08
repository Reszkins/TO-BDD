using System;
using TO_BDD.Enums;

namespace TO_BDD.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class BookComparer : Comparer<Book>
    {
        public override int Compare(Book x, Book y)
        {
            int val = x.Title.CompareTo(y.Title);
            if (val != 0)
            {
                return val;
            }

            val = x.Description.CompareTo(y.Description);
            if (val != 0)
            {
                return val;
            }

            val = x.Author.CompareTo(y.Author);
            if (val != 0)
            {
                return val;
            }

            val = x.Type.CompareTo(y.Type);
            if (val != 0)
            {
                return val;
            }

            return val;
        }
    }
}
