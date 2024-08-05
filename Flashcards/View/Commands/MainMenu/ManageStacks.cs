using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factories;

namespace Flashcards.View.Commands.MainMenu;

internal sealed class ManageStacks : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IMenuChoicesFactory<StackChoices> _flashcardChoicesFactory;

    public ManageStacks(IStacksRepository stacksRepository, IMenuChoicesFactory<StackChoices> flashcardChoicesFactory)
    {
        _stacksRepository = stacksRepository;
        _flashcardChoicesFactory = flashcardChoicesFactory;
    }
    
    public void Execute()
    {
        var entry = _flashcardChoicesFactory.Create(StackChoices.ChooseStack);
        entry.Execute();
    }
}