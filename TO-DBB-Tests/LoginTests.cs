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
            var userContextMock = new Mock<UserContext>();
            userContextMock.Setup(x => x.User.Register(It.IsAny<User>())).Returns((User u) => u);
            var userService = new UserService(userContextMock.Object);

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
            var userContextMock = new Mock<UserContext>();
            userContextMock.Setup(x => x.User.Register(It.IsAny<User>())).Returns((User u) => u);
            var userService = new UserService(userContextMock.Object);

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
            var userContextMock = new Mock<UserContext>();
            userContextMock.Setup(x => x.User.Register(It.IsAny<User>())).Returns((User u) => u);
            var userService = new UserService(userContextMock.Object);

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
            var userContextMock = new Mock<UserContext>();
            userContextMock.Setup(x => x.User.Register(It.IsAny<User>())).Returns((User u) => u);
            var userService = new UserService(userContextMock.Object);

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
            var userContextMock = new Mock<UserContext>();
            userContextMock.Setup(x => x.User.Register(It.IsAny<User>())).Returns((User u) => u);
            var userService = new UserService(userContextMock.Object);

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
