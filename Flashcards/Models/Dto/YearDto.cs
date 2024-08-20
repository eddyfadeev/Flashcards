using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Dto;

internal record YearDto : IYear
{
    public int ChosenYear { get; set; }
}