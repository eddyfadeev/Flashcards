using Flashcards.Extensions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Spectre.Console;

namespace Flashcards.View.Commands.MainMenu;

internal sealed class ManageStacks : ICommand
{
    private readonly IEditableEntryHandler _choosableEntryHandler;
    private readonly IStacksRepository _stacksRepository;

    public ManageStacks(IStacksRepository stacksRepository, IEditableEntryHandler choosableEntryHandler)
    {
        _stacksRepository = stacksRepository;
        _choosableEntryHandler = choosableEntryHandler;
    }

    public void Execute()
    {
        var stacks = _stacksRepository.GetAll().ToList();
        var entries = stacks.ExtractNamesToList();

        if (entries.Count == 0)
        {
            AnsiConsole.WriteLine("No stacks found.");
            return;
        }

        var userChoice = _choosableEntryHandler.HandleEditableEntry(entries);

        // TODO: Assign chosen stack to a variable inside StackRepository
        AnsiConsole.WriteLine($"You chose: {userChoice}");
    }
}