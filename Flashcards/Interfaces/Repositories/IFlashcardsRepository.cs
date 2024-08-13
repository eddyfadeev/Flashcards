using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

internal interface IFlashcardsRepository : IRepository<IFlashcard>
{
    internal int? StackId { get; set; }
    public string? StackName { get; set; }
    internal IEnumerable<IFlashcard> GetAll();
}