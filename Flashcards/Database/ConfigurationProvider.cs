﻿using Microsoft.Extensions.Configuration;
using IConfigurationProvider = Flashcards.Interfaces.Database.IConfigurationProvider;

namespace Flashcards.Database;

internal class ConfigurationProvider : IConfigurationProvider
{
    private const string AppSettingsFileName = "appsettings.json";

    public string GetConfiguration() => BuildConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];

    private static IConfiguration BuildConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile(AppSettingsFileName)
            .Build();
}