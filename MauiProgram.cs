using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SimonApp1.Database;
using SimonApp1.Services;
using SimonApp1.Views;

namespace SimonApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // ✅ Регистрируем сервисы
        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<ThemeService>();
        builder.Services.AddSingleton<LanguageService>(); // ← заменили LocalizationService на LanguageService

        // ✅ Регистрируем базу данных
        builder.Services.AddSingleton<AppDatabase>();

        // ✅ Регистрируем страницы
        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<SettingsPage>();

        return builder.Build();
    }
}
