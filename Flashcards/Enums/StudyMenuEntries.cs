using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums;

internal enum StudyMenuEntries
{
    [Display(Name = "Start Study Session")]
    StartStudySession,
    [Display(Name = "Study History")]
    StudyHistory,
    [Display(Name = "Return to Main Menu")]
    ReturnToMainMenu
}