using Flashcards.Interfaces.Models;
using Flashcards.Models;
using Flashcards.Models.Dto;
using Flashcards.Models.Entity;

namespace Flashcards.Extensions;

public static class ModelsExtensions 
{
    public static FlashcardDto ToDto(this IFlashcard flashcard) => 
        new FlashcardDto
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            StackId = flashcard.StackId
        };
    
    public static StackDto ToDto(this IStack stack) =>
        new StackDto
        {
            Id = stack.Id,
            Name = stack.Name
        };
    
    public static Flashcard ToEntity(this IFlashcard flashcard) => 
        new Flashcard
        {
            Id = flashcard.Id,
            Question = flashcard.Question,
            Answer = flashcard.Answer,
            StackId = flashcard.StackId
        };
    
    public static Stack ToEntity(this IStack stack) =>
        new Stack
        {
            Id = stack.Id,
            Name = stack.Name
        };
}