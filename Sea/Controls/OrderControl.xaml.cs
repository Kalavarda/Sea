using System.Linq;
using Kalavarda.Primitives.Utils;
using Sea.Models;
using Sea.Models.Interfaces;
using Sea.Models.Utils;

namespace Sea.Controls
{
    public partial class OrderControl
    {
        private Order _order;
        private Game _game;

        public Order Order
        {
            get => _order;
            set
            {
                if (_order == value)
                    return;

                _order = value;

                RefreshControls();
            }
        }

        public Game Game
        {
            get => _game;
            set
            {
                if (_game == value)
                    return;
                _game = value;
                RefreshControls();
            }
        }

        public IPathFinder PathFinder { get; set; }

        public OrderControl()
        {
            InitializeComponent();

            RefreshControls();
        }

        private void RefreshControls()
        {
            if (Order != null && Game != null && PathFinder != null)
            {
                var option = Game.Economy.OrderOptions.First(oo => oo.Id == Order.OrderOptionId);
                var goods = Game.Economy.Goods.First(g => g.Id == option.GoodsId);
                _tbGoods.Text = $"{goods.Name} {Order.DeliveredMass.ToStr()} / {Order.Mass.ToStr()}";

                var port2 = Game.World.Islands.SelectMany(i => i.Ports).First(p => p.Id == option.DestPortId);
                _tbDistance.Text = PathFinder.Find(Game.World.Ship, port2).Length.ToStr();
            }
            else
            {
                _tbGoods.Text = string.Empty;
                _tbDistance.Text = string.Empty;
            }
        }
    }
}
