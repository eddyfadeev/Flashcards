using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Flashcards;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = ServicesConfigurator.ServiceCollection;

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var stacksRepository = serviceProvider.GetRequiredService<IMenuCommandFactory<StackMenuEntries>>();
        
        ShowWelcomeMessage();
        MakeInitialChoice(stacksRepository);
        
        var mainMenuCommandFactory = serviceProvider.GetRequiredService<IMenuHandler<MainMenuEntries>>();
        mainMenuCommandFactory.HandleMenu();
    }
    
    private static void ShowWelcomeMessage()
    {
        AnsiConsole.WriteLine("Welcome to Flashcards!");
        AnsiConsole.WriteLine("Please choose a stack you would like to work on.");
    }
    
    private static void MakeInitialChoice(IMenuCommandFactory<StackMenuEntries> stacksRepository)
    {
        var userChoice = stacksRepository.Create(StackMenuEntries.ChooseStack);
        userChoice.Execute();
    }
}