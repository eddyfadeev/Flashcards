using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Repositories;

internal interface IFlashcardsRepository : 
    IInsertIntoRepository<IFlashcard>,
    IGetAllFromRepository<IFlashcard>,
    IDeleteFromRepository,
    IUpdateInRepository,
    ISelectableRepositoryEntry<IFlashcard>,
    IAssignableStackId
{
    public string? StackName { get; set; }
}