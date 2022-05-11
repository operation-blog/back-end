using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GF.DAL.Abstractions;
using GF.DAL.Entities;

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

        public async Task<bool> UserExist(string username)
        {
            var users = await GetAll();

            foreach (var user in users)
                if (user.Username == username)
                    return true;

            return false;
        }

        public async Task<User> GetUserByDetails(string username, string password)
        {
            var users = await GetAll();

            foreach (var user in users)
                if (user.Username == username && user.Password == password)
                    return user;

            return null;
        }
    }
}
