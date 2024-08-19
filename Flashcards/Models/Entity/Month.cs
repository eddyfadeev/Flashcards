using System.Globalization;
using Flashcards.Extensions;
using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Entity;

internal class Month : IMonth, IDbEntity<IMonth>
{
    private int _chosenMonth;

    public int ChosenMonth
    {
        get => _chosenMonth;
        set
        {
            _chosenMonth = value;
            DateTimeFormatInfo dtfi = CultureInfo.CurrentCulture.DateTimeFormat;
            MonthName = dtfi.GetMonthName(value);
        }
    }

    public string? MonthName { get; set; }
    
    public IMonth MapToDto() => this.ToDto();
    
    public override string ToString() => MonthName;
}