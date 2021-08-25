using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sea.Controllers
{
    public class DragAndDropController
    {
        private readonly UIElement _uiElement;
        private readonly TranslateTransform _translateTransform;
        private readonly ScaleTransform _scaleTransform;
        private Point _startPosition;
        private Point _startTranslate;

        public DragAndDropController(UIElement uiElement, TranslateTransform translateTransform)
        {
            _uiElement = uiElement ?? throw new ArgumentNullException(nameof(uiElement));
            _translateTransform = translateTransform ?? throw new ArgumentNullException(nameof(translateTransform));

            _uiElement.MouseDown += UiElement_MouseDown;
            _uiElement.MouseMove += UiElement_MouseMove;
            _uiElement.MouseUp += UiElement_MouseUp;
        }

        private void UiElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Middle)
                return;

            _startPosition = e.GetPosition(_uiElement);
            _uiElement.CaptureMouse();

            _startTranslate.X = _translateTransform.X;
            _startTranslate.Y = _translateTransform.Y;
        }

        private void UiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_uiElement.IsMouseCaptured)
                return;

            var position = e.GetPosition(_uiElement);
            var dx = position.X - _startPosition.X;
            var dy = position.Y - _startPosition.Y;
            _translateTransform.X = _startTranslate.X + dx;
            _translateTransform.Y = _startTranslate.Y + dy;
        }

        private void UiElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Middle)
                return;

            _uiElement.ReleaseMouseCapture();
        }
    }
}
