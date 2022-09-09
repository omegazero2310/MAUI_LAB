namespace MAUI_LAB.Helper
{
    /// <summary>
    /// Markup hỗ trợ dịch thuật trên xaml
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Xamarin.Forms.Xaml.IMarkupExtension&lt;Xamarin.Forms.BindingBase&gt;" />
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension<BindingBase>
    {
        public string Text { get; set; }
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                var binding = new Binding
                {
                    Mode = BindingMode.OneWay,
                    Path = $"[{Text}]",
                    Source = LocalizationResourceManager.Instance,
                };
                return binding;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
