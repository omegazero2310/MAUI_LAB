namespace MAUI_LAB.Helper.Validation.Rules
{
    public class IsNotContainSpecialCharacterRule: IValidationRule<string>
    {
        private const string SPECIALCHARACTERS = @"'/\%*‘;$£&#^@|?+=<>\""";
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            if (value.ToCharArray().Any(ch => SPECIALCHARACTERS.ToCharArray().Contains(ch)))
            {
                this.ValidationMessage = "MSG_USER_NAME_CONTAIN_SPECIALCHARACTERS";
                return false;
            }    
            else
            {
                this.ValidationMessage = "";
                return true;
            }    
        }
    }
}
