using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using QuestPDF.Infrastructure;
using Spectre.Console;

namespace Flashcards.Interfaces.Report;

/// <summary>
/// Represents a report generator used for generating study session reports.
/// </summary>
internal interface IReportGenerator
{
    /// <summary>
    /// Retrieves the report to display as a table.
    /// </summary>
    /// <param name="studySessions">The list of study sessions.</param>
    /// <returns>The report as a table.</returns>
    internal Table GetReportToDisplay(List<IStudySession> studySessions, ReportType reportType);
    
    /// <summary>
    /// Retrieves the average yearly report to display as a table.
    /// </summary>
    /// <param name="stackMonthlySessions">The list of study sessions with the average score per month.</param>
    /// <returns>The report as a table.</returns>
    internal Table GetReportToDisplay(List<IStackMonthlySessions> stackMonthlySessions, IYear year);

    /// <summary>
    /// Generates a report document with study session information and saves it to a PDF file.
    /// </summary>
    /// <param name="studySessions">A list of study sessions containing the information to be included in the report.</param>
    /// <returns>A document object representing the generated report.</returns>
    internal IDocument GenerateReportToFile(List<IStudySession> studySessions, ReportType reportType);
    
    internal IDocument GenerateReportToFile(List<IStackMonthlySessions> stackMonthlySessions, IYear year, ReportType reportType);

    /// <summary>
    /// Saves the full report to a PDF file.
    /// </summary>
    /// <param name="studySessions">
    /// A list of study sessions containing the information to be included in the report.
    /// </param>
    internal void SaveReportToPdf(List<IStudySession> studySessions, ReportType reportType);

    internal void SaveReportToPdf(List<IStackMonthlySessions> stackMonthlySessions, IYear year, ReportType reportType);
}