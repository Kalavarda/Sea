using System.Windows;
using Sea.Models;

namespace Sea.Controls
{
    public partial class PortControl
    {
        private Port _port;

        public Port Port
        {
            get => _port;
            set
            {
                if (_port == value)
                    return;

                _port = value;

                if (_port != null)
                {

                }
            }
        }

        public PortControl()
        {
            InitializeComponent();
        }

        private void OnBuyFuelClick(object sender, RoutedEventArgs e)
        {
            Port?.RaiseTradeFuel();
        }
    }
}
