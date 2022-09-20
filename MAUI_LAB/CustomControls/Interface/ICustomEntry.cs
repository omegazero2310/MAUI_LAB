using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_LAB.CustomControls.Interface
{
    public interface ICustomEntry : IView
    {
        public string Text { get; }
        public Color TextColor { get; }
    }
}
