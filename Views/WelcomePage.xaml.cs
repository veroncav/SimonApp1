using Microsoft.Maui.Controls;

namespace SimonApp1.Views;

public partial class WelcomePage : ContentPage
{
    private readonly Services.SettingsService _settings;
    private readonly Services.ThemeService _themeService;
    private readonly Database.AppDatabase _db;

    public WelcomePage(Services.SettingsService settings, Services.ThemeService themeService, Database.AppDatabase db)
    {
        InitializeComponent();
        _settings = settings;
        _themeService = themeService;
        _db = db;
    }

    private async void OnStartGameClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage(_settings, _themeService, _db));
    }

    private async void OnScoresClicked(object sender, EventArgs e)
    {
        await DisplayAlert("High Scores", "Эта функция скоро появится!", "OK");
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage(_settings, _themeService));
    }
}
