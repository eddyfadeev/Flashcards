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
    private readonly IMenuCommandFactory<FlashcardEntries> _flashcardMenuCommandFactory;

    public DeleteFlashcard(
        IFlashcardsRepository flashcardsRepository,
        IMenuCommandFactory<FlashcardEntries> flashcardMenuCommandFactory
            )
    {
        _flashcardsRepository = flashcardsRepository;
        _flashcardMenuCommandFactory = flashcardMenuCommandFactory;
    }

    public void Execute()
    {
        FlashcardHelperService.GetFlashcard(_flashcardMenuCommandFactory);

        var confirmation = GeneralHelperService.AskForConfirmation();
        
        if (!confirmation)
        {
            return;
        }
        
        var result = _flashcardsRepository.Delete();
        
        AnsiConsole.MarkupLine(
            result > 0 ? 
                "[green]You deleted a flashcard.[/]" : 
                "[red]Error while deleting a flashcard.[/]"
            );
        GeneralHelperService.ShowContinueMessage();
    }
}