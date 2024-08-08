using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums;

public enum MainMenuEntries
{
    [Display(Name = "Start Study Session")]
    StartStudySession,
    [Display(Name = "Manage Stacks")] ManageStacks,
    [Display(Name = "Manage Flashcards")] ManageFlashcards,
    [Display(Name = "Exit")] Exit
}