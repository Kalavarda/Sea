using Sea.Controllers;
using Sea.Models;

namespace Sea.Controls
{
    public partial class WorldControl
    {
        private World _world;

        public World World
        {
            get => _world;
            set
            {
                if (_world == value)
                    return;

                _world = value;

                if (_world != null)
                {
                    foreach (var island in _world.Islands)
                    {
                        var control = new IslandControl { Island = island };
                        _canvas.Children.Add(control);
                    }

                    _canvas.Children.Add(new ShipControl { Ship = _world.Ship });
                }
            }
        }

        public WorldControl()
        {
            InitializeComponent();
            new ZoomController(_root, _canvas, _scaleTransform, _translateTransform);
            new DragAndDropController(_root, _translateTransform);
        }
    }
}
