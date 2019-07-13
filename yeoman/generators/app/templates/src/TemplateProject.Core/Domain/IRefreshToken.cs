using System;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Core.Domain
{
    public interface IRefreshToken : IEntity<string>
    {
        string UserId { get; }

        DateTime ExpiresUtc { get; set; }
    }
}