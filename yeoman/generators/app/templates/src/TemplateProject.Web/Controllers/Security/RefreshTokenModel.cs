using System.ComponentModel.DataAnnotations;

namespace <%= projectNamespace %>.Web.Controllers.Security
{
    public sealed class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}