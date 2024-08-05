using System.ComponentModel.Design;
using Flashcards.Database;
using Flashcards.DataSeed;
using Flashcards.Enums;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Factories;
using Flashcards.Repositories;
using Flashcards.View;
using Flashcards.View.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Flashcards;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var flashcardsRepository = serviceProvider.GetRequiredService<IFlashcardsRepository>();
        var stacksRepository = serviceProvider.GetRequiredService<IStacksRepository>();
        var mainMenuChoicesFactory = serviceProvider.GetRequiredService<IMenuChoicesFactory<MainMenuChoices>>();
        var stackChoicesFactory = serviceProvider.GetRequiredService<IMenuChoicesFactory<StackChoices>>();
        var flashcardChoicesFactory = serviceProvider.GetRequiredService<IMenuChoicesFactory<FlashcardChoices>>();

        var ui = new MenuHandler(stacksRepository, flashcardsRepository, mainMenuChoicesFactory, stackChoicesFactory,
            flashcardChoicesFactory);
        
        ui.DisplayMenu();
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigurationProvider, ConfigurationProvider>();
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        
        services.AddSingleton<IDatabaseManager, DatabaseManager>();
        services.AddSingleton<IFlashcardsRepository, FlashcardsRepository>();
        services.AddSingleton<IStacksRepository, StacksRepository>();
        services.AddSingleton<IMenuChoicesFactory<MainMenuChoices>, MainMenuChoicesFactory>();
        services.AddSingleton<IMenuChoicesFactory<StackChoices>, StacksMenuChoicesFactory>();
        services.AddSingleton<IMenuChoicesFactory<FlashcardChoices>, FlashcardsMenuChoicesFactory>();
    }
}