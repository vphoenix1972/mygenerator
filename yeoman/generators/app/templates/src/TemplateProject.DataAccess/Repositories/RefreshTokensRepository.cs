using System;
using System.Linq;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;
using <%= projectNamespace %>.DataAccess.Models;
using <%= projectNamespace %>.Utils.Factories;

namespace <%= projectNamespace %>.DataAccess.Repositories
{
    public sealed class RefreshTokensRepository : RepositoryBase<IRefreshToken, RefreshToken, long,
            RefreshTokenDataModel>,
        IRefreshTokensRepository
    {
        public RefreshTokensRepository(ApplicationDbContext db, IFactory<RefreshToken> refreshTokensFactory) :
            base(db, db.RefreshTokens, refreshTokensFactory)
        {
        }

        public void DeleteExpired(DateTime beforeUtc)
        {
            Db.RefreshTokens.RemoveRange(Db.RefreshTokens.Where(e => e.ExpiresUtc <= beforeUtc));
        }

        public void DeleteByUserId(int userId)
        {
            Db.RefreshTokens.RemoveRange(Db.RefreshTokens.Where(e => e.UserId == userId));
        }

        protected override void Map(RefreshTokenDataModel source, RefreshToken dest)
        {
            dest.Id = source.Id;
            dest.UserId = source.UserId;
            dest.ExpiresUtc = source.ExpiresUtc;
        }

        protected override void Map(IRefreshToken source, RefreshTokenDataModel dest)
        {
            dest.UserId = source.UserId;
            dest.ExpiresUtc = source.ExpiresUtc;
        }
    }
}