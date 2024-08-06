using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factories;
using Flashcards.View.Commands.MainMenu;

namespace Flashcards.View.Factories;

internal sealed class MainMenuChoicesFactory : MenuChoicesFactoryBaseClass<MainMenuChoice>
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IMenuChoicesFactory<StackChoice> _stackChoicesFactory;
    private readonly IMenuChoicesFactory<FlashcardChoice> _flashcardChoicesFactory;
    
    public override Dictionary<MainMenuChoice, Func<ICommand>> ChoicesFactory { get; init; }
    
    public MainMenuChoicesFactory(
        IStacksRepository stacksRepository, 
        IFlashcardsRepository flashcardsRepository,
        IMenuChoicesFactory<StackChoice> stackChoicesFactory,
        IMenuChoicesFactory<FlashcardChoice> flashcardChoicesFactory)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _stackChoicesFactory = stackChoicesFactory;
        _flashcardChoicesFactory = flashcardChoicesFactory;
        
        ChoicesFactory = InitializeChoices();
    }

    private protected override Dictionary<MainMenuChoice, Func<ICommand>> InitializeChoices() =>
        new()
        {
            { MainMenuChoice.StartStudySession, () => new StartStudySession(_stacksRepository, _flashcardsRepository) },   
            { MainMenuChoice.ManageStacks, () => new ManageStacks(_stacksRepository, _stackChoicesFactory) },   
            { MainMenuChoice.ManageFlashcards, () => new ManageFlashcards(_flashcardsRepository) }
        };
}