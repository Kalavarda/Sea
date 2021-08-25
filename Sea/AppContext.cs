using Sea.Factories;
using Sea.Models;
using Sea.Models.Factories;
using Sea.Models.Repositories;
using Sea.Repositories;

namespace Sea
{
    public class AppContext
    {
        public Game Game { get; set; }

        public IGameRepository GameRepository { get; } = new FileGameRepository();

        public IWorldFactory WorldFactory { get; } = new WorldFactory();
    }
}
