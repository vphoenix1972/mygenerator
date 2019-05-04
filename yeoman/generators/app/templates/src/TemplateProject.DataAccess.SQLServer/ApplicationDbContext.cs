using Microsoft.EntityFrameworkCore;
using <%= projectNamespace %>.DataAccess.SQLServer.TodoItems;
using <%= projectNamespace %>.Utils.EntityFrameworkCore;

namespace <%= projectNamespace %>.DataAccess.SQLServer
{
    public sealed class ApplicationDbContext : DbContextWithSaveChangesHooks
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        public DbSet<TodoItemDataModel> TodoItems { get; set; }
    }
}
