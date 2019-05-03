using Microsoft.EntityFrameworkCore;
using TemplateProject.DataAccess.PostgreSQL.TodoItems;
using TemplateProject.Utils.EntityFrameworkCore;

namespace TemplateProject.DataAccess.PostgreSQL
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
