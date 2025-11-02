using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using SimonApp1.Services;

namespace SimonApp1.Views;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsService _settings;
    private readonly ThemeService _themeService;

    public SettingsPage(SettingsService settings, ThemeService themeService)
    {
        InitializeComponent();
        _settings = settings;
        _themeService = themeService;

        // »нициализаци€ текущих значений на странице
        SoundSwitch.IsToggled = _settings.SoundOn;
        ThemePicker.SelectedItem = _themeService.CurrentTheme == AppTheme.Dark ? "Dark" : "Light";
        LanguagePicker.SelectedItem = Preferences.Get("app_language", "Eesti");
    }

    private void OnSoundToggled(object sender, ToggledEventArgs e)
    {
        _settings.SoundOn = e.Value;
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        if (ThemePicker.SelectedItem is string theme)
        {
            AppTheme appTheme = theme == "Dark" ? AppTheme.Dark : AppTheme.Light;
            _themeService.SetTheme(appTheme);
        }
    }

    private void OnLanguageChanged(object sender, EventArgs e)
    {
        if (LanguagePicker.SelectedItem is string lang)
        {
            Preferences.Set("app_language", lang);
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
