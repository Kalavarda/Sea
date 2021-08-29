using System;
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
    }
}
