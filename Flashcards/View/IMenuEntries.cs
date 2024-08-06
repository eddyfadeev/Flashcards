using Spectre.Console;

namespace Flashcards.View;

internal interface IMenuEntries<T> where T : Enum
{
    public SelectionPrompt<string> GetMenuEntries();
}