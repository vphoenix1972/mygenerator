﻿namespace TemplateProject.DataAccess.PostgreSQL.Users
{
    public class UserRoleUserDataModel
    {
        public int UserId { get; set; }
        public UserDataModel User { get; set; }

        public int RoleId { get; set; }
        public UserRoleDataModel Role { get; set; }
    }
}
