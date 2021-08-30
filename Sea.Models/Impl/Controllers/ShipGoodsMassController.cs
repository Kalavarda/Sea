using System;
using System.Linq;

namespace Sea.Models.Impl.Controllers
{
    public class ShipGoodsMassController
    {
        private readonly Ship _ship;

        public ShipGoodsMassController(Ship ship)
        {
            _ship = ship ?? throw new ArgumentNullException(nameof(ship));

            ship.OrderItemAdded += Ship_OrderItemAdded;
            ship.OrderItemRemoved += Ship_OrderItemRemoved;
            foreach (var orderItem in ship.OrderItems)
                Ship_OrderItemAdded(orderItem);
        }

        private void Ship_OrderItemAdded(OrderItem orderItem)
        {
            orderItem.MassChanged += OrderItem_MassChanged;
            OrderItem_MassChanged(orderItem);
        }

        private void Ship_OrderItemRemoved(OrderItem orderItem)
        {
            orderItem.MassChanged -= OrderItem_MassChanged;
            OrderItem_MassChanged(orderItem);
        }

        private void OrderItem_MassChanged(OrderItem orderItem)
        {
            _ship.GoodsMass.Value = _ship.OrderItems.Sum(oi => oi.Mass);
        }
    }
}
