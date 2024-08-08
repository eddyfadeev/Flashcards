using Flashcards.Extensions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Spectre.Console;

namespace Flashcards.View.Commands.MainMenu;

internal sealed class ManageStacks : ICommand
{
    private readonly IStacksRepository _stacksRepository; 
    private readonly IChoosalbeEntryHandler _choosableEntryHandler;
    
    public ManageStacks(IStacksRepository stacksRepository, IChoosalbeEntryHandler choosableEntryHandler)
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

        var userChoice = _choosableEntryHandler.HandleChoosableEntry(entries);
        
        // TODO: Assign chosen stack to a variable inside StackRepository
        AnsiConsole.WriteLine($"You chose: {userChoice}");
    }
}