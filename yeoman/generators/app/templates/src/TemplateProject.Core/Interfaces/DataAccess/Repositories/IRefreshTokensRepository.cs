using System;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public interface IRefreshTokensRepository : IRepositoryBase<IRefreshToken, long>
    {
        void DeleteExpired(DateTime beforeUtc);

        void DeleteByUserId(int id);
    }
}