using System;
using Sea.Models.Interfaces;

namespace Sea.Models.Impl
{
    public class OrderCostCalculator: IOrderCostCalculator
    {
        private const float DistancePower = 1.0625f;

        private const float KilometerCost = 20;

        public decimal GetCost(float mass, float distance)
        {
            var oneItemCost = KilometerCost * MathF.Pow(distance / 1000, DistancePower);
            return (decimal)(mass * oneItemCost);
        }
    }
}
