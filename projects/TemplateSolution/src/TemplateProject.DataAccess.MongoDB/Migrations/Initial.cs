using MongoDB.Bson;
using MongoDB.Driver;

namespace TemplateProject.DataAccess.MongoDB.Migrations
{
    internal sealed class Initial : IMigration
    {
        public void Up(IMongoDatabase db)
        {
            SeedTodoItems(db);
            SeedUsers(db);
        }

        private void SeedTodoItems(IMongoDatabase db)
        {
            var collection = db.GetCollection<BsonDocument>("TodoItems");

            var items = new BsonDocument[100];

            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new BsonDocument { { "Name", $"Item {i + 1}" } };
            }

            collection.InsertMany(items);
        }

        private void SeedUsers(IMongoDatabase db)
        {
            var collection = db.GetCollection<BsonDocument>("Users");

            collection.InsertMany(new[]
            {
                new BsonDocument {
                    { "Name", "admin" },
                    { "EMail", "admin@gmail.com" },
                    { "PasswordEncrypted", "81dc9bdb52d04dc20036dbd8313ed055" },
                    { "Roles", new BsonArray { "admin", "user" } }
                },
                new BsonDocument {
                    { "Name", "user" },
                    { "EMail", "user@gmail.com" },
                    { "PasswordEncrypted", "c4ca4238a0b923820dcc509a6f75849b" },
                    { "Roles", new BsonArray { "user" } }
                }
            });
        }
    }
}