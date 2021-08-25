using System;
using System.Threading;
using System.Windows;
using Sea.Models;
using Sea.Models.Factories;

namespace Sea.Windows
{
    public partial class NewGameWindow
    {
        private readonly AppContext _appContext;

        public NewGameWindow()
        {
            InitializeComponent();
        }

        public NewGameWindow(AppContext appContext): this()
        {
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        }

        private void OnCreateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var worldParameters = new WorldParameters
                {
                    WorldSize = float.Parse(_tbWorldSize.Text) * 1000
                };
                _appContext.Game = new Game
                {
                    World = _appContext.WorldFactory.Create(worldParameters)
                };
                _appContext.GameRepository.Save(_appContext.Game, CancellationToken.None).Wait();
                DialogResult = true;
            }
            catch (Exception error)
            {
                App.ShowError(error);
            }
        }
    }
}
