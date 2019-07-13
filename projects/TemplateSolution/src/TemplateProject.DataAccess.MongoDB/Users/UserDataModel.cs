using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using TemplateProject.Utils.Entities;

namespace TemplateProject.DataAccess.MongoDB.Users
{
    internal sealed class UserDataModel : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string PasswordEncrypted { get; set; }

        public List<string> Roles { get; set; }
    }
}
