using Flashcards.Extensions;
using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Entity;

internal class Flashcard : IFlashcard, IDbEntity<IFlashcard>
{
    public int Id { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public int StackId { get; set; }

    public IFlashcard MapToDto() => this.ToDto();
    
    public override string ToString() => $"Question: {Question}, Answer: {Answer}";
}