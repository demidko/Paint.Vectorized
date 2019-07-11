using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DrawingCollege
{
    static class Utils
    {
        public static Border HitTest(this Canvas ws, in Point where)
        {
            Border border = null;
            HitTestResultBehavior findBorder(HitTestResult result)
            {
                if (result.VisualHit is Border b)
                {
                    border = b;
                    return HitTestResultBehavior.Stop;
                }
                return HitTestResultBehavior.Continue;
            }
            VisualTreeHelper.HitTest(ws, null, findBorder, new PointHitTestParameters(where));
            return border;
        }

        public static void CreateContext(this Border border, Canvas cnv)
        {
            
            var res = new ContextMenu();
            var del = new MenuItem()
            {
                Header = "Удалить",
                Icon = new Image()
                {
                   Source = new BitmapImage(new Uri("pack://application:,,,/Icons/Clear.png"))
                }
            };
            del.Click += (o, e) => cnv.Children.Remove(border);
            res.Items.Add(del);
            void rotate(in int angle)
            {
                var sh = (border.Child as Shape);
                sh.RenderTransform  = new RotateTransform(angle);
            }
            var rot = new MenuItem()
            {
                Header = "Повернуть на 10 градусов",
                Icon = new Image()
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Icons/Rotate.png"))
                }
            };
            rot.Click += (o, e) => rotate(10);
            res.Items.Add(rot);
            var rot30 = new MenuItem()
            {
                Header = "Повернуть на 30 градусов",
                Icon = new Image()
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Icons/Rotate.png"))
                }
            };
            rot30.Click += (o, e) => rotate(30);
            res.Items.Add(rot30);

            border.ContextMenu = res;
        }
    }
}
