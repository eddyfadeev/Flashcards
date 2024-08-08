using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;

namespace Flashcards.Repositories;

public class FlashcardsRepository : IFlashcardsRepository
{
    private readonly IDatabaseManager _databaseManager;

    public FlashcardsRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public int Insert(IDbEntity<IFlashcard> entity)
    {
        var stack = entity.GetObjectForInserting();
        var query = entity.GetInsertQuery();

        return _databaseManager.InsertEntity(query, stack);
    }

    public IEnumerable<IFlashcard> GetAll(int stackId)
    {
        const string query = "SELECT * FROM Flashcards WHERE StackId = @StackId;";
        object parameters = new { StackId = stackId };

        IEnumerable<IFlashcard> flashcards = _databaseManager.GetAllEntities<FlashcardDto>(query, parameters);
        flashcards = flashcards.Select(flashcard => flashcard.ToEntity());

        return flashcards;
    }
}