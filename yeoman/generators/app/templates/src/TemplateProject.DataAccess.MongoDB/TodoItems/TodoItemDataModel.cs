using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using <%= csprojName %>.Utils.Entities;

namespace <%= csprojName %>.DataAccess.MongoDB.TodoItems
{
    public sealed class TodoItemDataModel : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}