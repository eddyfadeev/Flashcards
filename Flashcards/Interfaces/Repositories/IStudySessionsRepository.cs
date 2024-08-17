using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Repositories;

internal interface IStudySessionsRepository :
    IInsertIntoRepository<IStudySession>,
    IGetAllFromRepository<IStudySession>,
    ISelectableRepositoryEntry<IStudySession>,
    IAssignableStackId,
    IAssignableStackName;