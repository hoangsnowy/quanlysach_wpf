using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace QuanLySach.ValidationRules
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? emailAddress = value as string;
            if (string.IsNullOrEmpty(emailAddress))
            {
                return new ValidationResult(false, $"Địa chỉ email rỗng");
            }

            if (!Regex.IsMatch(emailAddress, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                return new ValidationResult(false, $"Địa chỉ email mail không hợp lệ");
            }

            return ValidationResult.ValidResult;
        }
    }
}
