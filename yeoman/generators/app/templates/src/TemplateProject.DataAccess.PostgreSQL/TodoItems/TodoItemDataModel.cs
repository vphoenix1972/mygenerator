using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.DataAccess.PostgreSQL.TodoItems
{
    public sealed class TodoItemDataModel : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
