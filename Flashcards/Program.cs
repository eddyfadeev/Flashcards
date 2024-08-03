using Microsoft.Extensions.DependencyInjection;

namespace Flashcards;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var connectionProvider = serviceProvider.GetRequiredService<IConnectionProvider>();
        
        var databaseManager = new DatabaseManager(connectionProvider);
        
        databaseManager.CreateTables();
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigurationProvider, ConfigurationProvider>();
        services.AddTransient<IConnectionProvider, ConnectionProvider>();
        services.AddSingleton<IDatabaseManager, DatabaseManager>();
    }
}