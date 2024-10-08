﻿using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.StacksMenu;

/// <summary>
/// Represents a command to edit a stack.
/// </summary>
internal sealed class EditStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;

    public EditStack(IStacksRepository stacksRepository, IEditableEntryHandler<IStack> stackEntryHandler)
    {
        _stacksRepository = stacksRepository;
        _stackEntryHandler = stackEntryHandler;
    }

    public void Execute()
    {
        var stack = StackChooserService.GetStackFromUser(_stacksRepository, _stackEntryHandler);

        if (GeneralHelperService.CheckForNull(stack))
        {
            return;
        }

        var newStackName = AskNewStackName();
        
        stack.Name = newStackName;

        var result = _stacksRepository.Update(stack);
        
        AnsiConsole.MarkupLine(
            result > 0 ? 
                Messages.Messages.UpdateSuccessMessage : 
                Messages.Messages.UpdateFailedMessage
        );
        GeneralHelperService.ShowContinueMessage();
    }
    
    private static string AskNewStackName()
    {
        var newStackName = AnsiConsole.Ask<string>(Messages.Messages.EnterNameMessage);

        return newStackName;
    }
}