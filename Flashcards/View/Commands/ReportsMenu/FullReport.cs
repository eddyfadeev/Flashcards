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
        
        var table = _reportGenerator.GetReportToDisplay(studySessions);
        AnsiConsole.Write(table);
        
        _reportGenerator.SaveFullReportToPdf(studySessions);
        
        GeneralHelperService.ShowContinueMessage();
    }
}