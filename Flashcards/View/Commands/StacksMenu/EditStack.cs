using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.StacksMenu;

internal sealed class EditStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IMenuCommandFactory<StackMenuEntries> _menuCommandFactory;

    public EditStack(IStacksRepository stacksRepository, IMenuCommandFactory<StackMenuEntries> menuCommandFactory)
    {
        _stacksRepository = stacksRepository;
        _menuCommandFactory = menuCommandFactory;
    }

    public void Execute()
    {
        StackChooserService.GetStacks(_menuCommandFactory);

        var newStackName = AskNewStackName();
        
        _stacksRepository.ChosenEntry!.Name = newStackName;

        var result = _stacksRepository.Update();
        
        AnsiConsole.MarkupLine(
            result > 0 ? 
                "[green]Stack name updated successfully![/]" : 
                "[red]An error occurred while updating the stack name.[/]"
        );
        Console.ReadKey();
    }
    
    private static string AskNewStackName()
    {
        var newStackName = AnsiConsole.Ask<string>("Please enter the new name of the stack:");

        return newStackName;
    }
}