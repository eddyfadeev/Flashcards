using Spectre.Console;

namespace Flashcards.View;

internal interface IMenuHandler<T> where T : Enum
{
    void HandleMenu();
}