using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.FlashcardsMenu;

internal sealed class ViewFlashcards : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IEditableEntryHandler<IFlashcard> _editableEntryHandler;
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;

    public ViewFlashcards(
        IFlashcardsRepository flashcardsRepository, 
        IEditableEntryHandler<IFlashcard> editableEntryHandler,
        IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory)
    {
        _flashcardsRepository = flashcardsRepository;
        _editableEntryHandler = editableEntryHandler;
        _stackMenuCommandFactory = stackMenuCommandFactory;
    }

    public void Execute()
    {
        StackHelperService.GetStacks(_stackMenuCommandFactory);
        var flashcards = _flashcardsRepository.GetAll().ToList();

        if (flashcards.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No flashcards found.[/]");
            return;
        }

        var userChoice = _editableEntryHandler.HandleEditableEntry(flashcards);
        
        if (userChoice is null)
        {
            AnsiConsole.MarkupLine("[red]No flashcard chosen.[/]");
            return;
        }
        
        _flashcardsRepository.ChosenEntry = userChoice;
    }
}