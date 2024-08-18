using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Report;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.ReportsMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class ReportsMenuEntriesInitializer : IMenuEntriesInitializer<ReportsMenuEntries>
{
    private readonly IReportGenerator _reportGenerator;
    private readonly IStudySessionsRepository _studySessionsRepository;
    
    public ReportsMenuEntriesInitializer(IReportGenerator reportGenerator, IStudySessionsRepository studySessionsRepository)
    {
        _reportGenerator = reportGenerator;
        _studySessionsRepository = studySessionsRepository;
    }

    public Dictionary<ReportsMenuEntries, Func<ICommand>> InitializeEntries(
        IMenuCommandFactory<ReportsMenuEntries> menuCommandFactory) =>
        new()
        {
            { ReportsMenuEntries.FullReport, () => new FullReport(_studySessionsRepository, _reportGenerator) },
            { ReportsMenuEntries.ReportByStack, () => new ReportByStack() },
            { ReportsMenuEntries.ReportByMonth, () => new ReportByMonth() },
            { ReportsMenuEntries.ReportByYear, () => new ReportByYear() },
            { ReportsMenuEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException() }
        };
}