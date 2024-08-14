using Flashcards.Enums;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Interfaces.View.Factory;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.StudyMenu;

internal class ShowStudyHistory : ICommand
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IStacksRepository _stacksRepository;
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;
    
    public ShowStudyHistory(
        IStudySessionsRepository studySessionsRepository, 
        IStacksRepository stacksRepository,
        IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory
        )
    {
        _studySessionsRepository = studySessionsRepository;
        _stacksRepository = stacksRepository;
        _stackMenuCommandFactory = stackMenuCommandFactory;
    }
    
    public void Execute()
    {
        var stack = StackChooserService.GetStacks(_stackMenuCommandFactory, _stacksRepository);

        if (StackChooserService.CheckStackForNull(stack))
        {
            return;
        }
        
        _studySessionsRepository.StackId = stack.Id;
        
        var studySessions = _studySessionsRepository.GetAll();
        
        var table = new Table().Title($"[bold]Study history for { stack.Name }[/]");
        table.Border = TableBorder.Rounded;
        table.AddColumns("Date", "Stack", "Result", "Percentage", "Duration");

        foreach (var session in studySessions)
        {
            table.AddRow(
                session.Date.ToShortDateString(),
                session.StackName!,
                $"{ session.CorrectAnswers } out of { session.Questions }",
                $"{ session.Percentage }%",
                session.Time.ToString()
            );
        }
        
        AnsiConsole.Write(table);
        GeneralHelperService.ShowContinueMessage();
    }
}