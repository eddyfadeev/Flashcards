using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.MainMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

/// <summary>
/// Initializes the menu entries for the main menu.
/// </summary>
internal class MainMenuEntriesInitializer : IMenuEntriesInitializer<MainMenuEntries>
{
    private readonly IMenuHandler<FlashcardEntries> _flashcardsMenuHandler;
    private readonly IMenuHandler<StackMenuEntries> _stacksMenuHandler;
    private readonly IMenuHandler<ReportsMenuEntries> _reportsMenuHandler;
    
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IStacksRepository _stacksRepository;
    private readonly IFlashcardsRepository _flashcardsRepository;
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;

    public MainMenuEntriesInitializer(
        IMenuHandler<FlashcardEntries> flashcardsMenuHandler,
        IMenuHandler<StackMenuEntries> stacksMenuHandler,
        IMenuHandler<ReportsMenuEntries> reportsMenuHandler,
        IStudySessionsRepository studySessionsRepository, 
        IStacksRepository stacksRepository,
        IFlashcardsRepository flashcardsRepository,
        IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory
        )
    {
        _flashcardsMenuHandler = flashcardsMenuHandler;
        _stacksMenuHandler = stacksMenuHandler;
        _reportsMenuHandler = reportsMenuHandler;
        
        _studySessionsRepository = studySessionsRepository;
        _stacksRepository = stacksRepository;
        _flashcardsRepository = flashcardsRepository;
        _stackMenuCommandFactory = stackMenuCommandFactory;
    }

    public Dictionary<MainMenuEntries, Func<ICommand>> InitializeEntries(IMenuCommandFactory<MainMenuEntries> commandFactory) =>
        new()
        {
            { MainMenuEntries.StartStudySession, () => 
                new StartStudySession(
                    _stackMenuCommandFactory, 
                    _stacksRepository, 
                    _studySessionsRepository, 
                    _flashcardsRepository) },
            { MainMenuEntries.StudyHistory, () => new ShowStudyHistory(_reportsMenuHandler) },
            { MainMenuEntries.ManageStacks, () => new ManageStacks(_stacksMenuHandler) },
            { MainMenuEntries.ManageFlashcards, () => new ManageFlashcards(_flashcardsMenuHandler) },
            { MainMenuEntries.Exit, () => throw new ExitApplicationException()}
        };
}