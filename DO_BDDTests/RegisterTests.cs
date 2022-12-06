using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using TO_BDD.Services;

namespace TO_BDDTests
{
    [TestClass]
    public class RegisterTests
    {
        [TestMethod]
        public void Valid_Register_Data()
        {
            //Arrange
            UserService userService = new UserService;
            string username = "user";
            string password = "password";

            //Act
            bool result = userService.Register(username, password);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
