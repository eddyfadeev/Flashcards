using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Spectre.Console;

namespace Flashcards.Report;

/// <summary>
/// Generates reports for study sessions.
/// </summary>
internal class ReportGenerator : IReportGenerator
{
    private static readonly string[] FullReportColumns = ["Date", "Stack", "Result", "Percentage", "Duration"];
    private static readonly string[] AverageYearlyReportColumns = 
        ["Stack Name", "January", "February", "March", "April", "May", "June", 
            "July", "August", "September", "October", "November", "December"];
    private static readonly TableBorder ReportTableBorder = TableBorder.Rounded;

    
    public ReportGenerator()
    {
        SetLicence();
    }

    /// <summary>
    /// Retrieves the report to display as a table.
    /// </summary>
    /// <param name="studySessions">The list of study sessions.</param>
    /// <returns>The report as a table.</returns>
    public Table GetReportToDisplay(List<IStudySession> studySessions) => 
        GenerateReportTable(studySessions);

    /// <summary>
    /// Retrieves the average yearly report to display as a table.
    /// </summary>
    /// <param name="stackMonthlySessions">The list of study sessions with the average score per month.</param>
    /// <param name="year">Year object that determines the year of the report.</param>
    /// <returns>The report as a table.</returns>
    public Table GetReportToDisplay(List<IStackMonthlySessions> stackMonthlySessions, IYear year) =>
        GenerateReportTable(stackMonthlySessions, year);

    /// <summary>
    /// Generates a report document with study session information and saves it to a PDF file.
    /// </summary>
    /// <param name="studySessions">A list of study sessions containing the information to be included in the report.</param>
    /// <returns>A document object representing the generated report.</returns>
    public IDocument GenerateReportToFile(List<IStudySession> studySessions)
    {
        var document = new ReportDocument(studySessions);

        return document;
    }

    public IDocument GenerateReportToFile(List<IStackMonthlySessions> stackMonthlySessions, IYear year)
    {
        var document = new ReportDocument(stackMonthlySessions, year);

        return document;
    }

    /// <summary>
    /// Saves the full report to a PDF file.
    /// </summary>
    /// <param name="studySessions">
    /// A list of study sessions containing the information to be included in the report.
    /// </param>
    public void SaveReportToPdf(List<IStudySession> studySessions)
    {
        if (!AskToSaveReport())
        {
            return;
        }

        var pdfDocument = GenerateReportToFile(studySessions);
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var filePath = Path.Combine(desktopPath, $"Study Report-{DateTime.Today.Date.ToShortDateString()}.pdf");
        
        pdfDocument.GeneratePdf(filePath);
        AnsiConsole.MarkupLine($"Saved report to [bold]{ filePath }[/]");
    }
    
    public void SaveReportToPdf(List<IStackMonthlySessions> stackMonthlySessions, IYear year)
    {
        if (!AskToSaveReport())
        {
            return;
        }

        var pdfDocument = GenerateReportToFile(stackMonthlySessions, year);
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var filePath = Path.Combine(desktopPath, $"Average Yearly Report-{ year.ChosenYear }.pdf");
        
        pdfDocument.GeneratePdf(filePath);
        AnsiConsole.MarkupLine($"Saved report to [bold]{ filePath }[/]");
    }

    private static Table GenerateReportTable(List<IStudySession> studySessions)
    {
        var table = InitializeFullReportTable();
        table = PopulateFullReportTable(table, studySessions);

        return table;
    }
    
    private static Table GenerateReportTable(List<IStackMonthlySessions> stackMonthlySessions, IYear year)
    {
        var table = InitializeAverageYearlyReportTable(year);
        table = PopulateAverageYearlyReportTable(table, stackMonthlySessions);

        return table;
    }

    private static Table InitializeFullReportTable()
    {
        const string header = "[bold]Study History[/]";
        
        var table = new Table().Title(header);
        
        table.Border = ReportTableBorder;
        table.AddColumns(FullReportColumns);
        
        return table;
    }
    
    private static Table InitializeAverageYearlyReportTable(IYear year)
    {
        var header = $"[bold]Average Yearly Report for the { year.ChosenYear }[/]";
        
        var table = new Table().Title(header);
        
        table.Border = ReportTableBorder;
        table.AddColumns(AverageYearlyReportColumns);
        
        return table;
    }

    private static Table PopulateFullReportTable(Table table, List<IStudySession> studySessions)
    {
        foreach (var session in studySessions)
        {
            table.AddRow(
                session.Date.ToShortDateString(),
                session.StackName!,
                $"{ session.CorrectAnswers } out of { session.Questions }",
                $"{ session.Percentage }%",
                session.Time.ToString("g")[..7]
            );
        }
        
        return table;
    }
    
    private static Table PopulateAverageYearlyReportTable(Table table, List<IStackMonthlySessions> stackMonthlySessions)
    {
        foreach (var session in stackMonthlySessions)
        {
            table.AddRow(
                session.StackName,
                session.January.ToString(),
                session.February.ToString(),
                session.March.ToString(),
                session.April.ToString(),
                session.May.ToString(),
                session.June.ToString(),
                session.July.ToString(),
                session.August.ToString(),
                session.September.ToString(),
                session.October.ToString(),
                session.November.ToString(),
                session.December.ToString()
            );
        }
        
        return table;
    }
    
    private static bool AskToSaveReport()
    {
        var confirm = AnsiConsole.Confirm(Messages.Messages.SaveAsPdfMessage);

        return confirm;
    }
    
    private static void SetLicence() =>
        QuestPDF.Settings.License = LicenseType.Community;
}