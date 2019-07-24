using MongoDB.Bson;
using MongoDB.Driver;

namespace <%= csprojName %>.DataAccess.MongoDB.Migrations
{
    internal sealed class Initial : IMigration
    {
        public void Up(IMongoDatabase database)
        {
            var collection = database.GetCollection<BsonDocument>("TodoItems");

            var items = new BsonDocument[100];

            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new BsonDocument { { "Name", $"Item {i + 1}" } };
            }

            collection.InsertMany(items);
        }
    }
}