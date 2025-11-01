using Microsoft.Maui.Controls;
using SimonApp1.Services;

namespace SimonApp1.Views
{
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsService settings;
        private readonly ThemeService themeService;

        public SettingsPage(SettingsService settings, ThemeService themeService)
        {
            InitializeComponent();
            this.settings = settings;
            this.themeService = themeService;

            NameEntry.Text = settings.PlayerName;
            MaxSlider.Value = settings.MaxRounds;
            MaxLabel.Text = settings.MaxRounds.ToString();
            SoundSwitch.IsToggled = settings.SoundOn;

            // Выбираем текущую тему
            ThemePicker.SelectedItem = themeService.CurrentTheme == AppTheme.Dark ? "Dark" : "Light";

            MaxSlider.ValueChanged += (s, e) => MaxLabel.Text = ((int)e.NewValue).ToString();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            settings.PlayerName = NameEntry.Text?.Trim() ?? "Player";
            settings.MaxRounds = (int)Math.Round(MaxSlider.Value);
            settings.SoundOn = SoundSwitch.IsToggled;

            // Устанавливаем тему
            var selectedTheme = ThemePicker.SelectedItem?.ToString() ?? "Light";
            if (selectedTheme == "Dark")
                themeService.SetTheme(AppTheme.Dark);
            else
                themeService.SetTheme(AppTheme.Light);

            DisplayAlert("Сохранено", "Настройки сохранены", "ОК");
        }
    }
}
