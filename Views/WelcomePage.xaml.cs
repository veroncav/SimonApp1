using Microsoft.Maui.Controls;
using SimonApp1.Services;
using SimonApp1.Database;

namespace SimonApp1.Views
{
    public partial class WelcomePage : ContentPage
    {
        private readonly SettingsService _settings;
        private readonly ThemeService _themeService;
        private readonly AppDatabase _db;
        private readonly LanguageService _lang;

        public WelcomePage(SettingsService settings, ThemeService themeService, AppDatabase db, LanguageService lang)
        {
            InitializeComponent();
            _settings = settings;
            _themeService = themeService;
            _db = db;
            _lang = lang;

            ApplyLanguage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            StartGameButton.Text = _lang.T("start");
            SettingsButton.Text = _lang.T("settings");
            ScoresButton.Text = _lang.T("records");
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            // 🔥 СПРАШИВАЕМ ИМЯ ЗДЕСЬ, ПЕРЕД ИГРОЙ
            if (string.IsNullOrWhiteSpace(_settings.PlayerName))
            {
                var name = await DisplayPromptAsync(_lang.T("enter_name"), "", "OK", "Cancel", placeholder: "Player");

                if (string.IsNullOrWhiteSpace(name))
                    return; // не ввёл → не запускаем игру

                _settings.PlayerName = name.Trim();
            }

            await Navigation.PushAsync(
                App.Current.Handler.MauiContext.Services.GetService<MainPage>()
            );
        }

        private async void OnScoresClicked(object sender, EventArgs e)
        {
            await DisplayAlert(_lang.T("records"), _lang.T("soon"), "OK");
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                App.Current.Handler.MauiContext.Services.GetService<SettingsPage>()
            );
        }
    }
}
