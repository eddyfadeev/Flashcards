using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;
using Flashcards.Services;
using Spectre.Console;

namespace Flashcards.View.Commands.StudyMenu;

internal class ShowStudyHistory : ICommand
{
    private readonly IStudySessionsRepository _studySessionsRepository;
    
    public ShowStudyHistory(IStudySessionsRepository studySessionsRepository)
    {
        _studySessionsRepository = studySessionsRepository;
    }
    
    public void Execute()
    {
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