using Flashcards.Enums;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.ReportsMenu;

internal sealed class ReportByStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;
    private readonly IReportGenerator _reportGenerator;
    
    public ReportByStack(
        IStacksRepository stacksRepository,
        IStudySessionsRepository studySessionsRepository,
        IEditableEntryHandler<IStack> stackEntryHandler,
        IReportGenerator reportGenerator
        )
    {
        _stacksRepository = stacksRepository;
        _studySessionsRepository = studySessionsRepository;
        _stackEntryHandler = stackEntryHandler;
        _reportGenerator = reportGenerator;
    }
    
    public void Execute()
    {
        var stack = StackChooserService.GetStackFromUser(_stacksRepository, _stackEntryHandler);
        var studySessions = _studySessionsRepository.GetByStackId(stack).ToList();
        
        if (studySessions.Count == 0)
        {
            AnsiConsole.MarkupLine(Messages.Messages.NoEntriesFoundMessage);
            GeneralHelperService.ShowContinueMessage();
            return;
        }
        
        var table = _reportGenerator.GetReportToDisplay(studySessions, ReportType.ReportByStack);
        AnsiConsole.Write(table);
        
        _reportGenerator.SaveReportToPdf(studySessions, ReportType.ReportByStack);
        
        GeneralHelperService.ShowContinueMessage();
    }
}