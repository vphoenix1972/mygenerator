using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Domain
{
    public interface ITodoItem : IEntity<int?>
    {
        string Name { get; set; }
    }
}