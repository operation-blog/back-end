﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GF.DAL.Abstractions;
using GF.DAL.Entities;

namespace GC.BLL.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private IGenericRepository<AccessToken> _accessTokenRepository = null;

        public AccessTokenService(IGenericRepository<AccessToken> accessTokenRepository)
        {
            _accessTokenRepository = accessTokenRepository;
        }

        public async Task<AccessToken> CreateToken(User Creator)
        {
            AccessToken token = new AccessToken();

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = chars[random.Next(chars.Length)];

            var finalString = new string(stringChars);

            token.Creator = Creator;
            token.Token = finalString;

            _accessTokenRepository.Insert(token);
            await _accessTokenRepository.Save();

            return token;
        }

        public async Task<IEnumerable<AccessToken>> GetAll(int lastId)
        {
            return await _accessTokenRepository.GetAll(lastId);
        }

        public async Task<int> GetTokensCount()
        {
            return await _accessTokenRepository.GetCount();
        }

        public async Task<AccessToken> TokenExist(string token)
        {
            var found = await _accessTokenRepository.GetFirstFromQueryable(_accessTokenRepository.GetQueryable().Where(e => e.Token == token).Where(e => e.Used == false));

            return found;
        }

        public async Task MarkTokenUsed(AccessToken token, User user)
        {
            token.Used = true;
            token.UsedBy = user;

            await _accessTokenRepository.Save();
        }
    }
}
