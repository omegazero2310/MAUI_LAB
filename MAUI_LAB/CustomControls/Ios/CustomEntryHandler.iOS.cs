using MAUI_LAB.CustomControls.Interface;
using Microsoft.Maui.Handlers;

namespace MAUI_LAB.CustomControls.Ios
{
    /// <summary>
    /// vd custom hanlder của IOS
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 20/09/2022 created
    /// </Modified>
    public class CustomEntryHandlerIos : ViewHandler<ICustomEntry, UIKit.UITextView>
    {
        public CustomEntryHandlerIos(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {
        }

        protected override UIKit.UITextView CreatePlatformView()
        {
            return new UIKit.UITextView();
        }
        public static PropertyMapper<ICustomEntry, CustomEntryHandlerIos> CustomEntryMapper = new PropertyMapper<ICustomEntry, CustomEntryHandlerIos>(ViewHandler.ViewMapper)
        {
            [nameof(ICustomEntry.Text)] = MapText,
            [nameof(ICustomEntry.TextColor)] = MapTextColor,
        };
        static void MapText(CustomEntryHandlerIos handler, ICustomEntry entry)
        {
            handler.PlatformView.Text = entry.Text;
        }

        static void MapTextColor(CustomEntryHandlerIos handler, ICustomEntry entry)
        {
            UIKit.UIColor color = new UIKit.UIColor(entry.TextColor.Red, entry.TextColor.Green, entry.TextColor.Blue, entry.TextColor.Alpha);
            handler.PlatformView.TextColor = color;
        }
    }
}
