using Flashcards.Extensions;
using Spectre.Console;

namespace Flashcards.View;

internal abstract class MenuHandlerBaseClass<T> where T : Enum
{
    private protected abstract SelectionPrompt<string> MenuEntries { get; }
    public abstract void HandleMenu();
    
    private protected static T HandleUserChoice(SelectionPrompt<string> choices) => 
        AnsiConsole.Prompt(choices).GetValueFromDisplayName<T>();
}

//
// public void HandleMainMenu()
//     {
//         
//
//         //
//         // var mainMenu = new MainMenu(_mainMenuChoicesFactory);
//         // var choice = mainMenu.DisplayMenu();
//         //
//         // var userChoice = HandleUserChoice(choice);
//         //
//         // HandleMainMenuChoice(userChoice);
//     }
//     
//     public void HandleStackChoice()
//     {
//         var selection = MenuBaseClass<StackChoice>.DisplayMenu();
//         var userChoice = HandleUserChoice(selection);
//         var command = _stackChoicesFactory.Create(userChoice);
//         //command.Execute();
//     }
//     
//     public void HandleFlashcardChoice()
//     {
//         //var command = _flashcardChoicesFactory.Create(choice);
//         //command.Execute();
//     }
//
//     
//     
    
