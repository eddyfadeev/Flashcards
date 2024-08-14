using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Factory;

namespace Flashcards.Services;

internal abstract class StackChooserService
{
    internal static IStack GetStacks(
        IMenuCommandFactory<StackMenuEntries> menuCommandFactory,
        IStacksRepository stacksRepository)
    {
        var chooseCommand = menuCommandFactory.Create(StackMenuEntries.ChooseStack);
        chooseCommand.Execute();

        return stacksRepository.SelectedEntry;
    }
}