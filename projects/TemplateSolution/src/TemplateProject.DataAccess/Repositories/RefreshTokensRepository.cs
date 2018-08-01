using AutoMapper;
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
        public RefreshTokensRepository(ApplicationDbContext db, IMapper mapper, IFactory<RefreshToken> refreshTokensFactory) :
            base(db, mapper, db.RefreshTokens, refreshTokensFactory)
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
    }
}