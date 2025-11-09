using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SimonApp1.Database;
using SimonApp1.Services;
using SimonApp1.Views;
using Plugin.Maui.Audio;

#if ANDROID
using Microsoft.Maui.Handlers;
using Android.Content.Res;
using AColor = Android.Graphics.Color;
#endif

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

        // ✅ Сервисы
        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<ThemeService>();
        builder.Services.AddSingleton<LanguageService>();
        builder.Services.AddSingleton<AppDatabase>();
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddSingleton<SoundService>();

        // ✅ Страницы
        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<ScoresPage>(); // ✅ ВАЖНО — чтобы кнопка "Рекорды" работала
        builder.Services.AddTransient<RulesPage>();

#if ANDROID
        EntryHandler.Mapper.AppendToMapping("RemoveUnderline", (handler, view) =>
        {
            if (handler.PlatformView != null)
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(AColor.Transparent);
        });

        PickerHandler.Mapper.AppendToMapping("RemoveUnderline", (handler, view) =>
        {
            if (handler.PlatformView != null)
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(AColor.Transparent);
        });
#endif

        return builder.Build();
    }
}
