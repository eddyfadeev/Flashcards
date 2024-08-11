using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Entity;
using Spectre.Console;

namespace Flashcards.Repositories;

public class FlashcardsRepository : IFlashcardsRepository
{
    private readonly IDatabaseManager _databaseManager;

    public IFlashcard? ChosenEntry { get; set; }
    
    public FlashcardsRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }


    public int Insert(IDbEntity<IFlashcard> entity)
    {
        var stack = entity.MapToDto();
        var query = entity.GetInsertQuery();

        return _databaseManager.InsertEntity(query, stack);
    }

    public void Delete(int id)
    {
        // TODO: Template is good for now, ensure that stackId is properly passed to the delete method
        if (ChosenEntry is null)
        {
            AnsiConsole.MarkupLine("[red]No flashcard was chosen to delete.[/]");
            return;
        }
        
        var parameters = new { Id = id };
        
        const string deleteQuery = "DELETE FROM Flashcards WHERE Id = @Id;";
        
        _databaseManager.DeleteEntry(deleteQuery, parameters);
    }

    public IEnumerable<IFlashcard> GetAll()
    {
        const string query = "SELECT * FROM Flashcards WHERE StackId = @StackId;";
        object parameters = new
        {
            StackId = ChosenEntry?.StackId
        };
        
        var flashcards = _databaseManager.GetAllEntities<Flashcard>(query, parameters);
        
        flashcards = flashcards.Select(flashcard => flashcard.ToEntity());

        return flashcards;
    }
}