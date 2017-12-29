using System;

namespace TemplateProject.Utils.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}