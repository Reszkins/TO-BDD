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
        Task<List<Book>> GetAllProposedBooks();
        Task<Book> GetBookById(int id);
    }

    public class BookService : IBookService
    {
        private DbRepository _db;
        public BookService()
        {
            _db = new DbRepository();
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

        public Task<List<Book>> GetAllProposedBooks()
        {
            //ALGORYTM

            string sql = "SELECT * FROM [dbo].[Books] WHERE = COŚTAMZALGORYTMU";

            return _db.LoadData<Book>(sql);
        }

        public async Task<Book> GetBookById(int id)
        {
            string sql = $"SELECT * FROM dbo.Books WHERE Id = {id.ToString()}";
            List<Book> books = await _db.LoadData<Book>(sql);

            return books[0];
        }
    }
}
