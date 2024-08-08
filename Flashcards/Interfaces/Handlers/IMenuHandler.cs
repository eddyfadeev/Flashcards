namespace Flashcards.Interfaces.Handlers;

internal interface IMenuHandler<T> where T : Enum
{
    void HandleMenu();
}