using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Models.Entity;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.FlashcardsMenu;

internal sealed class AddFlashcard : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;

    public AddFlashcard(IFlashcardsRepository flashcardsRepository, IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory)
    {
        _flashcardsRepository = flashcardsRepository;
        _stackMenuCommandFactory = stackMenuCommandFactory;
    }

    public void Execute()
    {
        StackChooserService.GetStacks(_stackMenuCommandFactory);
        var question = FlashcardHelperService.GetQuestion();
        var answer = FlashcardHelperService.GetAnswer();
        
        if (_flashcardsRepository.StackId is null)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoStackChosenMessage);
            GeneralHelperService.ShowContinueMessage();
            return;
        }
        
        var stackId = _flashcardsRepository.StackId;
        
        var flashcard = new Flashcard
        {
            Question = question,
            Answer = answer,
            StackId = stackId!.Value
        };
        
        _flashcardsRepository.Insert(flashcard);
    }
}