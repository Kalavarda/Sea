using System;
using System.Linq;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
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
            _pathFinder
                .Setup(pf => pf.Find(It.IsAny<IHasPosition>(), It.IsAny<IHasPosition>()))
                .Returns(Path.Empty);

            var g1 = new Goods { Id = 12 };

            var port = new Port
            {
                Id = 11,
                Position = new PointF(portX, portY)
            };
            var port2 = new Port
            {
                Id = 22,
                Position = new PointF(portX, portY)
            };

            var game = new Game
            {
                World = new World
                {
                    Islands = new[]
                    {
                        new Island { Ports = new [] { port, port2 }}
                    },
                    Ship = new Ship
                    {
                        Position = new PointF(shipX, shipY),
                        GoodsMass = new RangeF { Max = 10 }
                    }
                },
                Economy = new Economy
                {
                    Goods = new[] { g1 },
                    OrderOptions = new []
                    {
                        new OrderOption { Id = 1, GoodsId = g1.Id, SourcePortId = port.Id, DestPortId = port2.Id },
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

        [TestCase(50, 30, 15, true)]
        [TestCase(50, 0, 50, true)]
        [TestCase(50, 0, 50.1f, false)]
        [TestCase(50, 30, 20.1f, false)]
        public void TakeOrder_CheckMass_Test(float maxMass, float currMass, float goodsCount, bool allow)
        {
            _pathFinder
                .Setup(pf => pf.Find(It.IsAny<IHasPosition>(), It.IsAny<IHasPosition>()))
                .Returns(Path.Empty);

            var g1 = new Goods { Id = 12 };

            var port = new Port
            {
                Id = 11,
                Position = new PointF()
            };
            var port2 = new Port
            {
                Id = 22,
                Position = new PointF()
            };

            var game = new Game
            {
                World = new World
                {
                    Islands = new[]
                    {
                        new Island { Ports = new [] { port, port2 }}
                    },
                    Ship = new Ship
                    {
                        GoodsMass = new RangeF { Max = maxMass, Value = currMass },
                        Position = new PointF()
                    }
                },
                Economy = new Economy
                {
                    Goods = new[] { g1 },
                    OrderOptions = new []
                    {
                        new OrderOption { Id = 1, GoodsId = g1.Id, SourcePortId = port.Id, DestPortId = port2.Id },
                    }
                }
            };

            var controller = new TakeOrderController(game, port, _pathFinder.Object, _orderCostCalculator.Object);

            Assert.Throws<Exception>(() =>
            {
                controller.TakeOrder(g1.Id, 0);
            });

            Assert.Throws<Exception>(() =>
            {
                controller.TakeOrder(g1.Id, -1);
            });

            if (allow)
                Assert.DoesNotThrow(() =>
                {
                    controller.TakeOrder(g1.Id, goodsCount);
                });
            else
                Assert.Throws<Exception>(() =>
                {
                    controller.TakeOrder(g1.Id, goodsCount);
                });
        }

        [Test]
        public void TakeOrder_Test()
        {
            _orderCostCalculator
                .Setup(cc => cc.GetCost(It.IsAny<float>(), It.IsAny<float>()))
                .Returns(1234);

            var g1 = new Goods { Id = 12 };

            var port1 = new Port
            {
                Id = 11,
                Position = new PointF()
            };
            var port2 = new Port
            {
                Id = 11,
                Position = new PointF()
            };

            var game = new Game
            {
                World = new World
                {
                    Islands = new[]
                    {
                        new Island { Ports = new [] { port1, port2 }}
                    },
                    Ship = new Ship
                    {
                        Position = new PointF(),
                        GoodsMass = new RangeF { Max = 100 }
                    }
                },
                Economy = new Economy
                {
                    Goods = new[] { g1 },
                    OrderOptions = new[]
                    {
                        new OrderOption { Id = 123, GoodsId = g1.Id, SourcePortId = port1.Id, DestPortId = port2.Id },
                    },
                    Orders = new Order[0]
                }
            };

            new ShipGoodsMassController(game.World.Ship);

            var controller = new TakeOrderController(game, port1, _pathFinder.Object, _orderCostCalculator.Object);
            controller.TakeOrder(g1.Id, 50);

            var order = game.Economy.Orders.Single();
            Assert.AreEqual(1234, order.Cost);
            Assert.AreEqual(0, order.DeliveredMass);
            Assert.AreEqual(50, order.Mass);
            Assert.AreEqual(123, order.OrderOptionId);
            Assert.AreEqual(50, game.World.Ship.GoodsMass.Value);

            Assert.AreEqual(g1.Id, game.World.Ship.OrderItems.Single().GoodsId);
            Assert.AreEqual(50, game.World.Ship.OrderItems.Single().Mass);
        }
    }
}
