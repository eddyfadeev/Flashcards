using Flashcards.Enums;
using Flashcards.Extensions;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Entity;
using Spectre.Console;

namespace Flashcards;

public class UserInterface
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;
    
    public UserInterface(IStacksRepository stacksRepository, IFlashcardsRepository flashcardsRepository)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
    }
    
    internal void MainMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var choices = GetMenuChoices<MainMenuChoices>();
            var userChoice = GetUserChoice<MainMenuChoices>(choices);

            switch (userChoice)
            {
                case MainMenuChoices.StartStudySession:
                    Console.WriteLine("Not yet implemented");
                    break;
                case MainMenuChoices.ManageStacks:
                    StacksMenu();
                    break;
                case MainMenuChoices.ManageFlashcards:
                    FlashcardsMenu();
                    break;
                case MainMenuChoices.Exit:
                    Console.WriteLine("Goodbye!");
                    isMenuRunning = false;
                    break;
            }
        }
    }
    
    private void StacksMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var choices = GetMenuChoices<StackChoices>();
            var userChoice = GetUserChoice<StackChoices>(choices);

            switch (userChoice)
            {
                case StackChoices.ViewStacks:
                    ViewStacks();
                    break;
                case StackChoices.AddStack:
                    AddStack();
                    break;
                case StackChoices.EditStack:
                    EditStack();
                    break;
                case StackChoices.DeleteStack:
                    DeleteStack();
                    break;
                case StackChoices.ReturnToMainMenu:
                    isMenuRunning = false;
                    break;
            }
        }
    }
    
    private void AddStack()
    {
        var stack = new Stack();
        var stackName = AnsiConsole.Ask<string>("Enter the name of the stack:");

        while (string.IsNullOrWhiteSpace(stackName))
        {
            stackName = AnsiConsole.Ask<string>("[red]Stack name cannot be empty. Please enter a name:[/]");
        }
        
        stack.Name = stackName;
        
        var result = _stacksRepository.Insert(stack);

        AnsiConsole.WriteLine(
            result > 0 ? 
                "[green]Stack added successfully![/]" : 
                "[red]An error occurred while adding the stack.[/]"
            );
    }

    private int ViewStacks()
    {
        var stacks = _stacksRepository.GetAll();

        var stacksArray = stacks as IStack[] ?? stacks.ToArray();
        
        if (stacksArray.Length == 0)
        {
            Console.WriteLine("No stacks found.");
            return 0;
        }
        
        string[] stackNames = stacksArray.Select(stack => stack.Name).ToArray()!;

        var userChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a stack to view:")
                .AddChoices(stackNames)
            );

        var stackId = stacksArray.Single(x => x.Name == userChoice).Id;
        
        return stackId;
    }


    private void EditStack() => throw new NotImplementedException();
    
    private void DeleteStack() => throw new NotImplementedException();

    private void FlashcardsMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var choices = GetMenuChoices<FlashcardChoices>();
            var userChoice = GetUserChoice<FlashcardChoices>(choices);

            switch (userChoice)
            {
                case FlashcardChoices.ViewFlashcards:
                    ViewFlashcards();
                    break;
                case FlashcardChoices.AddFlashcard:
                    AddFlashcard();
                    break;
                case FlashcardChoices.EditFlashcard:
                    EditFlashcard();
                    break;
                case FlashcardChoices.DeleteFlashcard:
                    DeleteFlashcard();
                    break;
                case FlashcardChoices.ReturnToMainMenu:
                    isMenuRunning = false;
                    break;
            }
        }
    }
    
    private void ViewFlashcards() => throw new NotImplementedException();
    
    private void AddFlashcard() => throw new NotImplementedException();
    
    private void EditFlashcard() => throw new NotImplementedException();
    
    private void DeleteFlashcard() => throw new NotImplementedException();
    
    private SelectionPrompt<string> GetMenuChoices<T>() where T : Enum => 
        new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .AddChoices(EnumExtensions.GetDisplayNames<T>().ToArray());
    
    private T GetUserChoice<T>(SelectionPrompt<string> choices) where T : Enum => 
        AnsiConsole.Prompt(choices).GetValueFromDisplayName<T>();
}