using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums;

public enum ReportsMenuEntries
{
    [Display(Name = "Total")]
    FullReport,
    [Display(Name = "Report By Stack")]
    ReportByStack,
    [Display(Name = "Report By Month")]
    ReportByMonth,
    [Display(Name = "Report By Year")]
    ReportByYear,
    [Display(Name = "Return to Main Menu")]
    ReturnToMainMenu
}