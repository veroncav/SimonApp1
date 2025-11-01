using Microsoft.Maui.Controls;
using SimonApp1.Database;
using SimonApp1.Models;
using SimonApp1.Services;

namespace SimonApp1.Views;

public partial class MainPage : ContentPage
{
    private readonly SettingsService settings;
    private readonly ThemeService themeService;
    private readonly AppDatabase db;

    private List<Button> colorButtons;
    private List<int> sequence = new();
    private List<int> userInput = new();
    private bool isUserTurn = false;
    private Random rnd = new();
    private int score = 0;

    public MainPage(SettingsService settings, ThemeService themeService, AppDatabase db)
    {
        InitializeComponent();
        this.settings = settings;
        this.themeService = themeService;
        this.db = db;

        colorButtons = new List<Button> { GreenButton, RedButton, BlueButton, YellowButton };
        NameEntry.Text = settings.PlayerName;
        UpdateScoreLabel();
    }

    private void UpdateScoreLabel() => ScoreLabel.Text = score.ToString();

    private async void OnStartClicked(object sender, EventArgs e)
    {
        var playerName = NameEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(playerName))
        {
            await DisplayAlert("Ошибка", "Введите имя игрока", "OK");
            return;
        }

        settings.PlayerName = playerName;
        sequence.Clear();
        userInput.Clear();
        score = 0;
        isUserTurn = false;
        StartButton.IsEnabled = false;

        UpdateScoreLabel();
        await Task.Delay(300);
        await NextRoundAsync();
    }

    private async Task NextRoundAsync()
    {
        if (sequence.Count >= settings.MaxRounds)
        {
            await ShowResultAsync(true);
            return;
        }

        sequence.Add(rnd.Next(colorButtons.Count));
        await PlaySequenceAsync();
        isUserTurn = true;
        userInput.Clear();
    }

    private async Task PlaySequenceAsync()
    {
        isUserTurn = false;
        foreach (var idx in sequence)
        {
            await HighlightButton(colorButtons[idx]);
            await Task.Delay(250);
        }
    }

    private async Task HighlightButton(Button btn)
    {
        var original = btn.BackgroundColor;
        await btn.ScaleTo(1.15, 120);
        btn.BackgroundColor = Colors.White;
        await Task.Delay(200);
        btn.BackgroundColor = original;
        await btn.ScaleTo(1.0, 120);
    }

    private async void OnColorClicked(object sender, EventArgs e)
    {
        if (!isUserTurn) return;

        var btn = (Button)sender;
        int idx = colorButtons.IndexOf(btn);

        await HighlightButton(btn);
        userInput.Add(idx);

        if (userInput[^1] != sequence[userInput.Count - 1])
        {
            await ShowResultAsync(false);
            return;
        }

        if (userInput.Count == sequence.Count)
        {
            score = sequence.Count;
            UpdateScoreLabel();
            isUserTurn = false;
            await Task.Delay(400);
            await NextRoundAsync();
        }
    }

    private async Task ShowResultAsync(bool won)
    {
        isUserTurn = false;
        Overlay.IsVisible = true;
        ResultTitle.Text = won ? "Победа!" : "Ошибка!";
        ResultText.Text = $"Очки: {score}";
        StartButton.IsEnabled = true;
    }

    private async void OnRestartClicked(object sender, EventArgs e)
    {
        Overlay.IsVisible = false;
        sequence.Clear();
        userInput.Clear();
        score = 0;
        UpdateScoreLabel();
        await Task.Delay(100);
    }

    private async void OnSaveScoreClicked(object sender, EventArgs e)
    {
        Overlay.IsVisible = false;
        await db.SaveScoreAsync(new ScoreRecord
        {
            PlayerName = settings.PlayerName,
            Score = score,
            Date = DateTime.Now
        });
        await DisplayAlert("Сохранено", "Рекорд записан!", "OK");
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage(settings, themeService));
    }
}
