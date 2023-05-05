using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.BaseVA
{
    public class BaseRegex : RegularExpressionAttribute
    {
        private readonly string example = "";
        public BaseRegex(string pattern, string example = "") : base(pattern)
        {
            this.example = example;
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (!base.IsValid(value))
            {
                string errMessage = string.Format(example, validationContext.DisplayName);
                return new ValidationResult(errMessage);
            }
            return ValidationResult.Success;
        }
    }
}
