using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace SimonApp1.Services
{
    public class ThemeService
    {
        private const string ThemeKey = "AppTheme";

        public AppTheme CurrentTheme { get; private set; }

        public ThemeService()
        {
            string saved = Preferences.Get(ThemeKey, "Light");
            SetTheme(saved == "Dark" ? AppTheme.Dark : AppTheme.Light, false);
        }

        public void SetTheme(AppTheme theme, bool save = true)
        {
            CurrentTheme = theme;

            if (save)
                Preferences.Set(ThemeKey, theme == AppTheme.Dark ? "Dark" : "Light");

            Application.Current!.UserAppTheme = theme;

            // 🔥 ПЕРЕРИСОВЫВАЕМ стили глобально
            UpdateColors();
        }

        public void ToggleTheme() =>
            SetTheme(CurrentTheme == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark);

        private void UpdateColors()
        {
            var resources = Application.Current!.Resources;

            if (CurrentTheme == AppTheme.Dark)
            {
                resources["BackgroundColor"] = Color.FromArgb("#1A1A1A");
                resources["CardColor"] = Color.FromArgb("#2A2A2A");
                resources["TextColor"] = Colors.White;
            }
            else
            {
                resources["BackgroundColor"] = Colors.White;
                resources["CardColor"] = Color.FromArgb("#F4F4F4");
                resources["TextColor"] = Colors.Black;
            }
        }
    }
}
