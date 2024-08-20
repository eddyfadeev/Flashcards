using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.FlashcardsMenu;

/// <summary>
/// Represents a command that allows editing a flashcard.
/// </summary>
internal sealed class EditFlashcard : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;
    private readonly IEditableEntryHandler<IFlashcard> _flashcardEntryHandler;

    public EditFlashcard(
        IStacksRepository stacksRepository,
        IFlashcardsRepository flashcardsRepository,
        IEditableEntryHandler<IStack> stackEntryHandler,
        IEditableEntryHandler<IFlashcard> flashcardEntryHandler
        )
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _stackEntryHandler = stackEntryHandler;
        _flashcardEntryHandler = flashcardEntryHandler;
    }

    public void Execute()
    {
        var flashcard = FlashcardHelperService.GetFlashcard(
            _stacksRepository,
            _flashcardsRepository,
            _stackEntryHandler,
            _flashcardEntryHandler
            );
        
        var updatedQuestion = FlashcardHelperService.GetQuestion();
        var updatedAnswer = FlashcardHelperService.GetAnswer();
        
        flashcard.Question = updatedQuestion;
        flashcard.Answer = updatedAnswer;
        
        var result = _flashcardsRepository.Update(flashcard);
        
        AnsiConsole.MarkupLine(result > 0
            ? Messages.Messages.UpdateSuccessMessage
            : Messages.Messages.UpdateFailedMessage);
        GeneralHelperService.ShowContinueMessage();
    }
}