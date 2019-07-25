using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;
using <%= projectNamespace %>.Utils.Entities;
using <%= projectNamespace %>.Utils.EntityFrameworkCore;
using <%= projectNamespace %>.Utils.Factories;

namespace <%= projectNamespace %>.DataAccess.PostgreSQL.TodoItems
{
    internal sealed class TodoItemsRepository : RepositoryBase<ITodoItem, TodoItem, TodoItemDataModel>,
        ITodoItemsRepository
    {
        public TodoItemsRepository(ApplicationDbContext db,
            IMapper mapper,
            IFactory<TodoItem> todoItemsFactory) :
            base(db, mapper, db?.TodoItems, todoItemsFactory)
    {
    }

        public (IList<ITodoItem> Items, int Total) GetMany(string nameFilter = null, int? limit = null, int? skip = null, string orderBy = null, SortOrder? orderDirection = null)
        {
            var query = DbSet.AsQueryable();

            if (nameFilter != null)
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{nameFilter}%"));

            var total = query.Count();

            if (orderBy != null && orderDirection != null)
                query = orderDirection.Value == SortOrder.Asc ?
                    query.OrderBy($"{orderBy} asc") :
                    query.OrderBy($"{orderBy} desc");

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (limit.HasValue)
                query = query.Take(limit.Value);

            var items = query
                .Select(Map)
                .ToList();

            return (items, total);
        }
    }
}
