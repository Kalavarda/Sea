using System;
using System.Windows;

namespace Sea
{
    public partial class App
    {
        public static void ShowError(Exception error, bool howStackTrace = false)
        {
            var exception = error.GetBaseException();
            var msg = exception.Message;
            if (howStackTrace)
                msg += Environment.NewLine + Environment.NewLine + exception.StackTrace;
            MessageBox.Show(msg, exception.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static string GetResourceFullFileName(string fileName)
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
        }
    }
}
