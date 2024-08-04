using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Entity;

public class Stack : IStack
{
    public int Id { get; set; }
    public string? Name { get; set; }
}