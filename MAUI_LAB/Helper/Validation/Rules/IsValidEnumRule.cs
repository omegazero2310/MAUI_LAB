namespace MAUI_LAB.Helper.Validation.Rules
{
    public class IsValidEnumRule<T> : IValidationRule<T> where T : struct, Enum
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            try
            {
                if (!Enum.IsDefined(typeof(T), value))
                {
                    this.ValidationMessage = "MSG_GENDER_NOT_VALID";
                    return false;
                }
                else
                {
                    this.ValidationMessage = "";
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
               
        }
    }
}
