using Flashcards.Enums;
using Flashcards.Extensions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Models.Entity;
using Spectre.Console;

namespace Flashcards.Services;

/// <summary>
/// Represents a service for choosing a stack from a menu.
/// </summary>
internal abstract class StackChooserService
{
    /// <summary>
    /// Gets the selected stack from the stack repository.
    /// </summary>
    /// <param name="stackEntryHandler">Menu entry handler for stacks.</param>
    /// <param name="stacksRepository">The stacks repository.</param>
    /// <returns>The selected stack from the stack repository.</returns>
    internal static Stack GetStack(
        IStacksRepository stacksRepository,
        IEditableEntryHandler<IStack> stackEntryHandler)
    {
        var stacks = stacksRepository.GetStackNames().ToList();
        var userChoice = stackEntryHandler.HandleEditableEntry(stacks)?.ToEntity();
        
        if (userChoice is null)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoStackChosenMessage);
            GeneralHelperService.ShowContinueMessage();
            return new Stack
            {
                Name = "Error getting Stack"
            };
        }
        
        return userChoice;
    }
}