using TO_BDD.Models;
using TO_BDD.Services;

namespace TO_DBB_Tests
{
    public class LoginTests
    {
        [Fact]
        public async Task Valid_Login_Data()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            await userService.Register(username, password);

            //Act
            var result = await userService.Login(username, password);

            //Assert
            await userService.RemoveUser(username);
            Assert.True(result);
        }

        [Fact]
        public async Task Invalid_Login_Data_Invalid_Username()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            await userService.Register(username, password);

            //Act
            var result = await userService.Login("user1", password);

            //Assert
            await userService.RemoveUser(username);
            Assert.False(result);
        }

        [Fact]
        public async Task Invalid_Login_Data_Invalid_Password()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            await userService.Register(username, password);

            //Act
            var result = await userService.Login(username, "password1");

            //Assert
            await userService.RemoveUser(username);
            Assert.False(result);
        }

        [Fact]
        public async Task Invalid_Login_Data_Invalid_Blank_Username()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            await userService.Register(username, password);

            //Act
            var result = await userService.Login("", password);

            //Assert
            await userService.RemoveUser(username);
            Assert.False(result);
        }

        [Fact]
        public async Task Invalid_Login_Data_Invalid_Blank_Password()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "password";
            await userService.Register(username, password);

            //Act
            var result = await userService.Login(username, "");

            //Assert
            await userService.RemoveUser(username);
            Assert.False(result);
        }
    }
}