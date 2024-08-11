using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.MainMenu;

internal sealed class StartStudySession : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;

    public StartStudySession(IStacksRepository stacksRepository, IFlashcardsRepository flashcardsRepository)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
    }
    
    public void Execute()
    {
        throw new NotImplementedException();
    }
}