using System;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Core.Domain
{
    public interface IRefreshToken : IEntity<long?>
    {
        int UserId { get; }

        DateTime ExpiresUtc { get; set; }
    }
}