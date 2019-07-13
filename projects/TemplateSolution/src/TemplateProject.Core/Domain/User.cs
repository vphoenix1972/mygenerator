﻿using System.Collections.Generic;

namespace TemplateProject.Core.Domain
{
    public sealed class User : IUser
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string PasswordEncrypted { get; set; }

        public IList<IUserRole> Roles { get; set; } = new List<IUserRole>();
    }
}