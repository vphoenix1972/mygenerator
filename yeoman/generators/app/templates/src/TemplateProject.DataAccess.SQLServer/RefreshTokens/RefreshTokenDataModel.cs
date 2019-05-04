using System;
using <%= projectNamespace %>.DataAccess.SQLServer.Users;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.DataAccess.SQLServer.RefreshTokens
{
    public sealed class RefreshTokenDataModel : IEntity<long>
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public UserDataModel User { get; set; }
    }
}
