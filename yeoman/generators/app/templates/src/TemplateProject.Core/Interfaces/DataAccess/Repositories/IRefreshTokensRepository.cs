using System;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public interface IRefreshTokensRepository : IRepositoryBase<IRefreshToken>
    {
        void DeleteExpired(DateTime beforeUtc);

        void DeleteByUserId(string id);
    }
}