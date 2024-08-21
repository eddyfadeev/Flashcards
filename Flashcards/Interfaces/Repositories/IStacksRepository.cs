using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing stacks.
/// </summary>
internal interface IStacksRepository :
    IInsertIntoRepository<IStack>,
    IGetAllFromRepository<IStack>,
    IDeleteFromRepository<IStack>,
    IUpdateInRepository<IStack>
{
    /// <summary>
    /// Retrieves all stack names from the repository.
    /// </summary>
    /// <returns>An enumerable collection of stack objects.</returns>
    internal IEnumerable<IStack> GetStackNames();
}