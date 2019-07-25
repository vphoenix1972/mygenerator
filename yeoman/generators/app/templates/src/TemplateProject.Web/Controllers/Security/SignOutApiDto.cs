using System.ComponentModel.DataAnnotations;

namespace <%= projectNamespace %>.Web.Controllers.Security
{
    public sealed class SignOutApiDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}