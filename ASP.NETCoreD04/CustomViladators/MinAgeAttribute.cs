using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreD04.CustomViladators
{
    public class MinAgeAttribute : ValidationAttribute, IClientModelValidator
    {
        /*------------------------------------------------------------------*/
        private readonly int _minAge;
        /*------------------------------------------------------------------*/
        public MinAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }
        /*------------------------------------------------------------------*/
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("DOB Is Required");
            }

            if (value is not DateOnly dob)
            {
                return new ValidationResult("Invalid DOB Format");
            }

            var today = DateOnly.FromDateTime(DateTime.Today);

            int age = today.Year - dob.Year;
            if (age < _minAge)
            {
                return new ValidationResult($"Minimum age is {_minAge}.");
            }

            return ValidationResult.Success!;
        }
        /*------------------------------------------------------------------*/
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-minage", $"Minimum age is {_minAge}.");
            context.Attributes.Add("data-val-minage-age", _minAge.ToString());
        }
        /*------------------------------------------------------------------*/
    }
}
