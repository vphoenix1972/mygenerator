using Microsoft.EntityFrameworkCore;
using TemplateProject.DataAccess.SQLite.TodoItems;
using TemplateProject.Utils.EntityFrameworkCore;

namespace TemplateProject.DataAccess.SQLite
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
