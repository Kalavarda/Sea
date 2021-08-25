using System;
using System.Windows.Threading;

namespace Sea
{
    public static class DispatcherExtensions
    {
        public static void Do(this DispatcherObject dispatcherObject, Action action)
        {
            if (dispatcherObject.CheckAccess())
                action();
            else
                dispatcherObject.Dispatcher.Invoke(action);
        }
    }
}
