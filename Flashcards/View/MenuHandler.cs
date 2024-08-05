using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Factories;

namespace Flashcards.View;

public class MenuHandler
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IMenuChoicesFactory<MainMenuChoices> _mainMenuChoicesFactory;
    private readonly IMenuChoicesFactory<StackChoices> _stackChoicesFactory;
    private readonly IMenuChoicesFactory<FlashcardChoices> _flashcardChoicesFactory;
    
    public MenuHandler(
        IStacksRepository stacksRepository, 
        IFlashcardsRepository flashcardsRepository,
        IMenuChoicesFactory<MainMenuChoices> mainMenuChoicesFactory,
        IMenuChoicesFactory<StackChoices> stackChoicesFactory,
        IMenuChoicesFactory<FlashcardChoices> flashcardChoicesFactory)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _mainMenuChoicesFactory = mainMenuChoicesFactory;
        _stackChoicesFactory = stackChoicesFactory;
        _flashcardChoicesFactory = flashcardChoicesFactory;
    }
    
    public void DisplayMenu()
    {
        var mainMenu = new MainMenu(_mainMenuChoicesFactory);
        mainMenu.ShowMenu();
    }
    
    public void HandleMainMenuChoice(MainMenuChoices choice)
    {
        var command = _mainMenuChoicesFactory.ChoicesFactory[choice]();
        command.Execute();
    }
    
    public void HandleStackChoice(StackChoices choice)
    {
        var command = _stackChoicesFactory.ChoicesFactory[choice]();
        command.Execute();
    }
    
    public void HandleFlashcardChoice(FlashcardChoices choice)
    {
        var command = _flashcardChoicesFactory.ChoicesFactory[choice]();
        command.Execute();
    }
}