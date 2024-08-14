using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Entity;
using Spectre.Console;

namespace Flashcards.Services;

internal static class StudySessionsHelperService
{
    internal static void SetStackIdsInRepositories(IStack stack, IFlashcardsRepository flashcardsRepository, IStudySessionsRepository studySessionsRepository)
    {
        GeneralHelperService.SetStackIdInRepository(flashcardsRepository, stack);
        GeneralHelperService.SetStackIdInRepository(studySessionsRepository, stack);
    }
    
    internal static StudySession CreateStudySession(List<IFlashcard> flashcards, IStack stack)
    {
        return new StudySession
        {
            Questions = flashcards.Count,
            StackId = stack.Id,
            Date = DateTime.Now
        };
    }
    
    internal static int GetCorrectAnswers(List<IFlashcard> flashcards)
    {
        var correctAnswers = 0;
        
        foreach (var flashcard in flashcards)
        {
            Console.Clear();
            var answer = AnsiConsole.Ask<string>($"{ flashcard.Question }: ");

            while (string.IsNullOrEmpty(answer))
            {
                answer = AnsiConsole.Ask<string>($"Answer cannot be empty. { flashcard.Question }: ");
            }

            if (string.Equals(answer.Trim(), flashcard.Answer, StringComparison.OrdinalIgnoreCase))
            {
                correctAnswers++;
                AnsiConsole.MarkupLine($"{ Messages.Messages.CorrectAnswerMessage }\n");
                GeneralHelperService.ShowContinueMessage();
            }
            else
            {
                AnsiConsole.MarkupLine($"{ Messages.Messages.IncorrectAnswerMessage } Correct answer is { flashcard.Answer }\n");
                GeneralHelperService.ShowContinueMessage();
            }
        }
        AnsiConsole.MarkupLine($"[white]You have { correctAnswers } out of { flashcards.Count }.[/]");
        
        GeneralHelperService.ShowContinueMessage();
        
        return correctAnswers;
    }
    
}