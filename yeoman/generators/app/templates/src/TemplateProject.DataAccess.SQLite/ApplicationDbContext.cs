using Microsoft.EntityFrameworkCore;
using <%= projectNamespace %>.DataAccess.SQLite.TodoItems;
using <%= projectNamespace %>.Utils.EntityFrameworkCore;

namespace <%= projectNamespace %>.DataAccess.SQLite
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
