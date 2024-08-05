using Flashcards.Enums;
using Flashcards.Interfaces.View.Factories;

namespace Flashcards.View;

internal sealed class FlashcardsMenu : MenuBaseClass<FlashcardChoices>
{
    private protected override IMenuChoicesFactory<FlashcardChoices> ChoicesFactory { get; init; }
    
    public FlashcardsMenu(IMenuChoicesFactory<FlashcardChoices> choicesFactory)
    {
        ChoicesFactory = choicesFactory;
    }
}