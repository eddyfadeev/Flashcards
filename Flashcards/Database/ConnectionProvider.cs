using Flashcards.Interfaces.Database;
using Microsoft.Data.SqlClient;

namespace Flashcards.Database;

internal class ConnectionProvider : IConnectionProvider
{
    private readonly string _connectionString;
    
    public ConnectionProvider(IConfigurationProvider configurationProvider)
    {
        _connectionString = configurationProvider.GetConfiguration();
    }
    
    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}