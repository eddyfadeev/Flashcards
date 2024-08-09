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

    public int InsertEntity<TEntity>(string query, TEntity entity)
    {
        try
        {
            using var connection = GetConnection();
            return connection.Execute(query, entity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"There was a problem inserting the entity {entity?.GetType().Name} into the database: {ex.Message}"
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

    public IEnumerable<TEntity> GetAllEntities<TEntity>(string query, TEntity entity)
    {
        try
        {
            using var connection = GetConnection();

            return connection.Query<TEntity>(query, entity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"There was a problem getting all entities of type {typeof(TEntity).Name} from the database: {ex.Message}"
            );
            return new List<TEntity>();
        }
    }

    public void DeleteEntry(string query, object parameters)
    {
        try
        {
            using var connection = GetConnection();
            
            connection.Execute(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem deleting the entry: {ex.Message}");
        }
    }

    public void BulkInsertRecords(List<Stack> stacks, List<Flashcard> flashcards)
    {
        SqlTransaction? transaction = null;

        try
        {
            using var connection = GetConnection();
            transaction = connection.BeginTransaction();

            connection.Execute("INSERT INTO Stacks (Name) VALUES (@Name);", stacks, transaction: transaction);
            connection.Execute(
                "INSERT INTO Flashcards (StackId, Question, Answer) VALUES (@StackId, @Question, @Answer);",
                flashcards,
                transaction: transaction
            );

            transaction.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem bulk inserting records into the database occured: {ex.Message}");
            transaction?.Rollback();
        }
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