using Microsoft.Maui.Controls;
using SimonApp1.Database;
using SimonApp1.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimonApp1.Views;

public partial class MainPage : ContentPage
{
    private readonly SettingsService settings;
    private readonly LanguageService lang;
    private readonly AppDatabase db;
    private readonly SoundService _sound;

    private readonly List<Button> colorButtons;
    private readonly List<int> sequence = new();
    private readonly List<int> userInput = new();
    private readonly Random rnd = new();

    private bool isUserTurn = false;
    private int score = 0;

    public MainPage(SettingsService settings, LanguageService lang, AppDatabase db, SoundService sound)
    {
        InitializeComponent();
        this.settings = settings;
        this.lang = lang;
        this.db = db;
        _sound = sound;

        colorButtons = new() { GreenButton, RedButton, BlueButton, YellowButton };

        DisableColorButtons();
        StartButton.IsEnabled = false;

        // Устанавливаем правильную иконку при входе
        SoundToggleButton.Text = _sound.IsSoundEnabled ? "🔊" : "🔇";
    }

    private void OnSoundToggleClicked(object sender, EventArgs e)
    {
        _sound.IsSoundEnabled = !_sound.IsSoundEnabled;
        SoundToggleButton.Text = _sound.IsSoundEnabled ? "🔊" : "🔇";
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

    private void OnConfirmNameClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(PlayerNameEntry.Text))
            return;

        settings.PlayerName = PlayerNameEntry.Text.Trim();
        NamePopup.IsVisible = false;
        StartButton.IsEnabled = true;
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        StartButton.IsEnabled = false;
        score = 0;
        sequence.Clear();
        userInput.Clear();
        UpdateScore();
        await NextRoundAsync();
    }

    private async Task NextRoundAsync()
    {
        if (sequence.Count >= settings.MaxRounds)
        {
            await WinGameAsync();
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
            await _sound.PlaySfxAsync("click1.wav", 0.8);
            await Task.Delay(200);
        }
    }

    private async void OnColorClicked(object sender, EventArgs e)
    {
        if (!isUserTurn) return;

        var btn = (Button)sender;
        int index = colorButtons.IndexOf(btn);

        await Blink(btn);
        await _sound.PlaySfxAsync("click1.wav", 0.9);

        userInput.Add(index);

        if (userInput[^1] != sequence[userInput.Count - 1])
        {
            await GameOver();
            return;
        }

        if (userInput.Count == sequence.Count)
        {
            score = sequence.Count;
            UpdateScore();
            isUserTurn = false;
            await NextRoundAsync();
        }
    }

    private async Task GameOver()
    {
        DisableColorButtons();
        isUserTurn = false;
        StartButton.IsEnabled = true;
        await DisplayAlert("Проигрыш", $"Очки: {score}", "OK");
    }

    private async Task WinGameAsync()
    {
        DisableColorButtons();
        isUserTurn = false;
        StartButton.IsEnabled = true;
        await DisplayAlert("Победа!", $"Ты прошёл {settings.MaxRounds} раундов!", "Круто!");
    }
}
