using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Flashcards;

internal static class Program
{
    private static void Main(string[] args)
    {
        var serviceCollection = ServicesConfigurator.ServiceCollection;

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var mainMenuHandler = serviceProvider.GetRequiredService<IMenuHandler<MainMenuEntries>>();
        
        ShowWelcomeMessage();
        ShowMainMenu(mainMenuHandler);
    }
    
    private static void ShowWelcomeMessage()
    {
        AnsiConsole.WriteLine("Welcome to Flashcards!");
        AnsiConsole.WriteLine("Please choose a stack you would like to work on.");
    }
    
    private static void ShowMainMenu(IMenuHandler<MainMenuEntries> menuHandler)
    {
        while (true)
        {
            Console.Clear();
            try
            {
                menuHandler.HandleMenu();
            }
            catch (ReturnToMainMenuException)
            {
                // Catching the exception to return to the main menu
            }
            catch (ExitApplicationException ex)
            {
                AnsiConsole.WriteLine(ex.Message);
                break;
            }
        }
    }
}