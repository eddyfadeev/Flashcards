using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Factory;
using Spectre.Console;

namespace Flashcards.Services;

internal static class FlashcardHelperService
{
    internal static string GetQuestion()
    {
        AnsiConsole.MarkupLine("Enter the question:");
        return AnsiConsole.Ask<string>("> ");
    }
    
    internal static string GetAnswer()
    {
        AnsiConsole.MarkupLine("Enter the answer:");
        return AnsiConsole.Ask<string>("> ");
    }

    internal static void GetFlashcard(IMenuCommandFactory<FlashcardEntries> menuCommandFactory)
    {
        var chooseCommand = menuCommandFactory.Create(FlashcardEntries.ChooseFlashcard);
        chooseCommand.Execute();
    }
}