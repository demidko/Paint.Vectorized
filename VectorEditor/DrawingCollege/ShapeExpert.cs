using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Time;

namespace DrawingCollege
{
    abstract class ShapeExpert : SimpleExpert
    {
        private UIElement CurrentShape;
        protected Border CurrentBorder;

        protected void ActivateBorder()
        {
            CurrentBorder.MouseMove += (o, e) => (o as Border).BorderThickness = new Thickness(0.5);
            CurrentBorder.MouseLeave += (o, e) => (o as Border).BorderThickness = new Thickness(0);
        }

        protected override void OnFirstClick(in Point where)
        {
            CurrentShape = CreateCurrentShape(where);
            CurrentBorder = new Border() { Child = CurrentShape };
            Workspace.Children.Add(CurrentBorder);
            Log.Register(new Adding(Workspace, CurrentBorder));
        }

        protected T GetCurrentShape<T>() where T : FrameworkElement => CurrentShape as T;

        protected abstract FrameworkElement CreateCurrentShape(in Point where);

        
    }
}
