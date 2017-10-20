using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Common.Infrastructure.Extension
{
    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly int _minValue = -1;
        private Type _enumType;

        public EnumValidationAttribute(Type type, int minValue) {
            _minValue = minValue;
            _enumType = type;
        }

        public override bool IsValid(object value) {
            return (int)value >= _minValue;
        }
    }
}