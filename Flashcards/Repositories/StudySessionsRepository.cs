using System.Globalization;
using Flashcards.Extensions;
using Flashcards.Interfaces.Database;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Models.Dto;
using Flashcards.Models.Entity;

namespace Flashcards.Repositories;

/// <summary>
/// Represents a repository for managing study sessions.
/// </summary>
internal class StudySessionsRepository : IStudySessionsRepository
{
    private readonly IDatabaseManager _databaseManager;
    
    public IStudySession? SelectedEntry { get; set; }
    public int? StackId { get; set; }
    public string? StackName { get; set; }
    
    public StudySessionsRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    /// <summary>
    /// Inserts a study session into the StudySessions table.
    /// </summary>
    /// <param name="entity">The study session entity to insert.</param>
    /// <returns>The number of rows affected by the insertion operation.</returns>
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

    /// <summary>
    /// Retrieves all study sessions.
    /// </summary>
    /// <returns>The list of study sessions.</returns>
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
    
    public IEnumerable<IStudySession> GetByStackId(int stackId)
    {
        const string query =
            """
                SELECT 
                    ss.Date,
                    ss.Questions,
                    ss.CorrectAnswers,
                    ss.Percentage,
                    ss.Time
                FROM
                    StudySessions ss
                INNER JOIN 
                    Stacks s ON ss.StackId = s.Id
                WHERE
                    ss.StackId = @StackId;
            """;

        var parameters = new Dictionary<string, object>
        {
            { "@StackId", stackId }
        };

        IEnumerable<IStudySession> studySessions = _databaseManager.GetAllEntities<StudySessionDto>(query, parameters).ToList();
        
        return studySessions.Select(studySession => studySession.ToEntity());
    }
    
    public IEnumerable<IStudySession> GetByYear(IYear year)
    {
        const string query =
            """
                SELECT
                    StackName,
                    [January], [February], [March], [April], [May], [June],
                    [July], [August], [September], [October], [November], [December]
                FROM
                    (SELECT
                        s.Name AS StackName,
                        DATENAME(MONTH, ss.Date) AS MonthName,
                        COUNT(*) AS SessionCount
                 FROM
                    StudySessions ss
                 JOIN
                    Stacks s ON ss.StackId = s.Id
                 GROUP BY
                    s.Name, DATENAME(MONTH, ss.Date)
                ) AS SourceTable
                PIVOT
                (
                    SUM(SessionCount)
                    FOR MonthName IN ([January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December])
                ) AS PivotTable
                ORDER BY
                    StackName;
            """;

        var parameters = new Dictionary<string, object>
        {
            { "@Year", year.ChosenYear }
        };

        IEnumerable<IStudySession> studySessions = _databaseManager.GetAllEntities<StudySessionDto>(query, parameters).ToList();
        
        return studySessions.Select(studySession => studySession.ToEntity());
    }
    
    public IEnumerable<IStudySession> GetByMonth(IYear year, IMonth month)
    {
        const string query =
            """
                SELECT 
                    ss.Date,
                    ss.Questions,
                    ss.CorrectAnswers,
                    ss.Percentage,
                    ss.Time
                FROM
                    StudySessions ss
                INNER JOIN 
                    Stacks s ON ss.StackId = s.Id
                WHERE
                    YEAR(ss.Date) = @Year AND MONTH(ss.Date) = @Month;
            """;
        
        var parameters = new Dictionary<string, object>
        {
            { "@Year", year.ChosenYear },
            { "@Month", month.ChosenMonth }
        };

        IEnumerable<IStudySession> studySessions = _databaseManager.GetAllEntities<StudySessionDto>(query, parameters).ToList();
        
        return studySessions.Select(studySession => studySession.ToEntity());
    }
    
    public IEnumerable<IYear> GetYears()
    {
        const string query = "SELECT DISTINCT YEAR(Date) as ChosenYear FROM StudySessions;";

        IEnumerable<IYear> years = _databaseManager.GetAllEntities<Year>(query).ToList();
        
        return years.Select(year => year.ToEntity());
    }
    
    public IEnumerable<IMonth> GetMonths(IYear year)
    {
        const string query = "SELECT DISTINCT MONTH(Date) as ChosenMonth FROM StudySessions;";

        IEnumerable<IMonth> months = _databaseManager.GetAllEntities<Month>(query);
        
        return months.Select(month => month.ToEntity());
    }
}