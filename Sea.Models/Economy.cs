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
    }
}
