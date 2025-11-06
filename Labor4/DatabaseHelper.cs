using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;

public static class DatabaseHelper
{
    private static string dbFile = "keywords.db";
    private static string connectionString = $"Data Source={dbFile};Version=3;";

    // 1. Adatbázis és tábla létrehozása, ha nem létezik
    public static void InitializeDatabase()
    {
        if (!File.Exists(dbFile))
        {
            SQLiteConnection.CreateFile(dbFile);
        }

        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string sql = "CREATE TABLE IF NOT EXISTS Keywords (Id INTEGER PRIMARY KEY AUTOINCREMENT, Word TEXT NOT NULL UNIQUE)";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    // 2. Kulcsszavak betöltése (10. pont)
    public static List<string> LoadKeywords()
    {
        var keywords = new List<string>();
        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string sql = "SELECT Word FROM Keywords";
            using (var cmd = new SQLiteCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    keywords.Add(reader["Word"].ToString());
                }
            }
        }
        return keywords;
    }

    // 3. Kulcsszó hozzáadása (4. pont)
    public static void AddKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return;

        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            // Használjunk paramétert az SQL Injection ellen
            string sql = "INSERT OR IGNORE INTO Keywords (Word) VALUES (@word)";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@word", keyword.Trim());
                cmd.ExecuteNonQuery();
            }
        }
    }

    // 4. Kulcsszó törlése (6. pont)
    public static void DeleteKeyword(string keyword)
    {
        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string sql = "DELETE FROM Keywords WHERE Word = @word";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@word", keyword);
                cmd.ExecuteNonQuery();
            }
        }
    }
}