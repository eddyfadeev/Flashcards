using Flashcards.Enums;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.Interfaces.View.Factory;

internal interface IMenuEntriesInitializer<T> where T : Enum
{
    internal IMenuCommandFactory<StackMenuEntries> StackMenuCommandFactory { get; set; }
    Dictionary<T, Func<ICommand>> InitializeEntries();
}