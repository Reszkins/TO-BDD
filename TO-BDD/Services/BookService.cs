using System.Collections.Generic;
using TO_BDD.Enums;
using TO_BDD.Models;
using TO_BDD.Repositories;

namespace TO_BDD.Services
{
    public interface IBookService 
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetAllBooksByType(BookType bookType);
        Task<List<Book>> GetAllProposedBooks(string username);
        Task<Book> GetBookById(int id);
        Task RemoveAllBooks();
        Task AddBook(Book book);
    }

    public class BookService : IBookService
    {
        private DbRepository _db;
        public BookService()
        {
            _db = new DbRepository();
        }

        public async Task AddBook(Book book)
        {
            string sql = $"INSERT INTO [dbo].[Books] (Title, Description, Author, Type) VALUES ('{book.Title}', '{book.Description}', '{book.Author}', '{book.Type}')";

            await _db.SaveData(sql);
        }

        public async Task RemoveAllBooks()
        {
            await _db.TruncateTable("[dbo].[Books]");
        }

        public Task<List<Book>> GetAllBooks()
        {
            string sql = "SELECT * FROM [dbo].[Books]";

            return _db.LoadData<Book>(sql);
        }

        public Task<List<Book>> GetAllBooksByType(BookType bookType)
        {
            string sql = $"SELECT * FROM [dbo].[Books] WHERE [Type] = {BookTypeMapper.GetBookTypeString(bookType)}";

            return _db.LoadData<Book>(sql);
        }

        public async Task<List<Book>> GetAllProposedBooks(string username)
        {
            var orderService = new OrderService();
            var orders = await orderService.GetAllOrders(username);

            foreach(var order in orders)
            {

            }

            string sql = "SELECT * FROM [dbo].[Books] WHERE = COŚTAMZALGORYTMU";

            return await _db.LoadData<Book>(sql);
        }

        public async Task<Book> GetBookById(int id)
        {
            string sql = $"SELECT * FROM dbo.Books WHERE Id = {id.ToString()}";
            List<Book> books = await _db.LoadData<Book>(sql);

            return books[0];
        }
    }
}
