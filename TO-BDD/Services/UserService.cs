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
        bool Login(string username, string password);
        bool Register(string username, string password);
    }
    public class UserService : IUserService
    {
        private DbRepository _dbRepository;
        public UserService(DbRepository dbRepository)
        {
            _dbRepository=dbRepository;
        }

        public bool Login(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            User user = GetUser(u => u.UserName.Equals(username));
            if(user == default)
            {
                return false;
            }

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return false;
            }

            return true;
        }

        public bool Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || 
                string.IsNullOrEmpty(password) ||
                !ContainsOnlyAlphaNumericCharacters(username) || 
                !ContainsOnlyAlphaNumericCharacters(password)
                )
            {
                return false;
            }

            if (GetUser(u => u.UserName.Equals(username))!=default)
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
            _dbRepository.Add(user);
            _dbRepository.SaveChanges();

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

        private User GetUser(Expression<Func<User, bool>> filter)
        {
            IQueryable<User> query = _dbRepository.Set<User>();
            query = query.Where(filter);
            User user = query.FirstOrDefault();
            return user;
        }

        private bool ContainsOnlyAlphaNumericCharacters(string inputString)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            return regexItem.IsMatch(inputString);
        }
    }
}
