using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;
using <%= projectNamespace %>.DataAccess.Models;
using <%= projectNamespace %>.Utils.Factories;

namespace <%= projectNamespace %>.DataAccess.Repositories
{
    public sealed class TodoItemsRepository : RepositoryBase<ITodoItem, TodoItem, int, TodoItemDataModel>,
        ITodoItemsRepository
    {
        public TodoItemsRepository(ApplicationDbContext db,
            IFactory<TodoItem> todoItemsFactory) :
            base(db, db?.TodoItems, todoItemsFactory)
        {
        }

        protected override void Map(TodoItemDataModel source, TodoItem dest)
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