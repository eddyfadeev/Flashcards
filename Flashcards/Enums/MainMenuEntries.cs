using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums;

internal enum MainMenuEntries
{
    [Display(Name = "Study Menu")] StudyMenu,
    [Display(Name = "Manage Stacks")] ManageStacks,
    [Display(Name = "Manage Flashcards")] ManageFlashcards,
    [Display(Name = "Exit")] Exit
}