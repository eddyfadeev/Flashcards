using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

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
        throw new NotImplementedException();
    }
}