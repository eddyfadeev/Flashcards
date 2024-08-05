using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;

namespace Flashcards.Repositories;

public class FlashcardsRepository : IFlashcardsRepository
{
    private readonly IDatabaseManager _databaseManager;
    
    public FlashcardsRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public void Insert(IDbEntity<IFlashcard> entity) => _databaseManager.InsertFlashcard(entity);
    
    public IEnumerable<IFlashcard> GetAll(int stackId)
    {
        var flashcards = _databaseManager.GetFlashcardsForStack(stackId);
        flashcards = flashcards.Select(flashcard => flashcard.ToEntity());
        
        return flashcards;
    }
}