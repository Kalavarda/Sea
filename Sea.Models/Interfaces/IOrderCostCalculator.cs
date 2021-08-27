namespace Sea.Models.Interfaces
{
    public interface IOrderCostCalculator
    {
        decimal GetCost(uint count, float distance);
    }
}
