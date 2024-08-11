using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.MainMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class MainMenuEntriesInitializer : IMenuEntriesInitializer<MainMenuEntries>
{
    private readonly IMenuHandler<FlashcardEntries> _flashcardsMenuHandler;
    private readonly IMenuHandler<StackMenuEntries> _stacksMenuHandler;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IStacksRepository _stacksRepository;

    public MainMenuEntriesInitializer(
        IMenuHandler<FlashcardEntries> flashcardsMenuHandler,
        IStacksRepository stacksRepository,
        IFlashcardsRepository flashcardsRepository,
        IMenuHandler<StackMenuEntries> stacksMenuHandler)
    {
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _flashcardsMenuHandler = flashcardsMenuHandler;
        _stacksMenuHandler = stacksMenuHandler;
    }


    public Dictionary<MainMenuEntries, Func<ICommand>> InitializeEntries(IMenuCommandFactory<MainMenuEntries> commandFactory) =>
        new()
        {
            {
                MainMenuEntries.StartStudySession, () => new StartStudySession(_stacksRepository, _flashcardsRepository)
            },
            { MainMenuEntries.ManageStacks, () => new ManageStacks(_stacksMenuHandler) },
            { MainMenuEntries.ManageFlashcards, () => new ManageFlashcards(_flashcardsMenuHandler) },
            { MainMenuEntries.Exit, () => throw new ExitApplicationException()}
        };
}