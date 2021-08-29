using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Path = Sea.Models.Path;

namespace Sea.Controllers
{
    public class ShowPathController
    {
        private readonly Canvas _canvas;
        private readonly Polyline _polyline = new Polyline
        {
            Stroke = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0)),
            StrokeThickness = 1,
            StrokeDashArray = new DoubleCollection { 10 }
        };

        public ShowPathController(IPathSelector pathSelector, Canvas canvas)
        {
            if (pathSelector == null) throw new ArgumentNullException(nameof(pathSelector));

            _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            _canvas.Children.Add(_polyline);

            pathSelector.SelectedPathChanged += PathSelector_SelectedPathChanged;
            PathSelector_SelectedPathChanged(pathSelector.SelectedPath);
        }

        private void PathSelector_SelectedPathChanged(Path path)
        {
            if (_canvas.Children.Contains(_polyline))
                _canvas.Children.Remove(_polyline);

            _polyline.Points.Clear();
            if (path != null)
                foreach (var p in path.Points)
                    _polyline.Points.Add(new Point(p.X, p.Y));

            _canvas.Children.Add(_polyline);
        }
    }

    public interface IPathSelector
    {
        Path SelectedPath { get; }

        event Action<Path> SelectedPathChanged;
    }
}
