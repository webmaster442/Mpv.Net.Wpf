using System;

namespace Mpv.Net.Wpf
{
    internal class Locker
    {
        public bool IsLocked { get; private set; }

        public void PerformLockAction(Action action)
        {
            if (action == null)
                return;

            IsLocked = true;
            action.Invoke();
            IsLocked = false;
        }
    }
}
