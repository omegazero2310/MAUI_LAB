using System.ComponentModel;

namespace MAUI_LAB.Helper.Trigger
{
    /// <summary>
    /// Trigger Action cho nút ẩn hiện mật khẩu (thay đổi icon ẩn hiện)
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Xamarin.Forms.TriggerAction&lt;Xamarin.Forms.ImageButton&gt;" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class ShowPasswordTriggerAction : TriggerAction<ImageButton>, INotifyPropertyChanged
    {
        public ImageSource ShowIcon { get; set; }
        public ImageSource HideIcon { get; set; }

        bool _hidePassword = true;

        public bool HidePassword
        {
            set
            {
                if (_hidePassword != value)
                {
                    _hidePassword = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HidePassword)));
                }
            }
            get => _hidePassword;
        }

        protected override void Invoke(ImageButton sender)
        {
            sender.Source = HidePassword ? ShowIcon : HideIcon;
            HidePassword = !HidePassword;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
