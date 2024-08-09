using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.MainMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class MainMenuEntriesInitializer : IMenuEntriesInitializer<MainMenuEntries>
{
    private readonly IMenuHandler<StackMenuEntries> _stacksMenuHandler;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IStacksRepository _stacksRepository;
    public IMenuCommandFactory<StackMenuEntries> StackMenuCommandFactory { get; set; }

    public MainMenuEntriesInitializer(
        IStacksRepository stacksRepository,
        IFlashcardsRepository flashcardsRepository,
        IMenuHandler<StackMenuEntries> stacksMenuHandler)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _stacksMenuHandler = stacksMenuHandler;
    }


    public Dictionary<MainMenuEntries, Func<ICommand>> InitializeEntries() =>
        new()
        {
            {
                MainMenuEntries.StartStudySession, () => new StartStudySession(_stacksRepository, _flashcardsRepository)
            },
            { MainMenuEntries.ManageStacks, () => new ManageStacks(_stacksMenuHandler) },
            { MainMenuEntries.ManageFlashcards, () => new ManageFlashcards(_flashcardsRepository) }
        };
}