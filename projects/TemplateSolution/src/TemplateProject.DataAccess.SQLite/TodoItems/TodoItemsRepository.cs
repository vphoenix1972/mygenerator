﻿using AutoMapper;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.Utils.EntityFrameworkCore;
using TemplateProject.Utils.Factories;

namespace TemplateProject.DataAccess.SQLite.TodoItems
{
    internal sealed class TodoItemsRepository : RepositoryBase<ITodoItem, TodoItem, int, TodoItemDataModel>,
        ITodoItemsRepository
    {
        public TodoItemsRepository(ApplicationDbContext db,
            IMapper mapper,
            IFactory<TodoItem> todoItemsFactory) :
            base(db, mapper, db?.TodoItems, todoItemsFactory)
    {
    }
}
}