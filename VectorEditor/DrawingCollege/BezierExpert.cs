using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingCollege
{
    class BezierExpert : ShapeExpert
    {
        private BezierSegment QBZ;
        private int QBZPointIndex = 3;
        private PathFigure CurrFigure;
        
        protected override FrameworkElement CreateCurrentShape(in Point where)
        {
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = where;
            CurrFigure = pthFigure;
            
            BezierSegment qbzSeg = new BezierSegment();
            qbzSeg.Point1 = qbzSeg.Point2 = where;
            QBZ = qbzSeg;

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(qbzSeg);

            pthFigure.Segments = myPathSegmentCollection;

            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);

            PathGeometry pthGeometry = new PathGeometry();
            pthGeometry.Figures = pthFigureCollection;

            Path arcPath = new Path();
            arcPath.Stroke = new SolidColorBrush(Colors.Tomato);
            arcPath.StrokeThickness = 2;
            arcPath.Data = pthGeometry;

            return arcPath;
        }

        protected override void OnNextClick(in Point pos, in int clickCount)
        {
            if(QBZPointIndex == 3)
            {
                QBZPointIndex = 2;
                return;
            }
            if(QBZPointIndex == 2)
            {
                StopProcess(pos);
            }
        }

        protected override void OnLastClick(in Point where)
        {
            QBZPointIndex = 3;
            var lst = new[] { QBZ.Point1, QBZ.Point2, QBZ.Point3 };
            
            var x = lst.Min(p => p.X);
            var y = lst.Min(p => p.Y);


            Canvas.SetTop(CurrentBorder, y);
            Canvas.SetLeft(CurrentBorder, x);

            QBZ.Point1 = new Point { X = QBZ.Point1.X - x, Y = QBZ.Point1.Y - y };
            QBZ.Point2 = new Point { X = QBZ.Point2.X - x, Y = QBZ.Point2.Y - y };
            QBZ.Point3 = new Point { X = QBZ.Point3.X - x, Y = QBZ.Point3.Y - y };

            CurrFigure.StartPoint = QBZ.Point1;

            CurrentBorder.Width = lst.Max(p => p.X) - lst.Min(p => p.X);
            CurrentBorder.Height = lst.Max(p => p.Y) - lst.Min(p => p.Y);

            ActivateBorder();
        }

        protected override void OnMove(in Point p)
        {
            if(QBZPointIndex == 3)
            {
                QBZ.Point3 = p;
            }
            if(QBZPointIndex == 2)
            {
                QBZ.Point2 = p;
            }
        }
    }
}
