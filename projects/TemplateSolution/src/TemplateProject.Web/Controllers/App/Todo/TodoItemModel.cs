﻿using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Web.Controllers.App.Todo
{
    public class TodoItemModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}