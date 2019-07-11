using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Time;

namespace DrawingCollege
{
    class RectangleExpert : CompositionExpert
    {

        protected override FrameworkElement CreateChild() => new Rectangle();
    }
}
