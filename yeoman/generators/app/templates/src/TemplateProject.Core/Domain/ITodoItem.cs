using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Core.Domain
{
    public interface ITodoItem : IEntity<string>
    {
        string Name { get; set; }
    }
}