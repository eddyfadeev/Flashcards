#if DEBUG
using Flashcards.Interfaces.Database;
using Newtonsoft.Json;

namespace Flashcards.SeedData;

/// <summary>
/// This class provides methods for seeding data into the database.
/// </summary>
internal static class SeedData
{
    /// <summary>
    /// Processes the request by reading data from a JSON file, deserializing it, and inserting the data into the database.
    /// </summary>
    /// <param name="databaseManager">The database manager.</param>
    /// <param name="databaseInitializer">The database initializer used to initialize the database.</param>
    internal static void ProcessRequest(IDatabaseManager databaseManager, IDatabaseInitializer databaseInitializer)
    {
        var jsonString = File.ReadAllText(@"DataSeed\DataSeed.json");

        var seedData = DeserializeJson(jsonString);

        if (seedData is null)
        {
            Console.WriteLine("Deserialization failed.");
            return;
        }
        
        databaseManager.DeleteTables();
        databaseInitializer.Initialize();
        var result = databaseManager.BulkInsertRecords(seedData.Stacks, seedData.Flashcards);

        Console.WriteLine(result ? "Data seed was successful." : "Data seed failed.");
    }

    private static SeedDataModel? DeserializeJson(string json)
    {
        try
        {
            SeedDataModel seedData = JsonConvert.DeserializeObject<SeedDataModel>(json);

            if (seedData is null)
            {
                Console.WriteLine("Deserialization returned null.");
            }

            return seedData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Deserialization failed: {ex.Message}");
            return null;
        }
    }
}
#endif