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

    private List<Button> colorButtons;
    private List<int> sequence = new();
    private List<int> userInput = new();
    private Random rnd = new();
    private int score = 0;
    private bool isUserTurn = false;

    public MainPage(SettingsService settings, LanguageService lang, AppDatabase db)
    {
        InitializeComponent();
        this.settings = settings;
        this.lang = lang;
        this.db = db;

        colorButtons = new List<Button> { GreenButton, RedButton, BlueButton, YellowButton };

        SetLanguage();
        DisableColorButtons();
    }

    private void SetLanguage()
    {
        TitleLabel.Text = lang.T("game");
        StartButton.Text = lang.T("start");
    }

    private void DisableColorButtons() => colorButtons.ForEach(b => b.IsEnabled = false);
    private void EnableColorButtons() => colorButtons.ForEach(b => b.IsEnabled = true);

    private void OnConfirmNameClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(PlayerNameEntry.Text))
            return;

        settings.PlayerName = PlayerNameEntry.Text.Trim();
        NamePopup.IsVisible = false;
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        score = 0;
        sequence.Clear();
        userInput.Clear();
        ScoreLabel.Text = "0";
        StartButton.IsEnabled = false;
        await NextRoundAsync();
    }

    private async Task NextRoundAsync()
    {
        sequence.Add(rnd.Next(4));
        await PlaySequenceAsync();
        userInput.Clear();
        isUserTurn = true;
        EnableColorButtons();
    }

    private async Task PlaySequenceAsync()
    {
        DisableColorButtons();
        foreach (var index in sequence)
        {
            await Highlight(colorButtons[index]);
            await Task.Delay(200);
        }
    }

    private async Task Highlight(Button button)
    {
        var original = button.BackgroundColor;
        button.BackgroundColor = Colors.White;
        await Task.Delay(200);
        button.BackgroundColor = original;
    }

    private async void OnColorClicked(object sender, EventArgs e)
    {
        if (!isUserTurn) return;

        var btn = (Button)sender;
        int index = colorButtons.IndexOf(btn);

        await Highlight(btn);
        userInput.Add(index);

        if (userInput[^1] != sequence[userInput.Count - 1])
        {
            await ShowResult(false);
            return;
        }

        if (userInput.Count == sequence.Count)
        {
            score = sequence.Count;
            ScoreLabel.Text = score.ToString();
            isUserTurn = false;
            await Task.Delay(500);
            await NextRoundAsync();
        }
    }

    private async Task ShowResult(bool win)
    {
        DisableColorButtons();
        StartButton.IsEnabled = true;
        Overlay.IsVisible = true;
        ResultTitle.Text = win ? lang.T("win") : lang.T("lose");
        ResultText.Text = $"{lang.T("score")}: {score}";
        await db.SaveScoreAsync(new ScoreRecord { PlayerName = settings.PlayerName, Score = score, Date = DateTime.Now });
    }

    private void OnRestartClicked(object sender, EventArgs e)
    {
        Overlay.IsVisible = false;
        StartButton.IsEnabled = true;
    }

    private async void OnScoresClicked(object sender, EventArgs e)
    {
        await DisplayAlert(lang.T("records"), "Здесь будет список результатов (сделаем позже)", "OK");
    }
}
