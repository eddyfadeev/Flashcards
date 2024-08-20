using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.FlashcardsMenu;

/// <summary>
/// Represents a command that deletes a flashcard.
/// </summary>
internal sealed class DeleteFlashcard : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;
    private readonly IEditableEntryHandler<IFlashcard> _flashcardEntryHandler;

    public DeleteFlashcard(
        IStacksRepository stacksRepository,
        IFlashcardsRepository flashcardsRepository,
        IEditableEntryHandler<IStack> stackEntryHandler,
        IEditableEntryHandler<IFlashcard> flashcardEntryHandler)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _stackEntryHandler = stackEntryHandler;
        _flashcardEntryHandler = flashcardEntryHandler;
    }

    public void Execute()
    {
        var flashcard = FlashcardHelperService.GetFlashcardFromUser(
            _stacksRepository,
            _flashcardsRepository,
            _stackEntryHandler,
            _flashcardEntryHandler
            );

        var confirmation = GeneralHelperService.AskForConfirmation();
        
        if (!confirmation)
        {
            return;
        }
        
        var result = _flashcardsRepository.Delete(flashcard);
        
        AnsiConsole.MarkupLine(
            result > 0 ? 
                Messages.Messages.DeleteSuccessMessage : 
                Messages.Messages.DeleteFailedMessage
            );
        GeneralHelperService.ShowContinueMessage();
    }
}