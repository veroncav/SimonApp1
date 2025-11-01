using SQLite;
using SimonApp1.Models;

namespace SimonApp1.Database;

public class AppDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public AppDatabase()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "simonapp.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<ScoreRecord>().Wait();
    }

    public Task<int> SaveScoreAsync(ScoreRecord record)
    {
        return _database.InsertAsync(record);
    }

    public Task<List<ScoreRecord>> GetScoresAsync()
    {
        return _database.Table<ScoreRecord>().OrderByDescending(x => x.Score).ToListAsync();
    }
}
