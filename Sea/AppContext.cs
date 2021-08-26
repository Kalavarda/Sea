using Sea.Controllers;
using Sea.Factories;
using Sea.Models;
using Sea.Models.Controllers;
using Sea.Models.Factories;
using Sea.Models.Repositories;
using Sea.Repositories;

namespace Sea
{
    public class AppContext
    {
        private Game _game;
        private BuyFuelController _buyFuelController;

        public Game Game
        {
            get => _game;
            set
            {
                if (_game == value)
                    return;

                _game = value;
                _buyFuelController = null;
            }
        }

        public IGameRepository GameRepository { get; } = new FileGameRepository();

        public IWorldFactory WorldFactory { get; } = new WorldFactory();

        public IBuyFuelController BuyFuelController
        {
            get
            {
                return _buyFuelController ??= new BuyFuelController(Game);
            }
        }
    }
}
