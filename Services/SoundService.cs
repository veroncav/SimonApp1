using Microsoft.Maui.Storage;
using Plugin.Maui.Audio;

namespace SimonApp1.Services
{
    public class SoundService
    {
        private readonly IAudioManager _audioManager;
        private IAudioPlayer? _bgPlayer;

        private const string PrefKey = "soundEnabled";
        public bool IsSoundEnabled
        {
            get => Preferences.Get(PrefKey, true);
            set
            {
                Preferences.Set(PrefKey, value);
                if (!value) StopBackground();
                else _ = StartBackgroundAsync(); // включили Ч запустить
            }
        }

        public SoundService(IAudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        // ‘ќЌќ¬јя ћ”«џ ј
        public async Task StartBackgroundAsync()
        {
            if (!IsSoundEnabled) return;

            // если уже играет Ч не создаем второй раз
            if (_bgPlayer is { IsPlaying: true }) return;

            // из встроенных ресурсов (Resources/Raw/bg_music.mp3)
            var stream = await FileSystem.OpenAppPackageFileAsync("bg_music.mp3");
            _bgPlayer = _audioManager.CreatePlayer(stream);
            _bgPlayer.Loop = true;
            _bgPlayer.Volume = 0.35; // громкость фона
            _bgPlayer.Play();
        }

        public void StopBackground()
        {
            if (_bgPlayer != null)
            {
                _bgPlayer.Stop();
                _bgPlayer.Dispose();
                _bgPlayer = null;
            }
        }

        //  ќ–ќ“ »≈ «¬” » (клики/тоны)
        public async Task PlaySfxAsync(string fileName, double volume = 1.0)
        {
            if (!IsSoundEnabled) return;

            using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
            using var player = _audioManager.CreatePlayer(stream);
            player.Volume = volume;
            player.Play();
            // ∆дать окончани€ не об€зательно; плеер сам умрет по using
        }
    }
}
