using Flashcards.Enums;
using Flashcards.Interfaces.View.Factories;

namespace Flashcards.View;

internal sealed class MainMenu : MenuBaseClass<MainMenuChoices>
{
    private protected override IMenuChoicesFactory<MainMenuChoices> ChoicesFactory { get; init; }

    public MainMenu(IMenuChoicesFactory<MainMenuChoices> choicesFactory)
    {
        ChoicesFactory = choicesFactory;
    }
}