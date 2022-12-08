using TO_BDD.Models;
using TO_BDD.Services;

namespace TO_DBB_Tests
{
    public class RegisterTests
    {
        [Fact]
        public async Task Valid_Register_Data()
        {
            //Arrange
            UserService userService = new UserService();
            await userService.RemoveUser("user");
            string username = "user";
            string password = "password";

            //Act
            var result = await userService.Register(username, password);

            //Assert
            await userService.RemoveUser(username);
            Assert.True(result);
        }

        [Fact]
        public async void Invalid_Register_Data_Blank_Username()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "";
            string password = "password";

            //Act
            var result = await userService.Register(username, password);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void Invalid_Register_Data_Blank_Password()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "";

            //Act
            var result = await userService.Register(username, password);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void Invalid_Register_Data_User_Exists()
        {
            //Arrange
            UserService userService = new UserService();
            string username1 = "user";
            string password1 = "password";
            string username2 = "user";
            string password2 = "password";
            await userService.Register(username1, password1);

            //Act
            var result = await userService.Register(username2, password2);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void Invalid_Register_Data_Username_With_Special_Characters()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "us^er,";
            string password = "password";

            //Act
            var result = await userService.Register(username, password);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void Invalid_Register_Data_Password_With_Special_Characters()
        {
            //Arrange
            UserService userService = new UserService();
            string username = "user";
            string password = "passw,ord^";

            //Act
            var result = await userService.Register(username, password);

            //Assert
            Assert.False(result);
        }
    }
}