using Flashcards.Enums;
using Flashcards.Interfaces.View.Commands;
using Spectre.Console;

namespace Flashcards.View;

internal sealed class MainMenuHandler : MenuHandlerBaseClass<MainMenuChoice>, IMainMenuHandler
{
    
    
    private protected override  SelectionPrompt<string> MenuEntries { get; }
    
    public MainMenuHandler(
        IMenuEntries<MainMenuChoice> mainMenuEntries,
        IFlashcardMenuHandler flashcardMenuHandler,
        IStackMenuHandler stackMenuHandler
    )
    {
        MenuEntries = mainMenuEntries.GetMenuEntries();
        _flashcardMenuHandler = flashcardMenuHandler;
        _stackMenuHandler = stackMenuHandler;
    }

    public override void HandleMenu()
    {
        var userChoice = HandleUserChoice(MenuEntries);

        switch (userChoice)
        {
            case MainMenuChoice.StartStudySession:
                StartSession();
                break;
            case MainMenuChoice.ManageFlashcards:
                _flashcardMenuHandler.HandleMenu();
                break;
            case MainMenuChoice.ManageStacks:
                _stackMenuHandler.HandleMenu();
                break;
            case MainMenuChoice.Exit:
                Console.WriteLine("Goodbye!");
                return;
        }
    }
    
    private void StartSession() => throw new NotImplementedException();
}