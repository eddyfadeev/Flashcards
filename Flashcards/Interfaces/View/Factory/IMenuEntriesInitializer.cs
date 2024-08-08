using Flashcards.Interfaces.View.Commands;

namespace Flashcards.Interfaces.View.Factories;

internal interface IMenuEntriesInitializer<T> where T : Enum
{
    Dictionary<T, Func<ICommand>> InitializeEntries();
}