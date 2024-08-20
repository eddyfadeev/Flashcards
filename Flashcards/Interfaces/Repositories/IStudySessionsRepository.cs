using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Repositories;

/// <summary>
/// Represents a repository for study sessions.
/// </summary>
internal interface IStudySessionsRepository :
    IInsertIntoRepository<IStudySession>,
    IGetAllFromRepository<IStudySession>
{
    internal IEnumerable<IStudySession> GetAllStudySessionsByStack(IDbEntity<IStack> stack);
    /// <summary>
    /// Retrieves study sessions by stack ID.
    /// </summary>
    /// <param name="stackId">The ID of the stack.</param>
    /// <returns>An enumerable collection of study sessions.</returns>
    internal IEnumerable<IStudySession> GetByStackId(IDbEntity<IStack> stack);

    /// <summary>
    /// Gets the average yearly study sessions for a given year.
    /// </summary>
    /// <param name="year">The year for which to calculate the average.</param>
    /// <returns>
    /// An enumerable collection of stack monthly sessions representing the average yearly study sessions
    /// for the given year.
    /// </returns>
    internal IEnumerable<IStackMonthlySessions> GetAverageYearly(IYear year);

    /// <summary>
    /// Retrieves a list of all available years in which study sessions have taken place.
    /// </summary>
    /// <returns>A collection of <see cref="IYear"/> objects representing the years.</returns>
    /// <remarks>
    /// This method queries the database and retrieves distinct years from the "StudySessions" table.
    /// </remarks>
    internal IEnumerable<IYear> GetYears();
}