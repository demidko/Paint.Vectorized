using System.Windows;
using System.Windows.Controls;

namespace Time
{
    class Adding : Event
    {
        public Adding(Canvas canvas, UIElement addition) :
            base(() => canvas.Children.Remove(addition), () => canvas.Children.Add(addition))
        {
        }
    }
}
