using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Kalavarda.Primitives.Utils;
using Sea.Controllers;
using Sea.Models;
using Sea.Models.Utils;

namespace Sea.Controls
{
    public partial class OrdersControl : IPathSelector
    {
        private readonly Economy _economy;

        public OrdersControl()
        {
            InitializeComponent();
        }

        public OrdersControl(AppContext appContext): this()
        {
            _orderControl.Game = appContext.Game;
            _orderControl.PathFinder = appContext.PathFinder;

            _economy = appContext.Game.Economy;
            _economy.OrdersChanged += Economy_OrdersChanged;
            Economy_OrdersChanged();
        }

        private void Economy_OrdersChanged()
        {
            _listBox.ItemsSource = _economy.Orders;
        }

        private void _listBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _orderControl.Order = SelectedOrder;
            SelectedPath = SelectedOrder != null && _cbShowPath.IsChecked == true
                ? _orderControl.PathFinder.Find(_orderControl.Game.World.Ship, GetTargetPort(SelectedOrder))
                : null;
            SelectedPathChanged?.Invoke(SelectedPath);
        }

        private Port GetTargetPort(Order order)
        {
            var option = _orderControl.Game.Economy.OrderOptions.First(oo => oo.Id == order.OrderOptionId);
            return _orderControl.Game.World.Islands.SelectMany(i => i.Ports).First(p => p.Id == option.DestPortId);
        }

        public Path SelectedPath { get; private set; }

        public event Action<Path> SelectedPathChanged;

        public Order SelectedOrder => _listBox.SelectedItem as Order;

        private void ShowPath_OnChecked(object sender, RoutedEventArgs e)
        {
            _listBox_OnSelectionChanged(sender, null);
        }
    }

    public class OrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var order = (Order) value;
            return $"Заказ {order.DeliveredMass.ToStr()}/{order.Mass.ToStr()} на сумму {order.Cost.ToStr()}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
