﻿using System.ComponentModel.DataAnnotations;

namespace <%= projectNamespace %>.Web.Controllers.Security
{
    public sealed class SignInApiDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}