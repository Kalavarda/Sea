using System;
using Sea.Models;
using Sea.Models.Controllers;

namespace Sea.Controllers
{
    public class BuyFuelController: IBuyFuelController
    {
        private readonly Game _game;

        public BuyFuelController(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public void Buy(float fuelCount)
        {
            var freeSpace = _game.World.Ship.Fuel.Max - _game.World.Ship.Fuel.Value;
            if (fuelCount > freeSpace)
                throw new Exception("Недостаточно места для топлива");

            throw new NotImplementedException();
        }
    }
}
