using System.Text.RegularExpressions;

namespace MAUI_LAB.Helper.Validation.Rules
{
    public class IsVaildPhoneNumberVNRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            try
            {
                return IsValidPhoneNumber(value);
            }
            catch
            {
                return false;
            }
        }
        private bool IsValidPhoneNumber(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
                return false;
            //^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$
            return Regex.IsMatch(strIn, @"^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$");
        }
    }
}
