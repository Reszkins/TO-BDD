using System.Collections.Generic;
using TO_BDD.Models;
using TO_BDD.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TO_BDD.Services
{
    public interface IBookService 
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetAllBooksByType(string bookType);
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

        public Task<List<Book>> GetAllBooksByType(string bookType)
        {
            string sql = $"SELECT * FROM [dbo].[Books] WHERE [Type] = '{bookType}'";

            return _db.LoadData<Book>(sql);
        }

        public async Task<string> GetTypeByTitle(string title)
        {
            string sql = $"SELECT Type FROM [dbo].[Books] WHERE Title = '{title}'";

            var types = await _db.LoadData<string>(sql);

            return types.FirstOrDefault();
        }

        public async Task<List<Book>> GetAllProposedBooks(string username)
        {
            var orderService = new OrderService();
            var orders = await orderService.GetAllOrders(username);
            List<Book> resultBooks = new List<Book>();

            Dictionary<string, int> bookTypesCount = new Dictionary<string, int>();

            foreach(var order in orders)
            {
                foreach (var title in order.Books.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string type = await GetTypeByTitle(title);
                    if (bookTypesCount.ContainsKey(type))
                    {
                        bookTypesCount[type] = bookTypesCount[type] + 1;
                    } else
                    {
                        bookTypesCount.Add(type, 1);
                    }
                }
            }

            if(bookTypesCount.Count > 0)
            {
                int highestCount = bookTypesCount.Values.Max();
                foreach (var bookType in bookTypesCount.Keys)
                {
                    if (bookTypesCount[bookType] == highestCount)
                    {
                        resultBooks.AddRange(await GetAllBooksByType(bookType));
                    }
                }
            }

            return resultBooks;
        }

        public async Task<Book> GetBookById(int id)
        {
            string sql = $"SELECT * FROM dbo.Books WHERE Id = {id.ToString()}";
            List<Book> books = await _db.LoadData<Book>(sql);

            return books[0];
        }
    }
}
