using Flashcards.Enums;
using Flashcards.Interfaces.Report;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.ReportsMenu;

internal sealed class FullReport : ICommand
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IReportGenerator _reportGenerator;
    
    public FullReport(
        IStudySessionsRepository studySessionsRepository,
        IReportGenerator reportGenerator
        )
    {
        _studySessionsRepository = studySessionsRepository;
        _reportGenerator = reportGenerator;
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
        
        var table = _reportGenerator.GetReportToDisplay(studySessions, ReportType.FullReport);
        AnsiConsole.Write(table);
        
        _reportGenerator.SaveReportToPdf(studySessions, ReportType.FullReport);
        
        GeneralHelperService.ShowContinueMessage();
    }
}