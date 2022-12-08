using Azure.Core;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TO_BDD.Models;
using TO_BDD.Repositories;

namespace TO_BDD.Services
{
    public interface IUserService
    {
        Task<bool> Login(string username, string password);
        Task<bool> Register(string username, string password);
        Task<int> GetUserId(string username);
        Task RemoveUser(string username);
    }
    public class UserService : IUserService
    {
        private DbRepository _db;
        public UserService()
        {
            _db = new DbRepository();
        }

        public async Task RemoveUser(string username)
        {
            string sql = $"DELETE FROM [dbo].[Users] WHERE UserName = '{username}'";
            await _db.Remove(sql);
        }

        public async Task<int> GetUserId(string username)
        {
            var user = await _db.LoadData<int>($"SELECT Id FROM [dbo].[Users] WHERE UserName = '{username}'");
            return user.FirstOrDefault();
        }

        public async Task<bool> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            List<User> users = await GetUser(username);
            if (users.Count < 1)
            {
                return false;
            }

            if (!VerifyPasswordHash(password, users[0].PasswordHash, users[0].PasswordSalt))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                !ContainsOnlyAlphaNumericCharacters(username) ||
                !ContainsOnlyAlphaNumericCharacters(password)
                )
            {
                return false;
            }
            List<User> users = await GetUser(username);

            if(users.Count > 0)
            {
                return false;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new()
            {
                UserName = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await AddUser(user);

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private async Task<List<User>> GetUser(string username)
        {
            string sql = $"SELECT * FROM [dbo].[Users] WHERE [UserName] = '{username}'";

            return await _db.LoadData<User>(sql);
        }

        private async Task AddUser(User user)
        {
            await _db.SaveUserData(user.UserName, user.PasswordHash, user.PasswordSalt);
        }

        private bool ContainsOnlyAlphaNumericCharacters(string inputString)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            return regexItem.IsMatch(inputString);
        }
    }
}
