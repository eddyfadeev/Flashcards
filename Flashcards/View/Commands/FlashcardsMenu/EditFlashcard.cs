using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.FlashcardsMenu;

internal sealed class EditFlashcard : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;

    public EditFlashcard(IFlashcardsRepository flashcardsRepository)
    {
        _flashcardsRepository = flashcardsRepository;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}