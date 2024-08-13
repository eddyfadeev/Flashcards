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
    private readonly IMenuCommandFactory<StackMenuEntries> _stackMenuCommandFactory;
    
    public ShowStudyHistory(IStudySessionsRepository studySessionsRepository, IMenuCommandFactory<StackMenuEntries> stackMenuCommandFactory)
    {
        _studySessionsRepository = studySessionsRepository;
        _stackMenuCommandFactory = stackMenuCommandFactory;
    }
    
    public void Execute()
    {
        StackChooserService.GetStacks(_stackMenuCommandFactory);
        var studySessions = _studySessionsRepository.GetAll();
        
        var table = new Table();
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