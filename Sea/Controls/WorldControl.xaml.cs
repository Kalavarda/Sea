using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Sea.Controllers;
using Sea.Models;
using Sea.Models.Geometry;

namespace Sea.Controls
{
    public partial class WorldControl
    {
        private World _world;
        private ShipControl _shipControl;
        private RotateTransform _shipRotateTransform;
        private readonly Rectangle _sea = new Rectangle
        {
            Fill = Brushes.DarkCyan,
            Width = 1_000_000_000,
            Height = 1_000_000_000
        };

        public ViewInfo ViewInfo { get; } = new ViewInfo();

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
                    _canvas.Children.Add(_sea);
                    Canvas.SetLeft(_sea, -_sea.Width / 2);
                    Canvas.SetTop(_sea, -_sea.Height / 2);

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
            _shipRotateTransform.CenterX = _shipControl.Width / 2;
            _shipRotateTransform.CenterY = _shipControl.Height / 2;
            _shipRotateTransform.Angle = 180 * _world.Ship.Direction / MathF.PI - 90;
        }

        public PointF MouseWorldPosition { get; } = new PointF();

        public WorldControl()
        {
            InitializeComponent();
            new ZoomController(_root, _canvas, _scaleTransform, _translateTransform);
            new DragAndDropController(_root, _translateTransform, _scaleTransform);

            _scaleTransform.Changed += _transform_Changed;
            _translateTransform.Changed += _transform_Changed;
        }

        private void _transform_Changed(object sender, EventArgs e)
        {
            ViewInfo.Zoom.Value = (float)_scaleTransform.ScaleX;

            var screenCenter = new Point(ActualWidth / 2, ActualHeight / 2);
            var xy = _canvas.PointFromScreen(screenCenter);
            ViewInfo.Center.Set((float)xy.X, (float)xy.Y);
        }

        private void _canvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            var window = Window.GetWindow(this);
            var screenPoint = window.PointToScreen(Mouse.GetPosition(window));
            var worldPoint = _canvas.PointFromScreen(screenPoint);
            MouseWorldPosition.Set((float)worldPoint.X, (float)worldPoint.Y);
        }
    }

    public class ViewInfo
    {
        public RangeF Zoom { get; } = new RangeF { Min = 0, Max = 200 };

        public PointF Center { get; set; } = new PointF();
    }
}
