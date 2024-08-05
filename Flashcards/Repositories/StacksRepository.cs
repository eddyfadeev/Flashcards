using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;

namespace Flashcards.Repositories;

public class StacksRepository : IStacksRepository
{
    private readonly IDatabaseManager _databaseManager;
    
    public StacksRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public int Insert(IDbEntity<IStack> entity)
    {
        var stack = entity.GetObjectForInserting();
        var query = entity.GetInsertQuery();
        
        return _databaseManager.InsertEntity(query, stack);
    }

    public IEnumerable<IStack> GetAll()
    {
        const string query = "SELECT * FROM Stacks;";
        
        IEnumerable<IStack> stacks = _databaseManager.GetAllEntities<StackDto>(query);
        
        stacks = stacks.Select(stack => stack.ToEntity());
        
        return stacks;
    }
}