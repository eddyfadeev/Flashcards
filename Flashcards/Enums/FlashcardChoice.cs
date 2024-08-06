using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums;

public enum FlashcardChoice
{
    [Display(Name = "View Flashcards")]
    ViewFlashcards,
    [Display(Name = "Add Flashcard")]
    AddFlashcard,
    [Display(Name = "Delete Flashcard")]
    DeleteFlashcard,
    [Display(Name = "Edit Flashcard")]
    EditFlashcard,
    [Display(Name = "Return to Main Menu")]
    ReturnToMainMenu
}