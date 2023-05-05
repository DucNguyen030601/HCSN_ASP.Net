using Demo.WebApplication.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.BaseVA
{
    public class BaseRequired : RequiredAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (!base.IsValid(value))
            {
                string errMessage = string.Format(Resources.ValidationAttribute.RequiredError, validationContext.DisplayName);
                return new ValidationResult(errMessage);
            }
            return ValidationResult.Success;
        }
    }
}
