using System;
using System.Windows;

namespace Sea
{
    public partial class App
    {
        public static void ShowError(Exception error)
        {
            var exception = error.GetBaseException();
            MessageBox.Show(exception.Message + Environment.NewLine + Environment.NewLine + exception.StackTrace,
                exception.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
