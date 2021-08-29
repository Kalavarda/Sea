namespace Sea.Models.Interfaces
{
    public interface IGameRepository
    {
        Game Load();

        void Save(Game game);
    }
}
