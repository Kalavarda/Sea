namespace Sea.Models.Factories
{
    public interface IWorldFactory
    {
        World Create(WorldParameters parameters);
    }

    public class WorldParameters
    {
        public float WorldSize { get; set; }
    }
}
