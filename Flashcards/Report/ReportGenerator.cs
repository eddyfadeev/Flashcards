using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Spectre.Console;

namespace Flashcards.Report;

internal class ReportGenerator : IReportGenerator
{
    public ReportGenerator()
    {
        SetLicence();
    }
    public Table GetReportToDisplay(List<IStudySession> studySessions) => 
        GenerateReportTable(studySessions);

    public IDocument GenerateReportToFile(List<IStudySession> studySessions)
    {
        var document = new ReportDocument(studySessions);

        return document;
    }

    public void SaveFullReportToPdf(IDocument pdfDocument)
    {
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var filePath = Path.Combine(desktopPath, $"Study Report-{DateTime.Today.Date.ToShortDateString()}.pdf");
        pdfDocument.GeneratePdf(filePath);
    }

    private static Table GenerateReportTable(List<IStudySession> studySessions)
    {
        var table = InitializeTable();
        table = PopulateTable(table, studySessions);

        return table;
    }

    private static Table InitializeTable()
    {
        var table = new Table().Title("[bold]Study History[/]");
        table.Border = TableBorder.Rounded;
        
        table.AddColumns("Date", "Stack", "Result", "Percentage", "Duration");
        
        return table;
    }

    private static Table PopulateTable(Table table, List<IStudySession> studySessions)
    {
        foreach (var session in studySessions)
        {
            table.AddRow(
                session.Date.ToShortDateString(),
                session.StackName!,
                $"{ session.CorrectAnswers } out of { session.Questions }",
                $"{ session.Percentage }%",
                session.Time.ToString()
            );
        }
        
        return table;
    }
    
    private static void SetLicence()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }
}