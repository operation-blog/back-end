using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.DAL.Entities;

namespace GC.BLL.Abstractions
{
    public interface IAccessTokenService
    {
        Task<AccessToken> CreateToken(User Creator);

        Task<IEnumerable<AccessToken>> GetAll(int lastId = 0);

        Task<int> GetTokensCount();

        Task<AccessToken> TokenExist(string token);

        Task MarkTokenUsed(AccessToken token, User user);
    }
}
