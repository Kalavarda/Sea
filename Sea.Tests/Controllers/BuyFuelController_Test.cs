using System;
using NUnit.Framework;
using Sea.Controllers;
using Sea.Models;

namespace Sea.Tests.Controllers
{
    public class BuyFuelController_Test
    {
        [TestCase(20, 30, 5, true)]
        [TestCase(20, 30, 15, false)]
        public void Buy_FreeSpace_Test(float currentFuel, float maxFuel, float buyCount, bool allow)
        {
            var game = new Game
            {
                World = new World
                {
                    Ship = new Ship
                    {
                        Fuel = new RangeF { Max = maxFuel, Value = currentFuel }
                    }
                },
                Economy = new Economy
                {
                    Money = 1_000,
                    FuelPrice = 10
                }
            };
            
            var controller = new BuyFuelController(game);

            if (allow)
                Assert.DoesNotThrow(() => { controller.Buy(buyCount); });
            else
                Assert.Throws<Exception>(() => { controller.Buy(buyCount); });
        }
    }
}
