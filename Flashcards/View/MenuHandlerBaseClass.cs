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