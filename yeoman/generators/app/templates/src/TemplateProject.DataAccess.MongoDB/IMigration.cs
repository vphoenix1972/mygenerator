using MongoDB.Driver;

namespace <%= csprojName %>.DataAccess.MongoDB
{
    internal interface IMigration
    {
        void Up(IMongoDatabase database);
    }
}