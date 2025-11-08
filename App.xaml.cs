using SimonApp1.Services;

namespace SimonApp1
{
    public partial class App : Application
    {
        private readonly ThemeService _themeService;
        private readonly SoundService _soundService; // ✅ добавили звук

        public App(ThemeService themeService, SoundService soundService) // ✅ получаем из DI
        {
            InitializeComponent();

            _themeService = themeService;
            _soundService = soundService;

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
            await _soundService.StartBackgroundAsync(); // ✅ запускаем фоновую музыку
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            _soundService.StopBackground(); // ✅ останавливаем при сворачивании
        }

        protected override async void OnResume()
        {
            base.OnResume();
            await _soundService.StartBackgroundAsync(); // ✅ продолжаем при возврате
        }
    }
}
