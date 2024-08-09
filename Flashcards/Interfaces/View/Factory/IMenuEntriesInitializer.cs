using Flashcards.Enums;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.Interfaces.View.Factory;

internal interface IMenuEntriesInitializer<T> where T : Enum
{
    Dictionary<T, Func<ICommand>> InitializeEntries(IMenuCommandFactory<T> menuCommandFactory);
}