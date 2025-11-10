using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SimonApp1.Services;
using SimonApp1.Views;
using SimonApp1.Database;
using Plugin.Maui.Audio;

namespace SimonApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        // Services
        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<ThemeService>();
        builder.Services.AddSingleton<AudioManager>();
        builder.Services.AddSingleton<SoundService>();
        builder.Services.AddSingleton<AppDatabase>();
        builder.Services.AddSingleton<Plugin.Maui.Audio.IAudioManager>(Plugin.Maui.Audio.AudioManager.Current);

        // ✅ Pages must be Transient (NOT Singleton)
        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ScoresPage>();
        builder.Services.AddTransient<RulesPage>();

        var app = builder.Build();
        ServiceHelper.Services = app.Services;

        // ✅ Apply saved language at launch
        var settings = app.Services.GetService<SettingsService>();
        settings?.ApplyLanguage(settings.Language);

        return app;
    }
}
