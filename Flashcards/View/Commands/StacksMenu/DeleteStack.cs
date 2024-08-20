using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.StacksMenu;

/// <summary>
/// Represents a command for deleting a stack.
/// </summary>
internal sealed class DeleteStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;

    public DeleteStack(IStacksRepository stacksRepository, IEditableEntryHandler<IStack> stackEntryHandler)
    {
        _stacksRepository = stacksRepository;
        _stackEntryHandler = stackEntryHandler;
    }

    public void Execute()
    {
        var stack = StackChooserService.GetStack(_stacksRepository, _stackEntryHandler);

        if (GeneralHelperService.CheckForNull(stack))
        {
            return;
        }

        var confirmation = GeneralHelperService.AskForConfirmation();
        
        if (!confirmation)
        {
            return;
        }
        
        var result = _stacksRepository.Delete(stack);

        AnsiConsole.MarkupLine(
            result > 0 ? 
                Messages.Messages.DeleteSuccessMessage : 
                Messages.Messages.DeleteFailedMessage
            );
        GeneralHelperService.ShowContinueMessage();
    }
}