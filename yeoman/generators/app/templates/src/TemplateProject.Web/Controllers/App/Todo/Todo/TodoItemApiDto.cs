﻿using System.ComponentModel.DataAnnotations;

namespace <%= projectNamespace %>.Web.Controllers.App.Todo
{
    public class TodoItemApiDto
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
