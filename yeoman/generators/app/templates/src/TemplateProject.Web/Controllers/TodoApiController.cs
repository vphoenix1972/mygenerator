using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace <%= projectNamespace %>.Web.Controllers
{
    [Route("api/todo")]
    public sealed class TodoApiController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            Thread.Sleep(3000);

            var items = new List<object>()
            {
                new {Id = 1, Name = "Item 1"},
                new {Id = 2, Name = "Item 2"},
                new {Id = 3, Name = "Item 3"}
            };

            return Ok(items);
        }
    }
}