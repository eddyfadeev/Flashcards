using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.MainMenu;

internal sealed class ManageFlashcards : ICommand
{
    private readonly IFlashcardsRepository _flashcardsRepository;

    public ManageFlashcards(IFlashcardsRepository flashcardsRepository)
    {
        _flashcardsRepository = flashcardsRepository;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}