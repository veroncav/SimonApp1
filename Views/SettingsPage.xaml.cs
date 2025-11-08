using Microsoft.Maui.Controls;
using SimonApp1.Services;

namespace SimonApp1.Views
{
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsService _settings;
        private readonly ThemeService _themeService;
        private readonly LanguageService _lang;
        private readonly SoundService _sound; // ✅ добавили

        public SettingsPage(SettingsService settings, ThemeService themeService, LanguageService lang, SoundService sound)
        {
            InitializeComponent();
            _settings = settings;
            _themeService = themeService;
            _lang = lang;
            _sound = sound; // ✅ получили звук

            // Initialize UI
            SoundSwitch.IsToggled = _sound.IsSoundEnabled; // ✅ теперь привязано к SoundService
            ThemePicker.SelectedItem = _themeService.CurrentTheme == AppTheme.Dark ? "Dark" : "Light";

            LanguagePicker.ItemsSource = new List<string> { "ru", "en", "et" };
            LanguagePicker.SelectedItem = _lang.CurrentLanguage;

            MaxRoundsSlider.Value = _settings.MaxRounds;
            MaxRoundsValue.Text = _settings.MaxRounds.ToString();

            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            LabelSettings.Text = _lang.T("settings");
            LabelTheme.Text = _lang.T("theme");
            LabelSound.Text = _lang.T("sound");
            LabelLanguage.Text = _lang.T("language");
            LabelMaxRounds.Text = _lang.T("max_rounds");
            ButtonBack.Text = _lang.T("back");
        }

        // ✅ Исправлено — теперь звук реально выключается и включается
        private void OnSoundToggled(object sender, ToggledEventArgs e)
        {
            _settings.SoundOn = e.Value;        // сохраняем в настройки
            _sound.IsSoundEnabled = e.Value;    // включаем / выключаем музыку и клики
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            if (ThemePicker.SelectedItem is not string theme) return;
            _themeService.SetTheme(theme == "Dark" ? AppTheme.Dark : AppTheme.Light);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            if (LanguagePicker.SelectedItem is not string lang) return;
            _lang.CurrentLanguage = lang;
            ApplyLanguage();
        }

        private void OnMaxRoundsChanged(object sender, ValueChangedEventArgs e)
        {
            int value = (int)e.NewValue;
            _settings.MaxRounds = value;
            MaxRoundsValue.Text = value.ToString();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
