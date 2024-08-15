using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;
using Flashcards.Services;

namespace Flashcards.Repositories;

internal class StacksRepository : IStacksRepository
{
    private readonly IDatabaseManager _databaseManager;

    public IStack? SelectedEntry { get; set; }
    
    public StacksRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
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
        if (GeneralHelperService.CheckForNull(SelectedEntry))
        {
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
}