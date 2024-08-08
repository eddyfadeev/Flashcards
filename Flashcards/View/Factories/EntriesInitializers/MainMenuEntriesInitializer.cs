using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factories;
using Flashcards.View.Commands.MainMenu;

namespace Flashcards.View.Factories.EntriesInitializers;

internal class MainMenuEntriesInitializer : IMenuEntriesInitializer<MainMenuEntries>
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;

    public MainMenuEntriesInitializer(IStacksRepository stacksRepository, IFlashcardsRepository flashcardsRepository)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
    }

    public Dictionary<MainMenuEntries, Func<ICommand>> InitializeEntries() =>
        new()
        {
            { MainMenuEntries.StartStudySession, () => new StartStudySession(_stacksRepository, _flashcardsRepository) },
            { MainMenuEntries.ManageStacks, () => new ManageStacks() },
            { MainMenuEntries.ManageFlashcards, () => new ManageFlashcards(_flashcardsRepository) }
        };
}