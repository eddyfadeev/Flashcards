using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Report;
using Flashcards.Report.Strategies;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.ReportsMenu;

/// <summary>
/// Represents a command to retrieve a full report.
/// </summary>
internal sealed class FullReport : ICommand
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    
    public FullReport(
        IStudySessionsRepository studySessionsRepository)
        
    {
        _studySessionsRepository = studySessionsRepository;
    }
    
    public void Execute()
    {
        var studySessions = _studySessionsRepository.GetAll().ToList();
        
        if (studySessions.Count == 0)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoEntriesFoundMessage);
            GeneralHelperService.ShowContinueMessage();
            return;
        }
        
        var fullReportStrategy = new FullReportStrategy(studySessions);
        var reportGenerator = new ReportGenerator<IStudySession>(fullReportStrategy, ReportType.FullReport);
        
        var reportTable = reportGenerator.GetReportToDisplay();
        AnsiConsole.Write(reportTable);
        
        reportGenerator.SaveReportToPdf();
        
        GeneralHelperService.ShowContinueMessage();
    }
}