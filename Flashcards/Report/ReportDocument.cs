using Flashcards.Enums;
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
    private readonly ReportType _reportType;

    #region Full report fields
    private const string FullReportDocumentTitle = "Study History";
    #endregion

    #region Report by stack fields
    private const string ReportByStackDocumentTitle = "Report for";
    #endregion

    #region Average yearly report fields
    private static readonly string[] AverageYearlyReportMonthsColumnNames = [
        "Jan.", "Feb.", "Mar.", "Apr.", "May", "June", "July", "Aug.", "Sept.", "Oct.", "Nov.", "Dec."
    ];

    private const string AverageYearlyReportDocumentTitle = "Average Yearly Report";
    #endregion

    public ReportDocument(List<IStudySession> studySessions, ReportType reportType)
    {
        _studySessions = studySessions;
        _reportType = reportType;
    }

    public ReportDocument(List<IStackMonthlySessions> stackMonthlySessions, IYear year, ReportType reportType)
    {
        _stackMonthlySessions = stackMonthlySessions;
        _year = year;
        _reportType = reportType;
    }

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(20);
                page.Header().Text(DefineReportDocumentTitle()).AlignCenter();
                page.Size(SetPageSize());
                page.Content()
                    .Border(1)
                    .Table(ConfigureTableBasedOnReportType);
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

    private static void DefineFullReportColumns(TableDescriptor table)
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
    
    private static void DefineReportByStackColumns(TableDescriptor table)
    {
        table
            .ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(); // SessionDate column
                columns.RelativeColumn(); // Score column
                columns.RelativeColumn(); // Percentage column
            });
    }

    private static void DefineFullReportColumnsHeader(TableDescriptor table)
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

    private static void DefineAverageYearlyReportColumnsHeader(TableDescriptor table)
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
    
    private static void DefineReportByStackColumnsHeader(TableDescriptor table)
    {
        table.Header(header =>
        {
            header.Cell().Padding(5).Text("Session Date").Bold();
            header.Cell().Padding(5).Text("Score").Bold();
            header.Cell().Padding(5).Text("Percentage").Bold();

            header
                .Cell()
                .ColumnSpan(3)
                .ExtendHorizontal()
                .Height(1)
                .Background(Colors.Black);
        });
    }

    private void PopulateFullReportTable(TableDescriptor table)
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
    
    private void PopulateReportByStackTable(TableDescriptor table)
    {
        foreach (var studySession in _studySessions)
        {
            AddTableRow(
                table,
                studySession.Date.ToShortDateString(),
                $"{ studySession.CorrectAnswers } out of { studySession.Questions }",
                $"{ studySession.Percentage }%");
        }
    }

    private void ConfigureTableBasedOnReportType(TableDescriptor table)
    {
        if (_reportType == ReportType.FullReport)
        {
            DefineFullReportColumns(table);
            DefineFullReportColumnsHeader(table);
            PopulateFullReportTable(table);
        }
        else if (_reportType == ReportType.AverageYearlyReport)
        {
            DefineAverageYearlyReportColumns(table);
            DefineAverageYearlyReportColumnsHeader(table);
            PopulateAverageYearlyReportTable(table);
        }
        else if (_reportType == ReportType.ReportByStack)
        {
            DefineReportByStackColumns(table);
            DefineReportByStackColumnsHeader(table);
            PopulateReportByStackTable(table);
        }
    }

    private string DefineReportDocumentTitle()
    {
        if (_reportType == ReportType.FullReport)
        {
            return FullReportDocumentTitle;
        }

        if (_reportType == ReportType.AverageYearlyReport)
        {
            return $"{ AverageYearlyReportDocumentTitle} for the { _year?.ChosenYear }";
        }

        if (_reportType == ReportType.ReportByStack)
        {
            return $"{ ReportByStackDocumentTitle} { _studySessions?[0].StackName }";
        }

        return "Invalid report type";
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
    
    private static void AddTableRow(TableDescriptor table, string date, string score, string percentage)
    {
        table.Cell().Element(CellStyle).Text(date);
        table.Cell().Element(CellStyle).Text(score);
        table.Cell().Element(CellStyle).Text(percentage);
    }
}