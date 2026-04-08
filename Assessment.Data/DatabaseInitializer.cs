using Microsoft.Data.Sqlite;
using System.Data;

namespace Assessment.Data;

public static class DatabaseInitializer
{
    public static void InitializeDatabase(string connectionString)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        InitializeDatabase(connection);
    }

    public static void InitializeDatabase(IDbConnection connection)
    {
        var createBusinessFactorsTable = @"
            CREATE TABLE IF NOT EXISTS BusinessFactors (
                Business TEXT PRIMARY KEY NOT NULL,
                Factor REAL NOT NULL
            );";

        var createStateFactorsTable = @"
            CREATE TABLE IF NOT EXISTS StateFactors (
                State TEXT PRIMARY KEY NOT NULL,
                Factor REAL NOT NULL
            );";

        var createCarrierEligibilitiesTable = @"
            CREATE TABLE IF NOT EXISTS CarrierEligibilities (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Business TEXT NOT NULL,
                State TEXT NOT NULL,
                IsEligible INTEGER NOT NULL,
                UNIQUE(Business, State)
            );";

        using var command = connection.CreateCommand();

        command.CommandText = createBusinessFactorsTable;
        command.ExecuteNonQuery();

        command.CommandText = createStateFactorsTable;
        command.ExecuteNonQuery();

        command.CommandText = createCarrierEligibilitiesTable;
        command.ExecuteNonQuery();
    }
}
