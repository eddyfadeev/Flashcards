using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Dto;

public record StudySessionDto : IStudySession
{
    public int? StackId { get; set; }
    public int Id { get; set; }
    public string? StackName { get; set; }
    public DateTime Date { get; set; }
    public int Questions { get; set; }
    public int CorrectAnswers { get; set; }
    public int Percentage { get; set; }
    public TimeSpan Time { get; set; }
}