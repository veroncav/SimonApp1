using Microsoft.Maui.Controls;
using SimonApp1.Database;
using SimonApp1.Services;
using SimonApp1.Models;

namespace SimonApp1.Views
{
    public partial class ScoresPage : ContentPage
    {
        private readonly AppDatabase _db;
        private readonly LanguageService _lang;

        public ScoresPage(AppDatabase db, LanguageService lang)
        {
            InitializeComponent();
            _db = db;
            _lang = lang;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Title = _lang.T("history");

            var records = await _db.GetScoresAsync();
            var list = new List<ScoreView>();

            foreach (var r in records)
            {
                list.Add(new ScoreView
                {
                    PlayerName = r.PlayerName,
                    Points = $"{_lang.T("points")}: {r.Score}",
                    Result = $"{_lang.T("result")}: {_lang.T(r.Result.ToLower())}", // win/lose перевод
                    Date = $"{_lang.T("date")}: {r.Date:dd.MM.yyyy HH:mm}"
                });
            }

            ScoresList.ItemsSource = list;
        }
    }
}
