using Flashcards.Enums;
using Spectre.Console;

namespace Flashcards.View;

internal sealed class FlashcardMenuHandler : MenuHandlerBaseClass<FlashcardChoice>, IFlashcardMenuHandler
{
    private protected override SelectionPrompt<string> MenuEntries { get; }
    
    public FlashcardMenuHandler(IMenuEntries<FlashcardChoice> flashcardMenuEntries)
    {
        MenuEntries = flashcardMenuEntries.GetMenuEntries();
    }

    public override void HandleMenu()
    {
        throw new NotImplementedException();
    }
}