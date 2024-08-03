using Microsoft.Data.SqlClient;

namespace Flashcards.Interfaces.Database;

public interface IConnectionProvider
{
    SqlConnection GetConnection();
}