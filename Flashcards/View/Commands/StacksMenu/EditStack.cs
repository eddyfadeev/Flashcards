using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.StacksMenu;

internal sealed class EditStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;

    public EditStack(IStacksRepository stacksRepository)
    {
        _stacksRepository = stacksRepository;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}