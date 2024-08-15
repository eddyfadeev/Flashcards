using Flashcards.Enums;
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

    internal static void GetFlashcard(IMenuCommandFactory<FlashcardEntries> flashcardMenuCommandFactory)
    {
        var chooseCommand = flashcardMenuCommandFactory.Create(FlashcardEntries.ChooseFlashcard);
        chooseCommand.Execute();
    }
    
    internal static List<IFlashcard> GetFlashcards(IFlashcardsRepository flashcardsRepository)
    {
        var flashcards = flashcardsRepository.GetAll().ToList();
        
        if (flashcards.Count == 0)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoEntriesFoundMessage);
            GeneralHelperService.ShowContinueMessage();
            return new List<IFlashcard>();
        }
        
        return flashcards;
    }
}