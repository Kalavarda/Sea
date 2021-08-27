using System;

namespace Sea.Models
{
    public class Economy
    {
        private decimal _money;

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
        public Order[] Orders { get; set; } = new Order[0];
    }
}
