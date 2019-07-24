using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Web.Controllers.App.Todo
{
    public sealed class TodoController : AppControllerBase
    {
        private readonly IReadOnlyList<string> allowedColumnsToSortBy = new List<string>
        {
            nameof(ITodoItem.Id),
            nameof(ITodoItem.Name)
        };

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public TodoController(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("index")]
        public IActionResult Index(
            string nameFilter,
            int? limit,
            int? skip,
            string sortColumn,
            string sortDirection)
        {
            if (limit.HasValue && limit.Value < 1)
            {
                ModelState.AddModelError(nameof(limit), "Limit cannot be less than 1");
                return BadRequest(ModelState);
            }

            if (skip.HasValue && skip.Value < 0)
            {
                ModelState.AddModelError(nameof(skip), "Skip cannot be less than 0");
                return BadRequest(ModelState);
            }

            SortOrder? order = null;

            if (sortColumn != null)
            {
                if (allowedColumnsToSortBy.All(x => x != sortColumn))
                {
                    ModelState.AddModelError(nameof(sortColumn), $"Sort column '{sortColumn}' is not valid");
                    return BadRequest(ModelState);
                }

                if (sortDirection == null)
                {
                    ModelState.AddModelError(nameof(sortDirection), $"Sort direction '{sortDirection}' is not set");
                    return BadRequest(ModelState);
                }

                if (!Enum.TryParse(sortDirection, true, out SortOrder orderValue))
                {
                    ModelState.AddModelError(nameof(sortDirection), $"Sort direction '{sortDirection}' is not valid");
                    return BadRequest(ModelState);
                }

                order = orderValue;
            }

            var result = _databaseService.TodoItemsRepository.GetMany(nameFilter, limit, skip, sortColumn, order);

            return Ok(new { Items = _mapper.Map<List<TodoItemModel>>(result.Items), Total = result.Total });
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(string id)
        {
            var item = _databaseService.TodoItemsRepository.GetById(id);
            if (item == null)
                return ItemNotFound(id);

            return Ok(_mapper.Map<TodoItemModel>(item));
        }

        [HttpPost]
        public IActionResult Add([FromBody] TodoItemModel itemModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = _mapper.Map<TodoItem>(itemModel);

            var result = _databaseService.TodoItemsRepository.AddOrUpdate(item);

            _databaseService.SaveChanges();

            return Ok(_mapper.Map<TodoItemModel>(result));
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItemModel itemModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingItem = _databaseService.TodoItemsRepository.GetById(id);
            if (existingItem == null)
                return ItemNotFound(id);

            _mapper.Map(itemModel, existingItem);

            var result = _databaseService.TodoItemsRepository.AddOrUpdate(existingItem);

            _databaseService.SaveChanges();

            return Ok(_mapper.Map<TodoItemModel>(result));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _databaseService.TodoItemsRepository.DeleteById(id);

            _databaseService.SaveChanges();

            return Ok();
        }

        private IActionResult ItemNotFound<T>(T id)
        {
            ModelState.AddModelError(nameof(id), $"Item with id='{id}' is not found");
            return BadRequest(ModelState);
        }
    }
}