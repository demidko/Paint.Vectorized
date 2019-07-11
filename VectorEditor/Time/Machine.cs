using System.Collections.Generic;

namespace Time
{
    class Machine
    {
        private readonly List<Event> Timeline = new List<Event>();
        private int CurrentIndex = -1;

        public void Register(in Event p)
        {
            if(CurrentIndex + 1 != Timeline.Count)
            {
                Timeline.RemoveRange(CurrentIndex + 1, Timeline.Count - CurrentIndex - 1);
            }
            ++CurrentIndex;
            Timeline.Add(p);
        }

        public bool Undo()
        {
            if(CurrentIndex == -1)
            {
                return false;
            }
            Timeline[CurrentIndex].Undo();
            --CurrentIndex;
            return true;
        }

        public bool Redo()
        {
            if(CurrentIndex + 1 == Timeline.Count)
            {
                return false;
            }
            Timeline[++CurrentIndex].Redo();
            return true;
        }
    }
}
