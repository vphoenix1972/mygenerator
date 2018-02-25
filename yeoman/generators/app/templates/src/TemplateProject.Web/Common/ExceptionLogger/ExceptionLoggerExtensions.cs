using System;
using Microsoft.AspNetCore.Builder;

namespace <%= projectNamespace %>.Web.Common.ExceptionLogger
{
    public static class ExceptionLoggerExtensions
    {
        /// <summary>
        /// Adds a middleware to the pipeline that will catch exceptions, log them, and re-throw them to the next middleware
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionLogger(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ExceptionLoggerMiddleware>();
        }
    }
}