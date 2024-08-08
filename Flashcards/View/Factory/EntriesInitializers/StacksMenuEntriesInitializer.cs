using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factories;
using Flashcards.View.Commands.StacksMenu;

namespace Flashcards.View.Factories.EntriesInitializers;

internal class StacksMenuEntriesInitializer : IMenuEntriesInitializer<StackMenuEntries>
{
    private readonly IStacksRepository _stacksRepository;

    public StacksMenuEntriesInitializer(IStacksRepository stacksRepository)
    {
        _stacksRepository = stacksRepository;
    }

    public Dictionary<StackMenuEntries, Func<ICommand>> InitializeEntries() =>
        new()
        {
            { StackMenuEntries.AddStack, () => new AddStack(_stacksRepository) },
            { StackMenuEntries.DeleteStack, () => new DeleteStack(_stacksRepository) },
            { StackMenuEntries.EditStack, () => new EditStack(_stacksRepository) },
            { StackMenuEntries.ChooseStack, () => new ChooseStack(_stacksRepository) }
        };
}