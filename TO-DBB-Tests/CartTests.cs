//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TO_BDD.Enums;
//using TO_BDD.Models;
//using TO_BDD.Services;

//namespace TO_DBB_Tests
//{
//    public class CartTests
//    {
//        [Fact]
//        public void Correctly_added_book_to_empty_basket()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            cartService.RemoveBooks();
//            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));

//            //Act
//            bool result = cartService.AddToCard(book1);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void Correctly_added_book_to_non_empty_basket()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            cartService.RemoveBooks();

//            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
//            Book book2 = new Book("Przedwiosnie", "Opis Przedwiosnia", "Stefan Zeromski", new BookType("Poezja epicka"));
//            cartService.AddToCard(book1);

//            //Act
//            bool result = cartService.AddToCard(book2);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void Correctly_delete_book_from_non_empty_basket()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            cartService.RemoveBooks();

//            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
//            cartService.AddToCard(book1);

//            //Act
//            bool result = cartService.RemoveFromCart(book1);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void Correctly_delete_book_from_empty_basket()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            cartService.RemoveBooks();

//            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));

//            //Act
//            bool result = cartService.RemoveFromCart(book1);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void Correctly_delete_non_existing_book_from_empty_basket()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            cartService.RemoveBooks();

//            Book book1 = new Book("Nieistniejaca ksiazka", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));

//            //Act
//            bool result = cartService.RemoveFromCart(book1);

//            //Assert
//            Assert.True(result);
//        }

//    }
//}