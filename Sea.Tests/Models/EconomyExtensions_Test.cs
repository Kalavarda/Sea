using System.Linq;
using NUnit.Framework;
using Sea.Models;

namespace Sea.Tests.Models
{
    public class EconomyExtensions_Test
    {
        [Test]
        public void GetDestOrders_Test()
        {
            var port1 = new Port { Id = 111 };
            var port2 = new Port { Id = 222 };
            var order1 = new Order
            {
                OrderOptionId = 11
            };
            var order2 = new Order
            {
                OrderOptionId = 22
            };
            var economy = new Economy
            {
                OrderOptions = new []
                {
                    new OrderOption
                    {
                        Id = 11,
                        DestPortId = port1.Id
                    },
                    new OrderOption
                    {
                        Id = 22,
                        DestPortId = port2.Id
                    }
                },
                Orders = new []
                {
                    order1,
                    order2
                }
            };
            var orders = economy.GetDestOrders(port2);
            
            Assert.AreEqual(order2, orders.Single());
        }
    }
}
