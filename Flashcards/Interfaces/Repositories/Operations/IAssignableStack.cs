using Flashcards.Models.Entity;

namespace Flashcards.Interfaces.Repositories.Operations;

internal interface IAssignableStack
{
    internal Stack? SelectedStack { get; set; }
}