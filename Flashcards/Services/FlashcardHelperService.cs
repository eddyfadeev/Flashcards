using Spectre.Console;

namespace Flashcards.Services;

public static class FlashcardHelperService
{
    public static string GetQuestion()
    {
        AnsiConsole.MarkupLine("Enter the question:");
        return AnsiConsole.Ask<string>("> ");
    }
    
    public static string GetAnswer()
    {
        AnsiConsole.MarkupLine("Enter the answer:");
        return AnsiConsole.Ask<string>("> ");
    }
}