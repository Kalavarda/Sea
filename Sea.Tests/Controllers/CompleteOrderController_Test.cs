using NUnit.Framework;
using Sea.Models;
using Sea.Models.Impl.Controllers;

namespace Sea.Tests.Controllers
{
    public class CompleteOrderController_Test
    {
        [Test]
        public void TryComplete_Test()
        {
            var g1 = new Goods { Id = 11 };
            
            var orderOption = new OrderOption { Id = 111, GoodsId = g1.Id };

            var order = new Order
            {
                OrderOptionId = orderOption.Id,
                Mass = 50,
                DeliveredMass = 20,
                Cost = 1234
            };
            
            var orderItem = new OrderItem
            {
                GoodsId = g1.Id,
                Mass = 30
            };

            var game = new Game
            {
                Economy = new Economy
                {
                    OrderOptions = new [] { orderOption },
                    Orders = new [] { order },
                    Money = 4321
                },
                World = new World
                {
                    Ship = new Ship
                    {
                        OrderItems = new [] { orderItem },
                        GoodsMass = new RangeF { Max = 100 }
                    }
                }
            };

            new ShipGoodsMassController(game.World.Ship);
            Assert.AreEqual(30, game.World.Ship.GoodsMass.Value);

            CompleteOrderController.TryComplete(order, orderItem, game);
            Assert.AreEqual(0, game.Economy.Orders.Length);
            Assert.AreEqual(0, game.World.Ship.OrderItems.Length);
            Assert.AreEqual(0, game.World.Ship.GoodsMass.Value);
            Assert.AreEqual(5555, game.Economy.Money);
        }
    }
}
