using Flashcards.Enums;
using Flashcards.Extensions;
using Flashcards.Interfaces.View.Factories;

namespace Flashcards.View;

internal sealed class StacksMenu : MenuBaseClass<StackChoices>
{
    private protected override IMenuChoicesFactory<StackChoices> ChoicesFactory { get; init; }
    
    public StacksMenu(IMenuChoicesFactory<StackChoices> choicesFactory)
    {
        ChoicesFactory = choicesFactory;
    }
}