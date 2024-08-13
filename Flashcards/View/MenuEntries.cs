using Flashcards.Extensions;
using Flashcards.Interfaces.View;
using Spectre.Console;

namespace Flashcards.View;

internal sealed class MenuEntries<T> : IMenuEntries<T>
    where T : Enum
{
    public SelectionPrompt<string> GetMenuEntries() =>
        new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .AddChoices(EnumExtensions.GetDisplayNames<T>().ToArray());
}