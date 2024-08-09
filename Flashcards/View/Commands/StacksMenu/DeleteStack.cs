using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
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
        var chooseCommand = _menuCommandFactory.Create(StackMenuEntries.ChooseStack);
        chooseCommand.Execute();

        var stackId = _stacksRepository.ChosenEntry;

        if (stackId == null)
        {
            AnsiConsole.MarkupLine("[red]No stack was chosen.[/]");
            return;
        }
        
        _stacksRepository.Delete(stackId.Id);

        AnsiConsole.WriteLine($"You deleted a stack: {stackId.Id}");
    }
}