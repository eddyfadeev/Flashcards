using Dapper;
using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Models.Dto;

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
            using var conn = _connectionProvider.GetConnection();
            conn.Open();

            var query = flashcard.GetInsertQuery();
            var obj = flashcard.GetObjectForInserting();
            
            return conn.Execute(query, obj);
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
            using var conn = _connectionProvider.GetConnection();
            conn.Open();

            var query = stack.GetInsertQuery();
            var obj = stack.GetObjectForInserting();
            
            return conn.Execute(query, obj);
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
            using var conn = _connectionProvider.GetConnection();
            
            conn.Open();
            
            var records = conn.Query<StackDto>(query);
            
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
            using var conn = _connectionProvider.GetConnection();
            
            conn.Open();
            
            var records = conn.Query<FlashcardDto>(query, new { StackId = stackId });
            
            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<IFlashcard>();  
        }
    }
}