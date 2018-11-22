﻿using System;

namespace TemplateProject.Web.Security
{
    public sealed class JwtTokenInvalidException : Exception
    {
        public JwtTokenInvalidException(string message) : base(message)
        {
        }

        public JwtTokenInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}