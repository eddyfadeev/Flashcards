using Flashcards.Interfaces.Handlers;
using Spectre.Console;

namespace Flashcards.Handlers;

internal class EditableEntryHandler : IEditableEntryHandler
{
    public string HandleEditableEntry(List<string> entriesNames) =>
        AnsiConsole.Prompt(GetUserChoice(entriesNames));


    private static SelectionPrompt<string> GetUserChoice(List<string> entriesNames) =>
        new SelectionPrompt<string>()
            .Title("Choose an entry: ")
            .AddChoices(entriesNames);
}