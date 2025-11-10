using SimonApp1.Views;

namespace SimonApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(ScoresPage), typeof(ScoresPage));
            Routing.RegisterRoute(nameof(RulesPage), typeof(RulesPage));
        }
    }
}
