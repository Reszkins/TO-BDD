using Moq;
using TO_BDD.Enums;
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

            bookService.RemoveAllBooks();

            //Act
            List<Book> books = await bookService.GetAllBooks();

            //Assert
            Assert.Empty(books);

        }

        [Fact]
        public void Get_All_Books_When_One_Book_In_List()
        {
            //Arrange
            BookService bookService = new BookService();
            bookService.RemoveAllBooks();
            Book book = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
            bookService.AddBook(book);

            //Act
            List<Book> books = bookService.GetAllBooks();

            //Assert
            List<Book> booksExpected = new List<Book>();
            booksExpected.Add(book);

            Assert.Equal(booksExpected, books);

        }

        [Fact]
        public void Get_All_Books_When_Multiple_Books_In_List()
        {
            //Arrange
            BookService bookService = new BookService();
            bookService.RemoveAllBooks();
            Book book1 = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "historyczne"
            };
            //Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));  <- tak nie


            Book book2 = new Book("Lalka", "Opis costam blabla", "Bolesław Prus", new BookType("Powieść"));
            Book book3 = new Book("Solaris", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            bookService.AddBook(book1);
            bookService.AddBook(book2);
            bookService.AddBook(book3);

            //Act
            List<Book> books = bookService.GetAllBooks();

            //Assert
            List<Book> booksExpected = new List<Book>();
            booksExpected.Add(book1);
            booksExpected.Add(book2);
            booksExpected.Add(book3);

            Assert.Equal(booksExpected, books);

        }

        [Fact]
        public void Get_All_Books_By_Type_When_List_Empty()
        {
            //Arrange
            BookService bookService = new BookService();

            bookService.RemoveAllBooks();

            //Act
            List<Book> books = bookService.GetAllBookByType(new BookType("Powieść"));

            //Assert
            Assert.Empty(books);

        }

        [Fact]
        public void Get_All_Books_By_Type_When_No_Type()
        {
            //Arrange
            BookService bookService = new BookService();

            bookService.RemoveAllBooks();
            Book book = new Book("Solaris", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            bookService.AddBook(book);

            //Act
            List<Book> books = bookService.GetAllBookByType(new BookType("Powieść"));

            //Assert
            Assert.Empty(books);
        }

        [Fact]
        public void Get_All_Books_By_Type_When_Only_That_Type()
        {
            //Arrange
            BookService bookService = new BookService();

            bookService.RemoveAllBooks();
            Book book = new Book("Lalka", "Opis costam blabla", "Bolesław Prus", new BookType("Powieść"));
            bookService.AddBook(book);

            //Act
            List<Book> books = bookService.GetAllBookByType(new BookType("Powieść"));

            //Assert
            List<Book> booksExpected = new List<Book>();
            booksExpected.Add(book);

            Assert.Equal(booksExpected, books);
        }

        [Fact]
        public void Get_All_Books_By_Type_When_Multiple_Types()
        {
            //Arrange
            BookService bookService = new BookService();

            bookService.RemoveAllBooks();
            Book book1 = new Book("Lalka", "Opis costam blabla", "Bolesław Prus", new BookType("Powieść"));
            Book book2 = new Book("Solaris", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            bookService.AddBook(book1);
            bookService.AddBook(book2);

            //Act
            List<Book> books = bookService.GetAllBookByType(new BookType("Powieść"));

            //Assert
            List<Book> booksExpected = new List<Book>();
            booksExpected.Add(book1);

            Assert.Equal(booksExpected, books);
        }

        [Fact]
        public void Get_All_Proposed_Books_By_Type_When_No_Previous_Orders()
        {
            //Arrange
            BookService bookService = new BookService();
            OrderService orderService = new OrderService();

            bookService.RemoveAllBooks();
            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
            Book book2 = new Book("Lalka", "Opis costam blabla", "Bolesław Prus", new BookType("Powieść"));
            Book book3 = new Book("Solaris", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            bookService.AddBook(book1);
            bookService.AddBook(book2);
            bookService.AddBook(book3);

            orderService.RemoveAllOrders();

            //Act
            List<Book> books = bookService.GetAllProposedBooks();

            //Assert
            Assert.Empty(books);
        }

        [Fact]
        public void Get_All_Proposed_Books_By_Type_When_Nothing_Match()
        {
            //Arrange
            BookService bookService = new BookService();
            CartService cartService = new CartService();
            OrderService orderService = new OrderService();

            bookService.RemoveAllBooks();
            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
            Book book2 = new Book("Lalka", "Opis costam blabla", "Bolesław Prus", new BookType("Powieść"));
            Book book3 = new Book("Solaris", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            bookService.AddBook(book1);
            bookService.AddBook(book2);
            bookService.AddBook(book3);

            cartService.RemoveAllBooks();
            cartService.AddToCart("Pan Tadeusz"); //nwm czemu dodaje sie string a nie objekt Book ale ok
            Cart cart = cartService.GetCart();

            orderService.RemoveAllOrders();
            orderService.CreateOrder(cart);

            //Act
            List<Book> books = bookService.GetAllProposedBooks();

            //Assert
            Assert.Empty(books);  //zakladamy ze nie podpowiada ksiazki ktora juz kupil
        }

        [Fact]
        public void Get_All_Proposed_Books_By_Type_When_One_Match()
        {
            //Arrange
            BookService bookService = new BookService();
            CartService cartService = new CartService();
            OrderService orderService = new OrderService();

            bookService.RemoveAllBooks();
            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
            Book book2 = new Book("Lalka", "Opis costam blabla", "Bolesław Prus", new BookType("Powieść"));
            Book book3 = new Book("Solaris", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            Book book4 = new Book("Bajki Robotów", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            bookService.AddBook(book1);
            bookService.AddBook(book2);
            bookService.AddBook(book3);
            bookService.AddBook(book4);

            cartService.RemoveAllBooks();
            cartService.AddToCart("Solaris");
            Cart cart = cartService.GetCart();

            orderService.RemoveAllOrders();
            orderService.CreateOrder(cart);

            //Act
            List<Book> books = bookService.GetAllProposedBooks();

            //Assert
            List<Book> booksExpected = new List<Book>();
            booksExpected.Add(book4);

            Assert.Equal(booksExpected, books);
        }

        [Fact]
        public void Get_All_Proposed_Books_By_Type_When_Multiple_Orders()
        {
            //Arrange
            BookService bookService = new BookService();
            CartService cartService = new CartService();
            OrderService orderService = new OrderService();

            bookService.RemoveAllBooks();
            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
            Book book2 = new Book("Lalka", "Opis costam blabla", "Bolesław Prus", new BookType("Powieść"));
            Book book3 = new Book("Solaris", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            Book book4 = new Book("Bajki Robotów", "opis", "Stanisław Lem", new BookType("Science Fiction"));
            Book book5 = new Book("Potop", "opis", "Henryk Sienkiewicz", new BookType("Powieść"));
            Book book6 = new Book("Krzyżacy", "opis", "Henryk Sienkiewicz", new BookType("Powieść"));
            bookService.AddBook(book1);
            bookService.AddBook(book2);
            bookService.AddBook(book3);
            bookService.AddBook(book4);
            bookService.AddBook(book5);
            bookService.AddBook(book6);

            cartService.RemoveAllBooks();
            cartService.AddToCart("Solaris");
            cartService.AddToCart("Lalka");
            Cart cart1 = cartService.GetCart();

            cartService.RemoveAllBooks();
            cartService.AddToCart("Potop");
            Cart cart2 = cartService.GetCart();

            orderService.RemoveAllOrders();
            orderService.CreateOrder(cart1);
            orderService.CreateOrder(cart2);

            //Act
            List<Book> books = bookService.GetAllProposedBooks();

            //Assert
            List<Book> booksExpected = new List<Book>();
            booksExpected.Add(book6);

            Assert.Equal(booksExpected, books);
        }


    }
}
