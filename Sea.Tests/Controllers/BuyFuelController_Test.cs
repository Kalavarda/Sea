using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using NUnit.Framework;
using Sea.Models;
using Sea.Models.Impl.Controllers;

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
                    },
                    Islands = new []
                    {
                        new Island
                        {
                            Ports = new [] { new Port() }
                        }
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

        [TestCase(0, 0, false)]
        [TestCase(300, 200, true)]
        [TestCase(309, 200, true)]
        [TestCase(311, 200, false)]
        public void Buy_CheckDistance_Test(float shipX, float shipY, bool allow)
        {
            var port1 = new Port
            {
                Position = new PointF(-300, -200)
            };
            var port2 = new Port
            {
                Position = new PointF(300, 200)
            };
            var game = new Game
            {
                World = new World
                {
                    Ship = new Ship
                    {
                        Fuel = new RangeF { Max = 100 },
                        Position = new PointF(shipX, shipY)
                    },
                    Islands = new []
                    {
                        new Island
                        {
                            Ports = new [] { port1, port2 }
                        }
                    }
                },
                Economy = new Economy
                {
                    Money = 100,
                    FuelPrice = 10
                }
            };
            
            var controller = new BuyFuelController(game);

            if (allow)
                Assert.DoesNotThrow(() => { controller.Buy(1); });
            else
                Assert.Throws<Exception>(() => { controller.Buy(1); });
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
                    },
                    Islands = new []
                    {
                        new Island
                        {
                            Ports = new [] { new Port() }
                        }
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
