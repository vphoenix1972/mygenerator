using System;

namespace <%= projectNamespace %>.Core.Domain
{
    public class RefreshToken : IRefreshToken
    {
        public long? Id { get; set; }

        public int UserId { get; set; }

        public DateTime ExpiresUtc { get; set; }
    }
}