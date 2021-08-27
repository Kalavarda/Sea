using System;
using Sea.Models.Interfaces;

namespace Sea.Models.Impl
{
    public class OrderCostCalculator: IOrderCostCalculator
    {
        private const float DistancePower = 1.25f;

        private const decimal KilometerCost = 100;

        public decimal GetCost(uint count, float distance)
        {
            var oneItemCost = KilometerCost * (decimal)MathF.Pow(distance / 1000, DistancePower);
            return count * oneItemCost;
        }
    }
}
