using Flashcards.Enums;
using Spectre.Console;

namespace Flashcards.View;

internal sealed class StackMenuHandler : MenuHandlerBaseClass<StackChoice>, IStackMenuHandler
{
    private protected override SelectionPrompt<string> MenuEntries { get; }
    
    public StackMenuHandler(IMenuEntries<StackChoice> stackMenuEntries)
    {
        MenuEntries = stackMenuEntries.GetMenuEntries();
    }

    public override void HandleMenu()
    {
        var userChoice = HandleUserChoice(MenuEntries);

        switch (userChoice)
        {
            case StackChoice.AddStack:
                AddStack();
                break;
            case StackChoice.EditStack:
                Ed
        }
    }
}