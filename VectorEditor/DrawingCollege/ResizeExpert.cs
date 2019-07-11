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
    class ResizeExpert : Expert
    {
        private Border CurrentObject;
        private Point LastPoint;

        private enum HitType
        {
            None, Body, UL, UR, LR, LL, L, R, T, B
        };

        private bool DragInProgress = false;

        private HitType MouseHitType = HitType.None;

        // Return a HitType value to indicate what is at the point.
        private HitType SetHitType(in Point point)
        {
            double left = Canvas.GetLeft(CurrentObject);
            double top = Canvas.GetTop(CurrentObject);
            double right = left + CurrentObject.Width;
            double bottom = top + CurrentObject.Height;
            if (point.X < left) return HitType.None;
            if (point.X > right) return HitType.None;
            if (point.Y < top) return HitType.None;
            if (point.Y > bottom) return HitType.None;

            const double GAP = 10;
            if (point.X - left < GAP)
            {
                // Left edge.
                if (point.Y - top < GAP) return HitType.UL;
                if (bottom - point.Y < GAP) return HitType.LL;
                return HitType.L;
            }
            if (right - point.X < GAP)
            {
                // Right edge.
                if (point.Y - top < GAP) return HitType.UR;
                if (bottom - point.Y < GAP) return HitType.LR;
                return HitType.R;
            }
            if (point.Y - top < GAP) return HitType.T;
            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }

        // Set a mouse cursor appropriate for the current hit type.
        private void SetMouseCursor()
        {
            // See what cursor we should display.
            Cursor desired_cursor = Cursors.Arrow;
            switch (MouseHitType)
            {
                case HitType.None:
                    desired_cursor = Cursors.Arrow;
                    break;
                case HitType.Body:
                    desired_cursor = Cursors.ScrollAll;
                    break;
                case HitType.UL:
                case HitType.LR:
                    desired_cursor = Cursors.SizeNWSE;
                    break;
                case HitType.LL:
                case HitType.UR:
                    desired_cursor = Cursors.SizeNESW;
                    break;
                case HitType.T:
                case HitType.B:
                    desired_cursor = Cursors.SizeNS;
                    break;
                case HitType.L:
                case HitType.R:
                    desired_cursor = Cursors.SizeWE;
                    break;
            }

            // Display the desired cursor.
            if (Workspace.Cursor != desired_cursor) Workspace.Cursor = desired_cursor;
        }

        private void MainCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Workspace.HitTest(e.GetPosition(Workspace)) is var fix && fix == null)
            {
                return;
            }
            fix.BorderBrush = Brushes.Red;
            fix.BorderThickness = new Thickness(1);

            CurrentObject = fix;
            MouseHitType = SetHitType(Mouse.GetPosition(Workspace));
            SetMouseCursor();
            if (MouseHitType == HitType.None) return;

            LastPoint = Mouse.GetPosition(Workspace);
            DragInProgress = true;
        }

        private void MainCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentObject == null)
            {
                return;
            }
            if (!DragInProgress)
            {
                MouseHitType = SetHitType(Mouse.GetPosition(Workspace));
                SetMouseCursor();
            }
            else
            {
                // See how much the mouse has moved.
                Point point = Mouse.GetPosition(Workspace);
                double offset_x = point.X - LastPoint.X;
                double offset_y = point.Y - LastPoint.Y;

                // Get the rectangle's current position.
                double new_x = Canvas.GetLeft(CurrentObject);
                double new_y = Canvas.GetTop(CurrentObject);
                double new_width = CurrentObject.Width;
                double new_height = CurrentObject.Height;

                // Update the rectangle.
                switch (MouseHitType)
                {
                    case HitType.Body:
                        new_x += offset_x;
                        new_y += offset_y;
                        break;
                    case HitType.UL:
                        new_x += offset_x;
                        new_y += offset_y;
                        new_width -= offset_x;
                        new_height -= offset_y;
                        break;
                    case HitType.UR:
                        new_y += offset_y;
                        new_width += offset_x;
                        new_height -= offset_y;
                        break;
                    case HitType.LR:
                        new_width += offset_x;
                        new_height += offset_y;
                        break;
                    case HitType.LL:
                        new_x += offset_x;
                        new_width -= offset_x;
                        new_height += offset_y;
                        break;
                    case HitType.L:
                        new_x += offset_x;
                        new_width -= offset_x;
                        break;
                    case HitType.R:
                        new_width += offset_x;
                        break;
                    case HitType.B:
                        new_height += offset_y;
                        break;
                    case HitType.T:
                        new_y += offset_y;
                        new_height -= offset_y;
                        break;
                }

                // Don't use negative width or height.
                if ((new_width > 0) && (new_height > 0))
                {
                    // Update the rectangle.
                    Canvas.SetLeft(CurrentObject, new_x);
                    Canvas.SetTop(CurrentObject, new_y);
                    CurrentObject.Width = new_width;
                    CurrentObject.Height = new_height;

                    var sh = CurrentObject.Child as Shape;
                    sh.Height = new_height;
                    sh.Width = new_width;


                    // Save the mouse's new location.
                    LastPoint = point;
                }
            }
        }

        // Stop dragging.
        private void MainCanvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            CurrentObject.BorderBrush = Brushes.Yellow;
            CurrentObject.BorderThickness = new Thickness(0);
            DragInProgress = false;
        }

        public override bool Enable(in Canvas canvas, in Machine log)
        {
            if(!base.Enable(canvas, log))
            {
                return false;
            }
            Workspace.Cursor = Cursors.Arrow;
            Workspace.MouseLeftButtonDown += MainCanvasMouseDown;
            Workspace.MouseMove += MainCanvasMouseMove;
            Workspace.MouseUp += MainCanvasMouseUp;
            return true;
        }

        public override void Disable()
        {
            Workspace.Cursor = Cursors.Cross;
            Workspace.MouseLeftButtonDown -= MainCanvasMouseDown;
            Workspace.MouseMove -= MainCanvasMouseMove;
            Workspace.MouseUp -= MainCanvasMouseUp;
        }
    }
}
