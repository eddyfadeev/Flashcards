using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.Repositories;

internal class FlashcardsRepository : IFlashcardsRepository
{
    private readonly IDatabaseManager _databaseManager;
    
    public int? StackId { get; set; }
    public string? StackName { get; set; }
    public IFlashcard? SelectedEntry { get; set; }
    
    public FlashcardsRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public int Insert(IDbEntity<IFlashcard> entity)
    {
        var stack = entity.MapToDto();
        
        const string query = "INSERT INTO Flashcards (Question, Answer, StackId) VALUES (@Question, @Answer, @StackId);";

        return _databaseManager.InsertEntity(query, stack);
    }

    public int Delete()
    {
        if (FlashcardHelperService.CheckFlashcardForNull(SelectedEntry))
        {
            return 0;
        }
        
        var parameters = new { Id = SelectedEntry.Id };
        
        const string deleteQuery = "DELETE FROM Flashcards WHERE Id = @Id;";
        
        return _databaseManager.DeleteEntry(deleteQuery, parameters);
    }

    public int Update()
    {
        if (FlashcardHelperService.CheckFlashcardForNull(SelectedEntry))
        {
            return 0;
        }

        var flashcard = SelectedEntry.ToDto();
        
        const string query = "UPDATE Flashcards SET Question = @Question, Answer = @Answer WHERE Id = @Id;";
        
        return _databaseManager.UpdateEntry(query, flashcard);
    }

    public IEnumerable<IFlashcard> GetAll()
    {
        const string query = "SELECT * FROM Flashcards WHERE StackId = @StackId;";
        object parameters = new
        {
            StackId
        };
        
        IEnumerable<IFlashcard> flashcards = _databaseManager.GetAllEntities<FlashcardDto>(query, parameters);
        
        flashcards = flashcards.Select(flashcard => flashcard.ToEntity());

        return flashcards;
    }
}