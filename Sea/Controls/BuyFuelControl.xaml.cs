using System;
using System.Windows;
using Sea.Models.Controllers;

namespace Sea.Controls
{
    public partial class BuyFuelControl
    {
        private readonly IBuyFuelController _buyFuelController;

        public BuyFuelControl()
        {
            InitializeComponent();
        }

        public BuyFuelControl(IBuyFuelController buyFuelController): this()
        {
            _buyFuelController = buyFuelController ?? throw new ArgumentNullException(nameof(buyFuelController));
        }

        private void OnBuyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _buyFuelController.Buy(float.Parse(_tbCount.Text));
            }
            catch (Exception error)
            {
                App.ShowError(error);
            }
        }
    }
}
