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
            { "score", "Очки:" },

            { "start", "Начать игру" },
            { "restart", "Заново" },
            { "save", "Сохранить" },

            { "settings", "Настройки" },
            { "records", "Рекорды" },
            { "sound", "Звук" },
            { "theme", "Тема" },
            { "language", "Язык" },
            { "max_rounds", "Количество раундов" },   
            { "back", "Назад" },

            { "warning", "Внимание" },
            { "enter_name", "Введите имя перед началом игры" },
            { "soon", "Функция скоро будет доступна" },
            { "saved", "Сохранено" },
            { "records_saved", "Рекорд сохранён" },
            { "error", "Ошибка" },
            { "wrong", "Неверная последовательность" }
        }
    },
    { "en", new()
        {
            { "welcome", "Welcome" },
            { "game", "Game" },
            { "player", "Player" },
            { "score", "Score:" },

            { "start", "Start Game" },
            { "restart", "Restart" },
            { "save", "Save" },

            { "settings", "Settings" },
            { "records", "High Scores" },
            { "sound", "Sound" },
            { "theme", "Theme" },
            { "language", "Language" },
            { "max_rounds", "Number of rounds" }, 
            { "back", "Back" },

            { "warning", "Warning" },
            { "enter_name", "Enter your name before starting" },
            { "soon", "Feature coming soon" },
            { "saved", "Saved" },
            { "records_saved", "Record saved" },
            { "error", "Error" },
            { "wrong", "Wrong sequence" }
        }
    },
    { "et", new()
        {
            { "welcome", "Tere tulemast" },
            { "game", "Mäng" },
            { "player", "Mängija" },
            { "score", "Punktid:" },

            { "start", "Alusta mängu" },
            { "restart", "Alusta uuesti" },
            { "save", "Salvesta" },

            { "settings", "Seaded" },
            { "records", "Rekordid" },
            { "sound", "Heli" },
            { "theme", "Teema" },
            { "language", "Keel" },
            { "max_rounds", "Raundide arv" }, 
            { "back", "Tagasi" },

            { "warning", "Hoiatus" },
            { "enter_name", "Sisesta nimi enne mängu" },
            { "soon", "Funktsioon tuleb varsti" },
            { "saved", "Salvestatud" },
            { "records_saved", "Rekord salvestatud" },
            { "error", "Viga" },
            { "wrong", "Vale jada" }
        }
    },
};

        public string T(string key)
        {
            var lang = CurrentLanguage;
            return _t.TryGetValue(lang, out var map) && map.TryGetValue(key, out var val)
                ? val
                : key;
        }
    }
}
