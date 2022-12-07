using Moq;
using TO_BDD.Models;
using TO_BDD.Services;

namespace TO_DBB_Tests
{
    public class LoginTests
    {
        [Fact]
        public void Valid_Login_Data()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            userService.Register(username, password);

            //Act
            bool result = userService.Login(username, password);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Invalid_Login_Data_Invalid_Username()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            userService.Register(username, password);

            //Act
            bool result = userService.Login("user1", password);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Invalid_Login_Data_Invalid_Password()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            userService.Register(username, password);

            //Act
            bool result = userService.Login(username, "password1");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Invalid_Login_Data_Invalid_Blank_Username()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            userService.Register(username, password);

            //Act
            bool result = userService.Login("", password);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Invalid_Login_Data_Invalid_Blank_Password()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            userService.Register(username, password);

            //Act
            bool result = userService.Login(username, "");

            //Assert
            Assert.False(result);
        }
    }
}
