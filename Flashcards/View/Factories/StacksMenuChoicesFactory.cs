using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.View.Commands.StacksMenu;

namespace Flashcards.View.Factories;

internal sealed class StacksMenuChoicesFactory : MenuChoicesFactoryBaseClass<StackChoice>
{
    private readonly IStacksRepository _stacksRepository;
    public override Dictionary<StackChoice, Func<ICommand>> ChoicesFactory { get; init; }

    public StacksMenuChoicesFactory(IStacksRepository stacksRepository)
    {
        _stacksRepository = stacksRepository;
        ChoicesFactory = InitializeChoices();
    }
    
    private protected override Dictionary<StackChoice, Func<ICommand>> InitializeChoices() =>
        new()
        {
            { StackChoice.AddStack, () => new AddStack(_stacksRepository) },
            { StackChoice.DeleteStack, () => new DeleteStack(_stacksRepository) },
            { StackChoice.EditStack, () => new EditStack(_stacksRepository) },
            { StackChoice.ChooseStack, () => new ChooseStack(_stacksRepository) }
        };
}