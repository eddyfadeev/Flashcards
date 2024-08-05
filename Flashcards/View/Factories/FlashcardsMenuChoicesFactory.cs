using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.View.Commands.FlashcardsMenu;

namespace Flashcards.View.Factories;

internal sealed class FlashcardsMenuChoicesFactory : MenuChoicesFactoryBaseClass<FlashcardChoices>
{
    private readonly IFlashcardsRepository _flashcardsRepository;
    
    public FlashcardsMenuChoicesFactory(IFlashcardsRepository flashcardsRepository)
    {
        _flashcardsRepository = flashcardsRepository;
        ChoicesFactory = InitializeChoices();
    }
    
    public override Dictionary<FlashcardChoices, Func<ICommand>> ChoicesFactory { get; init; }

    private protected override Dictionary<FlashcardChoices, Func<ICommand>> InitializeChoices() =>
        new()
        {
            { FlashcardChoices.ViewFlashcards, () => new ViewFlashcards(_flashcardsRepository) },
            { FlashcardChoices.AddFlashcard, () => new AddFlashcard(_flashcardsRepository) },
            { FlashcardChoices.EditFlashcard, () => new EditFlashcard(_flashcardsRepository) },
            { FlashcardChoices.DeleteFlashcard, () => new DeleteFlashcard(_flashcardsRepository) }
        };
}