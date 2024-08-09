﻿using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.FlashcardsMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class FlashcardsMenuEntriesInitializer : IMenuEntriesInitializer<FlashcardEntries>
{
    private readonly IFlashcardsRepository _flashcardsRepository;

    public FlashcardsMenuEntriesInitializer(IFlashcardsRepository flashcardsRepository)
    {
        _flashcardsRepository = flashcardsRepository;
    }

    public Dictionary<FlashcardEntries, Func<ICommand>> InitializeEntries(IMenuCommandFactory<FlashcardEntries> menuCommandFactory) =>
        new()
        {
            { FlashcardEntries.ViewFlashcards, () => new ViewFlashcards(_flashcardsRepository) },
            { FlashcardEntries.AddFlashcard, () => new AddFlashcard(_flashcardsRepository) },
            { FlashcardEntries.EditFlashcard, () => new EditFlashcard(_flashcardsRepository) },
            { FlashcardEntries.DeleteFlashcard, () => new DeleteFlashcard(_flashcardsRepository) }
        };
}