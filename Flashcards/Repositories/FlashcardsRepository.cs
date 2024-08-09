using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;
using Flashcards.Models.Entity;

namespace Flashcards.Repositories;

public class FlashcardsRepository : IFlashcardsRepository
{
    private readonly IDatabaseManager _databaseManager;

    public IFlashcard ChosenEntry { get; set; }
    
    public FlashcardsRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
        ChosenEntry = new Flashcard();
    }


    public int Insert(IDbEntity<IFlashcard> entity)
    {
        var stack = entity.MapToDto();
        var query = entity.GetInsertQuery();

        return _databaseManager.InsertEntity(query, stack);
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IFlashcard> GetAll(int stackId)
    {
        ChosenEntry = new Flashcard
        {
            StackId = stackId
        };
        
        var dto = ChosenEntry.ToDto();
        const string query = "SELECT * FROM Flashcards WHERE StackId = @StackId;";
        
        IEnumerable<IFlashcard> flashcards = _databaseManager.GetAllEntities(query, dto);
        
        flashcards = flashcards.Select(flashcard => flashcard.ToEntity());

        return flashcards;
    }
}