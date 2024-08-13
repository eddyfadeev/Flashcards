using Spectre.Console;

namespace Flashcards.Interfaces.View;

internal interface IMenuEntries<T> where T : Enum
{
    public SelectionPrompt<string> GetMenuEntries();
}