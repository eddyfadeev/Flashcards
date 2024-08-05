using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.FlashcardsMenu;

internal sealed class DeleteFlashcard : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    
    public DeleteFlashcard(IFlashcardsRepository flashcardsRepository)
    {
        _flashcardsRepository = flashcardsRepository;
    }
    
    public void Execute()
    {
        throw new NotImplementedException();
    }
}