using Spectre.Console;

namespace Flashcards.Services;

internal static class GeneralHelperService
{
    internal static bool AskForConfirmation()
    {
        const string message = "Are you sure you want to delete this entry? [red]This action cannot be undone![/]";
        
        var userChoice = AnsiConsole.Ask<bool>(message);
        
        return userChoice;
    }
    
    internal static void ShowContinueMessage()
    {
        AnsiConsole.MarkupLine("[white]Press any key to continue...[/]");
        Console.ReadKey();
    }
}