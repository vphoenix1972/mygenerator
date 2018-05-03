using System;
using TemplateProject.Core.Domain;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IRefreshTokensRepository : IRepositoryBase<IRefreshToken, long>
    {
        void DeleteExpired(DateTime beforeUtc);

        void DeleteByUserId(int id);
    }
}