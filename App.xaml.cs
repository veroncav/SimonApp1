using SimonApp1.Services;

namespace SimonApp1
{
    public partial class App : Application
    {
        private readonly ThemeService _themeService;

        public App()
        {
            InitializeComponent();

            _themeService = new ThemeService(); // Инициализация
            _themeService.SetTheme(_themeService.CurrentTheme); // Применяем сохранённую тему
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
