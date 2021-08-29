namespace Sea.Models.Interfaces
{
    public interface IOrderCostCalculator
    {
        decimal GetCost(float mass, float distance);
    }
}
