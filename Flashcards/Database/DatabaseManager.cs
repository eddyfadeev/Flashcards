using Dapper;
using Flashcards.Interfaces.Database;
using Flashcards.Models.Entity;
using Microsoft.Data.SqlClient;

namespace Flashcards.Database;

/// <summary>
/// Manages database operations for the Flashcards application.
/// </summary>
internal class DatabaseManager : IDatabaseManager
{
    private readonly IConnectionProvider _connectionProvider;

    public DatabaseManager(IConnectionProvider connectionProvider, IDatabaseInitializer databaseInitializer)
    {
        _connectionProvider = connectionProvider;
        
        #if DEBUG
        SeedData.SeedData.ProcessRequest(this, databaseInitializer);
        #else
        databaseInitializer.Initialize();
        #endif
    }

    /// <summary>Inserts a new entity into the database.</summary>
    /// <param name="query">The query to execute for inserting the entity.</param>
    /// <param name="parameters">The parameters required by the query.</param>
    /// <returns>The number of affected rows.</returns>
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

    /// <summary>
    /// Retrieves all entities of type TEntity from the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities to be retrieved.</typeparam>
    /// <param name="query">The SQL query to execute.</param>
    /// <returns>A collection of entities retrieved from the database.</returns>
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

    /// <summary>
    /// Retrieves all entities of type TEntity from the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities to retrieve.</typeparam>
    /// <param name="query">The SQL query to execute.</param>
    /// <param name="parameters">The parameters to pass to the query.</param>
    /// <returns>A collection of entities.</returns>
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

    /// <summary>
    /// Deletes an entry from the database based on the given query and parameters.
    /// </summary>
    /// <param name="query">The SQL query used for deleting the entry.</param>
    /// <param name="parameters">The parameters to be used in the query.</param>
    /// <returns>The number of rows affected by the delete operation.</returns>
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

    /// <summary>
    /// Updates an entry in the database.
    /// </summary>
    /// <param name="query">The SQL query to update the entry.</param>
    /// <param name="parameters">The parameters required for the query.</param>
    /// <returns>The number of affected rows.</returns>
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

    /// <summary>
    /// Bulk inserts a list of stacks and flashcards into the database.
    /// </summary>
    /// <param name="stacks">The list of stack objects to be inserted.</param>
    /// <param name="flashcards">The list of flashcard objects to be inserted.</param>
    /// <returns>
    /// <c>true</c> if the bulk insert was successful;
    /// otherwise, <c>false</c>.
    /// </returns>
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

    /// <summary>
    /// Deletes all tables from the database.
    /// </summary>
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