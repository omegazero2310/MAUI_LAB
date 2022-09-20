using MAUI_LAB.CustomControls.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_LAB.CustomControls
{
    /// <summary>
    /// Custom control dùng custom handler
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 20/09/2022 created
    /// </Modified>
    /// <seealso cref="Microsoft.Maui.Controls.View" />
    /// <seealso cref="MAUI_LAB.CustomControls.Interface.ICustomEntry" />
    public class CustomEntry : View, ICustomEntry
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }
    }
}
