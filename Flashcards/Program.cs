using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Flashcards;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = ServicesConfigurator.ServiceCollection;

        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var mainMenuCommandFactory = serviceProvider.GetRequiredService<IMenuHandler<MainMenuEntries>>();
        mainMenuCommandFactory.HandleMenu();
    }
}