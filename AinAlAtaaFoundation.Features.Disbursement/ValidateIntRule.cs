using System.Globalization;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.DisbursementManagement
{
    internal class ValidateIntRule : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(int.TryParse(value as string, out int id))
            {
                return System.Windows.Controls.ValidationResult.ValidResult;
            }
            return new System.Windows.Controls.ValidationResult(false, "إدخال غير صالح !");
        }
    }
}
