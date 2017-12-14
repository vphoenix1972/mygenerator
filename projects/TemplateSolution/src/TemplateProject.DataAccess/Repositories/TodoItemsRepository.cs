using System.Collections.Generic;
using System.Linq;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;

namespace TemplateProject.DataAccess.Repositories
{
    public sealed class TodoItemsRepository : ITodoItemsRepository
    {
        private static readonly IList<ITodoItem> items = new List<ITodoItem>()
        {
            new TodoItem {Id = 1, Name = "Item 1"},
            new TodoItem {Id = 2, Name = "Item 2"},
            new TodoItem {Id = 3, Name = "Item 3"}
        };

        private static int _counter = 4;

        public IList<ITodoItem> GetAll()
        {
            return items.ToList();
        }

        public ITodoItem GetById(int id)
        {
            return items.FirstOrDefault(e => e.Id == id);
        }

        public ITodoItem AddOrUpdate(ITodoItem item)
        {
            if (item.Id.HasValue)
            {
                var existing = GetById(item.Id.Value);
                if (existing != null)
                {
                    Map(item, existing);

                    return existing;
                }
            }

            item.Id = _counter++;

            items.Add(item);

            return item;
        }

        public void DeleteById(int id)
        {
            var item = GetById(id);
            if (item == null)
                return;

            items.Remove(item);
        }

        private void Map(ITodoItem source, ITodoItem dest)
        {
            dest.Name = source.Name;
        }
    }
}