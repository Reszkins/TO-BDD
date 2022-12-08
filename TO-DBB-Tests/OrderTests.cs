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
//    public class OrderTests
//    {
//        [Fact]
//        public void Correctly_create_order_when_basket_non_empty()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            OrderService orderService = new OrderService();
//            cartService.RemoveBooks();
//            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
//            cartService.AddToCart(book1);

//            //Act
//            bool result = orderService.CreateOrder(cartService.getCart());

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void Correctly_create_order_when_empty_basket()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            OrderService orderService = new OrderService();
//            cartService.RemoveBooks();
//            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));


//            //Act
//            bool result = orderService.CreateOrder(cartService.getCart());

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void Correctly_return_orders_when_non_exist()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            OrderService orderService = new OrderService();
//            cartService.RemoveBooks();
//            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
//            Book book2 = new Book("Przedwiosnie", "opis", "Zeromski", new BookType("Poezja epicka"));


//            //Act
//            int result = orderService.getAllOrders();

//            //Assert
//            Assert.True(result == 0);
//        }

//        [Fact]
//        public void Correctly_return_orders_when_exist()
//        {
//            //Arrange
//            CartService cartService = new CartService();
//            OrderService orderService = new OrderService();
//            cartService.RemoveBooks();
//            Book book1 = new Book("Pan Tadeusz", "Opis Pana Tadeusza", "Adam Mickiewicz", new BookType("Poezja epicka"));
//            Book book2 = new Book("Przedwiosnie", "opis", "Zeromski", new BookType("Poezja epicka"));
//            cartService.AddToCart(book1);
//            cartService.AddToCart(book2);
//            orderService.CreateOrder(cartService.getCart());

//            //Act
//            int result = orderService.getAllOrders();

//            //Assert
//            Assert.True(result != 0);
//        }
//    }
//}