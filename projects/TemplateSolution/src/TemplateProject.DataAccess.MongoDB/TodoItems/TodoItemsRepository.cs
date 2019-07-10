using AutoMapper;
using MongoDB.Driver;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.Utils.Factories;

namespace TemplateProject.DataAccess.MongoDB.TodoItems
{
    internal sealed class TodoItemsRepository : RepositoryBase<ITodoItem, TodoItem, TodoItemDataModel>, ITodoItemsRepository
    {
        public TodoItemsRepository(IMongoDatabase db, IMapper mapper, IFactory<TodoItem> todoItemsFactory) :
            base("TodoItems", db, mapper, todoItemsFactory)
        {
            
        }
    }
}
