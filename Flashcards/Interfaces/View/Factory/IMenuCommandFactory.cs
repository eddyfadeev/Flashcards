using Flashcards.Interfaces.View.Commands;

namespace Flashcards.Interfaces.View.Factory;

internal interface IMenuCommandFactory<in T> where T : Enum
{
    ICommand Create(T entry);
}