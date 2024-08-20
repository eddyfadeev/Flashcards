using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.FlashcardsMenu;

/// <summary>
/// Represents a command that allows the user to choose a flashcard.
/// </summary>
internal sealed class ViewFlashcards : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IStacksRepository _stacksRepository;
    private readonly IEditableEntryHandler<IFlashcard> _editableEntryHandler;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;

    public ViewFlashcards(
        IFlashcardsRepository flashcardsRepository, 
        IStacksRepository stacksRepository,
        IEditableEntryHandler<IFlashcard> editableEntryHandler,
        IEditableEntryHandler<IStack> stackEntryHandler
        )
    {
        _flashcardsRepository = flashcardsRepository;
        _stacksRepository = stacksRepository;
        _editableEntryHandler = editableEntryHandler;
        _stackEntryHandler = stackEntryHandler;
    }

    public void Execute()
    {
        var stack = StackChooserService.GetStack(_stacksRepository, _stackEntryHandler);
        
        _flashcardsRepository.SelectedStack = stack;

        var flashcards = _flashcardsRepository.GetFlashcards(stack).ToList();

        if (flashcards.Count == 0)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoFlashcardsFoundMessage);
            GeneralHelperService.ShowContinueMessage();
            return;
        }
        _editableEntryHandler.HandleEditableEntry(flashcards);
    }
}