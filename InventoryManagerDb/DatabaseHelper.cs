using System.Data.SQLite;
using System.IO;

public static class DatabaseHelper
{
    private static string dbFile = "Inventory.db";
    private static string connectionString = $"Data Source={dbFile};Version=3;";

    public static void InitializeDatabase()
    {
        // Step 1: Create DB file if it doesn't exist
        if (!File.Exists(dbFile))
        {
            SQLiteConnection.CreateFile(dbFile);
        }

        // Step 2: Connect to DB
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            // Step 3: Create table
            string query = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    Quantity INTEGER NOT NULL CHECK(Quantity >= 0),
                    Price REAL NOT NULL CHECK(Price >= 0)
                );";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}