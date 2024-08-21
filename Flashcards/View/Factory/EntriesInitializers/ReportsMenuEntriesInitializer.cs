using Flashcards.Enums;
using Flashcards.Exceptions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View.Commands.ReportsMenu;

namespace Flashcards.View.Factory.EntriesInitializers;

internal class ReportsMenuEntriesInitializer : IMenuEntriesInitializer<ReportsMenuEntries>
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IStacksRepository _stacksRepository;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;
    private readonly IEditableEntryHandler<IYear> _yearEntryHandler;
    
    public ReportsMenuEntriesInitializer(
        IStudySessionsRepository studySessionsRepository,
        IStacksRepository stacksRepository,
        IEditableEntryHandler<IStack> stackEntryHandler,
        IEditableEntryHandler<IYear> yearEntryHandler)
    {
        _studySessionsRepository = studySessionsRepository;
        _stacksRepository = stacksRepository;
        _stackEntryHandler = stackEntryHandler;
        _yearEntryHandler = yearEntryHandler;
    }

    public Dictionary<ReportsMenuEntries, Func<ICommand>> InitializeEntries(
        IMenuCommandFactory<ReportsMenuEntries> menuCommandFactory) =>
        new()
        {
            { ReportsMenuEntries.FullReport, () => new FullReport(
                _studySessionsRepository
                ) 
            },
            { ReportsMenuEntries.ReportByStack, () => new ReportByStack(
                _stacksRepository,
                _studySessionsRepository, 
                _stackEntryHandler
                ) 
            },
            { ReportsMenuEntries.AverageYearlyReport, () => new AverageYearlyReport(_studySessionsRepository, _yearEntryHandler) },
            { ReportsMenuEntries.ReturnToMainMenu, () => throw new ReturnToMainMenuException() }
        };
}