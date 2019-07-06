using System;
using System.Collections.Generic;
using System.Text;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;

namespace TemplateProject.DataAccess.MongoDB.TodoItems
{
    internal sealed class TodoItemsRepository : ITodoItemsRepository
    {
        public ITodoItem AddOrUpdate(ITodoItem item)
        {
            return item;
        }

        public void DeleteById(int id)
        {
            
        }

        public IList<ITodoItem> GetAll()
        {
            return new List<ITodoItem>();
        }

        public ITodoItem GetById(int id)
        {
            return null;
        }
    }
}
