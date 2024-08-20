using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.ReportsMenu;

internal sealed class AverageYearlyReport : ICommand
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IEditableEntryHandler<IYear> _yearEntryHandler;
    private readonly IReportGenerator _reportGenerator;
    public AverageYearlyReport(
        IStudySessionsRepository studySessionsRepository, 
        IEditableEntryHandler<IYear> yearEntryHandler,
        IReportGenerator reportGenerator)
    {
        _studySessionsRepository = studySessionsRepository;
        _yearEntryHandler = yearEntryHandler;
        _reportGenerator = reportGenerator;
    }

    public void Execute()
    {
        var selectedYear = StudySessionsHelperService.GetYearFromUser(_studySessionsRepository, _yearEntryHandler);
        
        var studySessions = _studySessionsRepository.GetAverageYearly(selectedYear).ToList();

        var table = _reportGenerator.GetReportToDisplay(studySessions, selectedYear);
        AnsiConsole.Write(table);
        
        _reportGenerator.SaveReportToPdf(studySessions, selectedYear, ReportType.AverageYearlyReport);
        
        GeneralHelperService.ShowContinueMessage();
    }
}