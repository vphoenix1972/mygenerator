using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.Web.Controllers
{
    [Route("todo")]
    public sealed class TodoController : Controller
    {
        private static readonly IList<Item> items = new List<Item>()
        {
            new Item {Id = 1, Name = "Item 1"},
            new Item {Id = 2, Name = "Item 2"},
            new Item {Id = 3, Name = "Item 3"}
        };

        private static int _counter = 4;

        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok(items);
        }

        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var item = items.FirstOrDefault(e => e.Id == id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Item item)
        {
            if (item == null)
                return BadRequest();

            item.Id = _counter++;

            items.Add(item);

            return Ok(item);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Item item)
        {
            if (item == null)
                return BadRequest();

            var existingItem = items.FirstOrDefault(e => e.Id == id);
            if (existingItem == null)
                return NotFound();

            existingItem.Name = item.Name;

            return Ok(existingItem);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = items.FirstOrDefault(e => e.Id == id);
            if (item == null)
                return NotFound();

            items.Remove(item);

            return Ok();
        }

        public class Item
        {
            public int? Id { get; set; }

            public string Name { get; set; }
        }
    }
}