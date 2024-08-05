using Flashcards.Extensions;
using Flashcards.Interfaces.View.Factories;
using Spectre.Console;

namespace Flashcards.View;

internal abstract class MenuBaseClass<T> where T : Enum
{
    private protected abstract IMenuChoicesFactory<T> ChoicesFactory { get; init; }

    public virtual void ShowMenu()
    {
        var isMenuRunning = true;
        
        while (isMenuRunning)
        {
            var choices = GetMenuChoices;
            var userChoice = GetUserChoice(choices);
            
            if (userChoice.IsReturnToMainMenuOrExit())
            {
                isMenuRunning = false;
                continue;
            }
            
            var command = ChoicesFactory.Create(userChoice);
            
            command.Execute();
        }
    }
    private protected static SelectionPrompt<string> GetMenuChoices => 
        new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .AddChoices(EnumExtensions.GetDisplayNames<T>().ToArray());
    
    private protected static T GetUserChoice(SelectionPrompt<string> choices) => 
        AnsiConsole.Prompt(choices).GetValueFromDisplayName<T>();
}

