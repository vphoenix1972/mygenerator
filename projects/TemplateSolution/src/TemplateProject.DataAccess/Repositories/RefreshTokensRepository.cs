using System;
using System.Linq;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.DataAccess.Models;
using TemplateProject.Utils.Factories;

namespace TemplateProject.DataAccess.Repositories
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