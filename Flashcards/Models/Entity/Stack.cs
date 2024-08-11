using Flashcards.Extensions;
using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Entity;

public class Stack : IStack, IDbEntity<IStack>
{
    public int Id { get; init; }
    public string? Name { get; set; }

    public string GetInsertQuery() =>
        "INSERT INTO Stacks (Name) VALUES (@Name);";

    public IStack MapToDto() => this.ToDto();
    public override string ToString() => Name;
}