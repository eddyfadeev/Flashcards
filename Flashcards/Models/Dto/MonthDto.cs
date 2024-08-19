using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Dto;

internal record MonthDto : IMonth
{
    public int ChosenMonth { get; set; }
    public string? MonthName { get; set; }
}