using MAUI_LAB.Properties;
using System.ComponentModel;
using System.Globalization;

namespace MAUI_LAB.Helper
{
    /// <summary>
    /// Class hỗ trợ dịch thuật từ resource
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        public static LocalizationResourceManager Instance { get; } = new LocalizationResourceManager();

        public string this[string text]
        {
            get
            {
                return AppResource.ResourceManager.GetString(text, AppResource.Culture) ?? text;
            }
        }

        public void SetCulture(CultureInfo language)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            AppResource.Culture = language;

            Invalidate();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
