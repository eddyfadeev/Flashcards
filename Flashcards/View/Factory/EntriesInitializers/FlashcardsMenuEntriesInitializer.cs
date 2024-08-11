using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.FlashcardsMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class FlashcardsMenuEntriesInitializer : IMenuEntriesInitializer<FlashcardEntries>
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IEditableEntryHandler<IFlashcard> _editableEntryHandler;

    public FlashcardsMenuEntriesInitializer(IFlashcardsRepository flashcardsRepository, IEditableEntryHandler<IFlashcard> editableEntryHandler)
    {
        _flashcardsRepository = flashcardsRepository;
        _editableEntryHandler = editableEntryHandler;
    }

    public Dictionary<FlashcardEntries, Func<ICommand>> InitializeEntries(IMenuCommandFactory<FlashcardEntries> menuCommandFactory) =>
        new()
        {
            { FlashcardEntries.ViewFlashcards, () => new ViewFlashcards(_flashcardsRepository, _editableEntryHandler) },
            { FlashcardEntries.AddFlashcard, () => new AddFlashcard(_flashcardsRepository) },
            { FlashcardEntries.EditFlashcard, () => new EditFlashcard(_flashcardsRepository) },
            { FlashcardEntries.DeleteFlashcard, () => new DeleteFlashcard(_flashcardsRepository) },
            { FlashcardEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException()}
        };
}