using Microsoft.Maui.Controls;
using SimonApp1.Database;
using SimonApp1.Models;
using SimonApp1.Services;

namespace SimonApp1.Views;

public partial class MainPage : ContentPage
{
    private readonly SettingsService settings;
    private readonly LanguageService lang;
    private readonly AppDatabase db;

    private readonly List<Button> colorButtons;
    private readonly List<int> sequence = new();
    private readonly List<int> userInput = new();
    private readonly Random rnd = new();

    private bool isUserTurn = false;
    private int score = 0;

    public MainPage(SettingsService settings, LanguageService lang, AppDatabase db)
    {
        InitializeComponent();
        this.settings = settings;
        this.lang = lang;
        this.db = db;

        colorButtons = new() { GreenButton, RedButton, BlueButton, YellowButton };

        ApplyLanguage();
        DisableColorButtons();
        StartButton.IsEnabled = false;
    }

    private void ApplyLanguage()
    {
        StartButton.Text = lang.T("start");
    }

    private void OnConfirmNameClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(PlayerNameEntry.Text))
            return;

        settings.PlayerName = PlayerNameEntry.Text.Trim();
        NamePopup.IsVisible = false;
        StartButton.IsEnabled = true;
    }

    private void DisableColorButtons() => colorButtons.ForEach(b => b.IsEnabled = false);
    private void EnableColorButtons() => colorButtons.ForEach(b => b.IsEnabled = true);
    private void UpdateScore() => ScoreLabel.Text = score.ToString();

    private async Task Blink(Button btn)
    {
        var original = btn.BackgroundColor;
        await btn.ScaleTo(1.2, 120);
        btn.BackgroundColor = Colors.White;
        await Task.Delay(200);
        btn.BackgroundColor = original;
        await btn.ScaleTo(1.0, 120);
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        StartButton.IsEnabled = false;
        score = 0;
        sequence.Clear();
        userInput.Clear();
        UpdateScore();

        await Task.Delay(250);
        await NextRoundAsync();
    }

    private async Task NextRoundAsync()
    {
        // ✅ Окончание игры при достижении лимита
        if (sequence.Count == settings.MaxRounds)
        {
            await GameOver(win: true);
            return;
        }

        sequence.Add(rnd.Next(4));
        await PlaySequenceAsync();
        userInput.Clear();
        isUserTurn = true;
        EnableColorButtons();
    }

    private async Task PlaySequenceAsync()
    {
        DisableColorButtons();
        foreach (var i in sequence)
        {
            await Blink(colorButtons[i]);
            await Task.Delay(200);
        }
    }

    private async void OnColorClicked(object sender, EventArgs e)
    {
        if (!isUserTurn) return;

        var btn = (Button)sender;
        int index = colorButtons.IndexOf(btn);

        await Blink(btn);
        userInput.Add(index);

        // ❌ Ошибка игрока
        if (userInput[^1] != sequence[userInput.Count - 1])
        {
            await GameOver(win: false);
            return;
        }

        // ✅ Игрок повторил всё → следующий раунд
        if (userInput.Count == sequence.Count)
        {
            score = sequence.Count;
            UpdateScore();
            isUserTurn = false;
            await Task.Delay(400);
            await NextRoundAsync();
        }
    }

    private async Task GameOver(bool win)
    {
        DisableColorButtons();
        isUserTurn = false;
        StartButton.IsEnabled = true;

        await db.SaveScoreAsync(new ScoreRecord
        {
            PlayerName = settings.PlayerName,
            Score = score,
            Date = DateTime.Now
        });

        string title = win ? "🎉 Победа!" : lang.T("lose");
        string msg = $"{lang.T("score")}: {score}";

        await DisplayAlert(title, msg, "OK");
    }
}
