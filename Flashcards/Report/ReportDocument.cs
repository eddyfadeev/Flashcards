using Flashcards.Interfaces.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Flashcards.Report;

/// <summary>
/// Represents a report document for generating study history reports.
/// </summary>
internal class ReportDocument : IDocument
{
    private readonly List<IStudySession>? _studySessions;
    private readonly List<IStackMonthlySessions>? _stackMonthlySessions;
    private readonly IYear? _year;
    
    private static readonly string[] AverageYearlyReportMonthsColumnNames = [
        "Jan.", "Feb.", "Mar.", "Apr.", "May", "June", "July", "Aug.", "Sept.", "Oct.", "Nov.", "Dec."
    ];

    public ReportDocument(List<IStudySession> studySessions)
    {
        _studySessions = studySessions;
    }

    public ReportDocument(List<IStackMonthlySessions> stackMonthlySessions, IYear year)
    {
        _stackMonthlySessions = stackMonthlySessions;
        _year = year;
    }

    /// <summary>
    /// Composes a study history report document.
    /// </summary>
    /// <param name="container">The document container to compose the report in.</param>
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(20);
                page.Header().Text("Study History").AlignCenter();
                page.Size(SetPageSize());
                page.Content()
                    .Border(1)
                    .Table(table =>
                    {
                        if (_studySessions != null)
                        {
                            DefineStudySessionColumns(table);
                            DefineStudySessionHeader(table);
                            PopulateStudySessionTable(table);
                        }
                        else if (_stackMonthlySessions != null && _year != null)
                        {
                            DefineAverageYearlyReportColumns(table);
                            DefineAverageYearlyReportHeader(table);
                            PopulateAverageYearlyReportTable(table);
                        }
                    });
            });
    }
    
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    
    private PageSize SetPageSize()
    {
        if (_stackMonthlySessions != null && _year != null)
        {
            return PageSizes.A4.Landscape();
        }
        
        return PageSizes.A4.Portrait();
    }

    private static void DefineStudySessionColumns(TableDescriptor table)
    {
        table
            .ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(); // Date column
                columns.RelativeColumn(); // Stack column
                columns.RelativeColumn(); // Result column
                columns.RelativeColumn(); // Percentage column
                columns.RelativeColumn(); // Duration column
            });
    }

    private static void DefineAverageYearlyReportColumns(TableDescriptor table)
    {
        table
            .ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(2); // Stack column
                for (int i = 0; i < 12; i++)
                {
                    columns.RelativeColumn(); // Month columns
                }
            });
    }
    
    private static void DefineStudySessionHeader(TableDescriptor table)
    {
        table.Header(header =>
        {
            header.Cell().Padding(5).Text("Date").Bold();
            header.Cell().Padding(5).Text("Stack").Bold();
            header.Cell().Padding(5).Text("Result").Bold();
            header.Cell().Padding(5).Text("Percentage").Bold();
            header.Cell().Padding(5).Text("Duration").Bold();

            header
                .Cell()
                .ColumnSpan(5)
                .ExtendHorizontal()
                .Height(1)
                .Background(Colors.Black);
        });
    }

    private static void DefineAverageYearlyReportHeader(TableDescriptor table)
    {
        table.Header(header =>
        {
            header.Cell().Padding(5).Text("Stack").Bold();
            foreach (var month in AverageYearlyReportMonthsColumnNames)
            {
                header.Cell().Padding(5).Text(month).Bold();
            }

            header
                .Cell()
                .ColumnSpan(13)
                .ExtendHorizontal()
                .Height(1)
                .Background(Colors.Black);
        });
    }

    private void PopulateStudySessionTable(TableDescriptor table)
    {
        foreach (var studySession in _studySessions)
        {
            AddTableRow(
                table, 
                studySession.Date.ToShortDateString(), 
                studySession.StackName!, 
                $"{ studySession.CorrectAnswers } out of { studySession.Questions }", 
                $"{ studySession.Percentage }%", 
                studySession.Time.ToString("g")[..7]);
        }
    }

    private void PopulateAverageYearlyReportTable(TableDescriptor table)
    {
        foreach (var session in _stackMonthlySessions)
        {
            table.Cell().Element(CellStyle).Text(session.StackName);
            table.Cell().Element(CellStyle).Text(session.January.ToString());
            table.Cell().Element(CellStyle).Text(session.February.ToString());
            table.Cell().Element(CellStyle).Text(session.March.ToString());
            table.Cell().Element(CellStyle).Text(session.April.ToString());
            table.Cell().Element(CellStyle).Text(session.May.ToString());
            table.Cell().Element(CellStyle).Text(session.June.ToString());
            table.Cell().Element(CellStyle).Text(session.July.ToString());
            table.Cell().Element(CellStyle).Text(session.August.ToString());
            table.Cell().Element(CellStyle).Text(session.September.ToString());
            table.Cell().Element(CellStyle).Text(session.October.ToString());
            table.Cell().Element(CellStyle).Text(session.November.ToString());
            table.Cell().Element(CellStyle).Text(session.December.ToString());
        }
    }

    private static IContainer CellStyle(IContainer container) =>
        container.Padding(5);
    
    private static void AddTableRow(TableDescriptor table, string date, string stack, string result, string percentage, string duration)
    {
        table.Cell().Element(CellStyle).Text(date);
        table.Cell().Element(CellStyle).Text(stack);
        table.Cell().Element(CellStyle).Text(result);
        table.Cell().Element(CellStyle).Text(percentage);
        table.Cell().Element(CellStyle).Text(duration);
    }
}