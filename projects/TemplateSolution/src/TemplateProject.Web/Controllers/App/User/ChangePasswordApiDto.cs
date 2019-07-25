using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Web.Controllers.App.User
{
    public sealed class ChangePasswordApiDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(4)]
        public string NewPassword { get; set; }
    }
}