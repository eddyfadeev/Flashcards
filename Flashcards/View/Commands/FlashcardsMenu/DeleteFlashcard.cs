using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.FlashcardsMenu;

internal sealed class DeleteFlashcard : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IMenuCommandFactory<StackMenuEntries> _menuCommandFactory;
    private readonly IEditableEntryHandler<IFlashcard> _editableEntryHandler;

    public DeleteFlashcard(
        IFlashcardsRepository flashcardsRepository, 
        IMenuCommandFactory<StackMenuEntries> menuCommandFactory,
        IEditableEntryHandler<IFlashcard> editableEntryHandler
            )
    {
        _flashcardsRepository = flashcardsRepository;
        _menuCommandFactory = menuCommandFactory;
        _editableEntryHandler = editableEntryHandler;
    }

    public void Execute()
    {
        StackChooserService.GetStacks(_menuCommandFactory);

        var flashcardsList = _flashcardsRepository.GetAll().ToList();
        var flashcard = _editableEntryHandler.HandleEditableEntry(flashcardsList);
        
        if (flashcard is null)
        {
            AnsiConsole.MarkupLine("[red]No flashcard was chosen.[/]");
            return;
        }
        
        _flashcardsRepository.ChosenEntry = flashcard;
        
        var result = _flashcardsRepository.Delete();
        
        AnsiConsole.MarkupLine(
            result > 0 ? 
                "[green]You deleted a flashcard.[/]" : 
                "[red]Error while deleting a flashcard.[/]"
            );
    }
}