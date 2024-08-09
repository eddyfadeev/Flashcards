using Spectre.Console;

namespace Flashcards.Interfaces.Handlers;

internal interface IMenuHandler<out T> where T : Enum
{
    void HandleMenu();
    public T HandleChoosableEntry(SelectionPrompt<string> entries);
}