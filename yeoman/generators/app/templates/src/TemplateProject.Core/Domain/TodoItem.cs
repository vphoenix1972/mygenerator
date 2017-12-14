namespace <%= projectNamespace %>.Core.Domain
{
    public sealed class TodoItem : ITodoItem
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }
}