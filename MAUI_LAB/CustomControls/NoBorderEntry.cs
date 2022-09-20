#if ANDROID
using Android.Graphics.Drawables;
using Microsoft.Maui.Controls.Platform;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_LAB.CustomControls
{
    public class NoBorderEntry : Entry
    {
        public NoBorderEntry() : base()
        {
            Init();
        }
        private void Init()
        {
            //AppendToMapping thêm vào mapping có sẵn
            //ModifyMapping thay đổi mapping có sẵn
            //PrependToMapping thay đổi mapping trước khi MAUI thêm mapping mặc định
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoBorderEntry", (handler, view) =>
            {
#if ANDROID
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Colors.Transparent.ToInt());
                handler.PlatformView.SetSelectAllOnFocus(true);
                handler.PlatformView.SetBackground(gd);
                handler.PlatformView.SetPadding(20, 0, 0, 0);
#elif IOS || MACCATALYST
            handler.PlatformView.EditingDidBegin += (s, e) =>
            {
                handler.PlatformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
            };
#elif WINDOWS
            handler.PlatformView.GotFocus += (s, e) =>
            {
                handler.PlatformView.SelectAll();
            };
#endif
            });
        }    
    }
}
