using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Spectre.Console;

namespace Flashcards.Interfaces.Report;

/// <summary>
/// Represents a report generator used for generating study session reports.
/// </summary>
internal interface IReportGenerator
{
    internal Table GetReportToDisplay();
    internal void SaveReportToPdf();
}