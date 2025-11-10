using Microsoft.Maui.Controls;
using SimonApp1.Database;
using SimonApp1.Models;
using SimonApp1.Resources.Localization;
using System.Collections.Generic;

namespace SimonApp1.Views
{
    public partial class ScoresPage : ContentPage
    {
        private readonly AppDatabase _db;

        // ✅ Shell будет использовать этот конструктор
        public ScoresPage() : this(ServiceHelper.Get<AppDatabase>())
        { }

        public ScoresPage(AppDatabase db)
        {
            InitializeComponent();
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Title = AppResources.History;

            var records = await _db.GetScoresAsync();
            var list = new List<ScoreView>();

            foreach (var r in records)
            {
                list.Add(new ScoreView
                {
                    PlayerName = r.PlayerName,
                    Points = $"{AppResources.Points}: {r.Score}",
                    Result = $"{AppResources.Result}: {TranslateResult(r.Result)}",
                    Date = $"{AppResources.Date}: {r.Date:dd.MM.yyyy HH:mm}"
                });
            }

            ScoresList.ItemsSource = list;
        }

        private string TranslateResult(string result)
        {
            return result.ToLower() switch
            {
                "win" => AppResources.Win,
                "lose" => AppResources.Lose,
                _ => result
            };
        }
    }
}
