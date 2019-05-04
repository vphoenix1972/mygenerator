using TemplateProject.Utils.Entities;

namespace TemplateProject.DataAccess.SQLite.TodoItems
{
    public sealed class TodoItemDataModel : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
