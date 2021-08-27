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
                port.TradeFuel += Port_TradeFuel;
        }

        private void Port_TradeFuel(Port port)
        {
            var buyFuelControl = new BuyFuelControl(_appContext.BuyFuelController);
            _gameWindow.ShowToolWindow(buyFuelControl, 300, 200, "Купить топливо");
        }
    }
}
