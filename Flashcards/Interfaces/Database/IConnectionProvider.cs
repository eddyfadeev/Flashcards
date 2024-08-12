using Microsoft.Data.SqlClient;

namespace Flashcards.Interfaces.Database;

internal interface IConnectionProvider
{
    SqlConnection GetConnection();
}