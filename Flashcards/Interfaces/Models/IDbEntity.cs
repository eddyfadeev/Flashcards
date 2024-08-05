namespace Flashcards.Interfaces.Models;

public interface IDbEntity<out T>
{
    internal string GetInsertQuery();
    internal T GetObjectForInserting();
}