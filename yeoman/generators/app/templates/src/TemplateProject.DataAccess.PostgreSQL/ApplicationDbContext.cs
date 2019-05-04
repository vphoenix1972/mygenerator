using Microsoft.EntityFrameworkCore;
using <%= projectNamespace %>.DataAccess.PostgreSQL.TodoItems;
using <%= projectNamespace %>.Utils.EntityFrameworkCore;

namespace <%= projectNamespace %>.DataAccess.PostgreSQL
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
