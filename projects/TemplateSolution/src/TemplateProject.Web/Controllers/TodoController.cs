using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.Web.Controllers
{
    [Authorize(Roles = WebProjectConstants.RoleUser)]
    [Route("todo")]
    public sealed class TodoController : Controller
    {
        private readonly IDatabaseService _databaseService;

        public TodoController(IDatabaseService databaseService)
        {
            if (databaseService == null)
                throw new ArgumentNullException(nameof(databaseService));

            _databaseService = databaseService;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok(_databaseService.TodoItemsRepository.GetAll());
        }

        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var item = _databaseService.TodoItemsRepository.GetById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] TodoItem item)
        {
            if (item == null)
                return BadRequest();

            var result = _databaseService.TodoItemsRepository.AddOrUpdate(item);

            _databaseService.SaveChanges();

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] TodoItem item)
        {
            if (item == null)
                return BadRequest();

            var existingItem = _databaseService.TodoItemsRepository.GetById(id);
            if (existingItem == null)
                return NotFound();

            var result = _databaseService.TodoItemsRepository.AddOrUpdate(item);

            _databaseService.SaveChanges();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _databaseService.TodoItemsRepository.DeleteById(id);

            _databaseService.SaveChanges();

            return Ok();
        }
    }
}