using System;
using System.Windows.Media;
using Sea.Controllers;
using Sea.Models;

namespace Sea.Controls
{
    public partial class WorldControl
    {
        private World _world;
        private ShipControl _shipControl;
        private RotateTransform _shipRotateTransform;

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

                    _shipRotateTransform = new RotateTransform();
                    _shipControl = new ShipControl
                    {
                        Ship = _world.Ship,
                        RenderTransform = _shipRotateTransform
                    };
                    _canvas.Children.Add(_shipControl);
                    _world.Ship.DirectionChanged += Ship_DirectionChanged;
                    Ship_DirectionChanged();

                    new SearchAtMapController(_canvas, _translateTransform, _scaleTransform, _world);
                }
            }
        }

        private void Ship_DirectionChanged()
        {
            _shipRotateTransform.Angle = 180 * _world.Ship.Direction / MathF.PI - 90;
        }

        public WorldControl()
        {
            InitializeComponent();
            new ZoomController(_root, _canvas, _scaleTransform, _translateTransform);
            new DragAndDropController(_root, _translateTransform);
        }
    }
}
