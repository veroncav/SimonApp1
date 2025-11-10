using Microsoft.Maui.Controls;
using SimonApp1.Services;
using SimonApp1.Resources.Localization;
using System.Globalization;

namespace SimonApp1.Views
{
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsService _settings;
        private readonly ThemeService _themeService;
        private readonly SoundService _sound;

        private bool _isUpdatingLanguage;

        // ✅ Shell будет использовать этот конструктор (без параметров)
        public SettingsPage() : this(
            ServiceHelper.Get<SettingsService>(),
            ServiceHelper.Get<ThemeService>(),
            ServiceHelper.Get<SoundService>())
        { }

        public SettingsPage(SettingsService settings, ThemeService themeService, SoundService sound)
        {
            InitializeComponent();
            _settings = settings;
            _themeService = themeService;
            _sound = sound;

            LoadSettings();
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            LabelSettings.Text = AppResources.Settings;
            LabelTheme.Text = AppResources.Theme;
            LabelSound.Text = AppResources.Sound;
            LabelLanguage.Text = AppResources.Language;
            LabelMaxRounds.Text = AppResources.MaxRounds;
            ButtonBack.Text = AppResources.Back;
        }

        private void LoadSettings()
        {
            ThemePicker.ItemsSource = new List<string> { "Light", "Dark" };
            ThemePicker.SelectedItem = _themeService.CurrentTheme == AppTheme.Dark ? "Dark" : "Light";

            SoundSwitch.IsToggled = _sound.IsSoundEnabled;

            MaxRoundsSlider.Value = _settings.MaxRounds;
            MaxRoundsValue.Text = _settings.MaxRounds.ToString();

            var lang = _settings.Language;
            LanguagePicker.SelectedIndex = lang switch
            {
                "ru" => 0,
                "en" => 1,
                "et" => 2,
                _ => 0
            };
        }

        private void OnSoundToggled(object sender, ToggledEventArgs e)
        {
            _sound.IsSoundEnabled = e.Value;
            _settings.SoundOn = e.Value;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            if (ThemePicker.SelectedItem is not string theme) return;
            _themeService.SetTheme(theme == "Dark" ? AppTheme.Dark : AppTheme.Light);
        }

        private async void OnLanguageChanged(object sender, EventArgs e)
        {
            if (_isUpdatingLanguage) return;

            int index = LanguagePicker.SelectedIndex;
            if (index < 0) return;

            string lang = index switch
            {
                0 => "ru",
                1 => "en",
                2 => "et",
                _ => "ru"
            };

            _isUpdatingLanguage = true;

            _settings.ApplyLanguage(lang);
            ApplyLanguage(); // обновляем текст на странице

            await Task.Delay(60);
            _isUpdatingLanguage = false;
        }

        private void OnMaxRoundsChanged(object sender, ValueChangedEventArgs e)
        {
            int value = (int)e.NewValue;
            _settings.MaxRounds = value;
            MaxRoundsValue.Text = value.ToString();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
