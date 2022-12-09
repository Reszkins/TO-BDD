using FluentAssertions;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO_BDD.Enums;
using TO_BDD.Models;
using TO_BDD.Services;

namespace TO_DBB_Tests
{
    public class CartTests
    {
        [Fact]
        public void Correctly_added_book_to_empty_basket()
        {
            //Arrange
            CartService cartService = new CartService();
            cartService.RemoveAllFromBasket();
            Book book = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            //Act
            cartService.AddToCart(book);

            //Assert
            List<Book> books = cartService.GetBooksFromCart();

            book.Should().BeEquivalentTo(books.First(), options =>
                options.Excluding(o => o.Id));
        }

        [Fact]
        public void Correctly_added_book_to_non_empty_basket()
        {
            //Arrange
            CartService cartService = new CartService();
            cartService.RemoveAllFromBasket();

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

            cartService.AddToCart(book1);

            //Act
            cartService.AddToCart(book2);

            //Assert
            List<Book> books = cartService.GetBooksFromCart();
            List<Book> booksExpected = new List<Book> { book1, book2 };

            booksExpected.Should().BeEquivalentTo(books, options =>
                options.Excluding(o => o.Id));

        }

        [Fact]
        public void Correctly_delete_book_from_non_empty_basket()
        {
            //Arrange
            CartService cartService = new CartService();
            cartService.RemoveAllFromBasket();

            Book book = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            cartService.AddToCart(book);

            //Act
            cartService.RemoveFromCart(book);

            //Assert
            List<Book> books = cartService.GetBooksFromCart();
            Assert.Empty(books);
        }

        [Fact]
        public void Correctly_delete_book_from_empty_basket()
        {
            //Arrange
            CartService cartService = new CartService();
            cartService.RemoveAllFromBasket();

            Book book = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            //Act
            cartService.RemoveFromCart(book);

            //Assert
            List<Book> books = cartService.GetBooksFromCart();
            Assert.Empty(books);
        }

        //ten test jest bez sensu bo nie dodajemy ksiazki jakby z bazy
        //[Fact]
        //public void Correctly_delete_non_existing_book_from_empty_basket()
        //{
        //    //Arrange
        //    CartService cartService = new CartService();
        //    cartService.RemoveAllFromBasket();

        //    Book book = new()
        //    {
        //        Id = 1,
        //        Title = "Nieistniejaca ksiazka",
        //        Description = "Opis Pana Tadeusza",
        //        Author = "Adam Mickiewicz",
        //        Type = "poetry"
        //    };

        //    //Act
        //    cartService.RemoveFromCart(book);

        //    //Assert
        //}

    }
}