namespace Sea.Models.Interfaces
{
    public interface IGameFactory
    {
        Game Create(GameParameters parameters);
    }

    public class GameParameters
    {
        public decimal Money { get; set; }

        public decimal FuelPrice { get; set; }

        public WorldParameters WorldParameters { get; set; }
    }
}
