using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.Repositories;

internal class StacksRepository : IStacksRepository
{
    private readonly IDatabaseManager _databaseManager;
    private readonly IFlashcardsRepository _flashcardsRepository;

    public IStack? SelectedEntry { get; set; }
    
    public StacksRepository(IDatabaseManager databaseManager, IFlashcardsRepository flashcardsRepository)
    {
        _databaseManager = databaseManager;
        _flashcardsRepository = flashcardsRepository;
    }
    
    public int Insert(IDbEntity<IStack> entity)
    {
        var stack = entity.MapToDto();
        const string query = "INSERT INTO Stacks (Name) VALUES (@Name);";

        return _databaseManager.InsertEntity(query, stack);
    }

    public int Delete()
    {
        const string deleteQuery = "DELETE FROM Stacks WHERE Id = @Id;";
        
        var parameters = new { SelectedEntry.Id };
        
        return _databaseManager.DeleteEntry(deleteQuery, parameters);
    }
    
    public int Update()
    {
        if (SelectedEntry is null)
        {
            AnsiConsole.MarkupLine("[red]No stack was chosen.[/]");
            GeneralHelperService.ShowContinueMessage();
            return 0;
        }

        var stack = SelectedEntry.ToDto();
        const string query = "UPDATE Stacks SET Name = @Name WHERE Id = @Id;";

        return _databaseManager.UpdateEntry(query, stack);
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
        if (SelectedEntry is null)
        {
            AnsiConsole.MarkupLine("[red]No stack was chosen.[/]");
            GeneralHelperService.ShowContinueMessage();
            return;
        }

        _flashcardsRepository.StackId = SelectedEntry.Id;
    }
    
    public void SetStackNameInFlashcardsRepository()
    {
        if (SelectedEntry is null)
        {
            AnsiConsole.MarkupLine("[red]No stack was chosen.[/]");
            GeneralHelperService.ShowContinueMessage();
            return;
        }

        _flashcardsRepository.StackName = SelectedEntry.Name;
    }
}