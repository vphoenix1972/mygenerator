using System.ComponentModel.DataAnnotations;

namespace <%= projectNamespace %>.Web.Controllers.App.Todo
{
    public class TodoItemModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
