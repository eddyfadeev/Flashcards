using Microsoft.Data.SqlClient;

namespace Flashcards;

public interface IConnectionProvider
{
    SqlConnection GetConnection();
}