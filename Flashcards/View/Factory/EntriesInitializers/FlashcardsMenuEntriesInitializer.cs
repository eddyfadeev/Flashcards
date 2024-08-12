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
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;

    public FlashcardsMenuEntriesInitializer(
        IFlashcardsRepository flashcardsRepository, 
        IEditableEntryHandler<IFlashcard> editableEntryHandler,
        IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory)
    {
        _flashcardsRepository = flashcardsRepository;
        _editableEntryHandler = editableEntryHandler;
        _stackMenuCommandFactory = stackMenuCommandFactory;
    }

    public Dictionary<FlashcardEntries, Func<ICommand>> InitializeEntries(IMenuCommandFactory<FlashcardEntries> flashcardsMenuCommandFactory) =>
        new()
        {
            { FlashcardEntries.ChooseFlashcard, () => new ChooseFlashcard(_flashcardsRepository, _editableEntryHandler, _stackMenuCommandFactory) },
            { FlashcardEntries.AddFlashcard, () => new AddFlashcard(_flashcardsRepository, _stackMenuCommandFactory) },
            { FlashcardEntries.EditFlashcard, () => new EditFlashcard(_flashcardsRepository, _stackMenuCommandFactory, flashcardsMenuCommandFactory) },
            { FlashcardEntries.DeleteFlashcard, () => new DeleteFlashcard(_flashcardsRepository, _stackMenuCommandFactory, flashcardsMenuCommandFactory) },
            { FlashcardEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException()}
        };
}