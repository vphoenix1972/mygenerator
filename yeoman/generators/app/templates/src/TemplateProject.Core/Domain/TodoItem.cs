namespace <%= projectNamespace %>.Core.Domain
{
    public sealed class TodoItem : ITodoItem
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}