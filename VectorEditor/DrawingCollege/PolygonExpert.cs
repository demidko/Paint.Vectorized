using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingCollege
{
    class PolygonExpert: PolylineExpert
    {
        protected override void OnLastClick(in Point where)
        {
            base.OnLastClick(where);
            CurrentBorder.Child = new Polygon() { Points = GetCurrentShape<Polyline>().Points };
        }
    }
}
