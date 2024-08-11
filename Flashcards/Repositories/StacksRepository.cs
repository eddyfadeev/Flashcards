using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;
using Flashcards.Models.Entity;
using Spectre.Console;

namespace Flashcards.Repositories;

internal class StacksRepository : IStacksRepository
{
    private readonly IDatabaseManager _databaseManager;
    private readonly IFlashcardsRepository _flashcardsRepository;

    public IStack? ChosenEntry { get; set; }
    
    public StacksRepository(IDatabaseManager databaseManager, IFlashcardsRepository flashcardsRepository)
    {
        _databaseManager = databaseManager;
        _flashcardsRepository = flashcardsRepository;
    }


    public int Insert(IDbEntity<IStack> entity)
    {
        var stack = entity.MapToDto();
        var query = entity.GetInsertQuery();

        return _databaseManager.InsertEntity(query, stack);
    }

    public void Delete(int id)
    {
        const string deleteQuery = "DELETE FROM Stacks WHERE Id = @Id;";
        
        var parameters = new { Id = id };
        
        _databaseManager.DeleteEntry(deleteQuery, parameters);
    }

    public IEnumerable<IStack> GetAll()
    {
        const string query = "SELECT * FROM Stacks;";

        IEnumerable<IStack> stacks = _databaseManager.GetAllEntities<StackDto>(query);

        stacks = stacks.Select(stack => stack.ToEntity());

        return stacks;
    }

    public void SetStackIdInFlashcardsRepository()
    {
        if (ChosenEntry is null)
        {
            AnsiConsole.MarkupLine("[red]No stack was chosen.[/]");
            return;
        }

        _flashcardsRepository.ChosenEntry = new Flashcard
        {
            StackId = ChosenEntry.Id
        };
    }
}