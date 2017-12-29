using System;
using System.Collections.Generic;
using System.Linq;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;
using <%= projectNamespace %>.DataAccess.Models;
using <%= projectNamespace %>.Utils.Factories;

namespace <%= projectNamespace %>.DataAccess.Repositories
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