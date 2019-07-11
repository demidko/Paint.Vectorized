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
    class MoveExpert : ModificationExpert
    {
        private double ShiftX, ShiftY;

        protected override void OnFirstClick(in Point start)
        {
            base.OnFirstClick(start);
            if (TrueHit)
            {
                Workspace.Cursor = Cursors.Hand;
                (ShiftX, ShiftY) = (start.X - Canvas.GetLeft(CurrentBorder), (start.Y - Canvas.GetTop(CurrentBorder)));
            }
        }

        protected override void OnMove(in Point pos)
        {
            Canvas.SetLeft(CurrentBorder, pos.X - ShiftX);
            Canvas.SetTop(CurrentBorder, pos.Y - ShiftY);
        }
    }
}
