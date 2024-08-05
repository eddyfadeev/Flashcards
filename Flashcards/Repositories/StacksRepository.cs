using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;

namespace Flashcards.Repositories;

public class StacksRepository : IStacksRepository
{
    private readonly IDatabaseManager _databaseManager;
    
    public StacksRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public void Insert(IDbEntity<IStack> entity) => _databaseManager.InsertStack(entity);
    
    public IEnumerable<IStack> GetAll()
    {
        var stacks = _databaseManager.GetAllStacks();
        stacks = stacks.Select(stack => stack.ToEntity());
        
        return stacks;
    }
}