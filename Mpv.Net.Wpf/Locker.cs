using System;
using System.Windows.Threading;

namespace Mpv.Net.Wpf
{
    internal class Locker: DispatcherObject
    {
        public bool IsLocked { get; private set; }

        public void PerformLockAction(Action action)
        {
            if (action == null)
                return;

            IsLocked = true;
            Dispatcher.Invoke(action);
            IsLocked = false;
        }
    }
}
