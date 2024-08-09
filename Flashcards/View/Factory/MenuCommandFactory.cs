using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;

namespace Flashcards.View.Factory;

internal class MenuCommandFactory<T> : IMenuCommandFactory<T> where T : Enum
{
    private readonly Dictionary<T, Func<ICommand>> _entriesFactory;

    public MenuCommandFactory(IMenuEntriesInitializer<T> entriesInitializer)
    {
        _entriesFactory = entriesInitializer.InitializeEntries();
    }

    public ICommand Create(T entry)
    {
        if (_entriesFactory.TryGetValue(entry, out var factory))
        {
            return factory();
        }

        throw new InvalidOperationException($"No factory found for the {entry}");
    }
}