using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Spectre.Console;

namespace Flashcards.View.Commands.MainMenu;

internal sealed class ManageStacks : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IChoosalbeEntryHandler<IStack> _choosableEntryHandler;
    
    public ManageStacks(IStacksRepository stacksRepository, IChoosalbeEntryHandler<IStack> choosableEntryHandler)
    {
        _stacksRepository = stacksRepository;
        _choosableEntryHandler = choosableEntryHandler;
    }
    
    public void Execute()
    {
        // TODO: Assign chosen stack to a variable inside StackRepository
        var entries = _stacksRepository.GetAll().ToList();
        
        if (entries.Count == 0)
        {
            AnsiConsole.WriteLine("No stacks found.");
            return;
        }

        var userChoice = _choosableEntryHandler.HandleChoosableEntry(entries);
    }
}

internal interface IChoosalbeEntryHandler<T> where T : class
{
    T HandleChoosableEntry(IEnumerable<T> entries);
}

internal class ChoosableEntryHandler<T> : IChoosalbeEntryHandler<T> where T : class
{
    public T HandleChoosableEntry(IEnumerable<T> entries) =>
        AnsiConsole.Prompt(GetUserChoice(entries));


    private static SelectionPrompt<T> GetUserChoice(IEnumerable<T> entries) =>
        new SelectionPrompt<T>()
            .Title("Choose an entry: ")
            .AddChoices(entries)
            // TODO: Use interface to access the name property
            .UseConverter(entry => (entry as IStack)?.Name ?? entry.ToString());
}