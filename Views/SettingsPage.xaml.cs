using Microsoft.Maui.Controls;
using SimonApp1.Services;

namespace SimonApp1.Views
{
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsService _settings;
        private readonly ThemeService _themeService;
        private readonly LanguageService _lang;

        public SettingsPage(SettingsService settings, ThemeService themeService, LanguageService lang)
        {
            InitializeComponent();
            _settings = settings;
            _themeService = themeService;
            _lang = lang;

            SoundSwitch.IsToggled = _settings.SoundOn;
            ThemePicker.SelectedItem = _themeService.CurrentTheme == AppTheme.Dark ? "Dark" : "Light";

            LanguagePicker.ItemsSource = new List<string> { "ru", "en", "et" };
            LanguagePicker.SelectedItem = _lang.CurrentLanguage;

            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            LabelSettings.Text = _lang.T("settings");
            LabelTheme.Text = _lang.T("theme");
            LabelSound.Text = _lang.T("sound");
            LabelLanguage.Text = _lang.T("language");
            ButtonBack.Text = _lang.T("back");
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            if (LanguagePicker.SelectedItem is string lang)
            {
                _lang.CurrentLanguage = lang;
                ApplyLanguage(); // 🔄 сразу обновить UI
            }
        }

        private void OnSoundToggled(object sender, ToggledEventArgs e) =>
            _settings.SoundOn = e.Value;

        private void OnThemeChanged(object sender, EventArgs e)
        {
            if (ThemePicker.SelectedItem is string theme)
                _themeService.SetTheme(theme == "Dark" ? AppTheme.Dark : AppTheme.Light);
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
