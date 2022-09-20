using Android.Widget;
using MAUI_LAB.CustomControls.Interface;
using Microsoft.Maui.Handlers;
using Color = Android.Graphics.Color;

namespace MAUI_LAB.CustomControls.Android
{
    /// <summary>
    /// VD Custom handler cho nền tảng android
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 20/09/2022 created
    /// </Modified>
    /// <seealso cref="Microsoft.Maui.Handlers.ViewHandler&lt;MAUI_LAB.CustomControls.Interface.ICustomEntry, Android.Widget.EditText&gt;" />
    public partial class CustomEntryHandlerAndroid : ViewHandler<ICustomEntry, EditText>
    {
        public CustomEntryHandlerAndroid(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {
        }
        protected override EditText CreatePlatformView()
        {
            return new EditText(Context);
        }
        public static PropertyMapper<ICustomEntry, CustomEntryHandlerAndroid> CustomEntryMapper = new PropertyMapper<ICustomEntry, CustomEntryHandlerAndroid>(ViewHandler.ViewMapper)
        {
            [nameof(ICustomEntry.Text)] = MapText,
            [nameof(ICustomEntry.TextColor)] = MapTextColor,
        };

        

        static void MapText(CustomEntryHandlerAndroid handler, ICustomEntry entry)
        {
            handler.PlatformView.Text = entry.Text;
        }

        static void MapTextColor(CustomEntryHandlerAndroid handler, ICustomEntry entry)
        {
            Color color = new Color(entry.TextColor.ToInt());
            handler.PlatformView.SetTextColor(color);
        }
    }
}
