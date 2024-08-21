using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Dto;

/// <summary>
/// Represents a data transfer object for a year.
/// </summary>
internal record YearDto : IYear
{
    public int ChosenYear { get; set; }
}