using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.MainMenu;

internal sealed class ManageFlashcards : ICommand
{
    private readonly IMenuHandler<FlashcardEntries> _flashcardsMenuHandler;

    public ManageFlashcards(IMenuHandler<FlashcardEntries> flashcardsMenuHandler)
    {
        _flashcardsMenuHandler = flashcardsMenuHandler;
    }

    public void Execute()
    {
        _flashcardsMenuHandler.HandleMenu();
    }
}