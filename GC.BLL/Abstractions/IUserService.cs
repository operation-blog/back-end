using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.DAL.Entities;

namespace GC.BLL.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> GetByName(string name);

        Task<bool> UserExist(string username);

        Task<User> CreateUser(string username, string password);

        Task<bool> UpdateUser(User user, string username, string picture);

        Task<User> GetUserByDetails(string username, string password);
    }
}
