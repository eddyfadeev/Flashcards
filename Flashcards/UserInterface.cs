using Flashcards.Enums;
using Spectre.Console;

namespace Flashcards;

public class UserInterface
{
    internal static void MainMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        MainMenuChoices.StartStudySession,
                        MainMenuChoices.ManageStacks,
                        MainMenuChoices.ManageFlashcards,
                        MainMenuChoices.Exit)
            );

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

    private static void StacksMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {


            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<StackChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        StackChoices.ViewStacks,
                        StackChoices.AddStack,
                        StackChoices.EditStack,
                        StackChoices.DeleteStack,
                        StackChoices.ReturnToMainMenu)
            );

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

    private static void ViewStacks() => throw new NotImplementedException();

    private static void AddStack() => throw new NotImplementedException();
    
    private static void EditStack() => throw new NotImplementedException();
    
    private static void DeleteStack() => throw new NotImplementedException();

    private static void FlashcardsMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<FlashcardChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        FlashcardChoices.ViewFlashcards,
                        FlashcardChoices.AddFlashcard,
                        FlashcardChoices.EditFlashcard,
                        FlashcardChoices.DeleteFlashcard,
                        FlashcardChoices.ReturnToMainMenu
                        )
            );

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
    
    private static void ViewFlashcards() => throw new NotImplementedException();
    
    private static void AddFlashcard() => throw new NotImplementedException();
    
    private static void EditFlashcard() => throw new NotImplementedException();
    
    private static void DeleteFlashcard() => throw new NotImplementedException();
}