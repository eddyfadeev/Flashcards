using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums;

internal enum FlashcardEntries
{
    [Display(Name = "View Flashcards")] ChooseFlashcard,
    [Display(Name = "Add Flashcard")] AddFlashcard,
    [Display(Name = "Delete Flashcard")] DeleteFlashcard,
    [Display(Name = "Edit Flashcard")] EditFlashcard,

    [Display(Name = "Return to Main Menu")]
    ReturnToMainMenu
}