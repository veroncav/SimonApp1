using SQLite;

namespace SimonApp1.Models
{
    public class ScoreRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public int Score { get; set; }

        // "Win" или "Lose"
        public string Result { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
