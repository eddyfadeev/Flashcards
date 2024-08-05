using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.StacksMenu;

internal sealed class DeleteStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    
    public DeleteStack(IStacksRepository stacksRepository)
    {
        _stacksRepository = stacksRepository;
    }
    
    public void Execute()
    {
        throw new NotImplementedException();
    }
}