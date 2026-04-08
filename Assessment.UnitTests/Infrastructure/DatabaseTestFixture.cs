using Assessment.Data;
using NUnit.Framework;

namespace Assessment.UnitTests.Infrastructure;

/// <summary>
/// Base test fixture for tests that require a database.
/// Creates a temporary SQLite database file for each test class.
/// </summary>
public abstract class DatabaseTestFixture
{
    protected string DbPath { get; private set; } = null!;
    protected SqliteConnectionFactory DbFactory { get; private set; } = null!;

    [SetUp]
    public void BaseSetUp()
    {
        // Create temporary database file
        DbPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.db");
        var connectionString = $"DataSource={DbPath}";

        DbFactory = new SqliteConnectionFactory(connectionString);

        // Initialize schema using shared DatabaseInitializer
        DatabaseInitializer.InitializeDatabase(connectionString);
    }

    [TearDown]
    public void BaseTearDown()
    {
        // Clean up temporary database file
        if (File.Exists(DbPath))
        {
            File.Delete(DbPath);
        }
    }
}
