using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Web.Controllers.Security
{
    public sealed class SignOutModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}