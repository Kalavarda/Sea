using System;
using System.Collections.Generic;
using System.Linq;
using Sea.Models.Utils;

namespace Sea.Models
{
    public class Economy
    {
        private decimal _money;
        private Order[] _orders = new Order[0];

        public decimal Money
        {
            get => _money;
            set
            {
                if (_money == value)
                    return;

                _money = value;

                MoneyChanged?.Invoke();
            }
        }

        public event Action MoneyChanged;

        public decimal FuelPrice { get; set; }

        /// <summary>
        /// Справочник
        /// </summary>
        public Goods[] Goods { get; set; }

        public OrderOption[] OrderOptions { get; set; } = new OrderOption[0];

        /// <summary>
        /// Принятые заказы
        /// </summary>
        public Order[] Orders
        {
            get => _orders;
            set
            {
                if (_orders == value)
                    return;
                _orders = value;
                OrdersChanged?.Invoke();
            }
        }

        public event Action OrdersChanged;

        public void Add(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            Orders = Orders.Add(order);
        }

        public void Remove(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            Orders = Orders.Remove(order);
        }
    }

    public static class EconomyExtensions
    {
        /// <summary>
        /// Возвращает заказы, которые должны быть сданы в указанном порту
        /// </summary>
        public static IReadOnlyCollection<Order> GetDestOrders(this Economy economy, uint portId)
        {
            if (economy == null) throw new ArgumentNullException(nameof(economy));

            var orders = new List<Order>();
            foreach (var order in economy.Orders)
            {
                var option = economy.OrderOptions.First(oo => oo.Id == order.OrderOptionId);
                if (option.DestPortId == portId)
                    orders.Add(order);
            }
            return orders;
        }

        /// <summary>
        /// Возвращает заказы, которые должны быть сданы в указанном порту
        /// </summary>
        public static IReadOnlyCollection<Order> GetDestOrders(this Economy economy, IIdentifable i)
        {
            return GetDestOrders(economy, i.Id);
        }

        /// <summary>
        /// Возвращает товар на судне, относящийся к указанному заказу
        /// </summary>
        public static OrderItem GetOrderItem(this Game game, Order order)
        {
            var option = game.Economy.OrderOptions.First(oo => oo.Id == order.OrderOptionId);

            return game.World.Ship.OrderItems
                .FirstOrDefault(orderItem => orderItem.GoodsId == option.GoodsId);
        }

        /// <summary>
        /// Находит ближайший (по прямой) к судну порт
        /// </summary>
        public static Port GetNearestPort(this Game game)
        {
            return game.World.Islands.SelectMany(i => i.Ports)
                .OrderBy(p => p.Position.DistanceTo(game.World.Ship.Position))
                .FirstOrDefault();
        }
    }
}
