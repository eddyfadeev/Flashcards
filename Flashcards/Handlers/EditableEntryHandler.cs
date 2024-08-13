using Flashcards.Interfaces.Handlers;
using Spectre.Console;

namespace Flashcards.Handlers;

internal class EditableEntryHandler<TEntry> : IEditableEntryHandler<TEntry> where TEntry : class
{
    public TEntry? HandleEditableEntry(List<TEntry> entries)
    {
        if (entries.Count == 0)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoEntriesFoundMessage);
            return default;
        }
        
        var entriesNames = entries.ConvertAll(entry => entry.ToString());
        
        var userChoice = AnsiConsole.Prompt(GetUserChoice(entriesNames!));

        return entries.Find(entry => entry.ToString() == userChoice);
    }


    private static SelectionPrompt<string> GetUserChoice(List<string> entriesNames) =>
        new SelectionPrompt<string>()
            .Title(Messages.Messages.ChooseEntryMessage)
            .AddChoices(entriesNames);
}