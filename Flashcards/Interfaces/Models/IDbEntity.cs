namespace Flashcards.Interfaces.Models;

public interface IDbEntity<out T>
{
    internal T MapToDto();
}