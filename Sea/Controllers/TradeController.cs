using System;
using System.Linq;
using Sea.Controls;
using Sea.Models;
using Sea.Windows;

namespace Sea.Controllers
{
    public class TradeController
    {
        private readonly AppContext _appContext;
        private readonly GameWindow _gameWindow;

        public TradeController(AppContext appContext, GameWindow gameWindow)
        {
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
            _gameWindow = gameWindow ?? throw new ArgumentNullException(nameof(gameWindow));

            foreach (var port in appContext.Game.World.Islands.SelectMany(i => i.Ports))
            {
                port.TradeFuel += Port_TradeFuel;
                port.TakeOrder += PortTakeOrder;
                port.CompleteOrder += PortCompleteOrder;
            }
        }

        private void PortCompleteOrder(Port port)
        {
            throw new NotImplementedException();
        }

        private void PortTakeOrder(Port port)
        {
            var control = new TakeOrderControl(_appContext.GetBuyController(port));
            _gameWindow.ShowToolWindow(control, 300, 300, "Принять заказ на доставку");
        }

        private void Port_TradeFuel(Port port)
        {
            var control = new BuyFuelControl(_appContext.BuyFuelController);
            _gameWindow.ShowToolWindow(control, 300, 200, "Купить топливо");
        }
    }
}
