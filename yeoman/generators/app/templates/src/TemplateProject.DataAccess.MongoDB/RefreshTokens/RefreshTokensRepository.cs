using AutoMapper;
using MongoDB.Driver;
using System;
using <%= csprojName %>.Core.Domain;
using <%= csprojName %>.Core.Interfaces.DataAccess.Repositories;
using <%= csprojName %>.Utils.Factories;

namespace <%= csprojName %>.DataAccess.MongoDB.RefreshTokens
{
    internal sealed class RefreshTokensRepository : RepositoryBase<IRefreshToken, RefreshToken, RefreshTokenDataModel>,
            IRefreshTokensRepository
    {
        public RefreshTokensRepository(IMongoDatabase db, IMapper mapper, IFactory<RefreshToken> refreshTokensFactory) :
            base("RefreshTokens", db, mapper, refreshTokensFactory)
        {

        }

        public void DeleteExpired(DateTime beforeUtc)
        {
            Collection.DeleteMany(x => x.ExpiresUtc <= beforeUtc);
        }

        public void DeleteByUserId(string userId)
        {
            Collection.DeleteMany(x => x.UserId == userId);
        }
    }
}
