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
    private readonly IStacksRepository _stacksRepository;
    private readonly IEditableEntryHandler<IYear> _yearEntryHandler;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;
    
    public ReportsMenuEntriesInitializer(
        IReportGenerator reportGenerator, 
        IStudySessionsRepository studySessionsRepository,
        IStacksRepository stacksRepository,
        IEditableEntryHandler<IYear> yearEntryHandler,
        IEditableEntryHandler<IStack> stackEntryHandler)
    {
        _reportGenerator = reportGenerator;
        _studySessionsRepository = studySessionsRepository;
        _stacksRepository = stacksRepository;
        _yearEntryHandler = yearEntryHandler;
        _stackEntryHandler = stackEntryHandler;
    }

    public Dictionary<ReportsMenuEntries, Func<ICommand>> InitializeEntries(
        IMenuCommandFactory<ReportsMenuEntries> menuCommandFactory) =>
        new()
        {
            { ReportsMenuEntries.FullReport, () => new FullReport(
                _studySessionsRepository, 
                _reportGenerator
                ) },
            { ReportsMenuEntries.ReportByStack, () => new ReportByStack(_stacksRepository, _studySessionsRepository, _stackEntryHandler) },
            { ReportsMenuEntries.AverageYearlyReport, () => new AverageYearlyReport(_studySessionsRepository, _yearEntryHandler, _reportGenerator) },
            { ReportsMenuEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException() }
        };
}