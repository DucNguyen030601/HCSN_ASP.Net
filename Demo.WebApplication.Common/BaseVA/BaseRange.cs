using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.BaseVA
{
    public class BaseRange : RangeAttribute
    {
        public BaseRange(double minimum, double maximum) : base(minimum, maximum)
        {
        }
       
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (!base.IsValid(value))
            {
                string errMessage = string.Format(Resources.ValidationAttribute.RangeError,
                    validationContext.DisplayName,Minimum,Maximum);
                return new ValidationResult(errMessage);
            }
            return ValidationResult.Success;
        }
    }
}
