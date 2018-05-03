using System;
using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Domain
{
    public interface IRefreshToken : IEntity<long?>
    {
        int UserId { get; }

        DateTime ExpiresUtc { get; set; }
    }
}