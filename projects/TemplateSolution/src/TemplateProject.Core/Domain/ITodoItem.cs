using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Domain
{
    public interface ITodoItem : IEntity<string>
    {
        string Name { get; set; }
    }
}