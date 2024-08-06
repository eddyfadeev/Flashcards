using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factories;
using Flashcards.View.Commands.FlashcardsMenu;

namespace Flashcards.View.Factories;

internal sealed class FlashcardsMenuChoicesFactory : MenuChoicesFactoryBaseClass<FlashcardChoice>, IFlashcardsMenuChoicesFactory
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    
    public FlashcardsMenuChoicesFactory(IFlashcardsRepository flashcardsRepository)
    {
        _flashcardsRepository = flashcardsRepository;
        ChoicesFactory = InitializeChoices();
    }
    
    public override Dictionary<FlashcardChoice, Func<ICommand>> ChoicesFactory { get; init; }

    private protected override Dictionary<FlashcardChoice, Func<ICommand>> InitializeChoices() =>
        new()
        {
            { FlashcardChoice.ViewFlashcards, () => new ViewFlashcards(_flashcardsRepository) },
            { FlashcardChoice.AddFlashcard, () => new AddFlashcard(_flashcardsRepository) },
            { FlashcardChoice.EditFlashcard, () => new EditFlashcard(_flashcardsRepository) },
            { FlashcardChoice.DeleteFlashcard, () => new DeleteFlashcard(_flashcardsRepository) }
        };
}

internal interface IFlashcardsMenuChoicesFactory : IMenuChoicesFactory<FlashcardChoice>;