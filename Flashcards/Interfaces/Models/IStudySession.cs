namespace Flashcards.Interfaces.Models;

internal interface IStudySession
{
    internal int Id { get; set; }
    internal int StackId { get; set; }
    internal DateTime Date { get; set; }
    internal int Questions { get; set; }
    internal int CorrectAnswers { get; set; }
    internal int Percentage { get; set; }
    internal TimeSpan Time { get; set; }
}