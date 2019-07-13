using MongoDB.Driver;

namespace TemplateProject.DataAccess.MongoDB
{
    internal interface IMigration
    {
        void Up(IMongoDatabase database);
    }
}