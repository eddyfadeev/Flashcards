using Dapper;
using Flashcards.Interfaces.Database;
using Flashcards.Models.Entity;
using Microsoft.Data.SqlClient;

namespace Flashcards.Database;

public class DatabaseManager : IDatabaseManager
{
    private readonly IConnectionProvider _connectionProvider;

    public DatabaseManager(IConnectionProvider connectionProvider, IDatabaseInitializer databaseInitializer)
    {
        _connectionProvider = connectionProvider;

        databaseInitializer.Initialize();
    }

    public int InsertEntity(string query, object parameters)
    {
        try
        {
            using var connection = GetConnection();
            return connection.Execute(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"There was a problem inserting into the database: {ex.Message}"
            );
            return 0;
        }
    }

    public IEnumerable<TEntity> GetAllEntities<TEntity>(string query)
    {
        try
        {
            using var connection = GetConnection();

            return connection.Query<TEntity>(query);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"There was a problem getting all entities of type {typeof(TEntity).Name} from the database: {ex.Message}"
            );
            return new List<TEntity>();
        }
    }

    public IEnumerable<TEntity> GetAllEntities<TEntity>(string query, object parameters)
    {
        try
        {
            using var connection = GetConnection();

            return connection.Query<TEntity>(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"There was a problem getting all entities of type {typeof(TEntity).Name} from the database: {ex.Message}"
            );
            return new List<TEntity>();
        }
    }

    public int DeleteEntry(string query, object parameters)
    {
        try
        {
            using var connection = GetConnection();
            
            return connection.Execute(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem deleting the entry: {ex.Message}");
            return 0;
        }
    }

    public int UpdateEntry(string query, object parameters)
    {
        try
        {
            using var connection = GetConnection();
            
            return connection.Execute(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem updating the entry: {ex.Message}");
            return 0;
        }
    }

    public bool BulkInsertRecords(List<Stack> stacks, List<Flashcard> flashcards)
    {
        SqlTransaction? transaction = null;
        var seedResult = false;

        try
        {
            using var connection = GetConnection();
            transaction = connection.BeginTransaction();

            var stacksResult = connection.Execute("INSERT INTO Stacks (Name) VALUES (@Name);", stacks, transaction: transaction);
            var flashcardsResult = connection.Execute(
                "INSERT INTO Flashcards (StackId, Question, Answer) VALUES (@StackId, @Question, @Answer);",
                flashcards,
                transaction: transaction
            );

            transaction.Commit();
            
            seedResult = stacksResult > 0 && flashcardsResult > 0;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem bulk inserting records into the database occured: {ex.Message}");
            transaction?.Rollback();
            return false;
        }
        
        return seedResult;
    }

    public void DeleteTables()
    {
        try
        {
            using var connection = GetConnection();

            const string dropFlashcardsQuery = "DROP TABLE IF EXISTS Flashcards;";
            connection.Execute(dropFlashcardsQuery);

            const string dropStacksQuery = "DROP TABLE IF EXISTS Stacks;";
            connection.Execute(dropStacksQuery);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem deleting tables: {ex.Message}");
        }
    }

    private SqlConnection GetConnection()
    {
        var connection = _connectionProvider.GetConnection();
        connection.Open();

        return connection;
    }
}