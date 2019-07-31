using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.Web.Common.Dto;
using <%= projectNamespace %>.Web.Common.Validation;

namespace <%= projectNamespace %>.Web.Controllers.App.Todo
{
    [Produces("application/json")]
    public sealed class TodoItemsController : ApiAppControllerBase
    {
        private const int DefaultLimit = 10;
        private const int DefaultSkip = 0;

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public TodoItemsController(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets items according to filters.
        /// </summary>
        /// <response code="200">Returns a list of items</response>
        /// <response code="400">Returns if there is a validation error</response>
        [ProducesResponseType(typeof(GetManyResponse), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [HttpGet]
        public IActionResult GetMany(GetManyParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _databaseService.TodoItemsRepository.GetMany(parameters.NameFilter,
                parameters.Limit,
                parameters.Skip,
                parameters.Order?.Field,
                parameters.Order?.Direction
            );

            return Ok(new GetManyResponse { Items = _mapper.Map<List<TodoItemApiDto>>(result.Items), Total = result.Total });
        }

        public sealed class GetManyParameters
        {
            /// <summary>
            /// Item's name filter
            /// </summary>
            public string NameFilter { get; set; }

            /// <summary>
            /// Limit for returned items
            /// </summary>
            [Range(1, WebConstants.MaxLimit)]
            [DefaultValue(DefaultLimit)]
            public int? Limit { get; set; } = DefaultLimit;

            /// <summary>
            /// Number of items to skip
            /// </summary>
            [Range(0, int.MaxValue)]
            [DefaultValue(DefaultSkip)]
            public int? Skip { get; set; } = DefaultSkip;

            [OrderApiDtoValidation(fields: new[] { nameof(ITodoItem.Id), nameof(ITodoItem.Name) })]
            public OrderApiDto Order { get; set; } = null;
        }

        public sealed class GetManyResponse
        {
            public IList<TodoItemApiDto> Items { get; set; }

            public int Total { get; set; }
        }

        /// <summary>
        /// Gets an item by id.
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <response code="200">Returns an item</response>
        /// <response code="404">Returns if item does not exist</response>
        [ProducesResponseType(typeof(TodoItemApiDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [HttpGet("{id}")]
        public IActionResult GetOne(string id)
        {
            var item = _databaseService.TodoItemsRepository.GetById(id);
            if (item == null)
                return ItemNotFound(id);

            return Ok(_mapper.Map<TodoItemApiDto>(item));
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="itemModel">Item</param>
        /// <response code="200">Returns a new created item</response>
        /// <response code="400">Returns if there is a validation error</response>
        [ProducesResponseType(typeof(TodoItemApiDto), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
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

        /// <summary>
        /// Updates the item by id.
        /// </summary>
        /// <remarks>
        /// Id cannot be updated and ignored on update.
        /// </remarks>
        /// <param name="id">Id of item</param>
        /// <param name="itemModel">Item</param>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">Returns if there is a validation error</response>
        /// <response code="404">Returns if item does not exist</response>
        [ProducesResponseType(typeof(TodoItemApiDto), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [ProducesResponseType(typeof(string), 404)]
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

        /// <summary>
        /// Deletes the item by id.
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <response code="200">Returns if item has been deleted successfully or does not exist</response>
        [ProducesResponseType(typeof(void), 200)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _databaseService.TodoItemsRepository.DeleteById(id);

            _databaseService.SaveChanges();

            return Ok();
        }

        private IActionResult ItemNotFound<T>(T id)
        {
            return NotFound($"Item with id='{id}' is not found");
        }
    }
}