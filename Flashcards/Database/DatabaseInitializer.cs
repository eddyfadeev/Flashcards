using Dapper;
using Flashcards.Interfaces.Database;

namespace Flashcards.Database;

internal class DatabaseInitializer : IDatabaseInitializer
{
    private readonly IConnectionProvider _connectionProvider;

    public DatabaseInitializer(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public void Initialize()
    {
        CreateStacks();
        CreateFlashcards();
    }

    private void CreateStacks()
    {
        try
        {
            using var conn = _connectionProvider.GetConnection();

            conn.Open();

            const string createStackTableSql =
                """
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Stacks')
                    CREATE TABLE Stacks (
                        Id INT IDENTITY(1,1) NOT NULL,
                        Name NVARCHAR(60) NOT NULL UNIQUE,
                        PRIMARY KEY (Id)
                    );
                """;

            conn.Execute(createStackTableSql);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem creating the Stacks table: {ex.Message}");
        }
    }

    private void CreateFlashcards()
    {
        try
        {
            using var conn = _connectionProvider.GetConnection();

            conn.Open();

            const string createFlashcardTableSql =
                """
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Flashcards')
                    CREATE TABLE Flashcards (
                        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        Question NVARCHAR(60) NOT NULL,
                        Answer NVARCHAR(60) NOT NULL,
                        StackId INT NOT NULL
                            FOREIGN KEY
                            REFERENCES Stacks(Id)
                            ON DELETE CASCADE
                            ON UPDATE CASCADE
                    );
                """;

            conn.Execute(createFlashcardTableSql);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem creating the Flashcards table: {ex.Message}");
        }
    }
}