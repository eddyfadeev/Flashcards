using Dapper;
using Flashcards.Interfaces.Database;

namespace Flashcards.Database;

public class DatabaseManager : IDatabaseManager
{
    private readonly IConnectionProvider _connectionProvider;
    
    public DatabaseManager(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public void CreateTables()
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
                        Name NVARCHAR(30) NOT NULL UNIQUE,
                        PRIMARY KEY (Id)
                    );
                """;

            conn.Execute(createStackTableSql);

            Console.WriteLine("Stacks table created successfully.");
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
                        Question NVARCHAR(30) NOT NULL,
                        Answer NVARCHAR(30) NOT NULL,
                        StackId INT NOT NULL
                            FOREIGN KEY
                            REFERENCES Stacks(Id)
                            ON DELETE CASCADE
                            ON UPDATE CASCADE
                    );
                """;
            
            conn.Execute(createFlashcardTableSql);
            
            Console.WriteLine("Flashcards table created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem creating the Flashcards table: {ex.Message}");
        }
    }
}