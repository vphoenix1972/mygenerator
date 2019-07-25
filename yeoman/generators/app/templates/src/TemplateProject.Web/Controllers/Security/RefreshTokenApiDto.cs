using System.ComponentModel.DataAnnotations;

namespace <%= projectNamespace %>.Web.Controllers.Security
{
    public sealed class RefreshTokenApiDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}