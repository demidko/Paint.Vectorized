using System.Windows.Controls;
using Time;

namespace DrawingCollege
{
    abstract class Expert
    {
        protected enum State
        {
            Disabled, Enabled, Process
        }

        protected Machine Log;
        protected Canvas Workspace;
        protected State CurrentState = State.Disabled;

        public bool IsEnabled => CurrentState == State.Enabled || CurrentState == State.Process;

        public virtual bool Enable(in Canvas canvas, in Machine log)
        {
            if (IsEnabled)
            {
                return false;
            }
            CurrentState = State.Enabled;
            Workspace = canvas;
            Log = log;
            return true;
        }

        public abstract void Disable();
    }
}
