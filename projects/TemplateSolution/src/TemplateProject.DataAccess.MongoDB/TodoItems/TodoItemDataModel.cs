using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TemplateProject.Utils.Entities;

namespace TemplateProject.DataAccess.MongoDB.TodoItems
{
    public sealed class TodoItemDataModel : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
