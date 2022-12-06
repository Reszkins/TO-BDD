using TO_BDD.Enums;
using TO_BDD.Models;

namespace TO_BDD.Services
{
    public interface IBookService 
    {
        List<Book> GetAllBooks();
        List<Book> GetAllBookByType(BookType bookType);
        List<Book> GetAllProposedBooks();

        //do testów
        bool RemoveAllBooks();
        bool AddBook(Book book);
    }

    public class BookService
    {
    }
}
