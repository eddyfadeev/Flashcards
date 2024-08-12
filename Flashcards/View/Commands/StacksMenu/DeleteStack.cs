using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.StacksMenu;

internal sealed class DeleteStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IMenuCommandFactory<StackMenuEntries> _menuCommandFactory;

    public DeleteStack(IStacksRepository stacksRepository, IMenuCommandFactory<StackMenuEntries> menuCommandFactory)
    {
        _stacksRepository = stacksRepository;
        _menuCommandFactory = menuCommandFactory;
    }

    public void Execute()
    {
        StackChooserService.GetStacks(_menuCommandFactory);

        var stack = _stacksRepository.ChosenEntry;

        if (stack is null)
        {
            AnsiConsole.MarkupLine("[red]No stack was chosen.[/]");
            return;
        }
        
        var result = _stacksRepository.Delete();

        AnsiConsole.MarkupLine(
            result > 0 ? 
                "You deleted a stack." : 
                "[red]Error while deleting a stack.[/]"
            );
    }
}