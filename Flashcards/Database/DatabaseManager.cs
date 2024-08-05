using Dapper;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Models.Dto;
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

    public int InsertFlashcard(IDbEntity<IFlashcard> flashcard)
    {
        try
        {
            using var connection = GetConnection();

            var query = flashcard.GetInsertQuery();
            var obj = flashcard.GetObjectForInserting();
            
            return connection.Execute(query, obj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"There was a problem inserting the flashcard into the database: {ex.Message}");
            return 0;
        }
    }
    
    public int InsertStack(IDbEntity<IStack> stack)
    {
        try
        {
            using var connection = GetConnection();

            var query = stack.GetInsertQuery();
            var obj = stack.GetObjectForInserting();
            
            return connection.Execute(query, obj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"There was a problem inserting the stack into the database: {ex.Message}");
            return 0;
        }
    }

    public IEnumerable<IStack> GetAllStacks()
    {
        try
        {
            const string query = "SELECT * FROM Stacks;";
            using var connection = GetConnection();
            
            connection.Open();
            
            var records = connection.Query<StackDto>(query);
            
            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<IStack>();  
        }
    }
    
    public IEnumerable<IFlashcard> GetFlashcardsForStack(int stackId)
    {
        try
        {
            const string query = "SELECT * FROM Flashcards WHERE StackId = @StackId;";
            using var connection = GetConnection();
            
            connection.Open();
            
            var records = connection.Query<FlashcardDto>(query, new { StackId = stackId });
            
            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<IFlashcard>();  
        }
    }
    
    private SqlConnection GetConnection()
    {
        var connection = _connectionProvider.GetConnection();
        connection.Open();
        
        return connection;
    }
}