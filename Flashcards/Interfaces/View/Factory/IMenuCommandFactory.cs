using Flashcards.Interfaces.View.Commands;

namespace Flashcards.Interfaces.View.Factory;

public interface IMenuCommandFactory<in T> where T : Enum
{
    ICommand Create(T entry);
}