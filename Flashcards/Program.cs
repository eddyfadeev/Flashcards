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
        var mainMenuChoicesFactory = serviceProvider.GetRequiredService<IMenuChoicesFactory<MainMenuChoice>>();
        var stackChoicesFactory = serviceProvider.GetRequiredService<IMenuChoicesFactory<StackChoice>>();
        var flashcardChoicesFactory = serviceProvider.GetRequiredService<IMenuChoicesFactory<FlashcardChoice>>();
        
        var mainMenuHandler = serviceProvider.GetRequiredService<IMainMenuHandler>();
        mainMenuHandler.HandleMenu();
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigurationProvider, ConfigurationProvider>();
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddTransient<IMainMenuHandler, MainMenuHandler>();
        services.AddTransient<IStackMenuHandler, StackMenuHandler>();
        services.AddTransient<IFlashcardMenuHandler, FlashcardMenuHandler>();
        services.AddTransient<IMenuEntries<MainMenuChoice>, MenuEntries<MainMenuChoice>>();
        services.AddTransient<IMenuEntries<StackChoice>, MenuEntries<StackChoice>>();
        services.AddTransient<IMenuEntries<FlashcardChoice>, MenuEntries<FlashcardChoice>>();
        services.AddTransient<IMainMenuHandler, MainMenuHandler>();
        services.AddTransient<IStackMenuHandler, StackMenuHandler>();
        services.AddTransient<IFlashcardMenuHandler, FlashcardMenuHandler>();
        
        services.AddSingleton<IDatabaseManager, DatabaseManager>();
        services.AddSingleton<IFlashcardsRepository, FlashcardsRepository>();
        services.AddSingleton<IStacksRepository, StacksRepository>();
        services.AddSingleton<IMenuChoicesFactory<MainMenuChoice>, MainMenuChoicesFactory>();
        services.AddSingleton<IMenuChoicesFactory<StackChoice>, StacksMenuChoicesFactory>();
        services.AddSingleton<IMenuChoicesFactory<FlashcardChoice>, FlashcardsMenuChoicesFactory>();
    }
}