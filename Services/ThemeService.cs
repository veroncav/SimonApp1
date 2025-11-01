using Microsoft.Maui;

namespace SimonApp1.Services
{
    public class ThemeService
    {
        const string THEME_KEY = "app_theme"; // "Light" or "Dark"

        public void ApplyTheme()
        {
            var stored = Preferences.Get(THEME_KEY, "Light");
            Application.Current.UserAppTheme = stored == "Dark" ? AppTheme.Dark : AppTheme.Light;
        }

        public void SetTheme(string theme)
        {
            Preferences.Set(THEME_KEY, theme);
            Application.Current.UserAppTheme = theme == "Dark" ? AppTheme.Dark : AppTheme.Light;
        }

        public string GetTheme() => Preferences.Get(THEME_KEY, "Light");
    }
}
