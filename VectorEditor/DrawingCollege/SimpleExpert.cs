using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Time;

namespace DrawingCollege
{

    abstract class SimpleExpert : Expert
    {
        protected Point MousePositionNow;

        // Абстрактное дополнительное действие при первом клике
        protected abstract void OnFirstClick(in Point where);

        // 1 клик
        private void MouseFirstClick(object sender, MouseButtonEventArgs e)
        {
            CurrentState = State.Process;
            Workspace.MouseLeftButtonDown -= MouseFirstClick;
            OnFirstClick(e.GetPosition(Workspace));
            Workspace.MouseMove += MouseMove;
            Workspace.MouseLeftButtonDown += MouseNextClick;
        }

        // Абстрактное действие при движении мыши
        protected abstract void OnMove(in Point p);

        // Движение мыши
        private void MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(Workspace);
            OnMove(pos);
            MousePositionNow = pos;
        }

        // Если переопределим этот метод, то нужно будет самостоятельно завершать где-то в нем процесс рисования
        protected virtual void OnNextClick(in Point pos, in int clickCount) => StopProcess(pos);

        // Следующий после движения клик
        private void MouseNextClick(object sender, MouseButtonEventArgs e) =>
            OnNextClick(e.GetPosition(Workspace), e.ClickCount);

        // Абстрактное дополнительное действие при последнем клике
        protected abstract void OnLastClick(in Point where);

        // Остановить процесс рисования
        protected void StopProcess(in Point where)
        {
            CurrentState = State.Enabled;
            Workspace.MouseMove -= MouseMove;
            Workspace.MouseLeftButtonDown -= MouseNextClick;
            Workspace.MouseLeftButtonDown += MouseFirstClick;
            OnLastClick(where);
        }



        public override bool Enable(in Canvas canvas, in Machine log)
        {
            if (base.Enable(canvas, log))
            {
                Workspace.MouseLeftButtonDown += MouseFirstClick;
                return true;
            }
            return false;
        }

        public override void Disable()
        {
            if (!IsEnabled)
            {
                return;
            }
            if (CurrentState == State.Process)
            {
                OnLastClick(MousePositionNow);
                Workspace.MouseMove -= MouseMove;
                Workspace.MouseLeftButtonDown -= MouseNextClick;
            }
            else
            {
                Workspace.MouseLeftButtonDown -= MouseFirstClick;
            }
            CurrentState = State.Disabled;
        }
    }
}
