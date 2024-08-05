using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Spectre.Console;

namespace Flashcards.View.Commands.StacksMenu;

internal sealed class ChooseStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    
    public ChooseStack(IStacksRepository stacksRepository)
    {
        _stacksRepository = stacksRepository;
    }
    
    public void Execute()
    {
        var stacks = _stacksRepository.GetAll();

        var stacksArray = stacks as IStack[] ?? stacks.ToArray();
        
        if (stacksArray.Length == 0)
        {
            Console.WriteLine("No stacks found.");
            //return 0;
        }
        
        string[] stackNames = stacksArray.Select(stack => stack.Name).ToArray()!;

        var userChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a stack to view:")
                .AddChoices(stackNames)
        );

        var stackId = stacksArray.Single(x => x.Name == userChoice).Id;

        //return stackId;
    }
}