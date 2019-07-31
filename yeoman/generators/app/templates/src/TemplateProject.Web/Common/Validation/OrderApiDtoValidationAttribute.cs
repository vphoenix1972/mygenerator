using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using <%= projectNamespace %>.Web.Common.Dto;

namespace <%= projectNamespace %>.Web.Common.Validation
{
    public class OrderApiDtoValidationAttribute : ValidationAttribute
    {
        private readonly HashSet<string> _fields = new HashSet<string>();

        public OrderApiDtoValidationAttribute(string[] fields)
        {
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));

            _fields = new HashSet<string>(fields);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!(value is OrderApiDto order))
                return new ValidationResult($"Value is not an '{nameof(OrderApiDto)}' class");

            if (!_fields.Contains(order.Field))
                return new ValidationResult($"Value of '{nameof(OrderApiDto)}.{nameof(OrderApiDto.Field)}' must be one of {string.Join(", ", _fields)}");

            return ValidationResult.Success;
        }
    }
}
