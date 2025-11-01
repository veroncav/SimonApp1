using Microsoft.Maui;
using Microsoft.Maui.Storage;

namespace SimonApp1.Services;

public class ThemeService
{
    private const string ThemeKey = "AppTheme";

    public AppTheme CurrentTheme { get; private set; }

    public ThemeService()
    {
        // Загружаем сохранённую тему
        var savedTheme = Preferences.Get(ThemeKey, "Light");
        CurrentTheme = savedTheme == "Dark" ? AppTheme.Dark : AppTheme.Light;
        ApplyTheme(CurrentTheme);
    }

    public void ToggleTheme()
    {
        if (CurrentTheme == AppTheme.Dark)
            SetTheme(AppTheme.Light);
        else
            SetTheme(AppTheme.Dark);
    }

    public void SetTheme(AppTheme theme)
    {
        CurrentTheme = theme;
        Preferences.Set(ThemeKey, theme == AppTheme.Dark ? "Dark" : "Light");
        ApplyTheme(theme);
    }

    private void ApplyTheme(AppTheme theme)
    {
        Application.Current!.UserAppTheme = theme;
    }
}
