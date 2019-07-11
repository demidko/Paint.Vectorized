using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Time;

namespace DrawingCollege
{
    class LineExpert : ShapeExpert
    {
        protected override void OnLastClick(in Point where)
        {
            var line = GetCurrentShape<Line>();
            if (line.X1 < line.X2 && line.Y1 < line.Y2)
            {
                Canvas.SetLeft(CurrentBorder, line.X1);
                Canvas.SetTop(CurrentBorder, line.Y1);
                (CurrentBorder.Height, CurrentBorder.Width) = (line.Y2 - line.Y1, line.X2 - line.X1);
                (line.X1, line.Y1, line.X2, line.Y2) = (0, 0, CurrentBorder.Width, CurrentBorder.Height);
            }
            if (line.X1 > line.X2 && line.Y1 > line.Y2)
            {
                Canvas.SetLeft(CurrentBorder, line.X2);
                Canvas.SetTop(CurrentBorder, line.Y2);
                (CurrentBorder.Height, CurrentBorder.Width) = (line.Y1 - line.Y2, line.X1 - line.X2);
                (line.X1, line.Y1, line.X2, line.Y2) = (CurrentBorder.Width, CurrentBorder.Height, 0, 0);
            }
            if (line.X1 > line.X2 && line.Y1 < line.Y2)
            {
                Canvas.SetLeft(CurrentBorder, line.X2);
                Canvas.SetTop(CurrentBorder, line.Y1);
                (CurrentBorder.Height, CurrentBorder.Width) = (line.Y2 - line.Y1, line.X1 - line.X2);
                (line.X1, line.Y1, line.X2, line.Y2) = (CurrentBorder.Width, 0, 0, CurrentBorder.Height);
            }
            if (line.X1 < line.X2 && line.Y1 > line.Y2)
            {
                Canvas.SetLeft(CurrentBorder, line.X1);
                Canvas.SetTop(CurrentBorder, line.Y2);
                (CurrentBorder.Height, CurrentBorder.Width) = (line.Y1 - line.Y2, line.X2 - line.X1);
                (line.X1, line.Y1, line.X2, line.Y2) = (0, CurrentBorder.Height, CurrentBorder.Width, 0);
            }
            if (line.X1 == line.X2 && line.Y1 == line.Y2)
            {
                Canvas.SetLeft(CurrentBorder, line.X1);
                Canvas.SetTop(CurrentBorder, line.Y1);
                CurrentBorder.Height = CurrentBorder.Width = 7;
                (line.X1, line.Y1, line.X2, line.Y2) = (0, 0, 0, 0);
            }
            if (line.X1 < line.X2 && line.Y1 == line.Y2)
            {
                CurrentBorder.Height = CurrentBorder.Width = (line.X2 - line.X1);
                Canvas.SetLeft(CurrentBorder, line.X1);
                var h = CurrentBorder.Height / 2;
                Canvas.SetTop(CurrentBorder, line.Y1 - h);
                (line.X1, line.Y1, line.X2, line.Y2) = (0, h, CurrentBorder.Width, h);
            }
            if (line.X1 > line.X2 && line.Y1 == line.Y2)
            {
                CurrentBorder.Height = CurrentBorder.Width = (line.X1 - line.X2);
                Canvas.SetLeft(CurrentBorder, line.X2);
                var h = CurrentBorder.Height / 2;
                Canvas.SetTop(CurrentBorder, line.Y1 - h);
                (line.X1, line.Y1, line.X2, line.Y2) = (CurrentBorder.Width, h, 0, h);
            }
            if (line.X1 == line.X2 && line.Y1 < line.Y2)
            {
                CurrentBorder.Height = CurrentBorder.Width = (line.Y2 - line.Y1);
                var w = CurrentBorder.Width / 2;
                Canvas.SetLeft(CurrentBorder, line.X1 - w);
                Canvas.SetTop(CurrentBorder, line.Y1);
                (line.X1, line.Y1, line.X2, line.Y2) = (w, 0, w, CurrentBorder.Height);
            }
            if (line.X1 == line.X2 && line.Y1 > line.Y2)
            {
                CurrentBorder.Height = CurrentBorder.Width = (line.Y1 - line.Y2);
                var w = CurrentBorder.Width / 2;
                Canvas.SetLeft(CurrentBorder, line.X1 - w);
                Canvas.SetTop(CurrentBorder, line.Y2);
                (line.X1, line.Y1, line.X2, line.Y2) = (w, CurrentBorder.Height, w, 0);
            }
            ActivateBorder();
        }

        protected override void OnMove(in Point to)
        {
            var line = GetCurrentShape<Line>();
            (line.X2, line.Y2) = (to.X, to.Y);
        }

        protected override FrameworkElement CreateCurrentShape(in Point where) => new Line() { X1 = where.X, Y1 = where.Y };
    }
}
