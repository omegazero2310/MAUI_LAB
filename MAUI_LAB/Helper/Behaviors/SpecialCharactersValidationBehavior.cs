namespace MAUI_LAB.Helper.Behaviors
{
    /// <summary>
    /// Behavior cho Entry, Entry không được phép nhập các kí tự đặc biệt
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Xamarin.Forms.Behavior&lt;Xamarin.Forms.Entry&gt;" />
    public class SpecialCharactersValidationBehavior : Behavior<Entry>
    {
        //Here we have added the characters we wanted to restrict while entering data
        private const string SpecialCharacters = @"'/\%*‘;$£&#^@|?+=<>\""";
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }
        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(x => !SpecialCharacters.Contains(x));

                ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }
    }
}
