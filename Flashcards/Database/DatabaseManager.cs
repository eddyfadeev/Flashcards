using Dapper;
using Flashcards.Interfaces.Database;
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
                $"There was a problem inserting the entity { entity?.GetType().Name } into the database: {ex.Message}"
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
                $"There was a problem getting all entities of type { typeof(TEntity).Name } from the database: {ex.Message}"
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
                $"There was a problem getting all entities of type { typeof(TEntity).Name } from the database: {ex.Message}"
            );
            return new List<TEntity>();
        }
    }
    
    private SqlConnection GetConnection()
    {
        var connection = _connectionProvider.GetConnection();
        connection.Open();
        
        return connection;
    }
}