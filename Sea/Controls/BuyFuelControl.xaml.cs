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
            _tbCount.Text = MathF.Floor(_buyFuelController.GetMaxAvailableCount()).ToString("### ### ###").Trim();
        }

        private void OnBuyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _buyFuelController.Buy(float.Parse(_tbCount.Text.Replace(" ", string.Empty)));
            }
            catch (Exception error)
            {
                App.ShowError(error);
            }
        }
    }
}
