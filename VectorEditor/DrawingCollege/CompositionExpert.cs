using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Time;

namespace DrawingCollege
{
    abstract class CompositionExpert : Expert
    {
        public override bool Enable(in Canvas canvas, in Machine log)
        {
            if (base.Enable(canvas, log))
            {
                Workspace.MouseLeftButtonDown += CreateComposition;
                return true;
            }
            return false;
        }

        protected abstract FrameworkElement CreateChild();

        private void CreateComposition(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var border = new Border()
            {
                Child = CreateChild()
            };
            Workspace.Children.Add(border);
            border.CreateContext(Workspace);
            Log.Register(new Adding(Workspace, border));
            var pos = e.GetPosition(Workspace);
            border.Height = border.Width = 125;
            Canvas.SetTop(border, pos.Y - 125 / 2);
            Canvas.SetLeft(border, pos.X - 125 / 2);
            border.MouseMove += (o, e) => (o as Border).BorderThickness = new Thickness(0.5);
            border.MouseLeave += (o, e) => (o as Border).BorderThickness = new Thickness(0);

        }

        public override void Disable()
        {
            Workspace.MouseLeftButtonDown -= CreateComposition;
        }
    }
}
