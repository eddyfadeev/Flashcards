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
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;
    private readonly IMenuCommandFactory<FlashcardEntries> _flashcardMenuCommandFactory;

    public DeleteFlashcard(
        IFlashcardsRepository flashcardsRepository, 
        IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory,
        IMenuCommandFactory<FlashcardEntries> flashcardMenuCommandFactory
            )
    {
        _flashcardsRepository = flashcardsRepository;
        _stackMenuCommandFactory = stackMenuCommandFactory;
        _flashcardMenuCommandFactory = flashcardMenuCommandFactory;
    }

    public void Execute()
    {
        StackChooserService.GetStacks(_stackMenuCommandFactory);
        FlashcardHelperService.GetFlashcard(_flashcardMenuCommandFactory);
        
        
        var result = _flashcardsRepository.Delete();
        
        AnsiConsole.MarkupLine(
            result > 0 ? 
                "[green]You deleted a flashcard.[/]" : 
                "[red]Error while deleting a flashcard.[/]"
            );
        Console.ReadKey();
    }
}