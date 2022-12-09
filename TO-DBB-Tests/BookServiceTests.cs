using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using TO_BDD.Models;
using TO_BDD.Services;

namespace TO_DBB_Tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task Get_All_Books_When_List_Empty()
        {
            //Arrange
            BookService bookService = new BookService();

            await bookService.RemoveAllBooks();

            //Act
            var books = await bookService.GetAllBooks();

            //Assert
            Assert.Empty(books);

        }

        [Fact]
        public async Task Get_All_Books_When_One_Book_In_List()
        {
            //Arrange
            BookService bookService = new BookService();
            await bookService.RemoveAllBooks();
            Book book = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };
            await bookService.AddBook(book);

            //Act
            var books = await bookService.GetAllBooks();

            //Assert
            List<Book> booksExpected = new List<Book> { book };

            book.Should().BeEquivalentTo(books.First(), options =>
                options.Excluding(o => o.Id));

        }

        [Fact]
        public async Task Get_All_Books_When_Multiple_Books_In_List()
        {
            //Arrange
            BookService bookService = new BookService();
            await bookService.RemoveAllBooks();
            Book book1 = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            Book book2 = new()
            {
                Id = 2,
                Title = "Lalka",
                Description = "Opis costam blabla",
                Author = "Boleslaw Prus",
                Type = "novel"
            };

            Book book3 = new()
            {
                Id = 3,
                Title = "Solaris",
                Description = "opis",
                Author = "Stanislaw Lem",
                Type = "scifi"
            };
            await bookService.AddBook(book1);
            await bookService.AddBook(book2);
            await bookService.AddBook(book3);

            List<Book> booksExpected = new List<Book> { book1, book2, book3 };

            //Act
            var books = await bookService.GetAllBooks();

            //Assert

            booksExpected.Should().BeEquivalentTo(books, options =>
                options.Excluding(o => o.Id));

        }

        [Fact]
        public async Task Get_All_Books_By_Type_When_List_Empty()
        {
            //Arrange
            BookService bookService = new BookService();

            await bookService.RemoveAllBooks();

            //Act
            var books = await bookService.GetAllBooksByType("novel");

            //Assert
            Assert.Empty(books);

        }

        [Fact]
        public async Task Get_All_Books_By_Type_When_No_Type()
        {
            //Arrange
            BookService bookService = new BookService();

            await bookService.RemoveAllBooks();
            Book book = new()
            {
                Id = 1,
                Title = "Solaris",
                Description = "opis",
                Author = "Stanislaw Lem",
                Type = "scifi"
            };
            await bookService.AddBook(book);

            //Act
            var books = await bookService.GetAllBooksByType("novel");

            //Assert
            Assert.Empty(books);
        }

        [Fact]
        public async Task Get_All_Books_By_Type_When_Only_That_Type()
        {
            //Arrange
            BookService bookService = new BookService();

            await bookService.RemoveAllBooks();
            Book book = new()
            {
                Id = 2,
                Title = "Lalka",
                Description = "Opis costam blabla",
                Author = "Boleslaw Prus",
                Type = "novel"
            };
            await bookService.AddBook(book);

            List<Book> booksExpected = new List<Book> { book };

            //Act
            var books = await bookService.GetAllBooksByType("novel");

            //Assert
            booksExpected.Should().BeEquivalentTo(books, options =>
                options.Excluding(o => o.Id));
        }

        //[Fact]
        //public async Task Get_All_Books_By_Type_When_Multiple_Types()
        //{
        //    //Arrange
        //    BookService bookService = new BookService();

        //    await bookService.RemoveAllBooks();
        //    Book book1 = new()
        //    {
        //        Id = 2,
        //        Title = "Lalka",
        //        Description = "Opis costam blabla",
        //        Author = "Boleslaw Prus",
        //        Type = "novel"
        //    };

        //    Book book2 = new()
        //    {
        //        Id = 3,
        //        Title = "Solaris",
        //        Description = "opis",
        //        Author = "Stanislaw Lem",
        //        Type = "scifi"
        //    };
        //    await bookService.AddBook(book1);
        //    await bookService.AddBook(book2);

        //    List<Book> booksExpected = new List<Book> { book1 };
        //    //Act
        //    var books = await bookService.GetAllBooksByType("novel");

        //    //Assert
        //    booksExpected.Should().BeEquivalentTo(books);
        //}

        [Fact]
        public async Task Get_All_Proposed_Books_By_Type_When_No_Previous_Orders()
        {
            //Arrange
            UserService userService = new UserService();
            BookService bookService = new BookService();
            OrderService orderService = new OrderService();

            await userService.Register("user", "password");


            await bookService.RemoveAllBooks();
            Book book1 = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            Book book2 = new()
            {
                Id = 2,
                Title = "Lalka",
                Description = "Opis costam blabla",
                Author = "Boleslaw Prus",
                Type = "novel"
            };

            Book book3 = new()
            {
                Id = 3,
                Title = "Solaris",
                Description = "opis",
                Author = "Stanislaw Lem",
                Type = "scifi"
            };
            await bookService.AddBook(book1);
            await bookService.AddBook(book2);
            await bookService.AddBook(book3);

            await orderService.RemoveAllOrders();

            //Act
            var books = await bookService.GetAllProposedBooks("user");

            //Assert
            Assert.Empty(books);
        }

        //[Fact]
        //public async Task Get_All_Proposed_Books_By_Type_When_Nothing_Match()
        //{
        //    //Arrange
        //    UserService userService = new UserService();
        //    BookService bookService = new BookService();
        //    OrderService orderService = new OrderService();

        //    await bookService.RemoveAllBooks();
        //    Book book1 = new()
        //    {
        //        Id = 1,
        //        Title = "Pan Tadeusz",
        //        Description = "Opis Pana Tadeusza",
        //        Author = "Adam Mickiewicz",
        //        Type = "poetry"
        //    };

        //    Book book2 = new()
        //    {
        //        Id = 2,
        //        Title = "Lalka",
        //        Description = "Opis costam blabla",
        //        Author = "Boleslaw Prus",
        //        Type = "novel"
        //    };

        //    Book book3 = new()
        //    {
        //        Id = 3,
        //        Title = "Solaris",
        //        Description = "opis",
        //        Author = "Stanislaw Lem",
        //        Type = "scifi"
        //    };

        //    await bookService.AddBook(book1);
        //    await bookService.AddBook(book2);

        //    await userService.Register("user", "password");
        //    await orderService.RemoveAllOrders();

        //    List<Book> booksOrdered = new List<Book> { book3 };

        //    await orderService.CreateOrder(booksOrdered, "user");

        //    //Act
        //    var books = await bookService.GetAllProposedBooks("user");

        //    //Assert
        //    Assert.Empty(books);
        //}

        [Fact]
        public async Task Get_All_Proposed_Books_By_Type_When_One_Match()
        {
            //Arrange
            UserService userService = new UserService();
            BookService bookService = new BookService();
            OrderService orderService = new OrderService();

            await bookService.RemoveAllBooks();
            Book book1 = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            Book book2 = new()
            {
                Id = 2,
                Title = "Lalka",
                Description = "Opis costam blabla",
                Author = "Boleslaw Prus",
                Type = "novel"
            };

            Book book3 = new()
            {
                Id = 3,
                Title = "Solaris",
                Description = "opis",
                Author = "Stanislaw Lem",
                Type = "scifi"
            };


            await bookService.AddBook(book1);
            await bookService.AddBook(book2);
            await bookService.AddBook(book3);

            await userService.Register("user", "password");
            await orderService.RemoveAllOrders();

            List<Book> booksOrdered = new List<Book> { book3 };
            await orderService.CreateOrder(booksOrdered, "user");

            List<Book> booksExpected = new List<Book> { book3 };

            //Act
            var books = await bookService.GetAllProposedBooks("user");

            //Assert
            booksExpected.Should().BeEquivalentTo(books, options =>
                options.Excluding(o => o.Id));
        }

        [Fact]
        public async Task Get_All_Proposed_Books_By_Type_When_Multiple_Matches()
        {
            //Arrange
            UserService userService = new UserService();
            BookService bookService = new BookService();
            OrderService orderService = new OrderService();

            await bookService.RemoveAllBooks();
            Book book1 = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            Book book2 = new()
            {
                Id = 2,
                Title = "Lalka",
                Description = "Opis costam blabla",
                Author = "Boleslaw Prus",
                Type = "novel"
            };

            Book book3 = new()
            {
                Id = 3,
                Title = "Solaris",
                Description = "opis",
                Author = "Stanislaw Lem",
                Type = "scifi"
            };

            Book book4 = new()
            {
                Id = 4,
                Title = "Bajki Robotów",
                Description = "opis",
                Author = "Stanislaw Lem",
                Type = "scifi"
            };

            await bookService.AddBook(book1);
            await bookService.AddBook(book2);
            await bookService.AddBook(book3);
            await bookService.AddBook(book4);

            await userService.Register("user", "password");
            await orderService.RemoveAllOrders();

            List<Book> booksOrdered = new List<Book> { book3 };
            await orderService.CreateOrder(booksOrdered, "user");

            List<Book> booksExpected = new List<Book> { book3, book4 };
            //Act
            var books = await bookService.GetAllProposedBooks("user");

            //Assert
            booksExpected.Should().BeEquivalentTo(books, options =>
                options.Excluding(o => o.Id));
        }

        [Fact]
        public async Task Get_All_Proposed_Books_By_Type_When_Multiple_Orders()
        {
            //Arrange
            UserService userService = new UserService();
            BookService bookService = new BookService();
            OrderService orderService = new OrderService();

            await bookService.RemoveAllBooks();
            Book book1 = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            Book book2 = new()
            {
                Id = 2,
                Title = "Lalka",
                Description = "Opis costam blabla",
                Author = "Boleslaw Prus",
                Type = "novel"
            };

            Book book3 = new()
            {
                Id = 3,
                Title = "Solaris",
                Description = "opis",
                Author = "Stanislaw Lem",
                Type = "scifi"
            };

            Book book4 = new()
            {
                Id = 4,
                Title = "Bajki Robotów",
                Description = "opis",
                Author = "Stanislaw Lem",
                Type = "scifi"
            };

            Book book5 = new()
            {
                Id = 5,
                Title = "Potop",
                Description = "opis",
                Author = "Henryk Sienkiewicz",
                Type = "novel"
            };

            //Book book6 = new()
            //{
            //    Id = 6,
            //    Title = "Krzyżacy",
            //    Description = "opis",
            //    Author = "Henryk Sienkiewicz",
            //    Type = "novel"
            //};

            await bookService.AddBook(book1);
            await bookService.AddBook(book2);
            await bookService.AddBook(book3);
            await bookService.AddBook(book4);
            await bookService.AddBook(book5);

            await userService.Register("user", "password");
            await orderService.RemoveAllOrders();

            List<Book> booksOrdered1 = new List<Book> { book3, book2 };
            List<Book> booksOrdered2 = new List<Book> { book5 };

            await orderService.CreateOrder(booksOrdered1, "user");
            await orderService.CreateOrder(booksOrdered2, "user");

            List<Book> booksExpected = new List<Book> { book2, book5 };

            //Act
            var books = await bookService.GetAllProposedBooks("user");


            //Assert
            booksExpected.Should().BeEquivalentTo(books, options =>
                options.Excluding(o => o.Id));
        }
        
    }
}
