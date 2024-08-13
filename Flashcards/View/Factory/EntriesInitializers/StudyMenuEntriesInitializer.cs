using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.StudyMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class StudyMenuEntriesInitializer : IMenuEntriesInitializer<StudyMenuEntries>
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IStacksRepository _stacksRepository;
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;
    
    public StudyMenuEntriesInitializer(
        IStudySessionsRepository studySessionsRepository, 
        IStacksRepository stacksRepository,
        IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory
        )
    {
        _studySessionsRepository = studySessionsRepository;
        _stacksRepository = stacksRepository;
        _stackMenuCommandFactory = stackMenuCommandFactory;
    }
    
    public Dictionary<StudyMenuEntries, Func<ICommand>> InitializeEntries(
        IMenuCommandFactory<StudyMenuEntries> menuCommandFactory) =>
        new()
        {
            { StudyMenuEntries.StartStudySession, () => new StartStudySession() },
            { StudyMenuEntries.StudyHistory, () => new ShowStudyHistory(_studySessionsRepository, _stacksRepository,_stackMenuCommandFactory) },
            { StudyMenuEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException() }
        };
}