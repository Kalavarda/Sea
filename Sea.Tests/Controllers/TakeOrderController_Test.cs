using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Sea.Models;
using Sea.Models.Geometry;
using Sea.Models.Impl.Controllers;
using Sea.Models.Interfaces;

namespace Sea.Tests.Controllers
{
    public class TakeOrderController_Test
    {
        private readonly Mock<IPathFinder> _pathFinder = new Mock<IPathFinder>();
        private readonly Mock<IOrderCostCalculator> _orderCostCalculator = new Mock<IOrderCostCalculator>();

        [Test]
        public void GetAvailableGoods_Test()
        {
            var g1 = new Goods { Id = 1 };
            var g2 = new Goods { Id = 2 };
            var g3 = new Goods { Id = 3 };

            var port = new Port
            {
                Id = 11
            };

            var game = new Game
            {
                World = new World
                {
                    Islands = new []
                    {
                        new Island { Ports = new [] { port }}
                    }
                },
                Economy = new Economy
                {
                    Goods = new[] { g1, g2, g3 },
                    OrderOptions = new []
                    {
                        new OrderOption { Id = 1, GoodsId = g1.Id, SourcePortId = port.Id, DestPortId = 22 },
                        new OrderOption { Id = 2, GoodsId = g2.Id, SourcePortId = port.Id, DestPortId = 33 },
                        new OrderOption { Id = 3, GoodsId = g3.Id, SourcePortId = 44, DestPortId = port.Id }
                    },
                    Orders = new []
                    {
                        new Order { OrderOptionId = 1 }
                    }
                }
            };

            var controller = new TakeOrderController(game, port, _pathFinder.Object, _orderCostCalculator.Object);
            Assert.AreEqual(2, controller.GetAvailableGoods().Single().Id);
        }

        [TestCase(300, 200, 300, 200, true)]
        [TestCase(300, 200, 309, 200, true)]
        [TestCase(300, 200, 300, 191, true)]
        [TestCase(300, 200, 300, 189, false)]
        [TestCase(300, 200, 308, 208, false)]
        public void TakeOrder_CheckDistance_Test(float portX, float portY, float shipX, float shipY, bool allow)
        {
            var g1 = new Goods { Id = 12 };

            var port = new Port
            {
                Id = 11,
                Position = new PointF { X = portX, Y = portY }
            };

            var game = new Game
            {
                World = new World
                {
                    Islands = new[]
                    {
                        new Island { Ports = new [] { port }}
                    },
                    Ship = new Ship
                    {
                        Position = new PointF { X = shipX, Y = shipY }
                    }
                },
                Economy = new Economy
                {
                    Goods = new[] { g1 },
                    OrderOptions = new []
                    {
                        new OrderOption { Id = 1, GoodsId = g1.Id, SourcePortId = port.Id, DestPortId = 22 },
                    }
                }
            };

            var controller = new TakeOrderController(game, port, _pathFinder.Object, _orderCostCalculator.Object);
            if (allow)
                Assert.DoesNotThrow(() =>
                {
                    controller.TakeOrder(g1.Id, 1);
                });
            else
                Assert.Throws<Exception>(() =>
                {
                    controller.TakeOrder(g1.Id, 1);
                });
        }
    }
}
