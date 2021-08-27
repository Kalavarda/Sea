using System;
using NUnit.Framework;
using Sea.Controllers;
using Sea.Models;

namespace Sea.Tests.Controllers
{
    public class BuyFuelController_Test
    {
        [TestCase(20, 30, 5, 1000, true)]
        [TestCase(20, 30, 15, 1000, false)]
        [TestCase(20, 30, 0, 1000, false)]
        [TestCase(20, 30, -1, 1000, false)]
        [TestCase(20, 30, 5, 49, false)]
        public void Buy_Checks_Test(float currentFuel, float maxFuel, float buyCount, decimal money, bool allow)
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
                    Money = money,
                    FuelPrice = 10
                }
            };
            
            var controller = new BuyFuelController(game);

            if (allow)
                Assert.DoesNotThrow(() => { controller.Buy(buyCount); });
            else
                Assert.Throws<Exception>(() => { controller.Buy(buyCount); });
        }

        [TestCase(20, 30, 1000, 10)]
        [TestCase(30, 30, 1000, 0)]
        [TestCase(20, 30, 40, 4)]
        [TestCase(20, 30, 0, 0)]
        public void GetMaxAvailableCount_Test(float currentFuel, float maxFuel, decimal money, float expected)
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
                    Money = money,
                    FuelPrice = 10
                }
            };
            
            var controller = new BuyFuelController(game);
            Assert.AreEqual(expected, controller.GetMaxAvailableCount(), 0.01);
        }

        [TestCase(20, 30, 5, 1000, 25, 950)]
        public void Buy_Test(float currentFuel, float maxFuel, float buyCount, decimal money, float resultFuel, decimal resultMpney)
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
                    Money = money,
                    FuelPrice = 10
                }
            };
            
            var controller = new BuyFuelController(game);
            controller.Buy(buyCount);

            Assert.AreEqual(resultFuel, game.World.Ship.Fuel.Value, 0.01f);
            Assert.AreEqual(resultMpney, game.Economy.Money);
        }
    }
}
