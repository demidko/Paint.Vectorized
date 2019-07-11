using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingCollege
{
    class PolylineExpert : ShapeExpert
    {
        protected override void OnMove(in Point to)
        {
            var poly = GetCurrentShape<Polyline>();
            poly.Points[poly.Points.Count - 1] = to;
        }

        protected override void OnNextClick(in Point pos, in int clickCount)
        {
            if (clickCount == 2)
            {
                StopProcess(pos);
            }
            else
            {
                GetCurrentShape<Polyline>().Points.Add(pos);
            }
        }

        protected override FrameworkElement CreateCurrentShape(in Point where) =>
            new Polyline() { Points = new PointCollection() { where, where } };

        protected override void OnLastClick(in Point where)
        {



            var poly = GetCurrentShape<Polyline>();
            var (p1, p2) =
                (poly.Points.Aggregate((a, b) => a.X < b.X ? a : b), poly.Points.Aggregate((a, b) => a.Y < b.Y ? a : b));
            var shift = ((p1 == p2) ? p1 : new Point { X = p1.X, Y = p2.Y });
            Canvas.SetLeft(CurrentBorder, shift.X);
            Canvas.SetTop(CurrentBorder, shift.Y);
            poly.Points = new PointCollection(poly.Points.Select(p => new Point { X = p.X - shift.X, Y = p.Y - shift.Y }));


            CurrentBorder.Width = poly.Points.Max(p => p.X) - poly.Points.Min(p => p.X);
            CurrentBorder.Height = poly.Points.Max(p => p.Y) - poly.Points.Min(p => p.Y);

            ActivateBorder();
        }
    }
}
