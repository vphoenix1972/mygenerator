using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Web.Controllers.Security
{
    public sealed class SignOutApiDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}