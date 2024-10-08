﻿using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.FlashcardsMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

/// <summary>
/// Initializes the menu entries for the flashcards menu.
/// </summary>
internal class FlashcardsMenuEntriesInitializer : IMenuEntriesInitializer<FlashcardEntries>
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IStacksRepository _stacksRepository;
    private readonly IEditableEntryHandler<IFlashcard> _flashcardEntryHandler;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;

    public FlashcardsMenuEntriesInitializer(
        IFlashcardsRepository flashcardsRepository, 
        IStacksRepository stacksRepository,
        IEditableEntryHandler<IFlashcard> flashcardEntryHandler,
        IEditableEntryHandler<IStack> stackEntryHandler)
    {
        _flashcardsRepository = flashcardsRepository;
        _stacksRepository = stacksRepository;
        _flashcardEntryHandler = flashcardEntryHandler;
        _stackEntryHandler = stackEntryHandler;
    }

    public Dictionary<FlashcardEntries, Func<ICommand>> InitializeEntries() =>
        new()
        {
            { FlashcardEntries.ChooseFlashcard, () => new ViewFlashcards(
                _flashcardsRepository, 
                _stacksRepository, 
                _flashcardEntryHandler, 
                _stackEntryHandler
                ) 
            },
            { FlashcardEntries.AddFlashcard, () => new AddFlashcard(
                _flashcardsRepository, 
                _stacksRepository, 
                _stackEntryHandler
                ) 
            },
            { FlashcardEntries.EditFlashcard, () => new EditFlashcard(
                _stacksRepository,
                _flashcardsRepository,
                _stackEntryHandler,
                _flashcardEntryHandler
                ) 
            },
            { FlashcardEntries.DeleteFlashcard, () => new DeleteFlashcard(
                _stacksRepository,
                _flashcardsRepository,
                _stackEntryHandler,
                _flashcardEntryHandler
                ) 
            },
            { FlashcardEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException()}
        };
}