using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Report;
using Flashcards.Report.Strategies;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.ReportsMenu;

internal sealed class AverageYearlyReport : ICommand
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IEditableEntryHandler<IYear> _yearEntryHandler;
    public AverageYearlyReport(
        IStudySessionsRepository studySessionsRepository, 
        IEditableEntryHandler<IYear> yearEntryHandler)
    {
        _studySessionsRepository = studySessionsRepository;
        _yearEntryHandler = yearEntryHandler;
    }

    public void Execute()
    {
        var selectedYear = StudySessionsHelperService.GetYearFromUser(_studySessionsRepository, _yearEntryHandler);
        var studySessions = _studySessionsRepository.GetAverageYearly(selectedYear).ToList();
        
        if (studySessions.Count == 0)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoEntriesFoundMessage);
            GeneralHelperService.ShowContinueMessage();
            return;
        }
        
        var reportStrategy = new AverageYearlyReportStrategy(studySessions, selectedYear);
        var reportGenerator = new ReportGenerator<IStackMonthlySessions>(reportStrategy, ReportType.AverageYearlyReport);
        
        var table = reportGenerator.GetReportToDisplay();
        AnsiConsole.Write(table);
        
        reportGenerator.SaveReportToPdf();
        
        GeneralHelperService.ShowContinueMessage();
    }
}