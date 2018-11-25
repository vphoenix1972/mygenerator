using System.ComponentModel.DataAnnotations;

namespace <%= projectNamespace %>.Web.Controllers.Security
{
    public sealed class SignOutModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}