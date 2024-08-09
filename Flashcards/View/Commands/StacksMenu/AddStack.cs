using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Models.Entity;
using Spectre.Console;

namespace Flashcards.View.Commands.StacksMenu;

internal sealed class AddStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;

    public AddStack(IStacksRepository stacksRepository)
    {
        _stacksRepository = stacksRepository;
    }

    public void Execute()
    {
        var stack = new Stack();
        var stackName = AnsiConsole.Ask<string>("Enter the name of the stack:");

        while (string.IsNullOrWhiteSpace(stackName))
        {
            stackName = AnsiConsole.Ask<string>("[red]Stack name cannot be empty. Please enter a name:[/]");
        }

        stack.Name = stackName;

        var result = _stacksRepository.Insert(stack);

        AnsiConsole.MarkupLine(
            result > 0 ? "[green]Stack added successfully![/]" : "[red]An error occurred while adding the stack.[/]"
        );
    }
}