using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using <%= csprojName %>.Utils.Entities;

namespace <%= csprojName %>.DataAccess.MongoDB.RefreshTokens
{
    internal sealed class RefreshTokenDataModel : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime ExpiresUtc { get; set; }
    }
}
