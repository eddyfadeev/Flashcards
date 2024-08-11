using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Models.Entity;
using Spectre.Console;

namespace Flashcards.View.Commands.FlashcardsMenu;

internal sealed class AddFlashcard : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;

    public AddFlashcard(IFlashcardsRepository flashcardsRepository)
    {
        _flashcardsRepository = flashcardsRepository;
    }

    public void Execute()
    {
        var question = GetQuestion();
        var answer = GetAnswer();
        
        if (_flashcardsRepository.StackId is null)
        {
            AnsiConsole.MarkupLine("[red]No stack was chosen.[/]");
            return;
        }
        
        var stackId = _flashcardsRepository.StackId;
        
        var flashcard = new Flashcard
        {
            Question = question,
            Answer = answer,
            StackId = stackId!.Value
        };
        
        _flashcardsRepository.Insert(flashcard);
    }
    
    private string GetQuestion()
    {
        AnsiConsole.MarkupLine("Enter the question:");
        return AnsiConsole.Ask<string>("> ");
    }
    
    private string GetAnswer()
    {
        AnsiConsole.MarkupLine("Enter the answer:");
        return AnsiConsole.Ask<string>("> ");
    }
}