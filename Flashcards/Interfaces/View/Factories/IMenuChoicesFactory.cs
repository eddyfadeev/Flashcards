using Flashcards.Interfaces.View.Commands;

namespace Flashcards.Interfaces.View.Factories;

public interface IMenuChoicesFactory<T> where T : Enum
{
    protected Dictionary<T, Func<ICommand>> ChoicesFactory { get; }
    ICommand Create(T entry);
}