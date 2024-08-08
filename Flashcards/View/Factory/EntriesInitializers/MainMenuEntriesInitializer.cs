using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factories;
using Flashcards.View.Commands.MainMenu;

namespace Flashcards.View.Factories.EntriesInitializers;

internal class MainMenuEntriesInitializer : IMenuEntriesInitializer<MainMenuEntries>
{
    private readonly IEditableEntryHandler _choosableEntryHandler;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IStacksRepository _stacksRepository;

    public MainMenuEntriesInitializer(
        IStacksRepository stacksRepository,
        IFlashcardsRepository flashcardsRepository,
        IEditableEntryHandler choosableEntryHandler)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _choosableEntryHandler = choosableEntryHandler;
    }

    public Dictionary<MainMenuEntries, Func<ICommand>> InitializeEntries() =>
        new()
        {
            {
                MainMenuEntries.StartStudySession, () => new StartStudySession(_stacksRepository, _flashcardsRepository)
            },
            { MainMenuEntries.ManageStacks, () => new ManageStacks(_stacksRepository, _choosableEntryHandler) },
            { MainMenuEntries.ManageFlashcards, () => new ManageFlashcards(_flashcardsRepository) }
        };
}