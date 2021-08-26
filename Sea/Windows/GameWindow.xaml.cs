using System;
using System.Windows;
using Sea.Controllers;
using Sea.Controls;

namespace Sea.Windows
{
    public partial class GameWindow
    {
        private readonly AppContext _appContext;

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(AppContext appContext): this()
        {
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
            _world.World = _appContext.Game.World;

            new TimeController(_appContext.Game.World);

            _world.MouseWorldPosition.Changed += MouseWorldPosition_Changed;
            MouseWorldPosition_Changed();

            Loaded += GameWindow_Loaded;
        }

        private void MouseWorldPosition_Changed()
        {
            _tbMouseWorldPos.Text = $"{MathF.Round(_world.MouseWorldPosition.X):### ### ###}; {MathF.Round(_world.MouseWorldPosition.Y):### ### ###}";
        }

        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowToolWindow(new ShipDashboard { Ship = _appContext.Game.World.Ship }, 300, 300);
        }

        private void ShowToolWindow(ShipDashboard content, int width, int height)
        {
            new Window
            {
                Content = content,
                Owner = this,
                ShowInTaskbar = false,
                Width = width,
                Height = height,
                WindowStyle = WindowStyle.ToolWindow
            }.Show();
        }
    }
}
