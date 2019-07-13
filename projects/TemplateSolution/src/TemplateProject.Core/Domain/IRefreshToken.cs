using System;
using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Domain
{
    public interface IRefreshToken : IEntity<string>
    {
        string UserId { get; }

        DateTime ExpiresUtc { get; set; }
    }
}