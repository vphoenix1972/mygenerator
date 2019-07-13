using System;
using TemplateProject.Core.Domain;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IRefreshTokensRepository : IRepositoryBase<IRefreshToken>
    {
        void DeleteExpired(DateTime beforeUtc);

        void DeleteByUserId(string id);
    }
}