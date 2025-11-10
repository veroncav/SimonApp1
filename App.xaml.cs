using SimonApp1.Services;

namespace SimonApp1
{
    public partial class App : Application
    {
        private readonly ThemeService _themeService;
        private readonly SoundService _soundService;
        private readonly SettingsService _settings; // ✅ добавили

        public App(ThemeService themeService, SoundService soundService, SettingsService settings)
        {
            InitializeComponent();

            _themeService = themeService;
            _soundService = soundService;
            _settings = settings;

            // ✅ применяем сохранённый язык ПРИ СТАРТЕ
            _settings.ApplyLanguage(_settings.Language);

            // ✅ применяем тему
            _themeService.SetTheme(_themeService.CurrentTheme);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await _soundService.StartBackgroundAsync();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            _soundService.StopBackground();
        }

        protected override async void OnResume()
        {
            base.OnResume();
            await _soundService.StartBackgroundAsync();
        }
    }
}
