using Microsoft.EntityFrameworkCore;
using TemplateProject.DataAccess.SQLServer.TodoItems;
using TemplateProject.Utils.EntityFrameworkCore;

namespace TemplateProject.DataAccess.SQLServer
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
