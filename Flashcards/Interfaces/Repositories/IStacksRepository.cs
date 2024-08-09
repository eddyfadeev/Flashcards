using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

internal interface IStacksRepository : IRepository<IStack>
{
    internal IEnumerable<IStack> GetAll();
}