using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Models.Entity;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.StudyMenu;

internal class StartStudySession : ICommand
{
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;
    private readonly IStacksRepository _stacksRepository;
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;
    
    public StartStudySession(
        IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory,
        IStacksRepository stacksRepository,
        IStudySessionsRepository studySessionsRepository,
        IFlashcardsRepository flashcardsRepository
        )
    {
        _stackMenuCommandFactory = stackMenuCommandFactory;
        _stacksRepository = stacksRepository;
        _studySessionsRepository = studySessionsRepository;
        _flashcardsRepository = flashcardsRepository;
    }
    
    public void Execute()
    {
        var stack = StackChooserService.GetStacks(_stackMenuCommandFactory, _stacksRepository);
        
        GeneralHelperService.SetStackIdInRepository(_studySessionsRepository, stack);
        GeneralHelperService.SetStackIdInRepository(_flashcardsRepository, stack);
        
        var flashcards = _flashcardsRepository.GetAll().ToList();
        
        if (flashcards.Count == 0)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoEntriesFoundMessage);
            GeneralHelperService.ShowContinueMessage();
            return;
        }
        
        StudySession studySession = new StudySession
        {
            Questions = flashcards.Count,
            StackId = stack.Id,
            Date = DateTime.Now
        };
        
        var correctAnswers = 0;
        
        foreach (var flashcard in flashcards)
        {
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
        
        studySession.CorrectAnswers = correctAnswers;
        studySession.Time = DateTime.Now - studySession.Date;

        _studySessionsRepository.Insert(studySession);
    }
}