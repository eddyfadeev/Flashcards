namespace Flashcards.Interfaces.Models;

internal interface IDbEntity<out T>
{
    internal T MapToDto();
}