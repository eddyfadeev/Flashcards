﻿using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.FlashcardsMenu;

internal sealed class EditFlashcard : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;
    private readonly IMenuCommandFactory<FlashcardEntries> _flashcardMenuCommandFactory;

    public EditFlashcard(
        IFlashcardsRepository flashcardsRepository, 
        IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory,
        IMenuCommandFactory<FlashcardEntries> flashcardMenuCommandFactory)
    {
        _flashcardsRepository = flashcardsRepository;
        _stackMenuCommandFactory = stackMenuCommandFactory;
        _flashcardMenuCommandFactory = flashcardMenuCommandFactory;
    }

    public void Execute()
    {
        StackChooserService.GetStacks(_stackMenuCommandFactory);
        FlashcardHelperService.GetFlashcard(_flashcardMenuCommandFactory);
        var updatedQuestion = FlashcardHelperService.GetQuestion();
        var updatedAnswer = FlashcardHelperService.GetAnswer();
        
        _flashcardsRepository.SelectedEntry.Question = updatedQuestion;
        _flashcardsRepository.SelectedEntry.Answer = updatedAnswer;
        
        var result = _flashcardsRepository.Update();

        AnsiConsole.MarkupLine(result > 0
            ? "[green]Flashcard was successfully updated.[/]"
            : "[red]Flashcard was not updated.[/]");
        Console.ReadKey();
    }
}