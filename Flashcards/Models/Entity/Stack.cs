using Flashcards.Extensions;
using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Entity;

internal class Stack : IStack, IDbEntity<IStack>
{
    public int Id { get; init; }
    public string? Name { get; set; }

    public IStack MapToDto() => this.ToDto();
    public override string ToString() => Name;
}