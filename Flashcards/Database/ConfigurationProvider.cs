using Microsoft.Extensions.Configuration;
using IConfigurationProvider = Flashcards.Interfaces.Database.IConfigurationProvider;

namespace Flashcards.Database;

public class ConfigurationProvider : IConfigurationProvider
{
    private const string AppSettingsFileName = "appsettings.json";

    public string GetConfiguration() => BuildConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];

    private IConfiguration BuildConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile(AppSettingsFileName)
            .Build();
}