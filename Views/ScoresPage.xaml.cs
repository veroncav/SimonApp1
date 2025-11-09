using Microsoft.Maui.Controls;
using SimonApp1.Database;
using SimonApp1.Models;
using System.Collections.ObjectModel;

namespace SimonApp1.Views
{
    public partial class ScoresPage : ContentPage
    {
        public ObservableCollection<ScoreRecord> Scores { get; set; } = new();

        private readonly AppDatabase _db;

        public ScoresPage(AppDatabase db)
        {
            InitializeComponent();
            _db = db;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Scores.Clear();

            var records = await _db.GetScoresAsync();
            foreach (var r in records)
                Scores.Add(r);
        }
    }
}
