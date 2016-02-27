using System;
using System.Windows.Controls;

namespace DataGrid
{
    public class WeightValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            // Value is not a string
            string strValue = value as string;
            if (strValue == null)
            {
                // not going to happen
                return new ValidationResult(false, "Invalid Weight - Value is not a string");
            }

            // Value can not be converted to double
            double result;
            bool converted = Double.TryParse(strValue, out result);
            if (!converted)
            {
                return new ValidationResult(false, "nvalid Weight - Please type a valid number");
            }

            // Value is not between 0 and 1000
            if ((result < 0) || (result > 1000))
            {
                return new ValidationResult(false, "Invalid Weight - You’re either too light or too heavy");
            }

            return ValidationResult.ValidResult;
        }
    }
}