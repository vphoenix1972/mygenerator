using TemplateProject.Utils.Entities;

namespace TemplateProject.DataAccess.Models
{
    public sealed class TodoItemDataModel : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}