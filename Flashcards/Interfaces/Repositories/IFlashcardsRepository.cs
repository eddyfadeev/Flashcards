using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

internal interface IFlashcardsRepository : IRepository<IFlashcard>
{
    internal IEnumerable<IFlashcard> GetAll();
}