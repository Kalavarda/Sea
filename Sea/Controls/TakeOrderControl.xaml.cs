using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Sea.Models;
using Sea.Models.Interfaces;

namespace Sea.Controls
{
    public partial class TakeOrderControl
    {
        private readonly ITakeOrderController _takeOrderController;

        private Goods SelectedGoods => _cbGoodsType.SelectedItem as Goods;

        public TakeOrderControl()
        {
            InitializeComponent();
        }

        public TakeOrderControl(ITakeOrderController takeOrderController): this()
        {
            _takeOrderController = takeOrderController ?? throw new ArgumentNullException(nameof(takeOrderController));

            RefreshGoods();
        }

        private void RefreshGoods()
        {
            var goods = _takeOrderController.GetAvailableGoods().OrderBy(g => g.Name).ToArray();
            _cbGoodsType.ItemsSource = goods;
            _cbGoodsType.SelectedItem = goods.FirstOrDefault();
        }

        private void OnTakeOrderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _takeOrderController.TakeOrder(SelectedGoods.Id, uint.Parse(_tbCount.Text));
                RefreshGoods();
            }
            catch (Exception error)
            {
                App.ShowError(error);
            }
        }

        private void OnTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedGoods != null)
            {
                _tbDistance.Text = _takeOrderController.GetDistance(SelectedGoods.Id).ToString("### ### ###").Trim();
                _tbCostByItem.Text = Math.Round(_takeOrderController.GetCost(SelectedGoods.Id, 1), 1).ToString("### ### ###.#").Trim();
                _btn.IsEnabled = true;
                return;
            }

            _tbDistance.Text = "-";
            _btn.IsEnabled = false;
        }
    }
}
