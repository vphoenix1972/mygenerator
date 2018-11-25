using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;

namespace <%= projectNamespace %>.Web.Controllers.App.Todo
{
    public sealed class TodoController : AppControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public TodoController(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            var todoItems = _databaseService.TodoItemsRepository.GetAll();

            return Ok(_mapper.Map<List<TodoItemModel>>(todoItems));
        }

        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
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

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] TodoItemModel itemModel)
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

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
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