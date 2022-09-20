using MAUI_LAB.CustomControls.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_LAB.CustomControls
{
    public class CustomEntry : View, ICustomEntry
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }
    }
}
