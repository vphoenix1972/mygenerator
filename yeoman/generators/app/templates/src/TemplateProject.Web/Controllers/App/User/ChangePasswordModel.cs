using System.ComponentModel.DataAnnotations;

namespace <%= projectNamespace %>.Web.Controllers.App.User
{
    public sealed class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(4)]
        public string NewPassword { get; set; }
    }
}