using MongoDB.Bson;
using MongoDB.Driver;

namespace TemplateProject.DataAccess.MongoDB.Migrations
{
    internal sealed class Initial : IMigration
    {
        public void Up(IMongoDatabase database)
        {
            var collection = database.GetCollection<BsonDocument>("TodoItems");

            collection.InsertMany(new[]
            {
                new BsonDocument { {"Name", "Item 1"} },
                new BsonDocument { {"Name", "Item 2"} },
                new BsonDocument { {"Name", "Item 3"} }
            });
        }
    }
}