using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
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
    private readonly IEditableEntryHandler<IYear> _yearEntryHandler;
    private readonly IEditableEntryHandler<IMonth> _monthEntryHandler;
    
    public ReportsMenuEntriesInitializer(
        IReportGenerator reportGenerator, 
        IStudySessionsRepository studySessionsRepository,
        IEditableEntryHandler<IYear> yearEntryHandler,
        IEditableEntryHandler<IMonth> monthEntryHandler)
    {
        _reportGenerator = reportGenerator;
        _studySessionsRepository = studySessionsRepository;
        _yearEntryHandler = yearEntryHandler;
        _monthEntryHandler = monthEntryHandler;
    }

    public Dictionary<ReportsMenuEntries, Func<ICommand>> InitializeEntries(
        IMenuCommandFactory<ReportsMenuEntries> menuCommandFactory) =>
        new()
        {
            { ReportsMenuEntries.FullReport, () => new FullReport(_studySessionsRepository, _reportGenerator) },
            { ReportsMenuEntries.ReportByStack, () => new ReportByStack() },
            { ReportsMenuEntries.AverageYearlyReport, () => new AverageYearlyReport(_studySessionsRepository, _yearEntryHandler, _monthEntryHandler, _reportGenerator) },
            { ReportsMenuEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException() }
        };
}