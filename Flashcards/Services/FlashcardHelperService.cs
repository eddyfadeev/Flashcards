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
        AnsiConsole.MarkupLine(Messages.Messages.EnterFlashcardQuestionMessage);
        return AnsiConsole.Ask<string>(Messages.Messages.PromptArrow);
    }
    
    internal static string GetAnswer()
    {
        AnsiConsole.MarkupLine(Messages.Messages.EnterFlashcardAnswerMessage);
        return AnsiConsole.Ask<string>(Messages.Messages.PromptArrow);
    }

    internal static void GetFlashcard(IMenuCommandFactory<FlashcardEntries> menuCommandFactory)
    {
        var chooseCommand = menuCommandFactory.Create(FlashcardEntries.ChooseFlashcard);
        chooseCommand.Execute();
    }
    
    internal static bool CheckFlashcardForNull(IFlashcard? flashcard)
    {
        if (flashcard is not null)
        {
            return false;
        }
        
        AnsiConsole.MarkupLine(Messages.Messages.NoFlashcardChosenMessage);
        GeneralHelperService.ShowContinueMessage();
        return true;
    }
}