using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Web.Controllers.Security
{
    public sealed class RefreshTokenApiDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}