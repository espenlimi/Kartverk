using System.ComponentModel.DataAnnotations;

namespace Kartverk.Mvc.ModelValidators
{
    public class PrimenumberValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var inputNumber = (int?)value;
            if (inputNumber == null)
                return ValidationResult.Success;
            if (IsPrime(inputNumber.Value))
                return ValidationResult.Success;

            return new ValidationResult(GetErrorMessage(inputNumber.Value));
        }
        private static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
        private static string GetErrorMessage(int number) => $"{number} er ikke et primtall";
    }
}
