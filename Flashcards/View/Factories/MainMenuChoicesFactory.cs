using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factories;
using Flashcards.View.Commands.MainMenu;

namespace Flashcards.View.Factories;

internal sealed class MainMenuChoicesFactory : MenuChoicesFactoryBaseClass<MainMenuChoices>
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IMenuChoicesFactory<StackChoices> _stackChoicesFactory;
    private readonly IMenuChoicesFactory<FlashcardChoices> _flashcardChoicesFactory;
    
    public override Dictionary<MainMenuChoices, Func<ICommand>> ChoicesFactory { get; init; }
    
    public MainMenuChoicesFactory(
        IStacksRepository stacksRepository, 
        IFlashcardsRepository flashcardsRepository,
        IMenuChoicesFactory<StackChoices> stackChoicesFactory,
        IMenuChoicesFactory<FlashcardChoices> flashcardChoicesFactory)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _stackChoicesFactory = stackChoicesFactory;
        _flashcardChoicesFactory = flashcardChoicesFactory;
        
        ChoicesFactory = InitializeChoices();
    }

    private protected override Dictionary<MainMenuChoices, Func<ICommand>> InitializeChoices() =>
        new()
        {
            { MainMenuChoices.StartStudySession, () => new StartStudySession(_stacksRepository, _flashcardsRepository) },   
            { MainMenuChoices.ManageStacks, () => new ManageStacks(_stacksRepository, _stackChoicesFactory) },   
            { MainMenuChoices.ManageFlashcards, () => new ManageFlashcards(_flashcardsRepository) }
        };
}