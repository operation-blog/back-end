using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GF.DAL.Abstractions;
using GF.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GC.BLL.Services
{
    public class UserService : IUserService
    {
        private IGenericRepository<User> _userRepository = null;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User> GetByName(string name)
        {
            var user = await _userRepository.Where(e => e.Username == name).FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> UserExist(string username)
        {
            var user = await _userRepository.Where(e => e.Username == username).FirstOrDefaultAsync();

            return user != null;
        }

        public async Task<User> CreateUser(string username, string password)
        {
            var nameExist = await UserExist(username);

            if (nameExist)
                return null;

            User user = new User();

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            user.Username = username;
            user.Password = hash[salt.Length..];
            user.Salt = salt;

            _userRepository.Insert(user);
            await _userRepository.Save();

            return user;
        }

        public async Task<User> GetUserByDetails(string username, string password)
        {
            var user = await GetByName(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Salt + user.Password))
                return null;

            return user;
        }
    }
}
