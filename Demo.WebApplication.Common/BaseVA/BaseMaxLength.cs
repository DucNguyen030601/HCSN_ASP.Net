using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.BaseVA
{
    public class BaseMaxLength : MaxLengthAttribute
    {
        public BaseMaxLength(int length) : base(length)
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!base.IsValid(value))
            {
                string errMessage = string.Format(Resources.ValidationAttribute.MaxLengthError, validationContext.DisplayName,Length);
                return new ValidationResult(errMessage);
            }
            return ValidationResult.Success;
        }
    }
}
