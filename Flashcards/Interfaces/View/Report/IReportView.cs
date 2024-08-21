using Spectre.Console;

namespace Flashcards.Interfaces.View.Report;

internal interface IReportView
{
    Table GetReportToDisplay();
}