using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Utils.Entities;

namespace TemplateProject.Web.Controllers.App.Todo
{
    public sealed class TodoItemsController : ApiControllerBase
    {
        private readonly int DefaultLimit = 10;
        private readonly int DefaultSkip = 0;
        private readonly IReadOnlyList<string> allowedColumnsToSortBy = new List<string>
        {
            nameof(ITodoItem.Id),
            nameof(ITodoItem.Name)
        };

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public TodoItemsController(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetMany(
            string nameFilter,
            int? limit,
            int? skip,
            string sortColumn,
            string sortDirection)
        {
            if (limit.HasValue)
            {
                if (limit.Value < 1)
                {
                    ModelState.AddModelError(nameof(limit), "Limit cannot be less than 1");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                limit = DefaultLimit;
            }

            if (skip.HasValue && skip.Value < 0)
            {
                if (skip.Value < 0)
                {
                    ModelState.AddModelError(nameof(skip), "Skip cannot be less than 0");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                skip = DefaultSkip;
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

            return Ok(new { Items = _mapper.Map<List<TodoItemApiDto>>(result.Items), Total = result.Total });
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(string id)
        {
            var item = _databaseService.TodoItemsRepository.GetById(id);
            if (item == null)
                return ItemNotFound(id);

            return Ok(_mapper.Map<TodoItemApiDto>(item));
        }

        [HttpPost]
        public IActionResult Add([FromBody] TodoItemApiDto itemModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = _mapper.Map<TodoItem>(itemModel);

            var result = _databaseService.TodoItemsRepository.AddOrUpdate(item);

            _databaseService.SaveChanges();

            return Ok(_mapper.Map<TodoItemApiDto>(result));
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItemApiDto itemModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingItem = _databaseService.TodoItemsRepository.GetById(id);
            if (existingItem == null)
                return ItemNotFound(id);

            _mapper.Map(itemModel, existingItem);

            var result = _databaseService.TodoItemsRepository.AddOrUpdate(existingItem);

            _databaseService.SaveChanges();

            return Ok(_mapper.Map<TodoItemApiDto>(result));
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