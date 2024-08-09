using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.StacksMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class StacksMenuEntriesInitializer : IMenuEntriesInitializer<StackMenuEntries>
{
    private readonly IStacksRepository _stacksRepository;
    public IMenuCommandFactory<StackMenuEntries> StackMenuCommandFactory { get; set; }
    private readonly IEditableEntryHandler _editableEntryHandler;

    public StacksMenuEntriesInitializer(
        IStacksRepository stacksRepository,
        IEditableEntryHandler editableEntryHandler
        )
    {
        _stacksRepository = stacksRepository;
        _editableEntryHandler = editableEntryHandler;
    }

    public Dictionary<StackMenuEntries, Func<ICommand>> InitializeEntries() =>
        new()
        {
            { StackMenuEntries.AddStack, () => new AddStack(_stacksRepository) },
            { StackMenuEntries.DeleteStack, () => new DeleteStack(_stacksRepository, StackMenuCommandFactory) },
            { StackMenuEntries.EditStack, () => new EditStack(_stacksRepository) },
            { StackMenuEntries.ChooseStack, () => new ChooseStack(_stacksRepository, _editableEntryHandler) }
        };
}