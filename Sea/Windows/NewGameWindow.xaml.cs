using System;
using System.Threading;
using System.Windows;
using Sea.Models.Interfaces;

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

        private async void OnCreateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var worldParameters = new WorldParameters
                {
                    WorldSize = float.Parse(_tbWorldSize.Text) * 1000,
                    IslandCount = uint.Parse(_tbIslandCount.Text)
                };
                var gameParameters = new GameParameters
                {
                    WorldParameters = worldParameters,
                    Money = decimal.Parse(_tbMoney.Text),
                    FuelPrice = decimal.Parse(_tbFuelPrice.Text)
                };
                _appContext.Game = _appContext.GameFactory.Create(gameParameters);
                await _appContext.GameRepository.Save(_appContext.Game, CancellationToken.None);

                DialogResult = true;
            }
            catch (Exception error)
            {
                App.ShowError(error);
            }
        }
    }
}
