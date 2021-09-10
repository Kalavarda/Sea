using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using NUnit.Framework;
using Sea.Models;
using Sea.Models.Impl.Controllers;

namespace Sea.Tests.Controllers
{
    public class ShipTimeController_Test
    {
        [Test]
        public void Process_Test()
        {
            var ship = new Ship
            {
                Engine = new Engine
                {
                    Acceleration = new RangeF { Max = 1 },
                    Rotation = new RangeF()
                },
                Fuel = new RangeF { Max = 1 },
                Position = new PointF()
            };
            ship.Engine.Acceleration.Value = 1;
            var controller = new ShipTimeController(ship);
            controller.Process(TimeSpan.FromSeconds(0.1));
            Assert.AreEqual(0, ship.Engine.Acceleration.Value, 0.001);
        }
    }
}
