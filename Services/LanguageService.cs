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
            { "ru", new()
                {
                    { "welcome", "Добро пожаловать" },
                    { "game", "Игра" },
                    { "player", "Игрок" },
                    { "score", "Очки" },
                    { "start", "Начать игру" },
                    { "enter_name", "Введите имя" },
                    { "ok", "Ок" }
                }
            },
            { "en", new()
                {
                    { "welcome", "Welcome" },
                    { "game", "Game" },
                    { "player", "Player" },
                    { "score", "Score" },
                    { "start", "Start Game" },
                    { "enter_name", "Enter name" },
                    { "ok", "OK" }
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

        // ✅ ВОТ ЭТО ДОБАВЛЕНО
        public string this[string key] => T(key);
    }
}
