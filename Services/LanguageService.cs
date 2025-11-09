using Microsoft.Maui.Storage;

namespace SimonApp1.Services
{
    public class LanguageService
    {
        public string CurrentLanguage
        {
            get => Preferences.Get("lang", "ru");
            set => Preferences.Set("lang", value);
        }

        private readonly Dictionary<string, Dictionary<string, string>> _t = new()
        {
            // 🇷🇺 Russian
            { "ru", new()
                {
                    { "welcome", "Добро пожаловать" },
                    { "start", "Начать игру" },
                    { "settings", "Настройки" },
                    { "records", "Рекорды" },
                    { "score", "Очки" },
                    { "player", "Игрок" },
                    { "enter_name", "Введите имя" },
                    { "ok", "Ок" },
                    { "rules", "Правила игры" },
                    { "rules_text", "Повторяй последовательность цветов. Каждый раунд она становится длиннее. Ошибся — игра окончена." },
                    { "back", "Назад" }
                }
            },

            // 🇬🇧 English
            { "en", new()
                {
                    { "welcome", "Welcome" },
                    { "start", "Start Game" },
                    { "settings", "Settings" },
                    { "records", "High Scores" },
                    { "score", "Score" },
                    { "player", "Player" },
                    { "enter_name", "Enter name" },
                    { "ok", "OK" },
                    { "rules", "Game Rules" },
                    { "rules_text", "Repeat the color sequence. Each round the sequence grows. If you make a mistake — the game ends." },
                    { "back", "Back" }
                }
            }
        };

        public string T(string key)
        {
            var lang = CurrentLanguage;
            return _t.TryGetValue(lang, out var map) && map.TryGetValue(key, out var val)
                ? val
                : key;
        }

        // ✅ Позволяет писать Binding [score]
        public string this[string key] => T(key);
    }
}
