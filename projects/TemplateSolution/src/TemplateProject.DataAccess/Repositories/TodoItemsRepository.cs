using System;
using System.Collections.Generic;
using System.Linq;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.DataAccess.Models;
using TemplateProject.Utils.Factories;

namespace TemplateProject.DataAccess.Repositories
{
    public sealed class TodoItemsRepository : RepositoryBase<ITodoItem, int, TodoItemDataModel>,
        ITodoItemsRepository
    {
        public TodoItemsRepository(ApplicationDbContext db,
            IFactory<ITodoItem> todoItemsFactory) :
            base(db, db?.TodoItems, todoItemsFactory)
        {
        }

        protected override void Map(TodoItemDataModel source, ITodoItem dest)
        {
            dest.Id = source.Id;
            dest.Name = source.Name;
        }

        protected override void Map(ITodoItem source, TodoItemDataModel dest)
        {
            dest.Name = source.Name;
        }
    }
}