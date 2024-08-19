using Flashcards.Extensions;
using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Entity;

internal class Year : IYear, IDbEntity<IYear>
{
    public int ChosenYear { get; set; }
    public IYear MapToDto() => this.ToDto();
    
    public override string ToString() => ChosenYear.ToString();
}