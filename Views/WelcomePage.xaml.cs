using Microsoft.Maui.Controls;
using SimonApp1.Services;
using SimonApp1.Resources.Localization;

namespace SimonApp1.Views
{
    public partial class WelcomePage : ContentPage
    {
        private readonly SettingsService _settings;

        // ✅ ВАЖНО — Shell будет создавать страницу через этот конструктор
        public WelcomePage() : this(ServiceHelper.Get<SettingsService>())
        { }

        public WelcomePage(SettingsService settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ApplyLanguage();
        }

        public void ApplyLanguage()
        {
            TitleLabel.Text = "SIMON SAYS"; // можно локализовать позже
            StartGameButton.Text = AppResources.Start;
            ScoresButton.Text = AppResources.Records;
            SettingsButton.Text = AppResources.Settings;
            RulesButton.Text = AppResources.Rules;
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        private async void OnScoresClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ScoresPage));
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private async void OnRulesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RulesPage));
        }
    }
}
