using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Factory;
using Spectre.Console;

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
    
    internal static bool CheckStackForNull(IStack? stack)
    {
        if (stack is null)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoStackChosenMessage);
            GeneralHelperService.ShowContinueMessage();
            return true;
        }

        return false;
    }
}