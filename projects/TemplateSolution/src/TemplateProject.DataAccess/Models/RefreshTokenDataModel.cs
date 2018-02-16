using System;
using TemplateProject.Utils.Entities;

namespace TemplateProject.DataAccess.Models
{
    public sealed class RefreshTokenDataModel : IEntity<long>
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public UserDataModel User { get; set; }
    }
}