using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.View.Factory;
using Spectre.Console;

namespace Flashcards.Services;

internal abstract class StackChooserService
{
    internal static void GetStacks(IMenuCommandFactory<StackMenuEntries> menuCommandFactory)
    {
        var chooseCommand = menuCommandFactory.Create(StackMenuEntries.ChooseStack);
        chooseCommand.Execute();
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