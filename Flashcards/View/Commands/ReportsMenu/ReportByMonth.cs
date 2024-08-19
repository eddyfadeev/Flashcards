using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.ReportsMenu;

internal sealed class ReportByMonth : ICommand
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IEditableEntryHandler<IYear> _yearEntryHandler;
    private readonly IEditableEntryHandler<IMonth> _monthEntryHandler;
    private readonly IReportGenerator _reportGenerator;
    public ReportByMonth(
        IStudySessionsRepository studySessionsRepository, 
        IEditableEntryHandler<IYear> yearEntryHandler,
        IEditableEntryHandler<IMonth> monthEntryHandler,
        IReportGenerator reportGenerator)
    {
        _studySessionsRepository = studySessionsRepository;
        _yearEntryHandler = yearEntryHandler;
        _monthEntryHandler = monthEntryHandler;
        _reportGenerator = reportGenerator;
    }

    public void Execute()
    {
        var years = _studySessionsRepository.GetYears().ToList();
        var selectedYear = _yearEntryHandler.HandleEditableEntry(years);

        var months = _studySessionsRepository.GetMonths(selectedYear).ToList();
        var selectedMonth = _monthEntryHandler.HandleEditableEntry(months);
        
        var studySessions = _studySessionsRepository.GetByMonth(selectedYear, selectedMonth).ToList();

        var table = _reportGenerator.GetReportToDisplay(studySessions);
        AnsiConsole.Write(table);
        
        GeneralHelperService.ShowContinueMessage();
    }
}