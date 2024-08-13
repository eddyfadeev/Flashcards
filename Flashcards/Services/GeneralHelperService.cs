using Spectre.Console;

namespace Flashcards.Services;

internal static class GeneralHelperService
{
    internal static bool AskForConfirmation()
    {
        var userChoice = AnsiConsole.Confirm(Messages.Messages.DeleteConfirmationMessage);
        
        return userChoice;
    }
    
    internal static void ShowContinueMessage()
    {
        AnsiConsole.MarkupLine(Messages.Messages.AnyKeyToContinueMessage);
        Console.ReadKey();
    }
}