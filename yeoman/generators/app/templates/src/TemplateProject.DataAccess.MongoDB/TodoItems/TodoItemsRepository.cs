using AutoMapper;
using MongoDB.Driver;
using <%= csprojName %>.Core.Domain;
using <%= csprojName %>.Core.Interfaces.DataAccess.Repositories;
using <%= csprojName %>.Utils.Factories;

namespace <%= csprojName %>.DataAccess.MongoDB.TodoItems
{
    internal sealed class TodoItemsRepository : RepositoryBase<ITodoItem, TodoItem, TodoItemDataModel>, ITodoItemsRepository
    {
        public TodoItemsRepository(IMongoDatabase db, IMapper mapper, IFactory<TodoItem> todoItemsFactory) :
            base("TodoItems", db, mapper, todoItemsFactory)
        {

        }
    }
}
