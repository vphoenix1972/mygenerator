using System.ComponentModel.DataAnnotations;

namespace TemplateProject.Web.Controllers.Security
{
    public sealed class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}