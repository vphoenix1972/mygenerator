using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Core.Domain
{
    public interface ITodoItem : IEntity<int?>
    {
        string Name { get; set; }
    }
}