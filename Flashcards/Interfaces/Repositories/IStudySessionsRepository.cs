using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Repositories;

/// <summary>
/// Represents a repository for study sessions.
/// </summary>
internal interface IStudySessionsRepository :
    IInsertIntoRepository<IStudySession>,
    IGetAllFromRepository<IStudySession>,
    ISelectableRepositoryEntry<IStudySession>,
    IAssignableStackId,
    IAssignableStackName
{
    internal IEnumerable<IStudySession> GetByStackId(int stackId);
    internal IEnumerable<IStudySession> GetByYear(IYear year);
    internal IEnumerable<IStudySession> GetByMonth(IYear year, IMonth month);
    internal IEnumerable<IYear> GetYears();
    internal IEnumerable<IMonth> GetMonths(IYear year);
}