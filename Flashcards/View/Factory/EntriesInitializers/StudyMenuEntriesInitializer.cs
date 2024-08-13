using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.StudyMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class StudyMenuEntriesInitializer : IMenuEntriesInitializer<StudyMenuEntries>
{
    public Dictionary<StudyMenuEntries, Func<ICommand>> InitializeEntries(
        IMenuCommandFactory<StudyMenuEntries> menuCommandFactory) =>
        new()
        {
            { StudyMenuEntries.StartStudySession, () => new StartStudySession() },
            { StudyMenuEntries.StudyHistory, () => new ShowStudyHistory() },
            { StudyMenuEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException() }
            
        };
}