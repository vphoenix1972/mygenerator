using AutoMapper;
using System;
using System.Linq;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.Utils.EntityFrameworkCore;
using TemplateProject.Utils.Factories;

namespace TemplateProject.DataAccess.SQLite.RefreshTokens
{
    public sealed class RefreshTokensRepository : RepositoryKeyLongIdStringBase<IRefreshToken, RefreshToken, RefreshTokenDataModel>,
                IRefreshTokensRepository
    {
        private ApplicationDbContext _db;

        public RefreshTokensRepository(ApplicationDbContext db, IMapper mapper, IFactory<RefreshToken> refreshTokensFactory) :
            base(db, mapper, db.RefreshTokens, refreshTokensFactory)
        {
            _db = db;
        }

        public void DeleteExpired(DateTime beforeUtc)
        {
            _db.RefreshTokens.RemoveRange(_db.RefreshTokens.Where(e => e.ExpiresUtc <= beforeUtc));
        }

        public void DeleteByUserId(string userId)
        {
            _db.RefreshTokens.RemoveRange(_db.RefreshTokens.Where(e => e.UserId == MapKey(userId)));
        }
    }
}
