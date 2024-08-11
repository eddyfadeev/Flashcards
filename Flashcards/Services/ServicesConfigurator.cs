using Flashcards.Database;
using Flashcards.Enums;
using Flashcards.Handlers;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Repositories;
using Flashcards.View;
using Flashcards.View.Factory;
using Flashcards.View.Factory.EntriesInitializers;
using Microsoft.Extensions.DependencyInjection;

namespace Flashcards.Services;

internal static class ServicesConfigurator
{
    internal static ServiceCollection ServiceCollection { get; } = [];
    
    static ServicesConfigurator()
    {
        ConfigureServices(ServiceCollection);
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigurationProvider, ConfigurationProvider>();
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        services.AddTransient<IMenuEntries<MainMenuEntries>, MenuEntries<MainMenuEntries>>();
        services.AddTransient<IMenuEntries<StackMenuEntries>, MenuEntries<StackMenuEntries>>();
        services.AddTransient<IMenuEntries<FlashcardEntries>, MenuEntries<FlashcardEntries>>();
        services.AddTransient<IMenuEntriesInitializer<MainMenuEntries>, MainMenuEntriesInitializer>();
        services.AddTransient<IMenuEntriesInitializer<StackMenuEntries>, StacksMenuEntriesInitializer>();
        services.AddTransient<IMenuEntriesInitializer<FlashcardEntries>, FlashcardsMenuEntriesInitializer>();
        services.AddTransient<IEditableEntryHandler<IStack>, EditableEntryHandler<IStack>>();
        services.AddTransient<IEditableEntryHandler<IFlashcard>, EditableEntryHandler<IFlashcard>>();
        services.AddTransient<IMenuCommandFactory<MainMenuEntries>, MenuCommandFactory<MainMenuEntries>>();
        services.AddTransient<IMenuCommandFactory<StackMenuEntries>, MenuCommandFactory<StackMenuEntries>>();
        services.AddTransient<IMenuCommandFactory<FlashcardEntries>, MenuCommandFactory<FlashcardEntries>>();
        
        services.AddSingleton<IDatabaseManager, DatabaseManager>();
        services.AddSingleton<IFlashcardsRepository, FlashcardsRepository>();
        services.AddSingleton<IStacksRepository, StacksRepository>();
        services.AddSingleton<IMenuHandler<MainMenuEntries>, MenuHandler<MainMenuEntries>>();
        services.AddSingleton<IMenuHandler<StackMenuEntries>, MenuHandler<StackMenuEntries>>();
        services.AddSingleton<IMenuHandler<FlashcardEntries>, MenuHandler<FlashcardEntries>>();
    }
}