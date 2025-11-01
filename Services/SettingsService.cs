namespace SimonApp1.Services
{
    public class SettingsService
    {
        const string NAME_KEY = "player_name";
        const string MAX_KEY = "max_rounds";
        const string SOUND_KEY = "sound_on";

        public string PlayerName
        {
            get => Preferences.Get(NAME_KEY, "Player");
            set => Preferences.Set(NAME_KEY, value);
        }

        public int MaxRounds
        {
            get => Preferences.Get(MAX_KEY, 10);
            set => Preferences.Set(MAX_KEY, value);
        }

        public bool SoundOn
        {
            get => Preferences.Get(SOUND_KEY, true);
            set => Preferences.Set(SOUND_KEY, value);
        }
    }
}
