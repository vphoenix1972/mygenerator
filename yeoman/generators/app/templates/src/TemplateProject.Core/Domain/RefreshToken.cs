using System;

namespace <%= projectNamespace %>.Core.Domain
{
    public class RefreshToken : IRefreshToken
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime ExpiresUtc { get; set; }
    }
}