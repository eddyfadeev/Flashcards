using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Repositories;

internal interface IFlashcardsRepository : 
    IInsertIntoRepository<IFlashcard>,
    IGetAllFromRepository<IFlashcard>,
    IRepositoryEntry<IFlashcard>,
    IDeleteFromRepository,
    IUpdateInRepository
{
    internal int? StackId { get; set; }
    public string? StackName { get; set; }
}