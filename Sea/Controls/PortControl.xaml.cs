using System.Windows;
using System.Windows.Controls;
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
            Port.RaiseTradeFuel();
        }

        private void OnTakeOrderClick(object sender, RoutedEventArgs e)
        {
            Port.RaiseTakeOrder();
        }

        private void OnCompleteOrderClick(object sender, RoutedEventArgs e)
        {
            Port.RaiseCompleteOrder();
        }

        private void OnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //_miTakeOrder.IsEnabled = Port != null && Port.Sales.Any();
        }
    }
}
