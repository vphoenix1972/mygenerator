using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Web.Controllers.Security
{
    public sealed class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string EMail { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}