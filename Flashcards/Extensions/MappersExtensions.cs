using Flashcards.Interfaces.Models;
using Flashcards.Models.Dto;
using Flashcards.Models.Entity;

namespace Flashcards.Extensions;

internal static class MappersExtensions
{
    public static FlashcardDto ToDto(this IFlashcard flashcard) =>
        new()
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            StackId = flashcard.StackId
        };

    public static StackDto ToDto(this IStack stack) =>
        new()
        {
            Id = stack.Id,
            Name = stack.Name
        };
    
    public static StudySessionDto ToDto(this IStudySession studySession) =>
        new()
        {
            Id = studySession.Id,
            StackId = studySession.StackId,
            Date = studySession.Date,
            Questions = studySession.Questions,
            CorrectAnswers = studySession.CorrectAnswers,
            Percentage = studySession.Percentage,
            Time = studySession.Time,
            StackName = studySession.StackName
        };

    public static Flashcard ToEntity(this IFlashcard flashcard) =>
        new()
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            StackId = flashcard.StackId
        };

    public static Stack ToEntity(this IStack stack) =>
        new()
        {
            Id = stack.Id,
            Name = stack.Name
        };
    
    public static StudySession ToEntity(this IStudySession studySession) =>
        new()
        {
            Id = studySession.Id,
            StackId = studySession.StackId,
            Date = studySession.Date,
            Questions = studySession.Questions,
            CorrectAnswers = studySession.CorrectAnswers,
            Percentage = studySession.Percentage,
            Time = studySession.Time,
            StackName = studySession.StackName
        };
}