using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO_BDD.Models;
using TO_BDD.Services;

namespace TO_DBB_Tests
{
    public class OrderTests
    {
        [Fact]
        public async Task Correctly_create_order_with_one_book()
        {
            //Arrange
            UserService userService = new UserService();
            OrderService orderService = new OrderService();
            Book book = new()
            {
                Id = 1,
                Title = "Pan Tadeusz",
                Description = "Opis Pana Tadeusza",
                Author = "Adam Mickiewicz",
                Type = "poetry"
            };

            await userService.Register("user", "password");
            await orderService.RemoveAllOrders();

            List<Book> booksOrdered = new List<Book> { book };
            var bookString = "";

            foreach (var bookInOrder in booksOrdered)
            {
                bookString += book.Title + ",";
            }

            //Act
            await orderService.CreateOrder(booksOrdered, "user");
            var result = await orderService.GetAllOrders("user");

            //Assert
            bookString.Should().BeEquivalentTo(result.First().Books);
        }

        [Fact]
        public async Task Correctly_create_order_with_no_books()
        {
            //Arrange
            UserService userService = new UserService();
            OrderService orderService = new OrderService();

            await userService.Register("user", "password");
            await orderService.RemoveAllOrders();
            List<Book> booksOrdered = new List<Book>();

            //Act
            await orderService.CreateOrder(booksOrdered, "user");

            //Assert
            var result = await orderService.GetAllOrders("user");
            Assert.Empty(result);
        }

        [Fact]
        public async Task Correctly_return_orders_when_non_exist()
        {
            //Arrange
            UserService userService = new UserService();
            OrderService orderService = new OrderService();

            await userService.Register("user", "password");
            await orderService.RemoveAllOrders();


            //Act
            var result = await orderService.GetAllOrders("user");

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Correctly_return_orders_when_exist()
        {
            //Arrange
            UserService userService = new UserService();
            CartService cartService = new CartService();
            OrderService orderService = new OrderService();

            await userService.Register("user", "password");
            await orderService.RemoveAllOrders();

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
                Author = "Bolesław Prus",
                Type = "novel"
            };

            await userService.Register("user", "password");
            await orderService.RemoveAllOrders();

            List<Book> booksOrdered1 = new List<Book> { book1 };
            await orderService.CreateOrder(booksOrdered1, "user");

            List<Book> booksOrdered2 = new List<Book> { book2 };
            await orderService.CreateOrder(booksOrdered2, "user");


            //Act
            var result = await orderService.GetAllOrders("user");

            //Assert
            Assert.True(result.Count == 2);
        }
    }
}