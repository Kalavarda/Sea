namespace Sea.Models.Interfaces
{
    public interface IWorldFactory
    {
        World Create(WorldParameters parameters);
    }

    public class WorldParameters
    {
        public float WorldSize { get; set; }
        
        public uint IslandCount { get; set; }
    }
}
