using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Repositories;

internal interface IStacksRepository : 
    IInsertIntoRepository<IStack>,
    IGetAllFromRepository<IStack>,
    ISelectableRepositoryEntry<IStack>,
    IDeleteFromRepository,
    IUpdateInRepository
{
    internal void SetStackIdInFlashcardsRepository();
    public void SetStackNameInFlashcardsRepository();
}