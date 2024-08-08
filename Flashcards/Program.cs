using Flashcards.Database;
using Flashcards.Enums;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Repositories;
using Flashcards.Repositories;
using Flashcards.View;
using Microsoft.Extensions.DependencyInjection;

namespace Flashcards;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigurationProvider, ConfigurationProvider>();
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddTransient<IMenuEntries<MainMenuEntries>, MenuEntries<MainMenuEntries>>();
        services.AddTransient<IMenuEntries<StackMenuEntries>, MenuEntries<StackMenuEntries>>();
        services.AddTransient<IMenuEntries<FlashcardEntries>, MenuEntries<FlashcardEntries>>();
        
        services.AddSingleton<IDatabaseManager, DatabaseManager>();
        services.AddSingleton<IFlashcardsRepository, FlashcardsRepository>();
        services.AddSingleton<IStacksRepository, StacksRepository>();
    }
}