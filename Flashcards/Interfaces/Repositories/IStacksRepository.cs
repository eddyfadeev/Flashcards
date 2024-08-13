using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

internal interface IStacksRepository : 
    IInsertIntoRepository<IStack>,
    IGetAllFromRepository<IStack>,
    IRepositoryEntry<IStack>,
    IDeleteFromRepository,
    IUpdateInRepository
{
    internal void SetStackIdInFlashcardsRepository();
    public void SetStackNameInFlashcardsRepository();
}