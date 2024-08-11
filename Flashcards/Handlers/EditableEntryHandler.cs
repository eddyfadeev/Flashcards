using Flashcards.Interfaces.Handlers;
using Spectre.Console;

namespace Flashcards.Handlers;

internal class EditableEntryHandler<TEntry> : IEditableEntryHandler<TEntry>
{
    public TEntry? HandleEditableEntry(List<TEntry> entries)
    {
        var entriesNames = entries.ConvertAll(entry => entry?.ToString());
        
        if (entries.Count == 0)
        {
            AnsiConsole.WriteLine("No entries found.");
            return default;
        }
        
        var userChoice = AnsiConsole.Prompt(GetUserChoice(entriesNames!));

        return entries.Find(entry => entry?.ToString() == userChoice);
    }


    private static SelectionPrompt<string> GetUserChoice(List<string> entriesNames) =>
        new SelectionPrompt<string>()
            .Title("Choose an entry: ")
            .AddChoices(entriesNames);
}