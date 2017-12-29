using System;

namespace <%= projectNamespace %>.Utils.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}