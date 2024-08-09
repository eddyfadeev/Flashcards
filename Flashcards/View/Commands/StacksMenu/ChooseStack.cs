using Flashcards.Extensions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Spectre.Console;

namespace Flashcards.View.Commands.StacksMenu;

internal sealed class ChooseStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IEditableEntryHandler _editableEntryHandler;

    public ChooseStack(IStacksRepository stacksRepository, IEditableEntryHandler editableEntryHandler)
    {
        _stacksRepository = stacksRepository;
        _editableEntryHandler = editableEntryHandler;
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
        
        var userChoice = _editableEntryHandler.HandleEditableEntry(entries);

        _stacksRepository.ChosenEntry = stacks.Find(stack => stack.Name == userChoice);
    }
}