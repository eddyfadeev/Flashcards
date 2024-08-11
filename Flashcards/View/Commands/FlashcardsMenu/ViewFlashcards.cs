using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.FlashcardsMenu;

internal sealed class ViewFlashcards : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IEditableEntryHandler<IFlashcard> _editableEntryHandler;

    public ViewFlashcards(IFlashcardsRepository flashcardsRepository, IEditableEntryHandler<IFlashcard> editableEntryHandler)
    {
        _flashcardsRepository = flashcardsRepository;
        _editableEntryHandler = editableEntryHandler;
    }

    public void Execute()
    {
        var flashcards = _flashcardsRepository.GetAll().ToList();

        if (flashcards.Count == 0)
        {
            Console.WriteLine("No flashcards found.");
            return;
        }

        _editableEntryHandler.HandleEditableEntry(flashcards);
    }
}