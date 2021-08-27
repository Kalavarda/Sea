using System;
using Sea.Models.Interfaces;

namespace Sea.Models.Impl.Controllers
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
            if (fuelCount <= 0)
                throw new Exception("Продажа топлива пока не работает");

            var freeSpace = _game.World.Ship.Fuel.Max - _game.World.Ship.Fuel.Value;
            if (fuelCount > freeSpace)
                throw new Exception("Недостаточно места для топлива");

            if ((decimal)fuelCount * _game.Economy.FuelPrice > _game.Economy.Money)
                throw new Exception("Недостаточно денег");

            _game.Economy.Money -= (decimal) fuelCount * _game.Economy.FuelPrice;
            _game.World.Ship.Fuel.Value += fuelCount;
        }

        public float GetMaxAvailableCount()
        {
            var max1 = _game.World.Ship.Fuel.Max - _game.World.Ship.Fuel.Value;
            var max2 = _game.Economy.Money / _game.Economy.FuelPrice;
            return MathF.Min(max1, (float)max2);
        }
    }
}
