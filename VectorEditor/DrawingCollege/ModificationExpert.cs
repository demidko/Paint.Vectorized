using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DrawingCollege
{
    abstract class ModificationExpert : SimpleExpert
    {
        protected Border CurrentBorder;
        protected bool TrueHit = false;

        protected override void OnFirstClick(in Point start)
        {
            if ((CurrentBorder = Workspace.HitTest(start)) is null)
            {
                TrueHit = false;
                StopProcess(start);
            }
            else
            {
                TrueHit = true;
                CurrentBorder.BorderBrush = Brushes.Red;
                CurrentBorder.BorderThickness = new Thickness(0.5);
                CurrentBorder.Background = Brushes.WhiteSmoke;
            }
        }

        protected override void OnLastClick(in Point where)
        {
            if (TrueHit)
            {
                CurrentBorder.BorderBrush = Brushes.Yellow;
                CurrentBorder.BorderThickness = new Thickness(0);
                CurrentBorder.Background = Brushes.Transparent;
                Workspace.Cursor = Cursors.Cross;
            }
            
        }
    }
}
