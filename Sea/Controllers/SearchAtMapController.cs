using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Sea.Models;

namespace Sea.Controllers
{
    public class SearchAtMapController
    {
        private readonly Canvas _canvas;
        private readonly TranslateTransform _translateTransform;
        private readonly ScaleTransform _scaleTransform;
        private readonly World _world;

        public SearchAtMapController(Canvas canvas, TranslateTransform translateTransform, ScaleTransform scaleTransform, World world)
        {
            _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            _translateTransform = translateTransform ?? throw new ArgumentNullException(nameof(translateTransform));
            _scaleTransform = scaleTransform ?? throw new ArgumentNullException(nameof(scaleTransform));
            _world = world ?? throw new ArgumentNullException(nameof(world));
            Window.GetWindow(canvas).KeyDown += UiElement_KeyDown;
        }

        private void UiElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            switch (e.Key)
            {
                case Key.F1:
                    _scaleTransform.ScaleX = 100 / _world.Ship.Size.Height;
                    _scaleTransform.ScaleY = _scaleTransform.ScaleX;

                    var screenCenter = new Point(_canvas.ActualWidth / 2, _canvas.ActualHeight / 2);
                    var shipPos = _canvas.PointToScreen(new Point(_world.Ship.Position.X, _world.Ship.Position.Y));

                    _translateTransform.X += screenCenter.X - shipPos.X;
                    _translateTransform.Y += screenCenter.Y - shipPos.Y;

                    e.Handled = true;
                    break;
            }
        }
    }
}
