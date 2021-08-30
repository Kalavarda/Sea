using System;
using System.Runtime.CompilerServices;
using Sea.Models.Interfaces;

[assembly:InternalsVisibleTo("Sea.Tests")]

namespace Sea.Models.Impl.Controllers
{
    public class CompleteOrderController: ICompleteOrderController
    {
        private readonly Game _game;
        private readonly Port _port;

        public CompleteOrderController(Game game, Port port)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _port = port ?? throw new ArgumentNullException(nameof(port));
        }

        public void Complete()
        {
            var orders = _game.Economy.GetDestOrders(_port);
            foreach (var order in orders)
            {
                var orderItem = _game.GetOrderItem(order);
                if (orderItem != null)
                    TryComplete(order, orderItem, _game);
            }
        }

        internal static void TryComplete(Order order, OrderItem orderItem, Game game)
        {
            var dMass = MathF.Min(orderItem.Mass, order.Mass - order.DeliveredMass);

            order.DeliveredMass += dMass;
            orderItem.Mass -= dMass;

            if (AreEquals(orderItem.Mass, 0))
                game.World.Ship.Remove(orderItem);

            if (AreEquals(order.Mass, order.DeliveredMass))
            {
                game.Economy.Money += order.Cost;
                game.Economy.Remove(order);
            }
        }

        private static bool AreEquals(float mass1, float mass2)
        {
            return MathF.Abs(mass2 - mass1) < 0.001;
        }
    }
}
