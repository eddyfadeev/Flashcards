using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factories;

namespace Flashcards.View.Factories;

internal abstract class MenuChoicesFactoryBaseClass<T> : IMenuChoicesFactory<T> where T : Enum
{
    public abstract Dictionary<T, Func<ICommand>> ChoicesFactory { get; init; }

    public ICommand Create(T entry)
    {
        if (ChoicesFactory.TryGetValue(entry, out var factory))
        {
            return factory();
        }
        
        throw new InvalidOperationException($"No factory found for the { entry }");
    }
    
    private protected abstract Dictionary<T, Func<ICommand>> InitializeChoices();
}