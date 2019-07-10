using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace <%= csprojName %>.DataAccess.MongoDB.TodoItems
{
    public sealed class TodoItemDataModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}