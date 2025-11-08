using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SimonApp1.Database;
using SimonApp1.Services;
using SimonApp1.Views;

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

        // ✅ Регистрируем сервисы
        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<ThemeService>();
        builder.Services.AddSingleton<LanguageService>();
        builder.Services.AddSingleton<AppDatabase>();

        // ✅ Регистрируем страницы
        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<SettingsPage>();

#if ANDROID
        // ✅ Убираем подчеркивание (underline) в Entry
        EntryHandler.Mapper.AppendToMapping("RemoveUnderline", (handler, view) =>
        {
            if (handler.PlatformView != null)
            {
                handler.PlatformView.BackgroundTintList =
                    ColorStateList.ValueOf(AColor.Transparent);
            }
        });

        // ✅ Убираем подчеркивание (underline) в Picker
        PickerHandler.Mapper.AppendToMapping("RemoveUnderline", (handler, view) =>
        {
            if (handler.PlatformView != null)
            {
                handler.PlatformView.BackgroundTintList =
                    ColorStateList.ValueOf(AColor.Transparent);
            }
        });
#endif

        return builder.Build();
    }
}
