using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.View.Commands.StacksMenu;

namespace Flashcards.View.Factories;

internal sealed class StacksMenuChoicesFactory : MenuChoicesFactoryBaseClass<StackChoices>
{
    private readonly IStacksRepository _stacksRepository;
    public override Dictionary<StackChoices, Func<ICommand>> ChoicesFactory { get; init; }

    public StacksMenuChoicesFactory(IStacksRepository stacksRepository)
    {
        _stacksRepository = stacksRepository;
        ChoicesFactory = InitializeChoices();
    }
    
    private protected override Dictionary<StackChoices, Func<ICommand>> InitializeChoices() =>
        new()
        {
            { StackChoices.AddStack, () => new AddStack(_stacksRepository) },
            { StackChoices.DeleteStack, () => new DeleteStack(_stacksRepository) },
            { StackChoices.EditStack, () => new EditStack(_stacksRepository) },
            { StackChoices.ChooseStack, () => new ChooseStack(_stacksRepository) }
        };
}