using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;

namespace Flashcards.Repositories;

internal class StudySessionsRepository : IStudySessionsRepository
{
    private readonly IDatabaseManager _databaseManager;
    
    public IStudySession? SelectedEntry { get; set; }
    public int? StackId { get; set; }
    
    public StudySessionsRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }
    public int Insert(IDbEntity<IStudySession> entity)
    {
        const string query = 
            """
                INSERT INTO StudySessions (Questions, CorrectAnswers, StackId, Time, Date) 
                VALUES (@Questions, @CorrectAnswers, @StackId, @Time, @Date);
            """;
        
        var studySession = entity.MapToDto();
        
        return _databaseManager.InsertEntity(query, studySession);
    }

    public IEnumerable<IStudySession> GetAll()
    {
        const string query =
            """
                SELECT 
                    s.Name as StackName,
                    ss.Date,
                    ss.Questions,
                    ss.CorrectAnswers,
                    ss.Percentage,
                    ss.Time
                FROM
                    StudySessions ss
                INNER JOIN 
                    Stacks s ON ss.StackId = s.Id;
            """;

        IEnumerable<IStudySession> studySessions = _databaseManager.GetAllEntities<StudySessionDto>(query).ToList();
        
        return studySessions.Select(studySession => studySession.ToEntity());
    }
}